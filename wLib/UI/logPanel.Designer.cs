namespace wLib.UI
{
    partial class logPanel
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.LogText = new System.Windows.Forms.TextBox();
            this.logExpender = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.LogText, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.logExpender, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(400, 200);
            this.tableLayoutPanel.TabIndex = 14;
            // 
            // LogText
            // 
            this.LogText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogText.Location = new System.Drawing.Point(3, 35);
            this.LogText.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.LogText.Multiline = true;
            this.LogText.Name = "LogText";
            this.LogText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogText.Size = new System.Drawing.Size(394, 160);
            this.LogText.TabIndex = 5;
            // 
            // logExpender
            // 
            this.logExpender.AutoSize = true;
            this.logExpender.Dock = System.Windows.Forms.DockStyle.Left;
            this.logExpender.FlatAppearance.BorderSize = 0;
            this.logExpender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logExpender.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.logExpender.Location = new System.Drawing.Point(3, 3);
            this.logExpender.Name = "logExpender";
            this.logExpender.Size = new System.Drawing.Size(69, 24);
            this.logExpender.TabIndex = 3;
            this.logExpender.TabStop = false;
            this.logExpender.Text = "로그 △";
            this.logExpender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.logExpender.UseVisualStyleBackColor = true;
            this.logExpender.MouseDown += new System.Windows.Forms.MouseEventHandler(this.On_MouseDown_log);
            // 
            // logPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "logPanel";
            this.Size = new System.Drawing.Size(400, 200);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button logExpender;
        public System.Windows.Forms.TextBox LogText;
    }
}
