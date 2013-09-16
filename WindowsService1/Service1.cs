using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Management;
using System.Globalization;
using WindowsServiceAsWatchdb;
using LitJson;

namespace WindowsService1
{
    public partial class ServiceAsWatchdb : ServiceBase
    {
        public static JsonData configData;

        public ServiceAsWatchdb()
        {
            InitializeComponent();
        } 

        /// <summary>
        /// 服务启动调用方法
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {            
            // TODO: 在此处添加代码以启动服务。
            fileScanTimer.Enabled = true;

            //加载配置文件
            loadConfig();

            int interval = 30000;
            interval = ((int)configData["interval"]) * 1000;//每次检测间隔时间
            fileScanTimer.Interval = interval;//单位：MS（秒）

            //开启检测定时器
            fileScanTimer.Start();
        }

        /// <summary>
        /// 服务关闭调用方法
        /// </summary>
        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            try
            {
                fileScanTimer.Enabled = false;
                fileScanTimer.Stop();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 加载配置信息
        /// </summary>
        private void loadConfig()
        {      
            string configFile = AppDomain.CurrentDomain.BaseDirectory + "config.ini";
            if (!File.Exists(configFile))
            {
                throw new Exception("config file is not exists !");
            }

            StreamReader sr = new StreamReader(configFile);
            string config = sr.ReadToEnd();
            configData = JsonMapper.ToObject(config);
        }

        /// <summary>
        /// 生成系统日志
        /// </summary>
        /// <param name="strLog"></param>
        public static void WriteLog(string strLog)
        {
            EventLog log = new EventLog();
            log.Source = "ServiceAsWatchdb";
            log.WriteEntry(strLog);
        }

        /// <summary>
        /// 执行DOS命令，返回DOS命令的输出
        /// </summary>
        /// <param name="dosCommand">dos命令</param>
        /// <returns>返回输出，如果发生异常，返回空字符串</returns>
        public static string ExecuteDos(string dosCommand)
        {
            string output = ""; //输出字符串
            if (dosCommand != null && dosCommand != "")
            {
                Process process = new Process(); //创建进程对象
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe"; //设定需要执行的命令 
                startInfo.Arguments = "/C \"" + dosCommand + "\""; //设定参数，其中的“/C”表示执行完命令后马上退出
                startInfo.UseShellExecute = false; //不使用系统外壳程序启动
                startInfo.RedirectStandardInput = false; //不重定向输入
                startInfo.RedirectStandardOutput = false; //重定向输出
                startInfo.CreateNoWindow = false; //不创建窗口 
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.StartInfo = startInfo;
                 
                try   
                {
                    Process p = new Process();

                    try
                    {
                        if (process.Start()) //开始进程
                        {
                            //process.WaitForExit(); //这里无限等待进程结束
                            output = dosCommand + "程序执行成功。";
                        }
                        WriteLog( dosCommand + " 执行成功！ 执行时间：" + DateTime.Now.ToString());
                    }
                    catch (Exception ex)
                    {
                        WriteLog("Dos命令" + dosCommand + "执行失败:" + ex.Message + "，时间" + DateTime.Now.ToString());
                    }
                    finally
                    {
                        if (process != null)
                            process.Close();
                    }
                }
                catch
                {
                    WriteLog("Dos命令" + dosCommand + "执行失败，时间" + DateTime.Now.ToString());
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
            }

            return output;

        }

        /// <summary>
        /// 定时器，检测相关服务是否被启动，否则自动启动
        /// 2010年12月26日16:31:07  
        /// modify date: 2012年9月16日0:01:11
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileScanTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //停止检测
            fileScanTimer.Enabled = false;  
            try
            {
                JsonData appItems = configData["apps"];
                int itemCnt = appItems.Count;
                string appPath;
                string processName;
                string startDate;
                string endDate;

                // 数组 items 中项的数量 
                foreach (JsonData app in appItems)
                // 遍历数组 items            
                {
                    if (app.ToJson().Contains("startDate"))
                    {
                        startDate = app["startDate"].ToString();
                        appPath = (string)app["path"];
                        if (DateTime.Now.ToString("HH:mm").Equals(startDate) && appPath != String.Empty)
                        {
                            ExecuteDos(appPath);
                        }
                    }
                    else if (app.ToJson().Contains("endDate"))
                    {
                        endDate = app["endDate"].ToString();
                        processName = (string)app["processName"];
                        if (DateTime.Now.ToString("HH:mm").Equals(endDate) && System.Diagnostics.Process.GetProcessesByName(processName).Length > 0 && processName != String.Empty)
                        {
                            ProcessUtility.KillTree(System.Diagnostics.Process.GetProcessesByName(processName)[0].Id);
                            WriteLog(processName + "进程结束成功。结束时间：" + DateTime.Now.ToString());
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                WriteLog("启动进程报错，时间" + DateTime.Now.ToString() + ".错误信息：" + ex.Message);
                return;
            }
            //开启检测
            fileScanTimer.Enabled = true;  
        }
    }
}
