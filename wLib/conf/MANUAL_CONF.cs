using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class MANUAL_CONF
    {
        CONFIG config = new CONFIG("MANUAL.ini");

        // 설정파일 항목([섹션])
        public string section;

        // MENU
        public bool used = true;
        public string title = "매뉴얼";

        // MANUAL
        public string dir_name = "doc";
        public string file_name = "manual.xps";

        public MANUAL_CONF(string section)
        {
            this.section = section;
            file_name = $"{section}.xps";

            ReadConfig();
        }

        public bool ReadConfig()
        {
            if (config.LastWriteTime == config.LastReadTime)
                return false;

            if (config.LockMutex())
            {
                config.LastReadTime = config.LastWriteTime;

                used = config.ReadBool(section, "USED", used);
                title = config.ReadString(section, "TITLE", title);

                dir_name = config.ReadString(section, "DIR_NAME", dir_name);
                file_name = config.ReadString(section, "FILE_NAME", file_name);

                config.ReleaseMutex();
            }

            return true;
        }
    }
}
