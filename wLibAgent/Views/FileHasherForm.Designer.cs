namespace wLibAgent.Views
{
    partial class FileHasherForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMd5 = new System.Windows.Forms.TextBox();
            this.txtSha1 = new System.Windows.Forms.TextBox();
            this.txtSha256 = new System.Windows.Forms.TextBox();
            this.txtSha384 = new System.Windows.Forms.TextBox();
            this.txtSha512 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(324, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 21);
            this.button1.TabIndex = 0;
            this.button1.TabStop = false;
            this.button1.Text = "열기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(70, 30);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(400, 21);
            this.txtFilePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "파일경로";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(400, 57);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 21);
            this.button2.TabIndex = 4;
            this.button2.TabStop = false;
            this.button2.Text = "계산";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "MD5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "SHA1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "SHA256";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "SHA384";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 233);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "SHA512";
            // 
            // txtMd5
            // 
            this.txtMd5.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.txtMd5.Location = new System.Drawing.Point(70, 95);
            this.txtMd5.Name = "txtMd5";
            this.txtMd5.ReadOnly = true;
            this.txtMd5.Size = new System.Drawing.Size(400, 22);
            this.txtMd5.TabIndex = 10;
            // 
            // txtSha1
            // 
            this.txtSha1.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.txtSha1.Location = new System.Drawing.Point(70, 125);
            this.txtSha1.Name = "txtSha1";
            this.txtSha1.ReadOnly = true;
            this.txtSha1.Size = new System.Drawing.Size(400, 22);
            this.txtSha1.TabIndex = 11;
            // 
            // txtSha256
            // 
            this.txtSha256.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.txtSha256.Location = new System.Drawing.Point(70, 155);
            this.txtSha256.Multiline = true;
            this.txtSha256.Name = "txtSha256";
            this.txtSha256.ReadOnly = true;
            this.txtSha256.Size = new System.Drawing.Size(400, 21);
            this.txtSha256.TabIndex = 12;
            // 
            // txtSha384
            // 
            this.txtSha384.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.txtSha384.Location = new System.Drawing.Point(70, 182);
            this.txtSha384.Multiline = true;
            this.txtSha384.Name = "txtSha384";
            this.txtSha384.ReadOnly = true;
            this.txtSha384.Size = new System.Drawing.Size(400, 42);
            this.txtSha384.TabIndex = 13;
            // 
            // txtSha512
            // 
            this.txtSha512.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.txtSha512.Location = new System.Drawing.Point(70, 230);
            this.txtSha512.Multiline = true;
            this.txtSha512.Name = "txtSha512";
            this.txtSha512.ReadOnly = true;
            this.txtSha512.Size = new System.Drawing.Size(400, 42);
            this.txtSha512.TabIndex = 14;
            // 
            // FileHasherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 291);
            this.Controls.Add(this.txtSha512);
            this.Controls.Add(this.txtSha384);
            this.Controls.Add(this.txtSha256);
            this.Controls.Add(this.txtSha1);
            this.Controls.Add(this.txtMd5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.Name = "FileHasherForm";
            this.Text = "FileHasherForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMd5;
        private System.Windows.Forms.TextBox txtSha1;
        private System.Windows.Forms.TextBox txtSha256;
        private System.Windows.Forms.TextBox txtSha384;
        private System.Windows.Forms.TextBox txtSha512;
    }
}