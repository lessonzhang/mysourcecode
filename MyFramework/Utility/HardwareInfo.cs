using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace MyFramework.Utility
{
   public class HardwareInfo
    {
        /// <summary>
        /// 获取MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetNetCardMacAddress()
        {
            ManagementClass mc;
            ManagementObjectCollection moc;
            mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            moc = mc.GetInstances();
            string str = "";
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                    str = mo["MacAddress"].ToString();

            }
            return str;
        }

        /// <summary>
        /// C盘序列号
        /// </summary>
        /// <returns></returns>
        public static string GetDiskVolumeSerialNumber()
        {
            ManagementObject disk;
            disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }

       /// <summary>
       /// 取CPU ID
       /// </summary>
       /// <returns></returns>
        public static string GetCPUSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();

            String strCpuID = null;
            foreach (ManagementObject mo in moc)
            {
                strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                break;
            }
            return strCpuID;

        }

        /// <summary>
        /// 取第一块硬盘编号
        /// </summary>
        /// <returns></returns>
        public static string GetHardDiskID()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            String strHardDiskID = null;
            foreach (ManagementObject mo in searcher.Get())
            {
                strHardDiskID = mo["SerialNumber"].ToString().Trim();
                break;
            }
            return strHardDiskID;
        }

        /// <summary>
        /// 获取当前网卡IP地址    
        /// </summary>
        /// <returns></returns>
        public static string GetNetCardIP()
        {
            try
            {
                string stringIP = "";
                ManagementClass MC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection MOC = MC.GetInstances();
                foreach (ManagementObject MO in MOC)
                {
                    if ((bool)MO["IPEnabled"] == true)
                    {
                        string[] IPAddresses = (string[])MO["IPAddress"];
                        if (IPAddresses.Length > 0)
                        {
                            stringIP = IPAddresses[0].ToString();
                        }
                    }
                }
                return stringIP;
            }
            catch
            {
                return "";
            }
        }
    }
}
