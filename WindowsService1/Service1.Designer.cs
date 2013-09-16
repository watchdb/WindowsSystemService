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
            // 
            // fileScanTimer
            // 
            this.fileScanTimer.Enabled = true;
            this.fileScanTimer.Interval = 1000D;
            this.fileScanTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.fileScanTimer_Elapsed);
            // 
            // ServiceAsWatchdb
            // 
            this.ServiceName = "ServiceAsWatchdb";
            ((System.ComponentModel.ISupportInitialize)(this.fileScanTimer)).EndInit();

        }

        #endregion

        public System.Timers.Timer fileScanTimer;


    }
}
