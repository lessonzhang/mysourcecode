using System;
using System.Collections.Generic;
using System.Text;

namespace MyFramework.Utility.Serialization
{
    /// <summary>
    /// �ǦC��
    /// </summary>
    public class Serialization
    {
        /// <summary>
        /// �ǦC��
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="SerializedType"></param>
        /// <returns></returns>
        public static object Serialize(object obj, ESerializedType SerializedType)
        {
            object result = null;
            switch (SerializedType)
            {
                case ESerializedType.BINARY:
                    result = BinarySerialization.Serialize(obj);
                    break;
                case ESerializedType.XML:
                    result = XMLSerialization.Serialize(obj);
                    break;
                case ESerializedType.JOSN:
                    result = JSONSerialization.Serialize(obj);
                    break;
            }
            return result;
        }

        /// <summary>
        /// �ϧǦC��
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Type"></param>
        /// <param name="SerializedType"></param>
        /// <returns></returns>
        public static object Deserialize(object obj, Type Type, ESerializedType SerializedType)
        {
            object result = null;
            switch (SerializedType)
            {
                case ESerializedType.BINARY:
                    result = BinarySerialization.Deserialize((byte[])obj);
                    break;
                case ESerializedType.XML:
                    result = XMLSerialization.Deserialize(obj.ToString(), Type);
                    break;
                case ESerializedType.JOSN:
                    result = JSONSerialization.Deserialize(obj.ToString(), Type);
                    break;
            }
            return result;
        }

        public static T Copy<T>(T obj, ESerializedType SerializedType)
        {
            object tmp = Serialize(obj, SerializedType);

            T Target = (T)Deserialize(tmp, typeof(T), SerializedType);
            return Target;
        }


    }
}
