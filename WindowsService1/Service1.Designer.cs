namespace WindowsService1
{
    partial class ServiceAsWatchdb
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.fileScanTimer = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.fileScanTimer)).BeginInit();
            this.timerCloseServer = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.timerCloseServer)).BeginInit();
            // 
            // fileScanTimer
            // 
            this.fileScanTimer.Enabled = true;
            this.fileScanTimer.Interval = 1000;
            this.fileScanTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.fileScanTimer_Elapsed);
            // 
            // timerCloseServer
            // 
            this.timerCloseServer.Enabled = true;
            this.timerCloseServer.Interval = 1000;
            this.timerCloseServer.Elapsed += new System.Timers.ElapsedEventHandler(this.timerCloseServer_Elapsed);
             
            // ServiceAsWatchdb
            // 
            this.ServiceName = "ServiceAsWatchdb";
            ((System.ComponentModel.ISupportInitialize)(this.fileScanTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerCloseServer)).EndInit();

        }

        #endregion

        public System.Timers.Timer fileScanTimer;
        public System.Timers.Timer timerCloseServer;


    }
}
