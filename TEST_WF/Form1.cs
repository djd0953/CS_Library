using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wLib.UI.settingPage;

namespace TEST_WF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            IDBPanelView view = new DBPanelView();
            IDBPanelRepository repository = new DBRepository();
            new DBPanelPresenter(view, repository);
            panel1 = (Panel)view;
        }
    }
}
