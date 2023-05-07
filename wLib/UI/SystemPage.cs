using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wLib.DB;
using static wLib.DB.AREA_CODE_T;

namespace wLib.UI
{
    public partial class SystemPage : Form, INotifyPropertyChanged
    {
        private string APP_NAME;

        private ObservableCollection<AREA_CODE> _areaList = new ObservableCollection<AREA_CODE>(new AREA_CODE_T().Items);
        public ObservableCollection<AREA_CODE> AreaList
        {
            get
            {
                return _areaList;
            }
            set
            {
                if (_areaList != value)
                {
                    _areaList = value;
                    //OnNotifyPropertyChanged();
                }
            }
        }

        AREA_CODE _areaList_Selected;// = new AREA_CODE("", "", "");
        public AREA_CODE AreaList_Selected
        {
            get => _areaList_Selected;
            set
            {
                if (_areaList_Selected != value)
                {
                    _areaList_Selected = value;
                    //OnNotifyPropertyChanged();
                }
            }
        }

        private string _areaCode = "255";
        public string AreaCode
        {
            get => _areaCode;
            set
            {
                _areaCode = value;
                AreaName = AreaList.First(x=>x.Code == value).Name;
            }
        }

        private string _areaName = "ㅇㅇ시";
        public string AreaName
        {
            get => _areaName; 
            set
            {
                _areaName = value;
                OnNotifyPropertyChanged();
            }
        }

        private string _systemName = "(ㅇㅇㅇ시스템)";
        public string SystemName
        {
            get => _systemName;
            set
            {
                _systemName = value;
                OnNotifyPropertyChanged();
            }
        }

        private string _programName = "ㅇㅇ프로그램";
        public string ProgramName
        {
            get => _programName;
            set
            {
                _programName = value;
                OnNotifyPropertyChanged();
            }
        }

        // 쓰레드
        private List<ComboBoxItem> _thread_list = new List<ComboBoxItem>
        {
            new ComboBoxItem() { Text = "자동", Tag = "-1" },
            new ComboBoxItem() { Text = "1개", Tag = "1" },
            new ComboBoxItem() { Text = "2개", Tag = "2" },
            new ComboBoxItem() { Text = "4개", Tag = "4" },
            new ComboBoxItem() { Text = "6개", Tag = "6" },
            new ComboBoxItem() { Text = "8개", Tag = "8" },
            new ComboBoxItem() { Text = "10개", Tag = "10" },
            new ComboBoxItem() { Text = "12개", Tag = "12" },
            new ComboBoxItem() { Text = "16개", Tag = "16" },
        };
        public List<ComboBoxItem> Thread_list { get => _thread_list; set => _thread_list = value; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotifyPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public SystemPage(string APP_NAME)
        {
            InitializeComponent();

            this.APP_NAME = APP_NAME;

            resetButton.Click += On_Click_Reset;
            lockButton.Click += On_Click_Lock;

            systemPageBindingSource.DataSource = this;
            areaListBindingSource.DataSource = this.AreaList;

            // RunInterval 리스트 생성
            #region RunInterval 리스트 생성
            
            runThread.DisplayMember = "Text";
            runThread.ValueMember = "Tag";
            runThread.DataSource = _thread_list;
            runThread.SelectedIndex = -1;
            #endregion

            ReadConfig();
        }

        private void ReadConfig()
        {
            APP_CONF app_conf = new APP_CONF(APP_NAME);
            {
                AreaList_Selected = AreaList.First(x => x.Code == app_conf.area_code);
                SystemName = app_conf.system_name;
                ProgramName = app_conf.program_name;

            }
            // APP_CONF
            //app_conf.ReadConfig(true);

            RUN_CONF run_conf = new RUN_CONF(APP_NAME);
            {
                foreach (ComboBoxItem item in runThread.Items)
                {
                    if (run_conf.run_thread_num == Convert.ToInt32(item.Tag))
                    {
                        runThread.SelectedItem = item;
                        break;
                    }
                }

                //runThread.SelectedItem = Thread_list.Find(x => x.Text == $"{run_conf.run_thread_num}");
                //foreach (ComboBoxItem item in runThread.Items)
                //{
                //    if (run_conf.run_thread_num == Convert.ToInt32(item.Tag))
                //    {
                //        runThread.SelectedItem = item;
                //        break;
                //    }
                //}
            }

            TABLE_CONF table_conf = new TABLE_CONF(APP_NAME);
            {

            }


            EnableConfig(false);
        }

        private void SaveConfig()
        {
            // APP_CONF
            APP_CONF app_conf = new APP_CONF(APP_NAME);
            {
                app_conf.area_code = AreaList_Selected.Code;
                app_conf.system_name = SystemName;
                app_conf.program_name = ProgramName;

                app_conf.SaveConfig();
            }

            // RUN_CONF
            RUN_CONF run_conf = new RUN_CONF(APP_NAME);
            {
                run_conf.run_thread_num = Convert.ToInt32(runThread.SelectedValue);
            }
            run_conf.SaveConfig();

            TABLE_CONF table_conf = new TABLE_CONF(APP_NAME);
            {

            }

        }

        private void EnableConfig(bool flag)
        {
            // APP_CONF
            areaList.Enabled = flag;
            systemName.Enabled = flag;
            programName.Enabled = flag;

            // RUN_CONF
            runThread.Enabled = flag;

            // SETTING_CONF
            nmsCheck.Enabled = flag;

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
