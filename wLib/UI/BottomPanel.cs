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
    public partial class BottomPanel : UserControl
    {
        public BottomPanel()
        {
            InitializeComponent();
        }

        private void On_Load(object sender, EventArgs e)
        {
            refresh_timer.Start();
        }

        private void On_RefreshTimer(object sender, EventArgs e)
        {
            clockPanel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
