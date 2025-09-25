using System;
using System.Collections.Generic;

namespace RoutesList.Build.Services.StaticFileBuilder
{
    /// <summary>
    /// Provides a singleton implementation for storing and managing HTML-related data using a type-based dictionary.
    /// This class ensures that only one instance exists throughout the application lifetime.
    /// </summary>
    public sealed class HtmlData
    {
        private static Dictionary<Type, object> Data { get; set; } = new Dictionary<Type, object>();
        private static HtmlData _instance;

        /// <summary>
        /// Gets the singleton instance of the HtmlData class.
        /// </summary>
        /// <returns>The single instance of HtmlData.</returns>
        public static HtmlData GetInstance()
        {
            _instance ??= new HtmlData();

            return _instance;
        }

        /// <summary>
        /// Adds data to the dictionary using the specified type as key. If the key already exists, updates the data.
        /// </summary>
        /// <typeparam name="T">The type to use as dictionary key.</typeparam>
        /// <param name="data">The data to store.</param>
        public static void Add<T>(object data) where T : class
        {
            Type type = typeof(T);

            if (!Data.TryAdd(type, data)) {
                Update<T>(data);
            }
        }

        /// <summary>
        /// Updates existing data in the dictionary for the specified type.
        /// </summary>
        /// <typeparam name="T">The type used as dictionary key.</typeparam>
        /// <param name="data">The new data to store.</param>
        private static void Update<T>(object data) where T : class
        {
            Type type = typeof(T);

            if (Data.ContainsKey(type)) {
                Data[type] = data;
            }
#if DEBUG
            else {
                Console.WriteLine("Debug.INFO: HtmlData has no key to update");
            }
#endif
        }

        /// <summary>
        /// Gets the entire dictionary of stored data.
        /// </summary>
        /// <returns>A dictionary containing all stored data with their type keys.</returns>
        public static Dictionary<Type, object> GetDatas()
        {
            return Data;
        }

        /// <summary>
        /// Retrieves data of specified type from the dictionary.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve.</typeparam>
        /// <returns>The stored data cast to the specified type.</returns>
        public static T GetData<T>() where T : class
        {
            Type type = typeof(T);
            return (T)Data[type];
        }
    }
}
