namespace wLib.UI
{
    partial class DatabasePage
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.dbPage_local = new System.Windows.Forms.TabPage();
            this.dbPanel_local = new wLib.dbPanel();
            this.dbPage_idc = new System.Windows.Forms.TabPage();
            this.dbPanel_idc = new wLib.dbPanel();
            this.tabControl.SuspendLayout();
            this.dbPage_local.SuspendLayout();
            this.dbPage_idc.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.dbPage_local);
            this.tabControl.Controls.Add(this.dbPage_idc);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(780, 400);
            this.tabControl.TabIndex = 3;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl_Selected);
            // 
            // dbPage_local
            // 
            this.dbPage_local.Controls.Add(this.dbPanel_local);
            this.dbPage_local.Location = new System.Drawing.Point(4, 22);
            this.dbPage_local.Name = "dbPage_local";
            this.dbPage_local.Padding = new System.Windows.Forms.Padding(3);
            this.dbPage_local.Size = new System.Drawing.Size(772, 374);
            this.dbPage_local.TabIndex = 0;
            this.dbPage_local.Text = "DB(LOCAL)";
            this.dbPage_local.UseVisualStyleBackColor = true;
            // 
            // dbPanel_local
            // 
            this.dbPanel_local.DbName = "warning";
            this.dbPanel_local.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbPanel_local.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dbPanel_local.Id = "WBEarly";
            this.dbPanel_local.Ip = "localhost";
            this.dbPanel_local.Location = new System.Drawing.Point(3, 3);
            this.dbPanel_local.Name = "dbPanel_local";
            this.dbPanel_local.Port = "3306";
            this.dbPanel_local.Pw = "#woobosys@early!";
            this.dbPanel_local.Size = new System.Drawing.Size(766, 368);
            this.dbPanel_local.TabIndex = 0;
            // 
            // dbPage_idc
            // 
            this.dbPage_idc.Controls.Add(this.dbPanel_idc);
            this.dbPage_idc.Location = new System.Drawing.Point(4, 22);
            this.dbPage_idc.Name = "dbPage_idc";
            this.dbPage_idc.Size = new System.Drawing.Size(772, 374);
            this.dbPage_idc.TabIndex = 1;
            this.dbPage_idc.Text = "DB(IDC)";
            this.dbPage_idc.UseVisualStyleBackColor = true;
            // 
            // dbPanel_idc
            // 
            this.dbPanel_idc.DbName = "warning";
            this.dbPanel_idc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbPanel_idc.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dbPanel_idc.Id = "WBEarly";
            this.dbPanel_idc.Ip = "localhost";
            this.dbPanel_idc.Location = new System.Drawing.Point(0, 0);
            this.dbPanel_idc.Name = "dbPanel_idc";
            this.dbPanel_idc.Padding = new System.Windows.Forms.Padding(3);
            this.dbPanel_idc.Port = "3306";
            this.dbPanel_idc.Pw = "#woobosys@early!";
            this.dbPanel_idc.Size = new System.Drawing.Size(772, 374);
            this.dbPanel_idc.TabIndex = 1;
            // 
            // DatabasePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 400);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DatabasePage";
            this.Text = "DatabasePage";
            this.tabControl.ResumeLayout(false);
            this.dbPage_local.ResumeLayout(false);
            this.dbPage_idc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage dbPage_local;
        private System.Windows.Forms.TabPage dbPage_idc;
        private dbPanel dbPanel_local;
        private dbPanel dbPanel_idc;
    }
}