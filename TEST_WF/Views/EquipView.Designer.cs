namespace TEST_WF.Views
{
    partial class EquipView
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.wBEQUIPVOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.keyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cddistobsvDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nmdistobsvDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connIpDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connPortDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connPhoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.useYNDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subObCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subobsvDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wBEQUIPVOBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(764, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(764, 311);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(756, 285);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 374);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(531, 21);
            this.textBox1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.keyDataGridViewTextBoxColumn,
            this.cddistobsvDataGridViewTextBoxColumn,
            this.nmdistobsvDataGridViewTextBoxColumn,
            this.connIpDataGridViewTextBoxColumn,
            this.connPortDataGridViewTextBoxColumn,
            this.connPhoneDataGridViewTextBoxColumn,
            this.lastDateDataGridViewTextBoxColumn,
            this.lastStatusDataGridViewTextBoxColumn,
            this.useYNDataGridViewTextBoxColumn,
            this.detCodeDataGridViewTextBoxColumn,
            this.subObCountDataGridViewTextBoxColumn,
            this.subobsvDataGridViewTextBoxColumn,
            this.dataDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.wBEQUIPVOBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(8, 46);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(531, 236);
            this.dataGridView1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(589, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 26);
            this.button1.TabIndex = 2;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(589, 141);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 26);
            this.button2.TabIndex = 3;
            this.button2.Text = "Add New";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(589, 173);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(93, 26);
            this.button3.TabIndex = 4;
            this.button3.Text = "Edit";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(589, 205);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 26);
            this.button4.TabIndex = 5;
            this.button4.Text = "Delete";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // wBEQUIPVOBindingSource
            // 
            this.wBEQUIPVOBindingSource.DataSource = typeof(wLib.DB.NDMS_EQUIP_VO);
            // 
            // keyDataGridViewTextBoxColumn
            // 
            this.keyDataGridViewTextBoxColumn.DataPropertyName = "Key";
            this.keyDataGridViewTextBoxColumn.HeaderText = "Key";
            this.keyDataGridViewTextBoxColumn.Name = "keyDataGridViewTextBoxColumn";
            this.keyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cddistobsvDataGridViewTextBoxColumn
            // 
            this.cddistobsvDataGridViewTextBoxColumn.DataPropertyName = "Cd_dist_obsv";
            this.cddistobsvDataGridViewTextBoxColumn.HeaderText = "Cd_dist_obsv";
            this.cddistobsvDataGridViewTextBoxColumn.Name = "cddistobsvDataGridViewTextBoxColumn";
            // 
            // nmdistobsvDataGridViewTextBoxColumn
            // 
            this.nmdistobsvDataGridViewTextBoxColumn.DataPropertyName = "Nm_dist_obsv";
            this.nmdistobsvDataGridViewTextBoxColumn.HeaderText = "Nm_dist_obsv";
            this.nmdistobsvDataGridViewTextBoxColumn.Name = "nmdistobsvDataGridViewTextBoxColumn";
            // 
            // connIpDataGridViewTextBoxColumn
            // 
            this.connIpDataGridViewTextBoxColumn.DataPropertyName = "ConnIp";
            this.connIpDataGridViewTextBoxColumn.HeaderText = "ConnIp";
            this.connIpDataGridViewTextBoxColumn.Name = "connIpDataGridViewTextBoxColumn";
            // 
            // connPortDataGridViewTextBoxColumn
            // 
            this.connPortDataGridViewTextBoxColumn.DataPropertyName = "ConnPort";
            this.connPortDataGridViewTextBoxColumn.HeaderText = "ConnPort";
            this.connPortDataGridViewTextBoxColumn.Name = "connPortDataGridViewTextBoxColumn";
            // 
            // connPhoneDataGridViewTextBoxColumn
            // 
            this.connPhoneDataGridViewTextBoxColumn.DataPropertyName = "ConnPhone";
            this.connPhoneDataGridViewTextBoxColumn.HeaderText = "ConnPhone";
            this.connPhoneDataGridViewTextBoxColumn.Name = "connPhoneDataGridViewTextBoxColumn";
            // 
            // lastDateDataGridViewTextBoxColumn
            // 
            this.lastDateDataGridViewTextBoxColumn.DataPropertyName = "LastDate";
            this.lastDateDataGridViewTextBoxColumn.HeaderText = "LastDate";
            this.lastDateDataGridViewTextBoxColumn.Name = "lastDateDataGridViewTextBoxColumn";
            // 
            // lastStatusDataGridViewTextBoxColumn
            // 
            this.lastStatusDataGridViewTextBoxColumn.DataPropertyName = "LastStatus";
            this.lastStatusDataGridViewTextBoxColumn.HeaderText = "LastStatus";
            this.lastStatusDataGridViewTextBoxColumn.Name = "lastStatusDataGridViewTextBoxColumn";
            // 
            // useYNDataGridViewTextBoxColumn
            // 
            this.useYNDataGridViewTextBoxColumn.DataPropertyName = "Use_YN";
            this.useYNDataGridViewTextBoxColumn.HeaderText = "Use_YN";
            this.useYNDataGridViewTextBoxColumn.Name = "useYNDataGridViewTextBoxColumn";
            // 
            // detCodeDataGridViewTextBoxColumn
            // 
            this.detCodeDataGridViewTextBoxColumn.DataPropertyName = "DetCode";
            this.detCodeDataGridViewTextBoxColumn.HeaderText = "DetCode";
            this.detCodeDataGridViewTextBoxColumn.Name = "detCodeDataGridViewTextBoxColumn";
            // 
            // subObCountDataGridViewTextBoxColumn
            // 
            this.subObCountDataGridViewTextBoxColumn.DataPropertyName = "SubObCount";
            this.subObCountDataGridViewTextBoxColumn.HeaderText = "SubObCount";
            this.subObCountDataGridViewTextBoxColumn.Name = "subObCountDataGridViewTextBoxColumn";
            // 
            // subobsvDataGridViewTextBoxColumn
            // 
            this.subobsvDataGridViewTextBoxColumn.DataPropertyName = "sub_obsv";
            this.subobsvDataGridViewTextBoxColumn.HeaderText = "sub_obsv";
            this.subobsvDataGridViewTextBoxColumn.Name = "subobsvDataGridViewTextBoxColumn";
            // 
            // dataDataGridViewTextBoxColumn
            // 
            this.dataDataGridViewTextBoxColumn.DataPropertyName = "Data";
            this.dataDataGridViewTextBoxColumn.HeaderText = "Data";
            this.dataDataGridViewTextBoxColumn.Name = "dataDataGridViewTextBoxColumn";
            // 
            // EquipView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 361);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "EquipView";
            this.Text = "EquipModelView";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wBEQUIPVOBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cddistobsvDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nmdistobsvDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connIpDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connPortDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connPhoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastStatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn useYNDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn detCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn subObCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn subobsvDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource wBEQUIPVOBindingSource;
    }
}