using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class TABLE_CONF
    {
        CONFIG config = new CONFIG("TABLE.ini");

        // 설정파일 항목([섹션])
        public string section;

        // 설정파일 항목(키=기본값)
        public bool wb_used = true;
        public bool jh_used = false;

        public bool nms_used = true;
        public bool qc_used = true;
        public bool rb_used = false;

        public TABLE_CONF(string section = "TABLE")
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
                jh_used = config.ReadBool(section, "JH_USED", jh_used);

                nms_used = config.ReadBool(section, "NMS_USED", nms_used);
                qc_used = config.ReadBool(section, "QC_USED", qc_used);
                rb_used = config.ReadBool(section, "RB_USED", rb_used);
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
                config.WriteBool(section, "JH_USED", jh_used);

                config.WriteBool(section, "NMS_USED", nms_used);
                config.WriteBool(section, "QC_USED", qc_used);
                config.WriteBool(section, "RB_USED", rb_used);

                config.LastReadTime = config.LastWriteTime;

                config.ReleaseMutex();
            }
        }
    }
}
