
namespace wLib
{
    partial class systemPanel
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.db_local_grid = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.resetButton = new System.Windows.Forms.Button();
            this.lockButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.programName = new System.Windows.Forms.TextBox();
            this.appBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.systemName = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.db_local_grid.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox2.Controls.Add(this.db_local_grid);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(780, 400);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "시스템 정보";
            // 
            // db_local_grid
            // 
            this.db_local_grid.ColumnCount = 3;
            this.db_local_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.db_local_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.db_local_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.db_local_grid.Controls.Add(this.panel2, 2, 3);
            this.db_local_grid.Controls.Add(this.label5, 0, 0);
            this.db_local_grid.Controls.Add(this.label2, 0, 1);
            this.db_local_grid.Controls.Add(this.programName, 1, 0);
            this.db_local_grid.Controls.Add(this.systemName, 1, 1);
            this.db_local_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.db_local_grid.Location = new System.Drawing.Point(3, 18);
            this.db_local_grid.Name = "db_local_grid";
            this.db_local_grid.RowCount = 4;
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.db_local_grid.Size = new System.Drawing.Size(774, 379);
            this.db_local_grid.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.resetButton);
            this.panel2.Controls.Add(this.lockButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(647, 352);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(124, 24);
            this.panel2.TabIndex = 13;
            // 
            // resetButton
            // 
            this.resetButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetButton.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.resetButton.Location = new System.Drawing.Point(4, 0);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(60, 24);
            this.resetButton.TabIndex = 1;
            this.resetButton.Text = "초기화";
            this.resetButton.UseVisualStyleBackColor = true;
            // 
            // lockButton
            // 
            this.lockButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.lockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lockButton.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lockButton.Location = new System.Drawing.Point(64, 0);
            this.lockButton.Name = "lockButton";
            this.lockButton.Size = new System.Drawing.Size(60, 24);
            this.lockButton.TabIndex = 0;
            this.lockButton.Text = "설정";
            this.lockButton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 30);
            this.label5.TabIndex = 4;
            this.label5.Text = "프로그램명";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "시스템명";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // programName
            // 
            this.programName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.appBindingSource, "program_name", true));
            this.programName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programName.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.programName.Location = new System.Drawing.Point(103, 3);
            this.programName.Name = "programName";
            this.programName.Size = new System.Drawing.Size(538, 22);
            this.programName.TabIndex = 9;
            this.programName.WordWrap = false;
            // 
            // appBindingSource
            // 
            this.appBindingSource.DataSource = typeof(wLib.APP_CONF);
            // 
            // systemName
            // 
            this.systemName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.appBindingSource, "system_name", true));
            this.systemName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.systemName.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.systemName.Location = new System.Drawing.Point(103, 33);
            this.systemName.Name = "systemName";
            this.systemName.Size = new System.Drawing.Size(538, 22);
            this.systemName.TabIndex = 10;
            this.systemName.WordWrap = false;
            // 
            // systemPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "systemPanel";
            this.Size = new System.Drawing.Size(780, 400);
            this.groupBox2.ResumeLayout(false);
            this.db_local_grid.ResumeLayout(false);
            this.db_local_grid.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.appBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel db_local_grid;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button lockButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox systemName;
        public System.Windows.Forms.TextBox programName;
        private System.Windows.Forms.BindingSource appBindingSource;
    }
}
