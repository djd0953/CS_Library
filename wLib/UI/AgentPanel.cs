using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ListView = System.Windows.Forms.ListView;

namespace wLib.UI
{

    public partial class AgentPanel : Form
    {
        // 로그
        LOG_T log = LOG_T.Instance;

        // 타이머
        DateTime process_time = new DateTime();

        // 설정 파일
        APP_CONF app_conf;

        ListViewControl appListView;
        Dictionary<string, APP_CONF> iniFileList;
        string iniPath = AppDomain.CurrentDomain.BaseDirectory + "etc/APP.ini";

        List<Dictionary<string, string>> lvItemList;

        public AgentPanel(string appName)
        {
            InitializeComponent();
            app_conf = new APP_CONF(appName);

            // 로그 설정
            log.Init(appName);
            log.ConnectTextBox(logPanel.LogText);

            appListView = new ListViewControl(lv_ProcessList, imageList1);
        }

        private void AgentPanel_Load(object sender, EventArgs e)
        {
            Text = app_conf.process.ProcessName + " V" + app_conf.process.Version;

            // STARTUP
            log.Info("");
            log.WriteLine($"========================================");
            log.WriteLine($"| 프로그램을 시작합니다.({Text})");
            log.WriteLine($"========================================");

            ReadConfig();
            process_timer.Start();
            refresh_timer.Start();
        }

        private void AgentPanel_Shown(object sender, EventArgs e)
        {
            loadAppListInfo();
            autoStartAppExecute();

            
            appListView.showListViewItems(lvItemList);
        }

        private void AgentPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 설정파일 저장
            SaveConfig();

            // 타이머 종료
            process_timer.Stop();
            refresh_timer.Stop();
        }

        private void AgentPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Text = app_conf.process.ProcessName + " V" + app_conf.process.Version;

