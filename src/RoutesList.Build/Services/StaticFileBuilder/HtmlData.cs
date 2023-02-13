using System;
using System.Collections.Generic;

namespace RoutesList.Build.Services.StaticFileBuilder
{
    public sealed class HtmlData
    {
        private static Dictionary<Type, object> Data { get; set; } = new Dictionary<Type, object>();
        private static HtmlData _instance;

        public static HtmlData GetInstance()
        {
            if (_instance == null) {
                _instance = new HtmlData();
            }

            return _instance;
        }

        public static void Add<T>(object data) where T : class
        {
            Type type = typeof(T);

            if (!Data.ContainsKey(type)) {
                Data.Add(type, data);
            } else {
                Update<T>(data);
            }
        }

        public static void Update<T>(object data) where T : class
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

        public static Dictionary<Type, object> GetDatas()
        {
            return Data;
        }

        public static T GetData<T>() where T : class
        {
            Type type = typeof(T);
            return (T)Data[type];
        }
    }
}
