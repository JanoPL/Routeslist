using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace RoutesList.Build.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// Converts an enum to a dictionary of integer values and display names.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="myEnum">The enum value to convert.</param>
        /// <returns>A dictionary containing enum values as keys and their display names as values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when myEnum is null.</exception>
        public static Dictionary<int, string> ToDictionary<T>(T myEnum) where T : Enum
        {
            if (myEnum == null)
            {
                throw new ArgumentNullException(nameof(myEnum));
            }
            var myEnumType = myEnum.GetType();
        
            var names = myEnumType.GetFields()
                .Where(m => m.GetCustomAttribute<DisplayAttribute>() != null)
                .Select(e => e.GetCustomAttribute<DisplayAttribute>()?.Name)
                .ToList();
        
            var values = Enum.GetValues(myEnumType).Cast<int>().ToList();
        
            return names.Zip(values, (n, v) => new KeyValuePair<int, string>(v, n ?? string.Empty))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        /// <summary>
        /// Gets a list of descriptions for all values in an enum type.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>A list of enum descriptions, or an empty list if the type is not an enum.</returns>
        public static List<string> GetListOfDescription<T>() where T : struct, Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(x => x.GetDescription() ?? x.ToString())
                .ToList();
        }

        /// <summary>
        /// Gets the description of an enum value using the DescriptionAttribute.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>The description of the enum value, or null if no description is found.</returns>
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                return null;
            }
            
            var field = value.GetType().GetField(value.ToString());
            return field?.GetCustomAttribute<DescriptionAttribute>()?.Description;
        }
    }
}
