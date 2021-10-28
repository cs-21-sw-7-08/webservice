using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WASP.Enums;
using WASP.Models;

namespace WASP.Utilities
{
    public class DataServiceUtil
    {
        public static void UpdateProperties<Source, Target>(Source sourceObj, Target targetObj)
        {
            var sourceObjPropertyInfos = typeof(Source).GetProperties();
            var targetObjPropertyInfos = typeof(Target).GetProperties();

            foreach (var sourceObjPropertyInfo in sourceObjPropertyInfos)
            {
                // Get property info
                var targetObjPropertyInfo = targetObjPropertyInfos.FirstOrDefault(x => x.Name == sourceObjPropertyInfo.Name);
                // Continue if property does not exist in target object
                if (targetObjPropertyInfo == null)
                    continue;
                // Set property
                targetObjPropertyInfo.SetValue(targetObj, sourceObjPropertyInfo.GetValue(sourceObj));
            }
        }

        public static void UpdateProperty<Target>(object sourceValue, string targetPropertyName, Target targetObj)
        {
            if (targetPropertyName == null)
                return;

            var targetObjPropertyInfos = typeof(Target).GetProperties();
            // Get property info from JSON property name first
            var targetObjPropertyInfo = targetObjPropertyInfos.FirstOrDefault(x =>
            {
                var attribute = x.GetCustomAttributes<JsonPropertyNameAttribute>().FirstOrDefault();                
                return attribute != null && attribute.Name == targetPropertyName;
            });
            // If no match with JSON property name then check property names
            if (targetObjPropertyInfo == null)
                targetObjPropertyInfos.FirstOrDefault(x => x.Name == targetPropertyName);
            // Return if property could not be found
            if (targetObjPropertyInfo == null)
                return;
            object value = sourceValue;
            if (sourceValue is JsonElement)
            {
                Type type = targetObjPropertyInfo.PropertyType;
                value = JsonSerializer.Deserialize(sourceValue.ToString(), type);
            }
            // Set property
            targetObjPropertyInfo.SetValue(targetObj, value);
        }

        public static async Task<TypeDataResponse> GetResponse<TypeDataResponse>(IDbContextFactory<HiveContext> contextFactory, Func<HiveContext, Task<TypeDataResponse>> getDataResponseMethod)
            where TypeDataResponse : DataResponse
        {
            TypeDataResponse getErrorResponse(int errorNo, string errorMessage)
            {
                Type type = typeof(TypeDataResponse);
                ConstructorInfo ctor = type.GetConstructor(new[] { typeof(int), typeof(string) });
                object instance = ctor.Invoke(new object[] { errorNo, errorMessage });
                return (TypeDataResponse)instance;
            }

            try
            {
                using (var context = contextFactory.CreateDbContext())
                {
                    return await getDataResponseMethod(context);
                }
            }
            catch (Exception exc)
            {
                return getErrorResponse(((int)ResponseErrors.AnExceptionOccurredInTheDAL), exc.Message);
            }
        }

    }
}
