using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wLib.UI
{
    public partial class logPanel : UserControl
    {
        public event EventHandler On_MouseDown_Right;

        public logPanel()
        {
            InitializeComponent();
        }

        private void On_MouseDown_log(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;

            switch (e.Button)
            {
            case MouseButtons.Left:
                if (button.Tag == null)
                {
                    this.Height = 180;
                    button.Text = "로그 ▽";
                    button.Tag = new object();
                }
                else
                {
                    this.Height = 30;
                    button.Text = "로그 △";
                    button.Tag = null;
                }
                break;

            case MouseButtons.Right:
                
                break;
            }
        }

        public void ShowDialog(Form owner)
        {
            //Text = log.GetPath()
            new wLib.UI.logForm() { Owner = owner }.ShowDialog();
        }
    }
}
