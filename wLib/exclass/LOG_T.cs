using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

namespace wLib
{
    public enum LOG_TYPE { NONE = 0, UI = 1, FILE = 2, UI_FILE = 3 }
    public enum LOG_LEVEL { NONE = 0, INFO = 1, WARNING = 2, ERROR = 3, FATAL = 4, DEBUG = 5, TRACE = 6 };

    public class LOG_T : INotifyPropertyChanged
    {
        LOG_CONF log_conf = new LOG_CONF();

        #region 싱글톤 패턴
        private static LOG_T _instance = null;
        private static object _instance_lock = new object();

        /// <summary>
        /// 프로그램 전역에서 동작하는 LOG_T 를 가져옵니다.
        /// </summary>
        public static LOG_T Instance
        {
            get
            {
                lock (_instance_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new LOG_T();
                    }
                }

                return _instance;
            }
        }
        #endregion

        #region UI 바인딩
        private System.Windows.Forms.TextBox _control;
        private System.Windows.Controls.TextBox _wpf_control;
        public event PropertyChangedEventHandler PropertyChanged;
        private List<string> _textList = new List<string>();

        /// <summary>
        /// 로그메시지
        /// </summary>
        public string Text
        {
            get
            {
                lock (_textList)
                {
                    return string.Join("", _textList.ToArray());
                }
            }
            set
            {
                lock (_textList)
                {
                    _textList.Add(value);
                    while (_textList.Count > log_conf.max_lines)
                    {
                        _textList.RemoveAt(0);
                    }
                }

                // WINFORM
                if (_control != null)
                {
                    if (PropertyChanged != null)
                    {
                        if (_control.InvokeRequired)
                        {
                            //_control.Invoke(new System.Windows.Forms.MethodInvoker(delegate ()
                            _control.Invoke(new Action(() =>
                            {
                                OnPropertyChanged("Text");
                            }));
                        }
                        else
                        {
                            OnPropertyChanged("Text");
                        }
                    }
                }

                // WPF
                if (_wpf_control != null)
                {
                    if (PropertyChanged != null)
                    {
                        _wpf_control.Dispatcher.Invoke(new Action(() =>
                        {
                            OnPropertyChanged("Text");
                        }));
                    }
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 내부 버퍼와 TextBox(UI)를 연결합니다
        /// </summary>
        /// <param name="textBox">TextBox UI</param>
        public void ConnectTextBox(object textBox)
        {
            if (textBox is System.Windows.Forms.TextBox)
            {
                _control = textBox as System.Windows.Forms.TextBox;

                _control.DataBindings.Clear();
                _control.DataBindings.Add(new System.Windows.Forms.Binding("Text", this, "Text"));
                _control.TextChanged += TextBox_TextChanged;
            }

            if (textBox is System.Windows.Controls.TextBox)
            {
                _wpf_control = textBox as System.Windows.Controls.TextBox;

                Binding binding = new Binding("Text")
                {
                    Source = this,
                    Path = new System.Windows.PropertyPath("Text"),
                    Mode = BindingMode.Default
                };

                _wpf_control.SetBinding(System.Windows.Controls.TextBox.TextProperty, binding);
                _wpf_control.TextChanged += TextBox_TextChanged;
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
            textBox.SelectionStart = textBox.Text.Length;
            textBox.ScrollToCaret();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            textBox.ScrollToEnd();
        }

        /// <summary>
        /// 내부 버퍼를 CLEAR 합니다.
        /// </summary>
        public void Clear()
        {
            lock (_textList)
            {
                _textList.Clear();
            }

            Text = "";
        }

     
        #endregion
        /// <summary>
        /// 로그파일이 저장될 폴더명 /etc/${process_name}/${header}_${yyyyMMdd}.${ext}
        /// </summary>
        string process_name = "";

        /// <summary>
        /// 로그파일의 헤더명
        /// ${header}_${yyyyMMdd}.${ext} 
        /// </summary>
        string header = "";
        /// <summary>
        /// 로그파일의 확장자
        /// ${header}_${yyyyMMdd}.${ext}
        /// </summary>
        string ext = "txt";

        /// <summary>
        /// LOG 클래스
        /// </summary>
        /// <param name="process_name">하위폴더: 프로세스별 로그 기록시 사용</param>
        /// <param name="header">파일헤더: 로그폴더/하위폴더/header_{yyyy-MM-dd}.log</param>
        public LOG_T(string process_name = "", string header = "")
        {
            Init(process_name, header);
        }

        public void Init(string process_name = "", string header = "")
        {
            this.process_name = process_name;
            this.header = header;
        }

        // 기록 파일 경로
        public string GetPath(bool isDebug = false)
        {
            string path;
            string file_name;

            if (isDebug == true)
                file_name = header + string.Format("{0}.debug", DateTime.Now.ToString("yyyy-MM-dd"));
            else file_name = header + string.Format("{0}.{1}", DateTime.Now.ToString("yyyy-MM-dd"), ext);

            try
            {
                // 폴더 확인 후 생성
                path = log_conf.dir_name;
                if (Directory.Exists(path) == false)
                    Directory.CreateDirectory(path);

                path = Path.Combine(log_conf.dir_name, process_name);
                if (Directory.Exists(path) == false)
                    Directory.CreateDirectory(path);

                path = Path.Combine(log_conf.dir_name, process_name, file_name);
            }
            catch
            {
                // 경로설정 불가시 바탕화면/wSystem.log 사용
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "wSystem.log");
            }

            return path;
        }

        private string Save(LOG_TYPE type, LOG_LEVEL category, string value)
        {
            string path;
            string text;

            if (category == LOG_LEVEL.NONE)
            {
                text = value;
            }
            else
            {
                text = string.Format("{0,7}, {1}, {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Enum.GetName(typeof(LOG_LEVEL), category), value);
            }

            if (type == LOG_TYPE.UI || type == LOG_TYPE.UI_FILE)
            {
                Text = text;
            }

            log_conf.ReadConfig();
            if (log_conf.verbose == 0)
            {

            }
            else if (log_conf.verbose >= 1)
            {
                Console.Write(text);

                // 로그파일 경로설정
                switch (category)
                {
                    case LOG_LEVEL.NONE:
                    case LOG_LEVEL.INFO:
                    case LOG_LEVEL.WARNING:
                    case LOG_LEVEL.ERROR:
                    case LOG_LEVEL.FATAL:
                        path = GetPath(false);
                        break;
                    case LOG_LEVEL.DEBUG:
                    case LOG_LEVEL.TRACE:
                        path = GetPath(true);
                        break;
                    default:
                        path = GetPath(false);
                        break;
                }

                if (type == LOG_TYPE.FILE || type == LOG_TYPE.UI_FILE)
                {
                    // PC상의 프로세서 접근 제어
                    using (Mutex mutex = new Mutex(false, path.Replace(Path.DirectorySeparatorChar, '/')))
                    {
                        if (mutex.WaitOne())
                        {
                            try
                            {
                                // 파일 기록
                                using (FileStream fs = File.Open(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                                {
                                    using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                                    {
                                        sw.Write(text);
                                        sw.Flush();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                WriteError("{0}: {1}", value, ex.Message);
                            }

                            mutex.ReleaseMutex();
                        }
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// 입력그대로 기록
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Write(string format, params object[] args)
        {
            return Write(LOG_TYPE.UI_FILE, format, args);
        }

        public string Write(LOG_TYPE type, string format, params object[] args)
        {
            string value = string.Format(format, args);
            value = value.Replace("[TIME]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            return Save(type, LOG_LEVEL.NONE, value);
        }

        /// <summary>
        /// 입력그대로 기록 + 개행문자 추가
        /// </summary>
        public string WriteLine()
        {
            return WriteLine(LOG_TYPE.UI_FILE, "\n");
        }

        /// <summary>
        /// 입력그대로 기록 + 개행문자 추가
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string WriteLine(string format, params object[] args)
        {
            return WriteLine(LOG_TYPE.UI_FILE, format, args);
        }

        /// <summary>
        /// 입력그대로 기록 + 개행문자 추가
        /// </summary>
        /// <param name="type"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string WriteLine(LOG_TYPE type, string format, params object[] args)
        {
            string value = string.Format(format, args) + Environment.NewLine;
            value = value.Replace("[TIME]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            return Save(type, LOG_LEVEL.NONE, value);
        }


        /// <summary>
        /// 로그 기록
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Info(string format, params object[] args)
        {
            return Info(LOG_TYPE.UI_FILE, format, args);
        }

        /// <summary>
        /// 로그 기록
        /// </summary>
        /// <param name="type"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Info(LOG_TYPE type, string format, params object[] args)
        {
            string value = string.Format(format, args) + Environment.NewLine;

            return Save(type, LOG_LEVEL.INFO, value);
        }

        /// <summary>
        /// 일부 서비스가 구동되지 못하는상황(복구가능)
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns>입력된 메시지</returns>
        public string Warning(string format, params object[] args)
        {
            return Warning(LOG_TYPE.UI_FILE, format, args);
        }

        /// <summary>
        /// 일부 서비스가 구동되지 못하는상황(복구가능)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Warning(LOG_TYPE type, string format, params object[] args)
        {
            string value = string.Format(format, args) + Environment.NewLine;

            return Save(type, LOG_LEVEL.WARNING, value);
        }

        /// <summary>
        /// 정상적인 서비스가 구동되지 못하는 상황(복구불가)
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns>입력된 메시지</returns>
        public string Error(string format, params object[] args)
        {
            return Error(LOG_TYPE.UI_FILE, format, args);
        }

        /// <summary>
        /// 정상적인 서비스가 구동되지 못하는 상황(복구불가)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Error(LOG_TYPE type, string format, params object[] args)
        {
            string value = string.Format(format, args) + Environment.NewLine;

            return Save(type, LOG_LEVEL.ERROR, value);
        }

        /// <summary>
        /// 로그 기록 후 프로세서가 중지되어야 하는 상황
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns>입력된 메시지</returns>
        public string Fatal(string format, params object[] args)
        {
            return Fatal(LOG_TYPE.UI_FILE, format, args);
        }

        /// <summary>
        /// 로그 기록 후 프로세서가 중지되어야 하는 상황
        /// </summary>
        /// <param name="type"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Fatal(LOG_TYPE type, string format, params object[] args)
        {
            string value = string.Format(format, args) + Environment.NewLine;

            return Save(type, LOG_LEVEL.FATAL, value);
        }

        /// <summary>
        /// 라인 띄우기(공백용)
        /// </summary>
        public void Line()
        {
            Line(LOG_TYPE.UI_FILE);
        }
        /// <summary>
        /// 라인 띄우기(공백용)
        /// </summary>
        /// <param name="type"></param>
        public void Line(LOG_TYPE type)
        {
            Save(type, LOG_LEVEL.NONE, Environment.NewLine);
        }

        /// <summary>
        /// 디버깅용 로그 log_conf.verbose 가 2 이상일 경우 yyyy-MM-dd.debug 에 기록
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns>입력된 메시지</returns>
        public string Debug(string format, params object[] args)
        {
            return Debug(LOG_TYPE.UI_FILE, format, args);
        }

        /// <summary>
        /// 디버깅용 로그 log_conf.verbose 가 2 이상일 경우 yyyy-MM-dd.debug 에 기록
        /// </summary>
        /// <param name="type"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Debug(LOG_TYPE type, string format, params object[] args)
        {
            string value = string.Format(format, args) + Environment.NewLine;

            // VERBOSE 2 이상일 경우 기록
            log_conf.ReadConfig();
            if (log_conf.verbose >= 2)
                Save(type, LOG_LEVEL.DEBUG, value);

            return value;
        }

        /// <summary>
        /// 디버깅용 로그 log_conf.verbose 가 3 이상일 경우 yyyy-MM-dd.debug 에 기록
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns>출력된 메시지</returns>
        public string Trace(string format, params object[] args)
        {
            return Trace(LOG_TYPE.UI_FILE, format, args);
        }

        /// <summary>
        /// 디버깅용 로그 log_conf.verbose 가 3 이상일 경우 yyyy-MM-dd.debug 에 기록
        /// </summary>
        /// <param name="type"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns>출력된 메시지</returns>
        public string Trace(LOG_TYPE type, string format, params object[] args)
        {
            string value = string.Format(format, args) + Environment.NewLine;

            // VERBOSE 3 이상일 경우 기록
            log_conf.ReadConfig();
            if (log_conf.verbose >= 3)
                return Save(type, LOG_LEVEL.TRACE, value);

            return value;
        }

        /// <summary>
        /// 로그기록에 문제가 있을 경우(폴더 또는 파일생성 권한 등) 바탕화면/wSystem.log 에 기록
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns>입력된 메시지</returns>
        public static string WriteError(string format, params object[] args)
        {
            string text = string.Format(format, args) + Environment.NewLine;
            Console.Write("[ERROR] {0}", text);

            try
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "wSystem.log");
                File.AppendAllText(path, text);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] LOG_T(WriteError({0})): {1}", format, ex.Message);
            }

            return text;
        }
    }
}
