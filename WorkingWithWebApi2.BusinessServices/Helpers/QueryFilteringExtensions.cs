using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace WorkingWithWebApi2.BusinessServices.Helpers
{
    public static class QueryFilteringExtensions
    {
        /// <summary>
        /// Generic filter experssion composer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, string filterCriteria) where T : class
        {
            // As an added security measure against parameter pollution attcks, we are going to check that the filterCriteria ..
            // (A) is presented in the correct format.
            //      The filterCriteria could have multiple filters seperated by a comma,
            //      Each filter is made up of a key/value pair which are sperated by a colon.
            // (B) contains property names that exist within the given class type <T>
            //     Property names that do not exist are simply discarded.

            if (!string.IsNullOrEmpty(filterCriteria))
            {                
                string[] filterList = filterCriteria.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); // Seperate out each filter criteria
                List<string> filterFields = new List<string>();     // We'll use this at the end to compose the filter expression
                Type classType = typeof(T);                         // Get the class type  
                              
                string[] keyValuePair;
                string propertyName = "";
                string propertyValue = "";
                Type propertyType = null;

                // Iterate through each criteria
                foreach (string filterPair in filterList)
                {
                    // Grab the key/value pair
                    if (filterPair.Contains(":"))
                    { 
                        keyValuePair = filterPair.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        propertyName = keyValuePair[0].Trim();
                        propertyValue = keyValuePair[1].Trim();
                    }

                    // Check if the property exists and grab it's type
                    if(classType.HasProperty(propertyName, out propertyType))
                    {
                        // Construct filter depending on property type
                        switch (propertyType.Name)
                        {
                            case "String" :
                                filterFields.Add($"{propertyName}.Contains(\"{propertyValue}\")");
                                break;
                            case "Char":
                                filterFields.Add($"{propertyName} == '{propertyValue}'");
                                break;
                            case "Boolean":
                                string filterString;
                                if (buildBooleanFilter(propertyName, propertyValue, out filterString))
                                {
                                    filterFields.Add(filterString);
                                }
                                break;
                            default:
                                filterFields.Add($"{propertyName} == {propertyValue}");
                                break;
                        }
                    }                    
                }
                // Build the complete expression
                string filterExpression = string.Join(" AND ", filterFields);
                return query = (string.IsNullOrEmpty(filterExpression)) ? query : query.Where(filterExpression);
            }
               
            return query;
        }

        /// <summary>
        /// Builds a filter expression for a boolean property
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        private static bool buildBooleanFilter(string propertyName, string propertyValue, out string filterExpression)
        {
            string falseValue = "false", trueValue = "true";
            filterExpression = null;

            // Check if any of the value characters are in the 'falseValue' string. Allowed Examples: 'f', 'fa', 'l', 'lse', 'false'.
            if (falseValue.Contains(propertyValue))
            {
                filterExpression = $"{propertyName} == {falseValue}";
            }
            // Check if any of the value characters are in the 'trueValue' string. Allowed Examples: 't', 'tr', 'u', 'ue', 'true'.
            else if (trueValue.Contains(propertyValue))
            {
                filterExpression = $"{propertyName} == {trueValue}";
            }

            return (!string.IsNullOrEmpty(filterExpression));
        }
    }
}
