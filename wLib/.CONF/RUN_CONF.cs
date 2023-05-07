using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class RUN_CONF
    {
        CONFIG config = new CONFIG("RUN.ini");

        // 설정파일 항목([섹션])
        private string section;

        // 설정파일 항목(키=기본값)
        public bool run_visible = false;
        public bool run_mode = true; // 0: 수동, 1:자동
        public int run_interval = 5; // 초
        public int run_thread_num { get; set; } = -1;

        public RUN_CONF(string section)
        {
            this.section = section;

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

                // config.Readxxxx(섹션, 키, 기본값)
                run_visible = config.ReadBool(section, "RUN_VISIBLE", run_visible);
                run_mode = config.ReadBool(section, "RUN_MODE", run_mode);
                run_interval = config.ReadInteger(section, "RUN_INTERVAL", run_interval);
                run_thread_num = config.ReadInteger(section, "RUN_THREAD_NUM", run_thread_num);

                config.ReleaseMutex();
            }

            return true;
        }
        public void SaveConfig()
        {
            if (config.LockMutex())
            {
                // config.Writexxxx(섹션, 키, 기본값)
                config.WriteBool(section, "RUN_VISIBLE", run_visible);
                config.WriteBool(section, "RUN_MODE", run_mode);
                config.WriteInteger(section, "RUN_INTERVAL", run_interval);
                config.WriteInteger(section, "RUN_THREAD_NUM", run_thread_num);

                config.LastReadTime = config.LastWriteTime;

                config.ReleaseMutex();
            }

            return;
        }
    }
}
