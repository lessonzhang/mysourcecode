using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MyFramework.Utility.Serialization
{
    /// <summary>
    /// 2进制序列化工具
    /// </summary>
    public class BinarySerialization<T>
    {

        #region  Members
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns>2进制数据</returns>
        public static byte[] Serialize(T obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            formatter.Serialize(memStream, obj);

            memStream.Position = 0;
            byte[] data = new byte[(int)memStream.Length];

            memStream.Read(data, 0, data.Length);

            return data;
        }

        /// <summary>
        /// 将2进制数据反序列化
        /// </summary>
        /// <param name="data">2进制数据</param>
        /// <param name="type">null</param>
        /// <returns>对象实例</returns>
        public static T Deserialize(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            memStream.Write(data, 0, data.Length);
            memStream.Position = 0;

            return (T)formatter.Deserialize(memStream);
        }

        /// <summary>
        /// 对象克隆
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CopyFrom(T obj)
        {
            byte[] bytes = Serialize(obj);

            return Deserialize(bytes);
        }
        #endregion
    }

    public class BinarySerialization
    {
        #region Members

        public static byte[] Serialize(object obj)
        {
            if (obj == null) return null;
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            formatter.Serialize(memStream, obj);

            memStream.Position = 0;
            byte[] data = new byte[(int)memStream.Length];

            memStream.Read(data, 0, data.Length);

            return data;
        }

        public static object Deserialize(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            memStream.Write(data, 0, data.Length);
            memStream.Position = 0;

            return formatter.Deserialize(memStream);
        }

        public static object CopyFrom(object obj)
        {
            return Deserialize(Serialize(obj));
        }

        #endregion
    }
}
