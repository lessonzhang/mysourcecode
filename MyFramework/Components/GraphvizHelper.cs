using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Drawing.Imaging;
using System.Drawing;


namespace MyFramework.Components
{
    public class GraphvizHelper
    {

  public static string GetNodeMap(string usidcourser,string strData)  
  {  
           string dotSource_temp = strData;
           string dotFileName_temp = usidcourser;  
            string dirPath = GetTempDirPath();  
           if (dirPath == "") {  
                return "";  
          }  
            string dotFileName = Path.ChangeExtension(Path.Combine(dirPath , Path.GetFileName(dotFileName_temp)), ".dot");  
            string pngFile = Path.ChangeExtension(dotFileName, ".svg");  
           //string pngFile_temp=  
          using (StreamWriter writer = new StreamWriter(dotFileName))  
            {  
                writer.Write(dotSource_temp);  
            }  
           string dotExePath_temp = System.Configuration.ConfigurationManager.AppSettings["dotExePath"] as string;  
           //调用服务器端exe进程  
           ProcessStartInfo info = new ProcessStartInfo()  
           {  
               FileName = dotExePath_temp,  
              WorkingDirectory = Path.GetDirectoryName(dotFileName),
               Arguments = string.Concat("-Tsvg " + dotFileName + " -o "+ pngFile),  
                RedirectStandardInput = false,  
                RedirectStandardOutput = false,  
                RedirectStandardError = true,  
               UseShellExecute = false,  
               CreateNoWindow = true  
            };  
            try  
           {  
                using (Process exe = Process.Start(info))  
                {  
                   exe.WaitForExit();
                   if (0 == exe.ExitCode)
                   {
                      // System.Web.HttpContext.Current.Response.Write(pngFile);
                   }
                   else
                   {
                       string errMsg;
                       using (StreamReader stdErr = exe.StandardError)
                       {
                           errMsg = stdErr.ReadToEnd();
                       }
                      // System.Web.HttpContext.Current.Response.Write(errMsg);  
                   }  
               }  
            }  
           catch(Exception ex)  
         {  
               return "";  
           }  
            String path="~/TempFile/Graphviz/"+Path.GetFileName(pngFile);  
           return path;  
       }//---- end of GetNodeMap ----  

         private static string GetTempDirPath()  
      {  
          string path = System.Web.HttpContext.Current.Server.MapPath("~")+"\\TempFile\\Graphviz";  
       if (!Function.CreateDir(path))  
          {  
               return "";  
           }  
           return path;  
      } 

    }
    public class Function  
   {  
       /// <summary>  
       /// 根据目录创建文件夹，如果存在就不创建  
       /// </summary>  
       /// <param name="path"></param>  
      /// <returns></returns>  
     public static bool CreateDir(string path)  
     {  
          bool result = true;  
           DirectoryInfo dir = new DirectoryInfo(path);  
         //目录不存在  

         if (!dir.Exists)  
          {  
              try  
                {  
                   dir.Create();  

               }  
                catch (Exception e)  
                {  
                   result = false;  
               }  
           }  
 
           return result;  
        }  
}
}
