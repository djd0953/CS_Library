using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class CAST_CONF
    {
        CONFIG config = new CONFIG("CAST.ini");

        // 설정파일 항목([섹션])
        public string section;

        // 설정파일 항목(키=기본값)
        public bool used = true;
        public string area_code = "255";

        public CAST_CONF(string section = "CAST")
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

                area_code = config.ReadString(section, "AREA_CODE", area_code);

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
                config.WriteString(section, "AREA_CODE", area_code);

                config.LastReadTime = config.LastWriteTime;

                config.ReleaseMutex();
            }

            return;
        }
    }
}
