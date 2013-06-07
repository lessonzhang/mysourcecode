using System;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.IO;
namespace MyFramework.Utility.Cryptography
{
    /// <summary>
    /// 用于数字签名的hash类
    /// </summary>
    public class MACTripleDESEncrypt
    {
        private MACTripleDES mact;
        private string __key = "ksn168ch";
        private byte[] __data = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public MACTripleDESEncrypt()
        {
            mact = new MACTripleDES();
        }
        /// <summary>
        /// 获取或设置用于数字签名的密钥
        /// </summary>
        public string Key
        {
            get { return this.__key; }
            set
            {
                int keyLength = value.Length;
                int[] keyAllowLengths = new int[] { 8, 16, 24 };
                bool isRight = false;
                foreach (int i in keyAllowLengths)
                {
                    if (keyLength == keyAllowLengths[i])
                    {
                        isRight = true;
                        break;
                    }
                }
                if (!isRight)
                    throw new ApplicationException("用于数字签名的密钥长度必须是8,16,24值之一");
                else
                    this.__key = value;
            }
        }
        /// <summary>
        /// 获取或设置用于数字签名的用户数据
        /// </summary>
        public byte[] Data
        {
            get { return this.__data; }
            set { this.__data = value; }
        }
        /// <summary>
        /// 得到签名后的hash值
        /// </summary>
        /// <returns></returns>
        public string GetHashValue()
        {
            if (this.Data == null)
                throw new Exception("没有设置要进行数字签名的用户" +
                    "数据(property:Data)");
            byte[] key = Encoding.Default.GetBytes(this.Key);
            this.mact.Key = key;
            byte[] hash_b = this.mact.ComputeHash(this.mact.ComputeHash(this.Data));
            return Encoding.Default.GetString(hash_b);
        }
    }
}
