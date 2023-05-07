using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public enum NDMS_TABLE_TYPE { DISTRICT = 0, INLANDWATERS = 1, RESERVOIR = 3, SLOPE = 4, FACILITIES = 5, FLUD = 20 };
    public class NDMS_TABLE_CONF
    {
        CONFIG config = new CONFIG("NDMS_TABLE_CONF.ini");

        // 설정파일 항목([섹션])
        public string section;

        // 설정파일 항목(키=기본값)
        public bool wb_used = true;
        public bool rb_used = false;

        public bool db_used = true;
        public bool rest_used = false;
        public string type = "district";

        public NDMS_TABLE_CONF(string section = "NDMS_TABLE")
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
                wb_used = config.ReadBool(section, "WB_USED", wb_used);
                rb_used = config.ReadBool(section, "RB_USED", rb_used);
                db_used = config.ReadBool(section, "DB_USED", db_used);
                rest_used = config.ReadBool(section, "REST_USED", rest_used);
                type = config.ReadString(section, "TYPE", type);

                config.ReleaseMutex();
            }

            return true;
        }

        public void SaveConfig()
        {
            if (config.LockMutex())
            {
                // config.Writexxxx(섹션, 키, 기본값)
                config.WriteBool(section, "WB_USED", wb_used);
                config.WriteBool(section, "RB_USED", rb_used);
                config.WriteBool(section, "DB_USED", db_used);
                config.WriteBool(section, "REST_USED", rest_used);
                config.WriteString(section, "Type", type);
                config.LastReadTime = config.LastWriteTime;

                config.ReleaseMutex();
            }
        }
    }
}
