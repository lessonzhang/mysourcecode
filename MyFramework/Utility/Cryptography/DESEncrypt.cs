using System;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.IO;

namespace MyFramework.Utility.Cryptography
{
    /// <summary>
    /// DES加解密类
    /// </summary>
    public class DESEncrypt
    {
        #region 私有字段
        private string iv = "fie9wwptj7843"; //私钥
        private string key = "lesson4mufeng";
        private Encoding encoding = new UnicodeEncoding();
        private DES des;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public DESEncrypt()
        {
            des = new DESCryptoServiceProvider();
        }

        #region 属性
        /// <summary>
        /// 设置加密密钥
        /// </summary>
        public string EncryptKey
        {
            get { return this.key; }
            set
            {
                this.key = value;
            }
        }
        /// <summary>
        /// 要加密字符的编码模式
        /// </summary>
        public Encoding EncodingMode
        {
            get { return this.encoding; }
            set { this.encoding = value; }
        }
        #endregion

        #region 方法

        /// <summary>
        /// Encrypt string
        /// </summary>
        /// <param name="strContent">string to be encrypted</param>
        /// <returns>string encrypted</returns>
        public string EncryptString(string strContent)
        {
            string result = "";
            byte[] byKey = null;
            byte[] byIV = null;
            byte[] inputByteArray = null;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = null;
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(this.key.Substring(0, 8));
                byIV = System.Text.Encoding.UTF8.GetBytes(this.iv);
                inputByteArray = Encoding.UTF8.GetBytes(strContent);
                //MemoryStream ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                cs.Close();
                ms.Close();
                result = Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                if (cs != null)
                {
                    cs.Close();
                }
                if (ms != null)
                {
                    ms.Close();
                }
                result = null;
            }
            return result;
        }

