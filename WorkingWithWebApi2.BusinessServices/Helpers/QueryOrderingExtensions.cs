using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace WorkingWithWebApi2.BusinessServices.Helpers
{
    public static class QueryOrderingExtensions
    {
        /// <summary>
        /// Generic IQueryable<T> OrderBy Expression Builder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="orderByCriteria"></param>
        /// <param name="defaultCriteria"></param>
        /// <returns></returns>
        public static IQueryable<T> ApplyOrder<T>(this IQueryable<T> query, string orderByCriteria, string defaultCriteria = null) where T : class
        {
            // Permitted 'Criteria' formats:
            // (1) Single: 'productname:asc' or 'productname: desc' or 'productname'
            // (2) Multiple: 'productName:asc, category.categoryname: desc,supplierName'

            // As an added security measure against parameter pollution attcks, we are going to check that the orderByCriteria ..
            // (A) is presented in the correct format.
            //      The orderByCriteria could have multiple criteria seperated by a comma,
            //      Each orderby is made up of key/value pairs consisting of a property name, then 'asc', 'desc' direction specifiers or no direction specifiers.
            // (B) contains property names that exist within the given class type <T>
            //     Property names that do not exist are simply discarded.

            if (!string.IsNullOrEmpty(orderByCriteria))
            {
                string[] OrderByList = orderByCriteria.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); // Seperate out each orderby criteria
                Type classType = typeof(T);                         // Get the class type
                List<string> orderByFields = new List<string>();    // We'll use this at the end to compose the orderby expression

                string[] keyValuePair;
                string propertyName = "";
                string direction = "";

                foreach (string orderByPair in OrderByList)
                {
                    // Grab the key/value pair
                    if (orderByPair.Contains(":"))
                    {
                        keyValuePair = orderByPair.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        propertyName = keyValuePair[0].Trim();
                        direction = keyValuePair[1].Trim();
                    }
                    else
                    {
                        propertyName = orderByPair;
                        direction = "asc"; //default to 'asc' 
                    }

                    // Build the orderby expression for each parameter if it exists
                    if (classType.HasProperty(propertyName))
                    {
                        // Allow 'asc' or 'desc' only.                     
                        string orderby = (direction.ToLower().Equals("asc")) ? $"{propertyName} {direction}" : (direction.ToLower().Equals("desc")) ? $"{propertyName} {direction}" : null;

                        if (!string.IsNullOrEmpty(orderby))
                        {
                            orderByFields.Add(orderby);
                        }
                    }
                }
                // Build the complete expression
                // If the property names provided by the client are invalid, we'll apply the default sort expression if available.
                string orderByExpression = string.Join(", ", orderByFields);

                if (!string.IsNullOrEmpty(orderByExpression))
                {
                    query = query.OrderBy(orderByExpression);
                }
                else
                {
                    if (!string.IsNullOrEmpty(defaultCriteria))
                    {
                        query = query.ApplyOrder(defaultCriteria);
                    }
                    else
                    {
                        //throw new ArgumentNullException("All", "Invalid arguements specified for sort.");
                        throw new ArgumentException("Invalid arguements specified for sort.");
                    }
                }
                // 
               // query = (!string.IsNullOrEmpty(orderByExpression)) ? query.OrderBy(orderByExpression) : (!string.IsNullOrEmpty(defaultCriteria)) ? query.ApplyOrder(defaultCriteria) : query;
            }
            else
            {
                // Try to apply the default criteria
                //query = (!string.IsNullOrEmpty(defaultCriteria)) ?  query.ApplyOrder(defaultCriteria) : query ;

                if (!string.IsNullOrEmpty(defaultCriteria))
                {
                    query = query.ApplyOrder(defaultCriteria);
                }
                else
                {
                    throw new ArgumentException("Invalid arguements specified for sort.");
                }
            }
            return query;
        }
    }
}
