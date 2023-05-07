
namespace wLib
{
    partial class dbPanel
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
            this.db_local_panel = new System.Windows.Forms.GroupBox();
            this.db_local_grid = new System.Windows.Forms.TableLayoutPanel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.resetButton = new System.Windows.Forms.Button();
            this.lockButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.testButton_ip = new System.Windows.Forms.Button();
            this.testButton_port = new System.Windows.Forms.Button();
            this.testButton_db = new System.Windows.Forms.Button();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.db_BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtDb = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtPw = new System.Windows.Forms.TextBox();
            this.db_local_panel.SuspendLayout();
            this.db_local_grid.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.db_BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // db_local_panel
            // 
            this.db_local_panel.BackColor = System.Drawing.SystemColors.Window;
            this.db_local_panel.Controls.Add(this.db_local_grid);
            this.db_local_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.db_local_panel.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.db_local_panel.Location = new System.Drawing.Point(0, 0);
            this.db_local_panel.Name = "db_local_panel";
            this.db_local_panel.Size = new System.Drawing.Size(780, 400);
            this.db_local_panel.TabIndex = 7;
            this.db_local_panel.TabStop = false;
            this.db_local_panel.Text = "DB 정보";
            // 
            // db_local_grid
            // 
            this.db_local_grid.BackColor = System.Drawing.SystemColors.Window;
            this.db_local_grid.ColumnCount = 3;
            this.db_local_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.db_local_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.db_local_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.db_local_grid.Controls.Add(this.buttonPanel, 2, 6);
            this.db_local_grid.Controls.Add(this.label5, 0, 1);
            this.db_local_grid.Controls.Add(this.label4, 0, 4);
            this.db_local_grid.Controls.Add(this.label3, 0, 3);
            this.db_local_grid.Controls.Add(this.label2, 0, 2);
            this.db_local_grid.Controls.Add(this.label1, 0, 0);
            this.db_local_grid.Controls.Add(this.testButton_ip, 2, 0);
            this.db_local_grid.Controls.Add(this.testButton_port, 2, 1);
            this.db_local_grid.Controls.Add(this.testButton_db, 2, 2);
            this.db_local_grid.Controls.Add(this.txtIp, 1, 0);
            this.db_local_grid.Controls.Add(this.txtPort, 1, 1);
            this.db_local_grid.Controls.Add(this.txtDb, 1, 2);
            this.db_local_grid.Controls.Add(this.txtId, 1, 3);
            this.db_local_grid.Controls.Add(this.txtPw, 1, 4);
            this.db_local_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.db_local_grid.Location = new System.Drawing.Point(3, 18);
            this.db_local_grid.Name = "db_local_grid";
            this.db_local_grid.RowCount = 7;
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.db_local_grid.Size = new System.Drawing.Size(774, 379);
            this.db_local_grid.TabIndex = 0;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.resetButton);
            this.buttonPanel.Controls.Add(this.lockButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(647, 352);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(124, 24);
            this.buttonPanel.TabIndex = 14;
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
            this.label5.Location = new System.Drawing.Point(3, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "PORT";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(3, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "PW";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(3, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "ID";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "DBNAME";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // testButton_ip
            // 
            this.testButton_ip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testButton_ip.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.testButton_ip.Location = new System.Drawing.Point(647, 3);
            this.testButton_ip.Name = "testButton_ip";
            this.testButton_ip.Size = new System.Drawing.Size(124, 19);
            this.testButton_ip.TabIndex = 5;
            this.testButton_ip.Text = "IP 테스트";
            this.testButton_ip.UseVisualStyleBackColor = true;
            // 
            // testButton_port
            // 
            this.testButton_port.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testButton_port.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.testButton_port.Location = new System.Drawing.Point(647, 28);
            this.testButton_port.Name = "testButton_port";
            this.testButton_port.Size = new System.Drawing.Size(124, 19);
            this.testButton_port.TabIndex = 6;
            this.testButton_port.Text = "PORT 테스트";
            this.testButton_port.UseVisualStyleBackColor = true;
            // 
            // testButton_db
            // 
            this.testButton_db.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testButton_db.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.testButton_db.Location = new System.Drawing.Point(647, 53);
            this.testButton_db.Name = "testButton_db";
            this.testButton_db.Size = new System.Drawing.Size(124, 19);
            this.testButton_db.TabIndex = 7;
            this.testButton_db.Text = "DB 테스트";
            this.testButton_db.UseVisualStyleBackColor = true;
            // 
            // txtIp
            // 
            this.txtIp.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.db_BindingSource, "Ip", true));
            this.txtIp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIp.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtIp.Location = new System.Drawing.Point(103, 3);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(538, 22);
            this.txtIp.TabIndex = 8;
            this.txtIp.WordWrap = false;
            // 
            // db_BindingSource
            // 
            this.db_BindingSource.DataSource = typeof(wLib.DB_CONF);
            // 
            // txtPort
            // 
            this.txtPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.db_BindingSource, "Port", true));
            this.txtPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPort.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPort.Location = new System.Drawing.Point(103, 28);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(538, 22);
            this.txtPort.TabIndex = 9;
            this.txtPort.WordWrap = false;
            // 
            // txtDb
            // 
            this.txtDb.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.db_BindingSource, "Dbname", true));
            this.txtDb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDb.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtDb.Location = new System.Drawing.Point(103, 53);
            this.txtDb.Name = "txtDb";
            this.txtDb.Size = new System.Drawing.Size(538, 22);
            this.txtDb.TabIndex = 10;
            this.txtDb.WordWrap = false;
            // 
            // txtId
            // 
            this.txtId.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.db_BindingSource, "Id", true));
            this.txtId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtId.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtId.Location = new System.Drawing.Point(103, 78);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(538, 22);
            this.txtId.TabIndex = 11;
            this.txtId.WordWrap = false;
            // 
            // txtPw
            // 
            this.txtPw.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.db_BindingSource, "Pw", true));
            this.txtPw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPw.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPw.Location = new System.Drawing.Point(103, 103);
            this.txtPw.Name = "txtPw";
            this.txtPw.PasswordChar = '*';
            this.txtPw.Size = new System.Drawing.Size(538, 22);
            this.txtPw.TabIndex = 12;
            this.txtPw.WordWrap = false;
            // 
            // dbPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.db_local_panel);
            this.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "dbPanel";
            this.Size = new System.Drawing.Size(780, 400);
            this.Load += new System.EventHandler(this.On_Load);
            this.db_local_panel.ResumeLayout(false);
            this.db_local_grid.ResumeLayout(false);
            this.db_local_grid.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.db_BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox db_local_panel;
        private System.Windows.Forms.TableLayoutPanel db_local_grid;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button lockButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button testButton_ip;
        private System.Windows.Forms.Button testButton_port;
        private System.Windows.Forms.Button testButton_db;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtDb;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtPw;
        private System.Windows.Forms.BindingSource db_BindingSource;
    }
}
