using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wLib
{
    public partial class systemPanel : UserControl
    {
        APP_CONF app_conf;

        public systemPanel()
        {
            InitializeComponent();

            resetButton.Click += On_Click_Reset;
            lockButton.Click += On_Click_Lock;
        }

        public void Connect(APP_CONF _app_conf)
        {
            app_conf = _app_conf;

            // 데이터 바인딩
            appBindingSource.DataSource = app_conf;

            ReadConfig();
        }

        private void ReadConfig()
        {
            // APP_CONF
            app_conf.ReadConfig(true);
            appBindingSource.ResetBindings(false);

            EnableConfig(false);
        }

        private void SaveConfig()
        {
            // APP_CONF
            app_conf.SaveConfig();
        }

        private void EnableConfig(bool flag)
        {
            // APP_CONF
            programName.Enabled = flag;
            systemName.Enabled = flag;

            if (flag == true)
            {
                lockButton.Tag = null;
            }
            else
            {
                lockButton.Tag = new object();
            }
        }

        private void On_Click_Reset(object sender, EventArgs e)
        {
            ReadConfig();

            if (lockButton.Tag != null)
            {
                lockButton.Text = "설정";
            }
        }

        private void On_Click_Lock(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button.Tag == null)
            {
                button.Text = "설정";
                SaveConfig();
                ReadConfig();
                return;
            }
            else
            {
                button.Text = "저장";
            }

            EnableConfig(true);
        }
    }
}
