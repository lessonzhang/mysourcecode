using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Xml;
using System.Net;
using System.Data.Odbc;
using System.Runtime.InteropServices;
using System.Drawing;
using Microsoft.Win32;

namespace MyFramework.Utility
{
    /// <summary>
    /// FileUtility 文件类工具
    /// </summary>
    public class FileUtility
    {
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="content">文件内容</param>
        public static void Create(string path, byte[] content)
        {
            FileUtility.EnsurePath(Path.GetDirectoryName(path));
            FileStream stream1 = new FileStream(path, FileMode.Create);
            stream1.Write(content, 0, content.Length);
            stream1.Close();
            System.Threading.Thread.Sleep(2000);
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="content">文件内容</param>
        public static void Create(string path, string content)
        {
            FileUtility.EnsurePath(Path.GetDirectoryName(path));
            FileStream stream1 = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter writer1 = new StreamWriter(stream1);
            writer1.WriteLine(content);
            writer1.Close();
            stream1.Close();
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="content">文件内容</param>
        /// <param name="encoding">内容编码</param>
        public static void Create(string path, string content, Encoding encoding)
        {
            FileUtility.EnsurePath(Path.GetDirectoryName(path));
            FileStream stream1 = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter writer1 = new StreamWriter(stream1, encoding);
            writer1.WriteLine(content);
            writer1.Close();
            stream1.Close();
        }

        /// <summary>
        /// 新建或打开现有文件，并添加内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="content">文件内容</param>
        public static void CreateAppend(string path, string content)
        {
            FileUtility.EnsurePath(Path.GetFullPath(path));
            FileStream stream1 = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter writer1 = new StreamWriter(stream1);
            writer1.WriteLine(content);
            writer1.Close();
            stream1.Close();
        }

        /// <summary>
        /// 新建或打开现有文件，并添加内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="content">文件内容</param>
        /// <param name="encoding">内容编码</param>
        public static void CreateAppend(string path, string content, Encoding encoding)
        {
            FileUtility.EnsurePath(Path.GetFullPath(path));
            FileStream stream1 = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter writer1 = new StreamWriter(stream1, encoding);
            writer1.WriteLine(content);
            writer1.Close();
            stream1.Close();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public static void Delete(string path)
        {
            File.Delete(path);
        }

        /// <summary>
        /// 判断路径是否存在，如果不存在则创建该路径目录结构
        /// </summary>
        /// <param name="path">路径</param>
        private static void EnsurePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 读取指定文件的内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>按行排列文本内容</returns>
        public static string[] Read(string path)
        {
            string text1;
            ArrayList list1 = new ArrayList();
            FileStream stream1 = new FileStream(path, FileMode.Open);
            StreamReader reader1 = new StreamReader(stream1);
            while ((text1 = reader1.ReadLine()) != null)
            {
                list1.Add(text1);
            }
            reader1.Close();
            stream1.Close();
            return (string[])list1.ToArray(typeof(string));
        }
        public static List<string> ReadForList(string path)
        {
            string text1;
            List<string> list1 = new List<string>();
            FileStream stream1 = new FileStream(path, FileMode.Open);
            StreamReader reader1 = new StreamReader(stream1);
            while ((text1 = reader1.ReadLine()) != null)
            {
                list1.Add(text1);
            }
            reader1.Close();
            stream1.Close();
            return list1;
        }
        /// <summary>
        /// 读取指定文件的全部内容
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns>文件内容</returns>
        public static string ReadALL(string Path)
        {
            ArrayList list1 = new ArrayList();
            FileStream stream1 = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader reader1 = new StreamReader(stream1);

            string text1 = reader1.ReadToEnd();
            reader1.Close();
            stream1.Close();
            return text1;
        }

        /// <summary>
        /// 输出二进制附件内容_默认为下载
        /// </summary>
        /// <param name="rs">HttpResponse对象</param>
        /// <param name="attach">附件内容</param>
        /// <param name="title">文件名</param>
        /// <param name="extname">扩展名</param>
        /// <param name="fsize">文件大小</param>
        public static void ResponseAttach(HttpResponse rs, byte[] attach, string title,
            string extname, int fsize)
        {
            rs.ContentType = "application/octet-stream";
            rs.AddHeader("Content-Disposition", "attachment;FileName=" + HttpUtility.UrlEncode(title, System.Text.Encoding.Default) + "." + extname);
            rs.OutputStream.Write(attach, 0, fsize);
            rs.End();
        }
        /// <summary>
        /// 输出二进制附件内容_默认为下载
        /// </summary>
        /// <param name="rs">HttpResponse对象</param>
        /// <param name="attach">附件内容</param>
        /// <param name="fileName">完整文件名</param>
        public static void ResponseAttach(HttpResponse rs, byte[] attach, string fileName)
        {
            rs.ContentType = "application/octet-stream";
            rs.AddHeader("Content-Disposition", "attachment;FileName=" + fileName);
            if (attach != null && attach.Length > 0)
            {
                rs.OutputStream.Write(attach, 0, attach.Length);
                rs.End();
            }
        }

        /// <summary>
        /// DownLoad File to Client
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="Context"></param>
        /// <param name="page"></param>
        public static void DownLoadFile(string FileName, byte[] Context, Page page)
        {
            page.Response.ContentType = "application/octet-stream";
            if (FileName == "") FileName = "Temp";
            page.Response.AddHeader("Content-Disposition", "attachment;FileName=" + FileName);
            if (Context != null && Context.Length > 0)
                page.Response.OutputStream.Write(Context, 0, Context.Length);
            else
                page.Response.BinaryWrite(new byte[1]);
            page.Response.End();
        }

        /// <summary>
        /// 上传二进制附件内容
        /// </summary>
        /// <param name="uploadFile">客户端文件</param>
        /// <param name="attach">保存文件的数组</param>
        ///<param name="fullName">文件名</param>
        /// <param name="extendName">文件扩展名</param>
        /// <param name="fSize">文件大小</param>
        public static void UploadAttach(HttpPostedFile uploadFile, ref byte[] attach, ref string fullName,
            ref string extendName, ref float fSize)
        {
            if (uploadFile.ContentLength > 0)
            {
                string nam = uploadFile.FileName;
                //获取文件名(抱括路径)
                int n = nam.LastIndexOf(@"\");
                int i = nam.LastIndexOf(".");
                //获取文件名
                fullName = nam.Substring(n + 1);
                //获取文件扩展名
                if (i > 0)
                {
                    extendName = nam.Substring(i + 1);
                }
                else
                {
                    extendName = "";
                }
                //获取文件大小
                fSize = uploadFile.ContentLength;

                //获取二进制文件
                attach = new byte[uploadFile.ContentLength];
                Stream StreamObject = uploadFile.InputStream;
                StreamObject.Read(attach, 0, uploadFile.ContentLength);
            }
        }


        /// <summary>
        /// 根据文件物理路径获取二进制数据
        /// </summary>
        /// <param name="uploadFile">HttpPostedFile对象</param>
        /// <returns></returns>
        public static byte[] GetAttach(HttpPostedFile uploadFile)
        {
            byte[] attach = new byte[uploadFile.ContentLength];
            if (uploadFile.ContentLength > 0)
            {
                uploadFile.InputStream.Read(attach, 0, uploadFile.ContentLength);
            }
            return attach;
        }

        /// <summary>
        /// 本地文件物理路径获取二进制数据
        /// 2007-12-8_15:59:01_by_Jie
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] GetFileContent(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return null;
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] retVal = new byte[(int)fs.Length];

                fs.Read(retVal, 0, (int)fs.Length);
                fs.Flush();

                fs.Close();
                return retVal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static byte[] GetFileContent(string FilePath, out string FileName)
        {
            FileName = null;
            if (!File.Exists(FilePath))
                return null;
            FileInfo fi = new FileInfo(FilePath);

            try
            {
                FileStream fs = fi.OpenRead();
                byte[] retVal = new byte[(int)fs.Length];
                fs.Read(retVal, 0, (int)fs.Length);
                fs.Flush();
                fs.Close();
                FileName = fi.Name;
                return retVal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 返回指定目录的所有文件信息
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public static List<FileInfo> GetFileInfoSet(string Path, string searchPattern)
        {
            List<FileInfo> result = new List<FileInfo>();

            DirectoryInfo di = new DirectoryInfo(Path);
            if (searchPattern != null)
                result.AddRange(di.GetFiles(searchPattern));
            else
                result.AddRange(di.GetFiles());
            return result;
        }

        /// <summary>
        /// 访问网络，加载XML文件
        /// </summary>
        /// <param name="Url">网络地址</param>
        /// <returns>XmlDocument</returns>
        public static XmlDocument DwonloadXmlDocument(string Url)
        {
            HttpWebRequest GWP_Request;
            HttpWebResponse GWP_Response = null;
            XmlDocument GWP_XMLdoc = null;
            try
            {
                GWP_Request = (HttpWebRequest)WebRequest.Create(Url);
                GWP_Request.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 6.0; en-UK; rv:1.8.1.4) Gecko/20070515 Firefox/2.0.0.4";
                GWP_Response = (HttpWebResponse)GWP_Request.GetResponse();
                GWP_XMLdoc = new XmlDocument();
                GWP_XMLdoc.Load(GWP_Response.GetResponseStream());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                GWP_Response.Close();
            }
            return GWP_XMLdoc;
        }

        /// <summary>
        /// 转换CSV到DataTable
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable ConvertCSV2DataTable(string fileName, string tableName)
        {
            DataTable dt = new DataTable(tableName);
            string _filePath, _fileName;
            _filePath = fileName.Substring(0, fileName.LastIndexOf(@"\") + 1);
            _fileName = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
            string conStr = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + _filePath + ";Extensions=asc,csv,tab,txt;";
            OdbcConnection oleCon = new OdbcConnection(conStr);
            OdbcDataAdapter da = new OdbcDataAdapter("Select * from " + _fileName, oleCon);
            da.Fill(dt);
            oleCon.Close();
            return dt;
        }

        public static DataTable ConvertCSV2DataTable(string fileName, string tableName, bool IsTextAdapter)
        {
            if (!IsTextAdapter)
                return ConvertCSV2DataTable(fileName, tableName);
            else
            {
                DataTable dt = new DataTable(tableName);
                List<string> Context = ReadForList(fileName);
                if (Context.Count > 0)
                {
                    #region Set Columns
                    string[] cols = Context[0].Split(',');
                    foreach (string col in cols)
                    {
                        dt.Columns.Add(col, typeof(string));
                    }
                    Context.RemoveAt(0);
                    #endregion

                    #region Set Rows
                    if (Context.Count > 0)
                    {
                        foreach (string record in Context)
                        {
                            DataRow dr = dt.Rows.Add();
                            string[] Fields = record.Split(',');
                            for (int i = 0; i < Fields.Length; i++)
                            {
                                string Field = Fields[i];
                                if (Field == string.Empty)
                                {
                                    dr[i] = DBNull.Value;
                                }
                                else
                                {
                                    dr[i] = Field;
                                }
                            }
                        }
                    }
                    #endregion
                }
                return dt;
            }
        }


        public static void DeleteAllFiles(string DirPath)
        {
            Directory.Delete(DirPath, true);
        }


        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }
        /// <summary>
        /// 返回系统设置的图标
        /// </summary>
        /// <param name="pszPath">文件路径 如果为""  返回文件夹的</param>
        /// <param name="dwFileAttributes">0</param>
        /// <param name="psfi">结构体</param>
        /// <param name="cbSizeFileInfo">结构体大小</param>
        /// <param name="uFlags">枚举类型</param>
        /// <returns>-1失败</returns>
        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref   SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
        [DllImport("shell32.dll")]
        private static extern uint ExtractIconEx(string lpszFile, int nIconIndex, int[] phiconLarge, int[] phiconSmall, uint nIcons);

        private enum SHGFI
        {
            SHGFI_ICON = 0x100,
            SHGFI_LARGEICON = 0x0,
            SHGFI_USEFILEATTRIBUTES = 0x10
        }
        /// <summary>
        /// 获取文件图标 
        /// </summary>
        /// <param name="p_Path">文件全路径</param>
        /// <returns>图标</returns>
        public static Icon GetFileIcon(string p_Path)
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(p_Path, 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON | SHGFI.SHGFI_USEFILEATTRIBUTES));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }
        /// <summary>
        /// 获取文件夹图标 
        /// </summary>
        /// <returns>图标</returns>
        public static Icon GetDirectoryIcon()
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(@"", 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }

        /// <summary>
        ///  给出文件扩展名（.*），返回相应图标
        /// 若不以"."开头则返回文件夹的图标。
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="isLarge"></param>
        /// <returns></returns>
        public static Icon GetIconByFileType(string fileType, bool isLarge)
        {
            if (fileType == null || fileType.Equals(string.Empty)) return null;

            RegistryKey regVersion = null;
            string regFileType = null;
            string regIconString = null;
            string systemDirectory = Environment.SystemDirectory + "\\";

            if (fileType[0] == '.')
            {
                //读系统注册表中文件类型信息
                regVersion = Registry.ClassesRoot.OpenSubKey(fileType, true);
                if (regVersion != null)
                {
                    regFileType = regVersion.GetValue("") as string;
                    regVersion.Close();
                    regVersion = Registry.ClassesRoot.OpenSubKey(regFileType + @"\DefaultIcon", true);
                    if (regVersion != null)
                    {
                        regIconString = regVersion.GetValue("") as string;
                        regVersion.Close();
                    }
                }
                if (regIconString == null)
                {
                    //没有读取到文件类型注册信息，指定为未知文件类型的图标
                    regIconString = systemDirectory + "shell32.dll,0";
                }
            }
            else
            {
                //直接指定为文件夹图标
                regIconString = systemDirectory + "shell32.dll,3";
            }
            string[] fileIcon = regIconString.Split(new char[] { ',' });
            if (fileIcon.Length != 2)
            {
                //系统注册表中注册的标图不能直接提取，则返回可执行文件的通用图标
                fileIcon = new string[] { systemDirectory + "shell32.dll", "2" };
            }
            Icon resultIcon = null;
            try
            {
                //调用API方法读取图标
                int[] phiconLarge = new int[1];
                int[] phiconSmall = new int[1];
                uint count = ExtractIconEx(fileIcon[0], Int32.Parse(fileIcon[1]), phiconLarge, phiconSmall, 1);
                IntPtr IconHnd = new IntPtr(isLarge ? phiconLarge[0] : phiconSmall[0]);
                resultIcon = Icon.FromHandle(IconHnd);
            }
            catch { }
            return resultIcon;
        }
    }
}
