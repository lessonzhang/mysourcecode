using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace MyFramework.Utility.Serialization
{

    public class JOSNSerialization<T>
    {
        #region Members

        public static string Serialize(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        #endregion
    }

    public class JSONSerialization
    {
        #region Members

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static object Deserialize(string data, Type type)
        {
            return JsonConvert.DeserializeObject(data, type);
        }


        #endregion
    }
}
