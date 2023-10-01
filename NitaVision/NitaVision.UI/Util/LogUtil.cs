using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NitaVision.UI.Util
{
    /// <summary>
    /// 日志工具类
    /// </summary>
    public class LogUtil
    {
        private static readonly string dirName = "Log";
        private static readonly string dateFormat = "yyyyMMdd";
        private static readonly string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private static readonly int LocalLogRetryTimes = 3;
        private static readonly int LocalLogRetryInterval = 3 * 1000;
        public static DateTime TimeCountTime = DateTime.Now;
        private static string detailPath = string.Empty;
        private static object syncObj = new object();
        public static bool LogEnable
        {
            get; set;
        }
        /// <summary>
        /// 明细目录
        /// </summary>
        public static string DetailPath
        {
            get { return detailPath; }
            set { detailPath = value; }
        }
        /// <summary>
        /// 记录日志信息   
        /// </summary>
        /// <param name="content">日志信息</param>
        public static void Log(string content, string folder = "")
        {
            lock (syncObj)
            {
                int retryTime = 0;
            logRetry:
                try
                {
                    string path = Path.Combine(GetAppPath(), detailPath);
                    if (!string.IsNullOrEmpty(folder))
                        path = Path.Combine(path, folder);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    path = Path.Combine(path, string.Format("{0}.txt", DateTime.Now.ToString(dateFormat)));

                    StringBuilder sb = new StringBuilder(512);

                    sb.Append(DateTime.Now.ToString(dateTimeFormat));
                    sb.Append("=>");
                    sb.AppendLine(content);

                    File.AppendAllText(path, sb.ToString());
                }
                catch (Exception e)
                {
                    if (retryTime <= LocalLogRetryTimes && e.ToString().IndexOf("正由另一进程使用") >= 0)
                    {
                        Thread.Sleep(LocalLogRetryInterval);
                        retryTime++;
                        goto logRetry;
                    }
                }
            }
        }

        /// <summary>
        /// 当前应用路径
        /// </summary>
        /// <returns></returns>
        private static string GetAppPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dirName);
        }
        
    }
}
