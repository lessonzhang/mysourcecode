using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace MyFramework.Utility.Serialization
{
    public class XMLSerialization<T>
    {
        #region Members
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns>2进制数据</returns>
        public static string Serialize(T obj)
        {
            if (obj == null)
                return "";

            MemoryStream memStream = new MemoryStream();
            XmlSerializer packageSerializer = new XmlSerializer(obj.GetType());
            packageSerializer.Serialize(memStream, obj);

            StreamReader sr = new StreamReader(memStream);
            sr.BaseStream.Position = 0;

            string s = sr.ReadToEnd();

            int i = s.Length;
            return s;
        }
        /// <summary>
        /// 将2进制数据反序列化
        /// </summary>
        /// <param name="data">2进制数据</param>
        /// <param name="type">null</param>
        /// <returns>对象实例</returns>
        public static T Deserialize(string data)
        {
            if (data == null || data.Length == 0)
                return default(T);

            MemoryStream memStream = new MemoryStream();

            StreamWriter sw = new StreamWriter(memStream);

            sw.Write(data);
            sw.Flush();
            memStream.Position = 0;

            XmlSerializer packageSerializer = new XmlSerializer(typeof(T));

            T obj = (T)packageSerializer.Deserialize(memStream);

            return obj;
        }


        #endregion
    }

    public class XMLSerialization
    {
        #region Members

        public static string Serialize(object obj)
        {
            if (obj == null)
                return "";

            MemoryStream memStream = new MemoryStream();
            XmlSerializer packageSerializer = new XmlSerializer(obj.GetType());
            packageSerializer.Serialize(memStream, obj);

            StreamReader sr = new StreamReader(memStream);
            sr.BaseStream.Position = 0;

            string s = sr.ReadToEnd();

            int i = s.Length;
            return s;
        }

        public static object Deserialize(string data, Type type)
        {
            if (data == null || data.Length == 0)
                return default(object);

            MemoryStream memStream = new MemoryStream();

            StreamWriter sw = new StreamWriter(memStream);

            sw.Write(data);
            sw.Flush();
            memStream.Position = 0;

            XmlSerializer packageSerializer = new XmlSerializer(type);

            object obj = packageSerializer.Deserialize(memStream);

            return obj;
        }


        #endregion
    }
}
