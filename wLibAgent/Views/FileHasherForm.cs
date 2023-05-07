using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wLibAgent.Views
{
    public partial class FileHasherForm : Form
    {
        public FileHasherForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }

            button2.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtMd5.Text = "";
            txtSha1.Text = "";
            txtSha256.Text = "";
            txtSha384.Text = "";
            txtSha512.Text = "";

            try
            {
                txtMd5.Text = Hasher.HashFile(txtFilePath.Text, HashType.MD5);
            }
            catch (FileNotFoundException)
            {
                txtMd5.Text = Hasher.HashString(txtFilePath.Text, HashType.MD5);
            }
            catch { }

            try
            {
                txtSha1.Text = Hasher.HashFile(txtFilePath.Text, HashType.SHA1);
            }
            catch { }

            try
            {
                txtSha256.Text = Hasher.HashFile(txtFilePath.Text, HashType.SHA256);
            }
            catch { }

            try
            {
                txtSha384.Text = Hasher.HashFile(txtFilePath.Text, HashType.SHA384);
            }
            catch { }

            try
            {
                txtSha512.Text = Hasher.HashFile(txtFilePath.Text, HashType.SHA512);
            }
            catch { }
        }
    }

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FileHasherForm());
        }
    }
}
