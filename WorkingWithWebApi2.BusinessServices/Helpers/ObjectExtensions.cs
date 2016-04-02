using System;
using System.Reflection;

namespace WorkingWithWebApi2.BusinessServices.Helpers
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks an object type for a public instance property with the specified name
        /// </summary>
        /// <param name="type">The Type to check for which the property may exist</param>
        /// <param name="propertyName">A case insensitive string containing the property name to check for</param>
        /// <param name="propertyType">'out' parameter that initialises the property type if it exists, null otherwise</param>
        /// <returns>true if the proerty exists, false otherwise.</returns>
        public static bool HasProperty(this Type type, string propertyName, out Type propertyType)
        {
            propertyType = null;    // Initialise out parameter

            if (!propertyName.Contains("."))
            {
                if(type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null)
                {
                    propertyType = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).PropertyType;
                    return true;
                }
                return false;
            }

            // If we have reached this far, we're dealing with a nested class/type
            string className = propertyName.Substring(0, propertyName.IndexOf("."));
            string newPropertyName = propertyName.Substring(propertyName.IndexOf(".") + 1);

            if(type.GetProperty(className, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null)
            {
                // We know now that the class/type exits, check if the property name exists within it.
                Type t1 = type.GetProperty(className, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).PropertyType;
                return t1.HasProperty(newPropertyName, out propertyType);
            }

            // Nested class does not exist, return false.
            return false;
        }

        /// <summary>
        /// Checks an object type for a public instance property with the specified name
        /// </summary>
        /// <param name="type">The Type to check for which the property may exist</param>
        /// <param name="propertyName">A case insensitive string containing the property name to check for</param>
        /// <returns>true if the proerty exists, false otherwise.</returns>
        public static bool HasProperty(this Type type, string propertyName)
        {
            if (!propertyName.Contains("."))
            {
                if (type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null)
                {
                    return true;
                }
                return false;
            }

            // If we have reached this far, we're dealing with a nested class/type
            string className = propertyName.Substring(0, propertyName.IndexOf("."));
            string newPropertyName = propertyName.Substring(propertyName.IndexOf(".") + 1);

            if (type.GetProperty(className, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null)
            {
                // We know now that the class/type exits, check if the property name exists within it.
                Type t1 = type.GetProperty(className, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).PropertyType;
                return t1.HasProperty(newPropertyName);
            }

            // Nested class does not exist, return false.
            return false;
        }
    }
}
