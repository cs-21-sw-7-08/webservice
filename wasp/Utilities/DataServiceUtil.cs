using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Utilities
{
    public class DataServiceUtil
    {
        public static void UpdateChangedProperties<Source, Target>(Source sourceObj, Target targetObj)
        {
            var sourceObjPropertyInfos = typeof(Source).GetProperties();
            var targetObjPropertyInfos = typeof(Target).GetProperties();

            /*foreach (var sourceObjPropertyInfo in sourceObjPropertyInfos)
            {
                // Get property info
                var targetObjPropertyInfo = targetObjPropertyInfos.FirstOrDefault(x => x.Name == sourceObjPropertyInfo.Name);
                // Continue if property exist in target object
                if (targetObjPropertyInfo == null)
                    continue;
                var sourceValue = sourceObjPropertyInfo.GetValue(sourceObj);
                var targetValue = targetObjPropertyInfo.GetValue(targetObj);
                if (sourceValue != targetValue)


                // Set property
                targetObjPropertyInfo.SetValue(newIssue, sourceObjPropertyInfo.GetValue(issue));
            }*/
        }

    }
}
