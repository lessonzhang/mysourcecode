using System;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.IO;

namespace MyFramework.Utility.Cryptography
{
    /// <summary>
    /// MD5加密类,注意经MD5加密过的信息是不能转换回原始数据的
    /// ,请不要在用户敏感的信息中使用此加密技术,比如用户的密码,
    /// 请尽量使用对称加密
    /// </summary>
    public class MD5Encrypt
    {
        private MD5 md5;
        /// <summary>
        /// 构造函数
        /// </summary>
        public MD5Encrypt()
        {
            md5 = new MD5CryptoServiceProvider();
        }
        /// <summary>
        /// 从字符串中获取散列值
        /// </summary>
        /// <param name="str">要计算散列值的字符串</param>
        /// <returns></returns>
        public string GetMD5FromString(string str)
        {
            byte[] toCompute = Encoding.Default.GetBytes(str);
            byte[] hashed = md5.ComputeHash(toCompute, 0, toCompute.Length);
            return BitConverter.ToString(hashed);
        }
        /// <summary>
        /// 从字符串中获取散列值
        /// </summary>
        /// <param name="str">要计算散列值的字符串</param>
        /// <returns></returns>
        public string GetMD5FromString(byte[] data)
        {
            // byte[] toCompute = Encoding.Unicode.GetBytes(str);
            byte[] hashed = md5.ComputeHash(data, 0, data.Length);
            return BitConverter.ToString(hashed);
        }
        /// <summary>
        /// 根据文件来计算散列值
        /// </summary>
        /// <param name="filePath">要计算散列值的文件路径</param>
        /// <returns></returns>
        public string GetMD5FromFile(string filePath)
        {
            bool isExist = File.Exists(filePath);
            if (isExist)//如果文件存在
            {
                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream, Encoding.Unicode);
                string str = reader.ReadToEnd();
                byte[] toHash = Encoding.Default.GetBytes(str);
                byte[] hashed = md5.ComputeHash(toHash, 0, toHash.Length);
                stream.Close();
                return BitConverter.ToString(hashed);
            }
            else//文件不存在
            {
                throw new FileNotFoundException("File not found!");
            }
        }


        public string GetMD5FromObject(object data)
        {
            byte[] bytes = Serialization.BinarySerialization.Serialize(data);

            return GetMD5FromString(bytes);
        }
    }
}
