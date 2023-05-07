namespace wLib.UI
{
    partial class TitlePanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TitlePanel));
            this.topLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.areaName = new System.Windows.Forms.Label();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.programName = new System.Windows.Forms.Label();
            this.systemName = new System.Windows.Forms.Label();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.topLayoutPanel.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // topLayoutPanel
            // 
            this.topLayoutPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("topLayoutPanel.BackgroundImage")));
            this.topLayoutPanel.ColumnCount = 3;
            this.topLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.topLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.topLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topLayoutPanel.Controls.Add(this.areaName, 0, 0);
            this.topLayoutPanel.Controls.Add(this.flowLayoutPanel, 0, 0);
            this.topLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.topLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.topLayoutPanel.Name = "topLayoutPanel";
            this.topLayoutPanel.RowCount = 1;
            this.topLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topLayoutPanel.Size = new System.Drawing.Size(380, 40);
            this.topLayoutPanel.TabIndex = 16;
            // 
            // areaName
            // 
            this.areaName.BackColor = System.Drawing.Color.Transparent;
            this.areaName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "AreaName", true));
            this.areaName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.areaName.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.areaName.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.areaName.Location = new System.Drawing.Point(323, 0);
            this.areaName.Name = "areaName";
            this.areaName.Size = new System.Drawing.Size(54, 40);
            this.areaName.TabIndex = 5;
            this.areaName.Text = "ㅇㅇ시";
            this.areaName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.topLayoutPanel.SetColumnSpan(this.flowLayoutPanel, 2);
            this.flowLayoutPanel.Controls.Add(this.programName);
            this.flowLayoutPanel.Controls.Add(this.systemName);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(314, 34);
            this.flowLayoutPanel.TabIndex = 2;
            // 
            // programName
            // 
            this.programName.AutoSize = true;
            this.programName.BackColor = System.Drawing.Color.Transparent;
            this.programName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "ProgramName", true));
            this.programName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programName.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.programName.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.programName.Location = new System.Drawing.Point(3, 0);
            this.programName.Name = "programName";
            this.programName.Size = new System.Drawing.Size(145, 30);
            this.programName.TabIndex = 1;
            this.programName.Text = "ㅇㅇ프로그램";
            this.programName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // systemName
            // 
            this.systemName.AutoSize = true;
            this.systemName.BackColor = System.Drawing.Color.Transparent;
            this.systemName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "SystemName", true));
            this.systemName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.systemName.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.systemName.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.systemName.Location = new System.Drawing.Point(154, 0);
            this.systemName.Name = "systemName";
            this.systemName.Size = new System.Drawing.Size(122, 30);
            this.systemName.TabIndex = 2;
            this.systemName.Text = "(ㅇㅇㅇ 시스템)";
            this.systemName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(wLib.UI.TitlePanel);
            // 
            // TitlePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.topLayoutPanel);
            this.Name = "TitlePanel";
            this.Size = new System.Drawing.Size(380, 40);
            this.topLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel topLayoutPanel;
        private System.Windows.Forms.Label areaName;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Label programName;
        private System.Windows.Forms.Label systemName;
        private System.Windows.Forms.BindingSource bindingSource;
    }
}
