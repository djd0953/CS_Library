namespace wLib.UI
{
    partial class AgentPanel
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentPanel));
            this.process_timer = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            this.clockPanel = new System.Windows.Forms.Label();
            this.lv_ProcessList = new System.Windows.Forms.ListView();
            this.logPanel = new wLib.UI.logPanel();
            this.refresh_timer = new System.Windows.Forms.Timer(this.components);
            this.mainLayoutPanel.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // process_timer
            // 
            this.process_timer.Interval = 1000;
            this.process_timer.Tick += new System.EventHandler(this.process_timer_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "orangered.ico");
            this.imageList1.Images.SetKeyName(1, "blue.ico");
            this.imageList1.Images.SetKeyName(2, "black.ico");
            // 
            // mainLayoutPanel
            // 
            this.mainLayoutPanel.ColumnCount = 1;
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayoutPanel.Controls.Add(this.progressBar, 0, 3);
            this.mainLayoutPanel.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.mainLayoutPanel.Controls.Add(this.lv_ProcessList, 0, 0);
            this.mainLayoutPanel.Controls.Add(this.logPanel, 0, 1);
            this.mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainLayoutPanel.Name = "mainLayoutPanel";
            this.mainLayoutPanel.RowCount = 4;
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.mainLayoutPanel.Size = new System.Drawing.Size(430, 200);
            this.mainLayoutPanel.TabIndex = 21;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(0, 199);
            this.progressBar.Margin = new System.Windows.Forms.Padding(0);
            this.progressBar.Maximum = 60;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(430, 1);
            this.progressBar.TabIndex = 24;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btn_stop, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_Start, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.clockPanel, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 169);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(430, 30);
            this.tableLayoutPanel3.TabIndex = 23;
            // 
            // btn_stop
            // 
            this.btn_stop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_stop.Location = new System.Drawing.Point(377, 3);
            this.btn_stop.Margin = new System.Windows.Forms.Padding(0);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(50, 24);
            this.btn_stop.TabIndex = 20;
            this.btn_stop.Text = "중지";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Start.Location = new System.Drawing.Point(327, 3);
            this.btn_Start.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(50, 24);
            this.btn_Start.TabIndex = 19;
            this.btn_Start.Text = "실행";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // clockPanel
            // 
            this.clockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clockPanel.Location = new System.Drawing.Point(6, 3);
            this.clockPanel.Name = "clockPanel";
            this.clockPanel.Size = new System.Drawing.Size(144, 24);
            this.clockPanel.TabIndex = 11;
            this.clockPanel.Text = "yyyy-MM-dd hh:mm:ss";
            this.clockPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lv_ProcessList
            // 
            this.lv_ProcessList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lv_ProcessList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv_ProcessList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_ProcessList.FullRowSelect = true;
            this.lv_ProcessList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_ProcessList.HideSelection = false;
            this.lv_ProcessList.LabelWrap = false;
            this.lv_ProcessList.Location = new System.Drawing.Point(3, 3);
            this.lv_ProcessList.Name = "lv_ProcessList";
            this.lv_ProcessList.Size = new System.Drawing.Size(424, 133);
            this.lv_ProcessList.TabIndex = 21;
            this.lv_ProcessList.UseCompatibleStateImageBehavior = false;
            this.lv_ProcessList.View = System.Windows.Forms.View.Details;
            this.lv_ProcessList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lv_ProcessList_MouseClick);
            // 
            // logPanel
            // 
            this.logPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logPanel.Location = new System.Drawing.Point(3, 142);
            this.logPanel.Name = "logPanel";
            this.logPanel.Size = new System.Drawing.Size(424, 24);
            this.logPanel.TabIndex = 22;
            // 
            // refresh_timer
            // 
            this.refresh_timer.Enabled = true;
            this.refresh_timer.Interval = 10000;
            this.refresh_timer.Tick += new System.EventHandler(this.refresh_timer_Tick);
            // 
            // AgentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(430, 200);
            this.Controls.Add(this.mainLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AgentPanel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AgentPanel_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AgentPanel_FormClosed);
            this.Load += new System.EventHandler(this.AgentPanel_Load);
            this.Shown += new System.EventHandler(this.AgentPanel_Shown);
            this.mainLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer process_timer;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private System.Windows.Forms.ListView lv_ProcessList;
        private logPanel logPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Label clockPanel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Timer refresh_timer;
    }
}