        /// <summary>
        /// 简单加密字符串
        /// </summary>
        /// <param name="strContent">需要加密的字符串内容</param>
        /// <returns>加密后的数据</returns>
        public string EncryptString_AsicII(string strContent)
        {
            string result = "";
            try
            {


                byte[] bytes = System.Text.UTF8Encoding.Default.GetBytes(strContent);
                for (int i = 0; i < bytes.Length; i++)
                {
                    // result += bytes[i].ToString().PadLeft(3, '0');
                    result += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        /// <summary>
        /// 简单解密字符串
        /// </summary>
        /// <param name="strContent">需要解密的字符串内容</param>
        /// <returns>解密后的数据</returns>
        public string DecryptString_AsicII(string strContent)
        {
            string result = "";

            try
            {

                string tmpStr = "";
                int cnt = 0;

                for (int i = 0; i < strContent.Length; i++)
                {
                    cnt++;
                    tmpStr += strContent[i];
                    if (cnt % 2 == 0)
                    {

                        byte by = Convert.ToByte(tmpStr, 16);
                        byte[] bb = new byte[1];
                        bb[0] = by;
                        result += System.Text.UTF8Encoding.Default.GetString(bb);
                        cnt = 0;
                        tmpStr = "";
                    }
                }
            }
            catch
            {
            }
            return result;

        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>加密后2进制数据</returns>
        public string EncryptStringReturnBytes(string str)
        {
            byte[] ivb = Encoding.ASCII.GetBytes(this.iv);
            byte[] keyb = Encoding.ASCII.GetBytes(this.EncryptKey);//得到加密密钥
            byte[] toEncrypt = this.EncodingMode.GetBytes(str);//得到要加密的内容
            byte[] encrypted;
            ICryptoTransform encryptor = des.CreateEncryptor(keyb, ivb);
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
            csEncrypt.FlushFinalBlock();
            encrypted = msEncrypt.ToArray();
            csEncrypt.Close();
            msEncrypt.Close();
            return encrypted.ToString();
        }

        /// <summary>
        /// 加密指定的文件,如果成功返回True,否则false
        /// </summary>
        /// <param name="filePath">要加密的文件路径</param>
        /// <param name="outPath">加密后的文件输出路径</param>
        public void EncryptFile(string filePath, string outPath)
        {
            bool isExist = File.Exists(filePath);
            if (isExist)//如果存在
            {
                byte[] ivb = Encoding.ASCII.GetBytes(this.iv);
                byte[] keyb = Encoding.ASCII.GetBytes(this.EncryptKey);
                //得到要加密文件的字节流
                FileStream fin = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(fin, this.EncodingMode);
                string dataStr = reader.ReadToEnd();
                byte[] toEncrypt = this.EncodingMode.GetBytes(dataStr);
                fin.Close();

                FileStream fout = new FileStream(outPath, FileMode.Create, FileAccess.Write);
                ICryptoTransform encryptor = des.CreateEncryptor(keyb, ivb);
                CryptoStream csEncrypt = new CryptoStream(fout, encryptor, CryptoStreamMode.Write);
                try
                {
                    //加密得到的文件字节流
                    csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
                    csEncrypt.FlushFinalBlock();
                }
                catch (Exception err)
                {
                    throw new ApplicationException(err.Message);
                }
                finally
                {
                    try
                    {
                        fout.Close();
                        csEncrypt.Close();
                    }
                    catch
                    {
                        ;
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("File not found!");
            }
        }
        /// <summary>
        /// 文件加密函数的重载版本,如果不指定输出路径,
        /// 那么原来的文件将被加密后的文件覆盖
        /// </summary>
        /// <param name="filePath"></param>
        public void EncryptFile(string filePath)
        {
            this.EncryptFile(filePath, filePath);
        }
        /// <summary>
        /// Decrypt string
        /// </summary>
        /// <param name="strContent">string to be decrypted</param>
        /// <returns>string decrypted</returns>
        public string DecryptString(string strContent)
        {
            string result = "";
            byte[] byKey = null;
            byte[] byIV = null;
            byte[] inputByteArray = new Byte[strContent.Length];
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = null;
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(this.key.Substring(0, 8));
                byIV = System.Text.Encoding.UTF8.GetBytes(this.iv);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strContent);
                cs = new CryptoStream(ms, des.CreateDecryptor(byKey, byIV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                cs.Close();
                ms.Close();
                System.Text.Encoding encoding = new System.Text.UTF8Encoding();
                result = encoding.GetString(ms.ToArray());
            }
            catch
            {
                if (cs != null)
                {
                    cs.Close();
                }
                if (ms != null)
                {
                    ms.Close();
                }
                //throw error;
                result = null;
            }
            return result;
        }
        /// <summary>
        /// Decrypt Binary
        /// </summary>
        /// <param name="toDecrypt">Binary  to be decrypted</param>
        /// <returns>string decrypted</returns>
        public string DecryptString(byte[] toDecrypt)
        {
            byte[] ivb = Encoding.ASCII.GetBytes(this.iv);
            byte[] keyb = Encoding.ASCII.GetBytes(this.EncryptKey);
            //byte[] toDecrypt=this.EncodingMode.GetBytes(str);
            byte[] deCrypted = new byte[toDecrypt.Length];
            ICryptoTransform deCryptor = des.CreateDecryptor(keyb, ivb);
            MemoryStream msDecrypt = new MemoryStream(toDecrypt);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, deCryptor, CryptoStreamMode.Read);
            try
            {
                csDecrypt.Read(deCrypted, 0, deCrypted.Length);
            }
            catch (Exception err)
            {
                throw new ApplicationException(err.Message);
            }
            finally
            {
                try
                {
                    msDecrypt.Close();
                    csDecrypt.Close();
                }
                catch { ;}
            }
            return this.EncodingMode.GetString(deCrypted);

        }

        /// <summary>
        /// 解密指定的文件
        /// </summary>
        /// <param name="filePath">要解密的文件路径</param>
        /// <param name="outPath">解密后的文件输出路径</param>
        public void DecryptFile(string filePath, string outPath)
        {
            bool isExist = File.Exists(filePath);
            if (isExist)//如果存在
            {
                byte[] ivb = Encoding.ASCII.GetBytes(this.iv);
                byte[] keyb = Encoding.ASCII.GetBytes(this.EncryptKey);
                FileInfo file = new FileInfo(filePath);
                byte[] deCrypted = new byte[file.Length];
                //得到要解密文件的字节流
                FileStream fin = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                //解密文件
                try
                {
                    ICryptoTransform decryptor = des.CreateDecryptor(keyb, ivb);
                    CryptoStream csDecrypt = new CryptoStream(fin, decryptor, CryptoStreamMode.Read);
                    csDecrypt.Read(deCrypted, 0, deCrypted.Length);
                }
                catch (Exception err)
                {
                    throw new ApplicationException(err.Message);
                }
                finally
                {
                    try
                    {
                        fin.Close();
                    }
                    catch { ;}
                }
                FileStream fout = new FileStream(outPath, FileMode.Create, FileAccess.Write);
                fout.Write(deCrypted, 0, deCrypted.Length);
                fout.Close();
            }
            else
            {
                throw new FileNotFoundException("File not found!");
            }
        }
        /// <summary>
        /// 解密文件的重载版本,如果没有给出解密后文件的输出路径,
        /// 则解密后的文件将覆盖先前的文件
        /// </summary>
        /// <param name="filePath"></param>
        public void DecryptFile(string filePath)
        {
            this.DecryptFile(filePath, filePath);
        }
        #endregion
    }
}
