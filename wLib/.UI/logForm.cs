using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wLib.UI
{
    public partial class logForm : Form
    {
        // 타이머
        System.Windows.Forms.Timer refresh_timer = new System.Windows.Forms.Timer();
        string path;

        DateTime LastReadTime { get; set; } = new DateTime();
        DateTime LastWriteTime
        {
            get
            {
                try
                {
                    return File.GetLastWriteTimeUtc(path);
                }
                catch (Exception)
                {
                    return new DateTime();
                }
            }
        }

        public logForm()
        {
            InitializeComponent();

            this.Load += logForm_Load;
            this.Shown += logForm_Shown;
            this.FormClosing += logForm_FormClosing;
            this.FormClosed += logForm_FormClosed;

            // 갱신 타이머 설정
            refresh_timer.Interval = 500; // 1초에 2회
            refresh_timer.Tick += On_tick_handler;
        }

        private void logForm_Load(object sender, EventArgs e)
        {
            LogText.TextChanged += LogText_TextChanged;
        }

        private void logForm_Shown(object sender, EventArgs e)
        {
            LogText.Focus();

            ReadFile();

            // 타이머 실행
            refresh_timer.Start();
        }

        private void logForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 타이머 종료
            refresh_timer.Stop();
        }

        private void logForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void ReadFile()
        {
            try
            {
                path = this.Text;
                using (Mutex mutex = new Mutex(false, path.Replace(Path.DirectorySeparatorChar, '/')))
                {
                    if (mutex.WaitOne())
                    {
                        try
                        {
                            using (FileStream fp = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                using (StreamReader sr = new StreamReader(fp, Encoding.Default))
                                {
                                    LogText.Text = sr.ReadToEnd();
                                }
                            }

                            LastReadTime = LastWriteTime;
                        }
                        catch (FileNotFoundException ex)
                        {
                            LogText.Text = ex.Message;
                        }
                        catch (Exception ex)
                        {
                            LogText.Text = ex.Message;
                        }

                        mutex.ReleaseMutex();
                    }
                }
            }
            catch { }
        }

        private void On_tick_handler(object sender, EventArgs e)
        {
            if (LastWriteTime == LastReadTime)
                return;

            ReadFile();
        }

        ////////////////////////////////////////////////////////////////////////////////
        // 구동 로직과는 상관 없는 부분
        ////////////////////////////////////////////////////////////////////////////////
        #region 로그창 커서 이동
        private void LogText_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            try
            {
                textBox.SelectionStart = textBox.TextLength;
                textBox.ScrollToCaret();
            }
            catch { }
        }
        #endregion
    }
}
