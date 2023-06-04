using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class HTI_CONF
    {
        CONFIG config = new CONFIG("HTI.ini");

        // 설정파일 항목([섹션])
        private string section;

        // 설정파일 항목(키=기본값)
        public bool used = false;

        public string output_dir = "";

        public HTI_CONF(string section = "HTI")
        {
            this.section = section;

            ReadConfig();
        }

        public bool ReadConfig()
        {
            if (config.LastWriteTime == config.LastReadTime)
                return false;

            if (config.LockMutex())
            {
                config.LastReadTime = config.LastWriteTime;

                // config.Readxxxx(섹션, 키, 기본값)
                used = config.ReadBool(section, "USED", used);

                output_dir = config.ReadString(section, "OUTPUT_DIR", output_dir);

                config.ReleaseMutex();
            }

            return true;
        }

        public void SaveConfig()
        {
            if (config.LockMutex())
            {
                // config.Writexxxx(섹션, 키, 기본값)
                config.WriteBool(section, "USED", used);

                config.WriteString(section, "OUTPUT_DIR", output_dir);

                config.LastReadTime = config.LastWriteTime;

                config.WriteString(section, "OUTPUT_DIR", output_dir);

                config.LastReadTime = config.LastWriteTime;

                config.WriteString(section, "OUTPUT_DIR", output_dir);

                config.LastReadTime = config.LastWriteTime;

                config.ReleaseMutex();
            }
        }
    }
}
