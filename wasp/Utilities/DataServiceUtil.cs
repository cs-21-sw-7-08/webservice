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
using WASP.Objects;

namespace WASP.Utilities
{
    public class DataServiceUtil
    {
        /// <summary>
        /// Get PropertyInfo with a given property name
        /// </summary>
        /// <param name="propertyInfos">Array of PropertyInfo objects</param>
        /// <param name="propertyName">Property name as a string</param>
        /// <returns></returns>
        private static PropertyInfo GetPropertyInfo(PropertyInfo[] propertyInfos, string propertyName, bool useJsonPropertyName = false)
        {
            PropertyInfo propertyInfo = null;
            if (useJsonPropertyName)
            {
                // Get PropertyInfo from JSON property name first
                propertyInfo = propertyInfos.FirstOrDefault(x =>
                {
                    var attribute = x.GetCustomAttributes<JsonPropertyNameAttribute>().FirstOrDefault();
                    return attribute != null && attribute.Name == propertyName;
                });
            }
            // If no match with JSON property name then check property name
            if (propertyInfo == null)
                propertyInfo = propertyInfos.FirstOrDefault(x => x.Name == propertyName);
            return propertyInfo;
        }

        /// <summary>
        /// Method used to update all property values in a target object by the ones given in a source object
        /// </summary>
        /// <typeparam name="Source">Source object's type</typeparam>
        /// <typeparam name="Target">Target object's type</typeparam>
        /// <param name="sourceObj">Source object</param>
        /// <param name="targetObj">Target object</param>
        public static void UpdateProperties<Source, Target>(Source sourceObj, Target targetObj)
        {
            // Get PropertyInfo objects from the source object's type
            var sourceObjPropertyInfos = typeof(Source).GetProperties();
            // Get PropertyInfo objects from the target object's type
            var targetObjPropertyInfos = typeof(Target).GetProperties();
            // Go through the source object's PropertyInfo objects
            foreach (var sourceObjPropertyInfo in sourceObjPropertyInfos)
            {
                // Get property info from target object's type
                var targetObjPropertyInfo = GetPropertyInfo(targetObjPropertyInfos, sourceObjPropertyInfo.Name);
                // Continue if property does not exist in target object
                if (targetObjPropertyInfo == null)
                    continue;
                // Set property
                targetObjPropertyInfo.SetValue(targetObj, sourceObjPropertyInfo.GetValue(sourceObj));
            }
        }  

        /// <summary>
        /// Method used to update a property value in an update function in the DataService
        /// 
        /// TODO: Optimize with a update properties
        /// </summary>
        /// <typeparam name="Target">Target object's type</typeparam>
        /// <param name="sourceValue">New value for the property</param>
        /// <param name="targetPropertyName">Property to be updated</param>
        /// <param name="targetObj">Target object</param>
        public static void UpdateProperty<Target>(object sourceValue, string targetPropertyName, Target targetObj)
        {
            // If Null is given return
            if (targetPropertyName == null)
                return;
            // Get PropertyInfo objects from the target object's type
            var targetObjPropertyInfos = typeof(Target).GetProperties();
            // Get PropertyInfo object with the given property name
            var targetObjPropertyInfo = GetPropertyInfo(targetObjPropertyInfos, targetPropertyName, useJsonPropertyName: true);
            // Check if PropertyInfo is null 
            if (targetObjPropertyInfo == null)
                return;
            object value = sourceValue;
            // If source value is a JSON element, convert it to
            // the property type defined in the PropertyInfo
            if (sourceValue is JsonElement)
            {                
                if (((JsonElement)sourceValue).ValueKind == JsonValueKind.String)
                {
                    value = sourceValue.ToString();
                }
                else
                {
                    value = JsonSerializer.Deserialize
                        (
                            sourceValue.ToString(),
                            targetObjPropertyInfo.PropertyType
                        );
                }
            }
            if (sourceValue is string)
            {
                if (targetObjPropertyInfo.PropertyType == typeof(int))
                    value = int.Parse(sourceValue.ToString());
            }
            // Set property
            targetObjPropertyInfo.SetValue(targetObj, value);            
        }

        /// <summary>
        /// Method used to create dataresponse
        /// 
        /// Adds transparency for the following:
        /// - Creation of the DbContext
        /// - Exception handling         
        /// </summary>
        /// <typeparam name="TypeDataResponse">Type for the DataResponse object</typeparam>
        /// <param name="contextFactory">Context factory used to create DbContext instances</param>
        /// <param name="getDataResponseMethod">Method creating the DataResponse object</param>
        /// <returns></returns>
        public static async Task<TypeDataResponse> GetResponse<TypeDataResponse>
            (
                IDbContextFactory<HiveContext> contextFactory, 
                Func<HiveContext, Task<TypeDataResponse>> getDataResponseMethod
            )
            where TypeDataResponse : DataResponse
        {
            // Nested function used for returning error response object
            TypeDataResponse getErrorResponse(int errorNo, string errorMessage)
            {
                Type type = typeof(TypeDataResponse);
                ConstructorInfo ctor = type.GetConstructor(new[] { typeof(int), typeof(string) });
                object instance = ctor.Invoke(new object[] { errorNo, errorMessage });
                return (TypeDataResponse)instance;
            }

            try
            {
                // Create new context
                using (var context = contextFactory.CreateDbContext())
                {
                    // Call function that returns data response object
                    return await getDataResponseMethod(context);
                }
            }
            catch (Exception exc)
            {
                // Return exception
                return getErrorResponse(((int)ResponseErrors.AnExceptionOccurredInTheDAL), exc.Message);
            }
        }

        /// <summary>
        /// Method used to determine if a given list of WASPUpdate objects follows the correct format
        /// </summary>
        /// <param name="input">List of WASPUpdate objects</param>
        /// <param name="allowedProperties">List of allowed properties that can be updated</param>
        /// <returns></returns>
        public static bool CheckWASPUpdateList(List<WASPUpdate> input, IEnumerable<string> allowedProperties)
        {
            // If input contains duplicates disallow it
            if (input.Distinct().Count() != input.Count)
                return false;
            // If a property given in the input is not present
            // in the allowed properties list disallow it            
            if (input.Any(x => !allowedProperties.Contains(x.Name)))
                return false;
            // Input is allowed
            return true;
        }

    }
}
