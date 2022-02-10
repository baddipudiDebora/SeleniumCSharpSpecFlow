using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BaseProject.SpecFlow
{
    public class SpecFlowUtility
    {

        /// <summary> 
        /// Method Name: StoreScenarioVariable
        /// Description: This method stores the input key and value pair in the Specflow Scenario Context object

        /// </summary>
        public static void StoreScenarioVariable(string key, object value, bool replace = true)
        {
            var keyExists = ScenarioContext.Current.ContainsKey(key);
            if (keyExists)
            {
                if (replace)
                {
                    ScenarioContext.Current[key] = value;
                }
                else
                {
                    ScenarioContext.Current[key] = string.Format("{0};{1}", ScenarioContext.Current[key], value);
                }
            }
            else
            {
                ScenarioContext.Current.Add(key, value);
            }
        }

        /// <summary> 
        /// Method Name: StoreFeatureContextVariable
        /// Description: This method stores the input key and value pair in the Specflow Feature Context object

        /// </summary>
        public static void StoreFeatureContextVariable(string key, object value, bool replace = true)
        {
            var keyExists = FeatureContext.Current.ContainsKey(key);
            if (keyExists)
            {
                if (replace)
                {
                    FeatureContext.Current[key] = value;
                }
                else
                {
                    FeatureContext.Current[key] = string.Format("{0};{1}", FeatureContext.Current[key], value);
                }
            }
            else
            {
                FeatureContext.Current.Add(key, value);
            }
        }

        /// <summary> 
        /// Method Name: GetValueFromFeatureContextVariable
        /// Description: This method for a given keyname, returns the value stored in Feature Context. If an input for valueStarts is provided, then it retrives the 
        /// string starting with the input provided from the list of values stored for the key in FeatureContext.
    
        /// </summary>
        public static object GetValueFromFeatureContextVariable(string keyname, string valueStarts)
        {
            var obj = new Object();
            obj = valueStarts;

            if (!string.IsNullOrEmpty(keyname))
            {
                if (FeatureContext.Current.ContainsKey(keyname))
                {
                    obj = FeatureContext.Current[keyname];
                }

                if (!string.IsNullOrEmpty(valueStarts))
                {
                    var listItems = obj.ToString().Split(';');
                    obj = listItems.FirstOrDefault(item => item.StartsWith(valueStarts));
                    if (obj == null)
                    {
                        obj = valueStarts;
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// Method Name: RemoveValueFromFeatureContext
        /// Description: Remove Value from feature context given a Key name and the input string value to remove
        /// </summary>
        /// <param name="keyname"></param>
        /// <param name="valueToRemove"></param>
        public static void RemoveValueFromFeatureContext(string keyname, string valueToRemove)
        {
            var valueFromStoredContext = GetValueFromFeatureContextVariable(keyname, string.Empty).ToString();
            valueToRemove = GetValueFromFeatureContextVariable(keyname, valueToRemove).ToString();
            var valueContext = valueFromStoredContext.Split(';').ToList();
            valueContext.Remove(valueToRemove);
            var updatedContextString = string.Join(";", valueContext);
            StoreFeatureContextVariable(keyname, updatedContextString);
        }
    }
}
