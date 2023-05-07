using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace wLib.UI
{
    public partial class DatabasePage : Form
    {
        // 로그
        LOG_T log = LOG_T.Instance;

        // 설정 파일
        private DB_CONF db_local_conf = new DB_CONF("LOCAL");

        private DB_CONF db_idc_conf = new DB_CONF("IDC");

        public DatabasePage()
        {
            InitializeComponent();

            dbPanel_local.db_conf = db_local_conf;
            dbPanel_idc.db_conf = db_idc_conf;

            ReadConfig();
        }

        private void ReadConfig()
        {
            // 탭 초기화
            tabControl.Controls.Clear();
        
            db_local_conf.ReadConfig();
            if (db_local_conf.used == true)
            {
                dbPage_local.Text = db_local_conf.comment;
                tabControl.TabPages.Add(dbPage_local);
            }
        
            db_idc_conf.ReadConfig();
            if (db_idc_conf.used == true)
            {
                dbPage_idc.Text = db_idc_conf.comment;
                tabControl.TabPages.Add(dbPage_idc);
            }
        }
        
        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == dbPage_local)
            {
                dbPanel_local.ReadConfig();
            }
            else if (e.TabPage == dbPage_idc)
            {
                dbPanel_idc.ReadConfig();
            }
        }
    }
}
