using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wLib.UI
{
    class ListViewControl
    {
        private ToolTip m_subItemTooltip = new ToolTip();
        private ListView ViewAppList;
        public ListViewControl(ListView listView, ImageList listViewImage)
        {
            ViewAppList = listView;

            ViewAppList.BeginUpdate();

            ViewAppList.View = View.Details;
            ViewAppList.SmallImageList = listViewImage;
            ViewAppList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            // 컬럼명과 컬럼사이즈 지정
            ViewAppList.Columns.Add("상태", 55, HorizontalAlignment.Left);
            ViewAppList.Columns.Add("프로세스명", 140, HorizontalAlignment.Left);
            ViewAppList.Columns.Add("버전", 60, HorizontalAlignment.Center);
            ViewAppList.Columns.Add("자동실행", 60, HorizontalAlignment.Center);
            ViewAppList.Columns.Add("등록선택", 60, HorizontalAlignment.Center);

            ViewAppList.EndUpdate();
        }
        public void ListViewClear()
        {
            ViewAppList.Items.Clear();
        }

        public void showListViewItems(List<Dictionary<string, string>> itemsList)
        {
            int count = ViewAppList.Items.Count + 1;
            ViewAppList.BeginUpdate();
            int imgIdx = 2;
            foreach (Dictionary<string, string> dicitem in itemsList)
            {
                switch (dicitem["isExc"])
                {
                    case "실행":
                        imgIdx = 1;
                        break;

                    case "중지":
                        imgIdx = 0;
                        break;

                    case "없음":
                    default:
                        imgIdx = 2;
                        break;
                }

                if (dicitem["isAuto"] != "hidden")
                {
                    ListViewItem lvitem = new ListViewItem(dicitem["isExc"], imgIdx);

                    lvitem.SubItems.Add(dicitem["descript"]);
                    lvitem.SubItems.Add(dicitem["ver"]);

                    if (dicitem["isAuto"] == "show") lvitem.SubItems.Add("OFF");
                    else lvitem.SubItems.Add("ON");

                    lvitem.SubItems.Add(dicitem["isReg"]);
                    ViewAppList.Items.Add(lvitem);
                    count++;
                }
            }


            ViewAppList.MouseHover += ViewAppList_MouseHover;
            ViewAppList.MouseLeave += delegate (object s, EventArgs e)
            {
                m_subItemTooltip.SetToolTip(s as ListView, string.Empty);
            };
            ViewAppList.EndUpdate();
        }

        private void ViewAppList_MouseHover(object sender, EventArgs e)
        {
            var a = sender as Control;
            ListView lv_ProcessList = (ListView)sender;
            Point m_LastPos = new Point(-1, -1);

            try
            {
                ListViewHitTestInfo info = lv_ProcessList.HitTest(a.Location.X, a.Location.Y);

                if (info.Item != null && info.SubItem != null)
                {
                    if (info.Item.SubItems.IndexOf(info.SubItem) == 0)
                    {
                        string tipText = string.Empty;
                        if (info.Item.ImageIndex == 0)
                            tipText = "중지";
                        else if (info.Item.ImageIndex == 1)
                            tipText = "실행";

                        m_subItemTooltip.Show(tipText, info.Item.ListView, new Point(a.Location.X, a.Location.Y + a.Cursor.Size.Height - 10), 3000);
                    }
                    else
                    {
                        m_subItemTooltip.Show(info.SubItem.Text, info.Item.ListView, new Point(a.Location.X, a.Location.Y + a.Cursor.Size.Height - 10), 3000);
                    }

                }
                else
                {
                    m_subItemTooltip.SetToolTip(lv_ProcessList, string.Empty);
                }
            }
            catch (Exception ex)
            {
                //WriteLogToFile("lv_ProcessList_MouseDoubleClick() - " + ex);
            }
        }

        public void showListViewUpdateItem(Dictionary<string, string> DicItems, ListViewItem selectedItem)
        {
            int imgIdx = 2;
            switch (DicItems["isExc"])
            {
                case "실행":
                    imgIdx = 1;
                    break;
                case "중지":
                    imgIdx = 0;
                    break;
                case "없음":
                default:
                    imgIdx = 2;
                    break;
            }

            ViewAppList.BeginUpdate();
            selectedItem.ImageIndex = imgIdx;
            selectedItem.SubItems[0].Text = DicItems["isExc"];
            selectedItem.SubItems[1].Text = DicItems["descript"];
            selectedItem.SubItems[2].Text = DicItems["ver"];
            selectedItem.SubItems[3].Text = DicItems["isAuto"];
            selectedItem.SubItems[4].Text = DicItems["isReg"];
            ViewAppList.EndUpdate();

        }

        public void showAutoColumnChg(ListViewHitTestInfo hitTestListView)
        {

            // Index of the clicked ListView column - 6 index
            int columnIndex = hitTestListView.Item.SubItems.IndexOf(hitTestListView.SubItem);
            if (columnIndex == 3)
            {
                ViewAppList.BeginUpdate();
                string selItemText = hitTestListView.Item.SubItems[columnIndex].Text;
                if (selItemText.Equals("ON"))
                {
                    hitTestListView.Item.SubItems[columnIndex].Text = "OFF";
                }
                else if (selItemText.Equals("OFF"))
                {
                    hitTestListView.Item.SubItems[columnIndex].Text = "ON";
                }
                ViewAppList.EndUpdate();
            }
        }

        public List<string> getSelectedAppNames()
        {
            List<string> selectedApps = null;
            if (ViewAppList.SelectedItems.Count > 0)
            {
                selectedApps = new List<string>();
                foreach (ListViewItem lv_selected in ViewAppList.SelectedItems)
                {
                    if (lv_selected.ImageIndex != 2)
                        selectedApps.Add(lv_selected.SubItems[1].Text);
                }
            }
            else
            {
                MessageBox.Show("선택된 정보가 없습니다.");
            }

            return selectedApps;
        }

        public void deSelectedViewList()
        {
            if (ViewAppList.SelectedItems.Count > 0)
            {
                foreach (ListViewItem lv_selected in ViewAppList.SelectedItems)
                {
                    lv_selected.Selected = false;
                    lv_selected.Focused = false;
                }
            }
        }

        public void showExcColumnChg(string appName, string status)
        {
            if (ViewAppList.InvokeRequired)
            {
                ViewAppList.Invoke(new Action(() =>
                {
                    ViewAppList.BeginUpdate();
                    foreach (ListViewItem lv_selected in ViewAppList.Items)
                    {
                        if (lv_selected.SubItems[1].Text.Equals(appName))
                        {
                            int imgIdx = 2;
                            switch (status)
                            {
                                case "실행":
                                    imgIdx = 1;
                                    break;
                                case "중지":
                                    imgIdx = 0;
                                    break;
                                case "없음":
                                default:
                                    imgIdx = 2;
                                    break;
                            }
                            lv_selected.ImageIndex = imgIdx;
                            lv_selected.SubItems[0].Text = status;
                        }
                    }

                    ViewAppList.EndUpdate();
                }));
            }
            else
            {
                ViewAppList.BeginUpdate();
                foreach (ListViewItem lv_selected in ViewAppList.Items)
                {
                    if (lv_selected.SubItems[1].Text.Equals(appName))
                    {
                        int imgIdx = 2;
                        switch (status)
                        {
                            case "실행":
                                imgIdx = 1;
                                break;
                            case "중지":
                                imgIdx = 0;
                                break;
                            case "없음":
                            default:
                                imgIdx = 2;
                                break;
                        }
                        lv_selected.ImageIndex = imgIdx;
                        lv_selected.SubItems[0].Text = status;
                    }
                }

                ViewAppList.EndUpdate();
            }

            
        }
    }
}
