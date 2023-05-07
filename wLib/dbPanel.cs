using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wLib
{
    public partial class dbPanel : UserControl, INotifyPropertyChanged
    {
        // 로그
        LOG_T log = LOG_T.Instance;

        // 설정 파일
        private string _ip;
        public string Ip
        {
            get => _ip;
            set
            {
                _ip = value;
                OnPropertyChanged();
            }
        }

        private string _port;
        public string Port
        {
            get => _port;
            set
            {
                _port = value;
                OnPropertyChanged();
            }
        }

        private string _dbName;
        public string DbName
        {
            get => _dbName;
            set
            {
                _dbName = value;
                OnPropertyChanged();
            }
        }


        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _pw;
        public string Pw 
        {
            get => _pw;
            set
            {
                _pw = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DB_CONF db_conf = new DB_CONF();

        public dbPanel()
        {
            InitializeComponent();

            // 테스트 버튼
            testButton_ip.Click += On_Click_IP;
            testButton_port.Click += On_Click_Port;
            testButton_db.Click += On_Click_DB;

            // 설정버튼 이벤트 등록
            resetButton.Click += On_Click_Reset;
            lockButton.Click += On_Click_Lock;

            // 테스트 버튼
            db_local_panel.Text = $"DB정보({db_conf.section})";

            // 데이터 바인딩
            db_BindingSource.DataSource = this;
        }

        private void On_Load(object sender, EventArgs e)
        {
            ReadConfig();
        }

        public void ReadConfig()
        {
            db_conf.ReadConfig(true);
            {
                Ip = db_conf.Ip;
                Port = db_conf.Port;
                DbName = db_conf.Dbname;
                Id = db_conf.Id;
                Pw = db_conf.Pw;
            }

            EnableConfig(false);
        }

        private void SaveConfig()
        {
            // db_conf
            db_conf.ReadConfig();
            {
                db_conf.Ip = Ip;
                db_conf.Port = Port;
                db_conf.Dbname = DbName;
                db_conf.Id = Id;
                db_conf.Pw = Pw;
            }
            db_conf.SaveConfig();
        }

        private void EnableConfig(bool flag)
        {
            // db_conf
            txtIp.Enabled = flag;
            txtPort.Enabled = flag;
            txtId.Enabled = flag;
            txtPw.Enabled = flag;
            txtDb.Enabled = flag;

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

        private async void On_Click_IP(object sender, EventArgs e)
        {
            Button button = sender as Button;

            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            string ip = "";
            string temp;

            button.Enabled = false;

            try
            {
                if (button == testButton_ip)
                {
                    ip = txtIp.Text;
                }
                else
                {
                    return;
                }

                System.Net.NetworkInformation.PingReply rtv = await ping.SendPingAsync(ip);
                if (rtv.Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    temp = log.Info($"{button.Text}: {ip} 연결에 성공하였습니다. 시간 = {rtv.RoundtripTime}ms");
                    MessageBox.Show(temp);
                }
                else
                {
                    throw new Exception(Enum.GetName(typeof(System.Net.NetworkInformation.IPStatus), rtv.Status));
                }
            }
            catch (Exception ex)
            {
                temp = log.Info($"{button.Text}: {ip} 연결에 실패하였습니다.\r\n{ex.Message}\r\n{ex.InnerException?.Message}");
                MessageBox.Show(temp);
            }

            button.Enabled = true;
        }

        // PORT 테스트
        private async void On_Click_Port(object sender, EventArgs e)
        {
            Button button = sender as Button;

            System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
            string ip = "", port = "";
            string temp;

            button.Enabled = false;

            try
            {
                if (button == testButton_port)
                {
                    ip = txtIp.Text;
                    port = txtPort.Text;
                }
                else
                {
                    return;
                }

                await client.ConnectAsync(ip, int.Parse(port));
                if (client.Client.Connected == true)
                {
                    temp = log.Info($"{button.Text}: {ip}:{port} 연결에 성공하였습니다.");
                    MessageBox.Show(temp);
                }
            }
            catch (Exception ex)
            {
                temp = log.Info($"{button.Text}: {ip}:{port} 연결에 실패하였습니다.\r\n{ex.Message}");
                MessageBox.Show(temp);
            }

            button.Enabled = true;
        }

        // DB 테스트
        private async void On_Click_DB(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string temp;

            button.Enabled = false;

            try
            {
                using (DB.MYSQL_T conn = new DB.MYSQL_T(Ip, Port, DbName, Id, Pw))
                {
                    await System.Threading.Tasks.Task.Run(() =>
                    {
                        conn.Open();
                    });

                    temp = log.Info($"{button.Text}: {Ip}/{DbName} 연결에 성공하였습니다.");
                    MessageBox.Show(temp);
                }
            }
            catch (Exception ex)
            {
                temp = log.Info($"{button.Text}: {Ip}/{DbName} 연결에 실패하였습니다.\r\n{ex.Message}");
                MessageBox.Show(temp);
            }

            button.Enabled = true;
        }
    }
}