            Thread.Sleep(300);
            log.Info("");
            log.WriteLine($"========================================");
            log.WriteLine($"| 프로그램을 종료합니다.({Text})");
            log.WriteLine($"========================================");
            log.WriteLine();
            log.WriteLine();
            log.WriteLine();
            log.WriteLine();
            log.WriteLine();
            log.WriteLine();
            //Environment.Exit(0);
        }


        ////////////////////////////////////////////////////////////////////////////////
        // 설정 파일
        ////////////////////////////////////////////////////////////////////////////////
        void ReadConfig()
        {
            app_conf.ReadConfig();
        }

        void SaveConfig()
        {
            app_conf.SaveConfig();
        }

        ////////////////////////////////////////////////////////////////////////////////
        // 로직
        ////////////////////////////////////////////////////////////////////////////////
        public Dictionary<string, APP_CONF> GetSectionNames()
        {
            Dictionary<string, APP_CONF> rtv = new Dictionary<string, APP_CONF>();
            List<string> list = new List<string>();

            string iniPath = AppDomain.CurrentDomain.BaseDirectory + "etc/APP.ini";
            FileStream inifs = File.OpenRead(iniPath);

            string line;

            using (StreamReader reader = new StreamReader(inifs))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string trimStart = line.TrimStart();

                    if (trimStart.Length > 0 && trimStart[0] == '[')
                    {
                        int sectionEnd = trimStart.IndexOf(']');
                        if (sectionEnd > 0)
                        {
                            string sectionName = trimStart.Substring(1, sectionEnd - 1).Trim();
                            list.Add(sectionName);
                        }
                    }
                }

                foreach (string section in list)
                {
                    APP_CONF _conf = new APP_CONF(section);
                    rtv.Add(section, _conf);
                }
            }

            return rtv;
        }

        private void loadAppListInfo()
        {
            try
            {
                lvItemList = new List<Dictionary<string, string>>();
                iniFileList = GetSectionNames();

                foreach (KeyValuePair<string, APP_CONF> conf in iniFileList)
                {
                    if (conf.Key == "GLOBAL" || conf.Key == "AppAgent") continue;
                    Dictionary<string, string> list = new Dictionary<string, string>();

                    Process[] process = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(conf.Value.program_path));
                    string isRunProgram = "중지";

                    if (process.Length > 0) isRunProgram = "실행";

                    list.Add("sectionName", conf.Key);
                    list.Add("fileName", conf.Value.program_path);
                    list.Add("descript", conf.Value.program_name);

                    if (File.Exists(conf.Value.program_path))
                    {
                        FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(conf.Value.program_path);
                        list.Add("ver", fileVersionInfo.FileVersion.Substring(0, 5));
                    }
                    else
                    {
                        list.Add("ver", "0.0");
                    }

                    list.Add("isAuto", conf.Value.program_agent);
                    list.Add("isExc", isRunProgram);
                    list.Add("isReg", "삭제");
                    lvItemList.Add(list);
                }
            }
            catch (Exception ex)
            {
                log.Warning($"{GetType()}: {ex.Message}");
            }
        }

        private void lv_ProcessList_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                ListView viewApps = (ListView)sender;
                if (viewApps.SelectedItems.Count > 0)
                {
                    // Hittestinfo of the clicked ListView location
                    ListViewHitTestInfo listViewHitTestInfo = viewApps.HitTest(e.X, e.Y);
                    int columnIdx = listViewHitTestInfo.Item.SubItems.IndexOf(listViewHitTestInfo.SubItem);
                    ListViewItem lv_selected = listViewHitTestInfo.Item;
                    foreach (Dictionary<string, string> seldDic in lvItemList)
                    {
                        if (seldDic["descript"].Equals(lv_selected.SubItems[1].Text))
                        {

                            if (columnIdx == 0)
                            {//실행 중지
                                string execState = string.Empty;
                                if (lv_selected.ImageIndex == 0)
                                {
                                    execState = "실행";
                                    startProcesses(seldDic["fileName"]);
                                    appListView.deSelectedViewList();
                                }
                                else if (lv_selected.ImageIndex == 1)
                                {
                                    execState = "중지";
                                    stopProcesses(seldDic["fileName"]);
                                    appListView.deSelectedViewList();
                                }
                                else if (lv_selected.ImageIndex == 2) execState = "없음";

                                appListView.showExcColumnChg(seldDic["descript"], execState);

                            }
                            else if (columnIdx == 3)
                            {//자동시작 여부
                                if (seldDic["isReg"].Equals("등록"))
                                {
                                    MessageBox.Show(seldDic["descript"] + " 은(는) 미등록 프로그램입니다.");
                                }
                                else
                                {
                                    string agentState = string.Empty;
                                    string autoState = string.Empty;
                                    if (lv_selected.SubItems[columnIdx].Text.Equals("OFF"))
                                    {
                                        autoState = "ON";
                                        agentState = "auto";
                                    }
                                    else if (lv_selected.SubItems[columnIdx].Text.Equals("ON"))
                                    {
                                        autoState = "OFF";
                                        agentState = "show";
                                    }

                                    APP_CONF _conf = iniFileList[seldDic["sectionName"]];
                                    _conf.program_agent = agentState;
                                    _conf.SaveConfig();

                                    seldDic["isAuto"] = autoState;
                                    appListView.showAutoColumnChg(listViewHitTestInfo);
                                    appListView.deSelectedViewList();
                                }
                            }
                            else if (columnIdx == 4)
                            {//등록여부
                                string agentState = string.Empty;

                                if (lv_selected.SubItems[columnIdx].Text.Equals("삭제"))
                                {
                                    seldDic["isReg"] = "등록";
                                    seldDic["isAuto"] = "OFF";
                                    agentState = "hidden";
                                    appListView.deSelectedViewList();

                                }
                                else if (lv_selected.SubItems[columnIdx].Text.Equals("등록"))
                                {
                                    seldDic["isReg"] = "삭제";
                                    agentState = "show";
                                    appListView.deSelectedViewList();
                                }

                                APP_CONF _conf = iniFileList[seldDic["sectionName"]];
                                _conf.program_agent = agentState;
                                _conf.SaveConfig();

                                appListView.showListViewUpdateItem(seldDic, lv_selected);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                log.Info("lv_ProcessList_MouseClick() - " + ex);
            }
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            fileShowHiddenChg("실행");
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            fileShowHiddenChg("중지");
        }

        private void fileShowHiddenChg(string status)
        {
            try
            {
                List<string> startName = appListView.getSelectedAppNames();
                if (startName != null)
                {
                    Dictionary<string, string> proc = new Dictionary<string, string>();
                    foreach (Dictionary<string, string> dicitem in lvItemList)
                    {
                        if (startName.Contains(dicitem["descript"]))
                            proc.Add(dicitem["descript"], dicitem["fileName"]);
                    }

                    if (proc != null && proc.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> appName in proc)
                        {
                            if (status == "실행") startProcesses(appName.Value);
                            else if (status == "중지") stopProcesses(appName.Value);

                            appListView.showExcColumnChg(appName.Key, status);
                        }

                        appListView.deSelectedViewList();
                    }
                }
                else throw new Exception("선택된 정보가 없습니다.");
            }
            catch
            {
                log.Warning("선택된 정보가 없습니다.");
            }
        }

        private void autoStartAppExecute()
        {
            try
            {
                foreach (KeyValuePair<string, APP_CONF> _conf in iniFileList)
                {
                
                    if (_conf.Value.program_agent.ToLower() == "auto")
                    {
                        Task.Run(() =>
                        {
                            Thread.Sleep(_conf.Value.program_agent_Interval * 1000);
                            if (startProcesses(_conf.Value.program_path))
                            {
                                appListView.showExcColumnChg(_conf.Value.program_name, "실행");
                            }
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                log.Warning("autoStartAppExecute exception - " + ex);
            }
        }

        private bool startProcesses(string execFile)
        {
            bool rtv = false;

            try
            {
                Process[] processList = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(execFile));
                string excutePath = execFile;
                excutePath = execFile;
                if (File.Exists(excutePath))
                {
                    if (processList.Length == 0)
                    {//실행된게 없다면 실행
                        Process process = Process.Start(excutePath);
                        log.WriteLine($"프로그램을 실행하였습니다. ({process.ProcessName}: ({process.Id}))");
                        rtv = true;

                        string swName = string.Empty;
                        foreach (Dictionary<string, string> appdic in lvItemList)
                        {
                            if (appdic["fileName"].Equals(execFile))
                            {
                                swName = appdic["descript"];
                                appdic["isExc"] = "실행";
                                break;
                            }
                        }
                        appListView.showExcColumnChg(swName, "실행");
                    }
                }
                else
                {
                    //실행파일없음
                    log.Warning(excutePath + " 파일이 없음");
                    throw new Exception("파일이 없습니다.");
                }
            }
            catch (Exception ex)
            {
                log.Warning("startProcesses exception - " + ex);
            }

            return rtv;
        }

        private void stopProcesses(string execFile)
        {
            try
            {
                Process[] processList = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(execFile));
                if (processList.Length > 0)
                {
                    foreach (Process execProc in processList)
                    {
                        execProc.Kill();
                        string swName = string.Empty;
                        foreach (Dictionary<string, string> appdic in lvItemList)
                        {
                            if (appdic["fileName"].Equals(execFile))
                            {
                                swName = appdic["descript"];
                                appdic["isExc"] = "중지";
                                break;
                            }

                        }
                        appListView.showExcColumnChg(swName, "중지");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Warning("stopProcesses exception - " + ex);
            }

        }

        ////////////////////////////////////////////////////////////////////////////////
        // 시간 갱신 타이머
        ////////////////////////////////////////////////////////////////////////////////
        private void process_timer_Tick(object sender, EventArgs e)
        {
            DateTime nowTime = DateTime.Now;

            clockPanel.Text = nowTime.ToString("yyyy-MM-dd HH:mm:ss");

            process_time = process_time.AddSeconds(1);

            int total_seconds = (int)(process_time - new DateTime()).TotalSeconds;
            int interval = 60;

            progressBar.Value = progressBar.Maximum - (total_seconds % interval);

            // 실행 이벤트 발생
            if (progressBar.Value == progressBar.Maximum)
            {
                app_conf.ReadConfig();
                loadAppListInfo();
                autoStartAppExecute();
                appListView.ListViewClear();
                appListView.showListViewItems(lvItemList);
            }
        }

        private void refresh_timer_Tick(object sender, EventArgs e)
        {
            loadAppListInfo();
            appListView.ListViewClear();
            appListView.showListViewItems(lvItemList);
        }
    }
}