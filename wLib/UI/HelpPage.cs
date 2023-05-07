using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Xps.Packaging;

namespace wLib.UI
{
    public partial class HelpPage : Form
    {
        // 로그
        LOG_T log = LOG_T.Instance;
        string APP_NAME;

        DocumentViewer documentViewer = new DocumentViewer();

        public HelpPage(string APP_NAME)
        {
            InitializeComponent();

            this.APP_NAME = APP_NAME;
        }

        private void On_Load(object sender, EventArgs e)
        {
            elementHost.Child = documentViewer;
        }

        private void On_Shown(object sender, EventArgs e)
        {
            MANUAL_CONF manual_conf = new MANUAL_CONF(APP_NAME);

            manual_conf.ReadConfig();
            if (manual_conf.used == false)
            {
                return;
            }

            // MANUAL_CONF
            Text = manual_conf.title;

            try
            {
                documentViewer.Document = null;

                string path = System.IO.Path.Combine(manual_conf.dir_name, manual_conf.file_name);
                using (XpsDocument xpsDocument = new XpsDocument(path, System.IO.FileAccess.Read))
                {
                    documentViewer.Document = xpsDocument.GetFixedDocumentSequence();
                    documentViewer.Zoom = 50;
                    documentViewer.MaxPagesAcross = 1;
                    documentViewer.FitToMaxPagesAcross();
                    //documentViewer.FitToWidth();
                }
            }
            catch (Exception ex)
            {
                log.Warning(ex.Message);
                MessageBox.Show(ex.Message, Text);
            }
        }
    }
}
