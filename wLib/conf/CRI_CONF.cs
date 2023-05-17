using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class CRI_CONF
    {
        CONFIG config = new CONFIG("CRITICAL.ini");

        // 설정파일 항목([섹션])
        public string section;

        // 설정파일 항목(키=기본값)
        public bool ndms_used = false;

        public int criticalDataTerm = 5;
        public int dataInsertTerm = 1;

        public CRI_CONF(string section = "CRITICAL")
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
                ndms_used = config.ReadBool(section, "NDMS_USED", ndms_used);

                criticalDataTerm = config.ReadInteger(section, "CRITICAL_DATA_TERM", criticalDataTerm);
                dataInsertTerm = config.ReadInteger(section, "DATA_INSERT_TERM", dataInsertTerm);

                config.ReleaseMutex();
            }

            return true;
        }

        public void SaveConfig()
        {
            if (config.LockMutex())
            {
                // config.Writexxxx(섹션, 키, 기본값)
                config.WriteBool(section, "NDMS_USED", ndms_used);

                config.WriteInteger(section, "CRITICAL_DATA_TERM", criticalDataTerm);
                config.WriteInteger(section, "DATA_INSERT_TERM", dataInsertTerm);

                config.LastReadTime = config.LastWriteTime;

                config.ReleaseMutex();
            }
        }
    }
}
