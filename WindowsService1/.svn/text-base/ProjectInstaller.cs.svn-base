using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Management;

namespace WindowsService1
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            SetServiceDesktopInsteract(this.serviceInstaller1.ServiceName);
            System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController();
            sc.ServiceName = this.serviceInstaller1.ServiceName;
            sc.Start();
        }

        protected override void OnAfterInstall(System.Collections.IDictionary savedState)
        {
            RegistryKey rk = Registry.LocalMachine;
            string key = @"SYSTEM\CurrentControlSet\Services\" + this.serviceInstaller1.ServiceName;
            RegistryKey sub = rk.OpenSubKey(key, true);
            int value = (int)sub.GetValue("Type");
            if (value < 256)
            {
                sub.SetValue("Type", value | 256);
            }
            base.OnAfterInstall(savedState);
        }

        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
           
        }

        private void SetServiceDesktopInsteract(string serviceName)
        {
            ManagementObject wmiService = new ManagementObject(string.Format("Win32_Service.Name='{0}'", serviceName));
            ManagementBaseObject changeMethod = wmiService.GetMethodParameters("Change");
            changeMethod["DesktopInteract"] = true;
            ManagementBaseObject OutParam = wmiService.InvokeMethod("Change", changeMethod, null);
        }
    }
}