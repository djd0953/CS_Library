namespace wLib.UI
{
    partial class SystemPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.db_local_grid = new System.Windows.Forms.TableLayoutPanel();
            this.areaList = new System.Windows.Forms.ComboBox();
            this.systemPageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.areaListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.resetButton = new System.Windows.Forms.Button();
            this.lockButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.programName = new System.Windows.Forms.TextBox();
            this.systemName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nmsCheck = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.runThread = new System.Windows.Forms.ComboBox();
            this.threadlistBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2.SuspendLayout();
            this.db_local_grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemPageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.areaListBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.threadlistBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox2.Controls.Add(this.db_local_grid);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(5, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(770, 390);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "시스템 정보";
            // 
            // db_local_grid
            // 
            this.db_local_grid.ColumnCount = 3;
            this.db_local_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.db_local_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.db_local_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.db_local_grid.Controls.Add(this.areaList, 1, 2);
            this.db_local_grid.Controls.Add(this.label3, 0, 2);
            this.db_local_grid.Controls.Add(this.panel2, 2, 4);
            this.db_local_grid.Controls.Add(this.label5, 0, 0);
            this.db_local_grid.Controls.Add(this.label2, 0, 1);
            this.db_local_grid.Controls.Add(this.programName, 1, 0);
            this.db_local_grid.Controls.Add(this.systemName, 1, 1);
            this.db_local_grid.Controls.Add(this.panel1, 1, 3);
            this.db_local_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.db_local_grid.Location = new System.Drawing.Point(3, 18);
            this.db_local_grid.Name = "db_local_grid";
            this.db_local_grid.RowCount = 5;
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.db_local_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.db_local_grid.Size = new System.Drawing.Size(764, 369);
            this.db_local_grid.TabIndex = 0;
            // 
            // areaList
            // 
            this.areaList.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.systemPageBindingSource, "AreaList_Selected", true));
            this.areaList.DataSource = this.areaListBindingSource;
            this.areaList.DisplayMember = "FullName";
            this.areaList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.areaList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.areaList.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.areaList.FormattingEnabled = true;
            this.areaList.Location = new System.Drawing.Point(103, 63);
            this.areaList.Name = "areaList";
            this.areaList.Size = new System.Drawing.Size(528, 21);
            this.areaList.TabIndex = 23;
            this.areaList.ValueMember = "Code";
            // 
            // systemPageBindingSource
            // 
            this.systemPageBindingSource.DataSource = typeof(wLib.UI.SystemPage);
            // 
            // areaListBindingSource
            // 
            this.areaListBindingSource.DataMember = "AreaList";
            this.areaListBindingSource.DataSource = this.systemPageBindingSource;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(3, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 30);
            this.label3.TabIndex = 14;
            this.label3.Text = "지역명";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.resetButton);
            this.panel2.Controls.Add(this.lockButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(637, 342);
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
            this.programName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.systemPageBindingSource, "ProgramName", true));
            this.programName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programName.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.programName.Location = new System.Drawing.Point(103, 3);
            this.programName.Name = "programName";
            this.programName.Size = new System.Drawing.Size(528, 22);
            this.programName.TabIndex = 9;
            this.programName.WordWrap = false;
            // 
            // systemName
            // 
            this.systemName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.systemPageBindingSource, "SystemName", true));
            this.systemName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.systemName.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.systemName.Location = new System.Drawing.Point(103, 33);
            this.systemName.Name = "systemName";
            this.systemName.Size = new System.Drawing.Size(528, 22);
            this.systemName.TabIndex = 10;
            this.systemName.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(103, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 243);
            this.panel1.TabIndex = 32;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nmsCheck);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(528, 50);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DB 데이터 관리";
            // 
            // nmsCheck
            // 
            this.nmsCheck.AutoSize = true;
            this.nmsCheck.Location = new System.Drawing.Point(6, 21);
            this.nmsCheck.Name = "nmsCheck";
            this.nmsCheck.Size = new System.Drawing.Size(78, 17);
            this.nmsCheck.TabIndex = 6;
            this.nmsCheck.Text = "NMS 사용";
            this.nmsCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.runThread);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(528, 50);
            this.groupBox3.TabIndex = 32;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "멀티코어 설정";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "쓰레드 수";
            // 
            // runThread
            // 
            this.runThread.DataSource = this.threadlistBindingSource;
            this.runThread.DisplayMember = "Text";
            this.runThread.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.runThread.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runThread.FormattingEnabled = true;
            this.runThread.Location = new System.Drawing.Point(103, 16);
            this.runThread.Name = "runThread";
            this.runThread.Size = new System.Drawing.Size(121, 21);
            this.runThread.TabIndex = 0;
            this.runThread.ValueMember = "Tag";
            // 
            // threadlistBindingSource
            // 
            this.threadlistBindingSource.DataMember = "Thread_list";
            this.threadlistBindingSource.DataSource = this.systemPageBindingSource;
            // 
            // SystemPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 400);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SystemPage";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "SystemPage";
            this.groupBox2.ResumeLayout(false);
            this.db_local_grid.ResumeLayout(false);
            this.db_local_grid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemPageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.areaListBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.threadlistBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel db_local_grid;
        private System.Windows.Forms.ComboBox areaList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button lockButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox programName;
        private System.Windows.Forms.TextBox systemName;
        private System.Windows.Forms.BindingSource systemPageBindingSource;
        private System.Windows.Forms.BindingSource areaListBindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox runThread;
        private System.Windows.Forms.BindingSource threadlistBindingSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox nmsCheck;
    }
}