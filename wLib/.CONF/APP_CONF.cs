using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace wLib
{
    public class APP_CONF
    {
        // new CONFIG(string "설정 파일명", string "폴더명:기본값etc")
        CONFIG config = new CONFIG("APP.ini");

        // 설정파일 항목([섹션])
        private string section;

        // 설정파일 항목(키=기본값)
        public APP_WINDOW window;
        public APP_PROCESS process;
        public APP_ICON icon;

        public string system_name { get; set; } = "";
        public string program_name { get; set; } = "";

        public APP_CONF(string value)
        {
            section = value;

            window = new APP_WINDOW(config);
            process = new APP_PROCESS(config);
            icon = new APP_ICON(config);

            ReadConfig();
        }

        public APP_CONF(string value, string title)
        {
            section = value;

            window = new APP_WINDOW(config) { Title = title };
            process = new APP_PROCESS(config);
            icon = new APP_ICON(config);

            program_name = title;

            ReadConfig();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isReload">true: 항상 읽음, false: 변경시에만 읽음</param>
        /// <returns></returns>
        public bool ReadConfig(bool isReload = false)
        {
            if (isReload == false)
            {
                if (config.LastWriteTime == config.LastReadTime)
                    return false;
            }

            if (config.LockMutex())
            {
                config.LastReadTime = config.LastWriteTime;

                system_name = config.ReadString("GLOBAL", "SYSTEM_NAME", system_name);
                program_name = config.ReadString(section, "PROGRAM_NAME", program_name);

                window.ReadConfig(section);
                process.ReadConfig(section);
                //icon.ReadConfig(section);
                

                config.ReleaseMutex();
            }

            return true;
        }

        public void SaveConfig()
        {
            if (config.LockMutex())
            {
                window.SaveConfig(section);
                //process.SaveConfig(section); // 읽기전용
                //icon.SaveConfig(section); // 읽기전용

                config.WriteString("GLOBAL", "SYSTEM_NAME", system_name);
                config.WriteString(section, "PROGRAM_NAME", program_name);

                config.LastReadTime = config.LastWriteTime;

                config.ReleaseMutex();
            }
        }
    }

    public class APP_PROCESS
    {
        CONFIG config;

        public string ProcessName { get; }
        public string Version { get; set; }

        public bool IsSerial
        {
            get
            {
                return Serial == ProcessSerial;
            }
        }

        public bool IsAlone
        {
            get
            {
                return (Alone == true && ProcessAlone == false) ? false : true;
            }
        }

        // 프로세스가 읽은 정보(설정파일 매칭)
        public uint ProcessSerial { get; set; }
        private bool ProcessAlone { get; set; }

        // 설정파일
        private uint Serial { get; set; } = 0;
        private bool Alone { get; set; } = true;
        

        public APP_PROCESS(CONFIG _config)
        {
            config = _config;

            // 프로세스 명
            try
            {
                ProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            }
            catch
            {
                ProcessName = "";
            }

            // 프로세스 파일버전
            try
            {
                System.Diagnostics.FileVersionInfo file_version = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);

                string[] temp = file_version.FileVersion.Split('.');
                Version = string.Format("{0}.{1}.{2}", temp[0], temp[1], temp[2]);
            }
            catch
            {
                Version = "";
            }

            // 프로세스 시리얼번호
            try
            {
                // System.Management 추가
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"C:\"");
                disk.Get();
                ProcessSerial = Convert.ToUInt32(disk["VolumeSerialNumber"].ToString(), 16);
                //ProcessSerial = Convert.ToUInt32("A275FB56", 16); // 2725641046
            }
            catch (Exception ex)
            {
                Console.WriteLine("[WARNING] APP_PROCESS(ManagementObject): {0}", ex.Message);

                ProcessSerial = 2000;
                Console.WriteLine("[INFO] APP_PROCESS(ProcessSerial): = {0}", ProcessSerial);
            }

            // 프로세스 ALONE
            try
            {
                System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName(ProcessName);
                if (ps.Length == 1)
                {
                    ProcessAlone = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[WARNING] APP_PROCESS(GetProcessesByName): {0}", ex.Message);

                ProcessAlone = true;
                Console.WriteLine("[INFO] APP_PROCESS(ProcessAlone):= {0}", ProcessAlone);
            }
        }

        public void ReadConfig(string section)
        {
#if DEFINE_SERIAL
            Serial = config.ReadUInteger(section, "SERIAL", Serial);

            //Microsoft.Win32.Registry.CurrentUser.CreateSubKey("WOOBO");
            //Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("WOOBO\\wSystem");
            //
            //try
            //{
            //    object key_value = key.GetValue("LICENSE");
            //    if (key_value == null)
            //    {
            //        throw new Exception();
            //    }
            //
            //    Serial = uint.Parse(key_value.ToString());
            //}
            //catch
            //{
            //    Serial = config.ReadUInteger(section, "SERIAL", Serial);
            //};
#endif

            Alone = config.ReadBool(section, "ALONE", Alone);
        }

        public void SaveConfig(string section)
        {
#if DEFINE_SERIAL
            config.WriteUInteger(section, "SERIAL", Serial);

            //Microsoft.Win32.Registry.CurrentUser.CreateSubKey("WOOBO");
            //Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("WOOBO\\wSystem");
            //key.SetValue("License", Serial, Microsoft.Win32.RegistryValueKind.String);
#endif
            config.WriteBool(section, "ALONE", Alone);
        }


    }

    public class APP_WINDOW
    {
        CONFIG config;

        public string Title { get; set; } = "우보재난시스템";
        public int Top { get; set; } = 0;
        public int Left { get; set; } = 0;
        public int Width { get; set; } = 400;
        public int Height { get; set; } = 500;
        public int State { get; set; } = 0;
        public bool Visible { get; set; } = true;


        public APP_WINDOW(CONFIG _config)
        {
            config = _config;
        }

        public void ReadConfig(string section)
        {
            Title = config.ReadString(section, "TITLE", Title);
            Top = config.ReadInteger(section, "TOP", Top);
            Left = config.ReadInteger(section, "LEFT", Left);
            Width = config.ReadInteger(section, "WIDTH", Width);
            Height = config.ReadInteger(section, "HEIGHT", Height);
            Visible = config.ReadBool(section, "VISIBLE", Visible);
            State = config.ReadInteger(section, "STATE", State);
        }

        public void SaveConfig(string section)
        {
            config.WriteString(section, "TITLE", Title);
            config.WriteInteger(section, "TOP", Top);
            config.WriteInteger(section, "LEFT", Left);
            config.WriteInteger(section, "WIDTH", Width);
            config.WriteInteger(section, "HEIGHT", Height);
            config.WriteBool(section, "VISIBLE", Visible);
            config.WriteInteger(section, "STATE", State);
        }
    }

    public class APP_ICON
    {
        CONFIG config;

        public bool ci_left_visible { get; set; } = true;
        public int ci_left_width { get; set; } = 200;
        public string ci_left_path { get; set; } = "ci/ci_left.png";
        public ImageSource ci_left_source = new BitmapImage();

        public bool ci_right_visible { get; set; } = false;
        public int ci_right_width { get; set; } = 200;
        public string ci_right_path { get; set; } = "ci/ci_right.png";
        public ImageSource ci_right_source = new BitmapImage();

        public bool minimized_visible { get; set; } = true;
        public bool maximized_visible { get; set; } = true;
        public bool closed_visible { get; set; } = true;

        public APP_ICON(CONFIG _config)
        {
            config = _config;
        }

        public void ReadConfig(string section)
        {
            ci_left_visible = config.ReadBool(section, "CI_LEFT_VISIBLE", ci_left_visible);
            ci_left_width = config.ReadInteger(section, "CI_LEFT_WIDTH", ci_left_width);
            ci_left_path = config.ReadString(section, "CI_LEFT_PATH", ci_left_path);

            try
            {
                FileStream fs = new FileStream(ci_left_path, FileMode.Open, FileAccess.Read);
                Byte[] bImage = new Byte[fs.Length];
                fs.Read(bImage, 0, (int)fs.Length);
                fs.Close();

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(bImage);
                bitmap.EndInit();

                ci_left_source = bitmap;
            }
            catch
            {
                ci_left_source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.ci_woobo_bar.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }

            ci_right_visible = config.ReadBool(section, "CI_RIGHT_VISIBLE", ci_right_visible);
            ci_right_width = config.ReadInteger(section, "CI_RIGHT_WIDTH", ci_right_width);
            ci_right_path = config.ReadString(section, "CI_RIGHT_PATH", ci_right_path);

            try
            {
                FileStream fs = new FileStream(ci_right_path, FileMode.Open, FileAccess.Read);
                Byte[] bImage = new Byte[fs.Length];
                fs.Read(bImage, 0, (int)fs.Length);
                fs.Close();

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(bImage);
                bitmap.EndInit();

                ci_right_source = bitmap;
            }
            catch
            {
                ci_right_source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.ci_woobo_bar.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }

            minimized_visible = config.ReadBool(section, "MINIMIZED_VISIBLE", minimized_visible);
            maximized_visible = config.ReadBool(section, "MAXIMIZED_VISIBLE", maximized_visible);
            closed_visible = config.ReadBool(section, "CLOSED_VISIBLE", closed_visible);
        }

        public void SaveConfig(string section)
        {
            config.WriteBool(section, "CI_LEFT_VISIBLE", ci_left_visible);
            config.WriteInteger(section, "CI_LEFT_WIDTH", ci_left_width);
            config.WriteString(section, "CI_LEFT_PATH", ci_left_path);

            config.WriteBool(section, "CI_RIGHT_VISIBLE", ci_right_visible);
            config.WriteInteger(section, "CI_RIGHT_WIDTH", ci_right_width);
            config.WriteString(section, "CI_RIGHT_PATH", ci_right_path);

            config.WriteBool(section, "MINIMIZED_VISIBLE", minimized_visible);
            config.WriteBool(section, "MAXIMIZED_VISIBLE", maximized_visible);
            config.WriteBool(section, "CLOSED_VISIBLE", closed_visible);
        }
    }

    public class APP_INFO
    {
        CONFIG config;

        public string info_sysname { get; set; } = "시스템명";
        public string info_progname { get; set; } = "프로그램명";

        public APP_INFO(CONFIG _config)
        {
            config = _config;
        }

        public void ReadConfig(string section)
        {
            info_sysname = config.ReadString(section, "INFO_SYSNAME", info_sysname);
            info_progname = config.ReadString(section, "INFO_PROGNAME", info_progname);
        }

        public void SaveConfig(string section)
        {
            config.WriteString(section, "INFO_SYSNAME", info_sysname);
            config.WriteString(section, "INFO_PROGNAME", info_progname);
        }
    }

    public class APP_RUN
    {
        CONFIG config;

        public bool RunMode { get; set; } = true; // 0: 수동, 1:자동
        public int RunInterval { get; set; } = 5; // 초

        public APP_RUN(CONFIG _config)
        {
            config = _config;
        }

        public void ReadConfig(string section)
        {
            RunMode = config.ReadBool(section, "RUN_MODE", RunMode);
            RunInterval = config.ReadInteger(section, "RUN_INTERVAL", RunInterval);
        }

        public void SaveConfig(string section)
        {
            config.WriteBool(section, "RUN_MODE", RunMode);
            config.WriteInteger(section, "RUN_INTERVAL", RunInterval);
        }
    }
}
