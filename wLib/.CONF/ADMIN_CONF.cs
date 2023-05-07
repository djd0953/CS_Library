using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class ADMIN_CONF
    {
        CONFIG config = new CONFIG("ADMIN.ini");

        // 설정파일 항목([섹션])
        public string section;

        // 설정파일 항목(키=기본값)
        public string user_name = "사용자";
        public string user_pass = "";
        public string test_name = "테스터";
        public string test_pass = "test";
        public string admin_name = "관리자";
        public string admin_pass = "admin";

        public ADMIN_CONF(string section = "ADMIN")
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
                user_name = config.ReadString(section, "USER_NAME", user_name);
                user_pass = config.ReadPassword(section, "USER_PASS", user_pass);
                test_name = config.ReadString(section, "TEST_NAME", test_name);
                test_pass = config.ReadPassword(section, "TEST_PASS", test_pass);
                admin_name = config.ReadString(section, "ADMIN_NAME", admin_name);
                admin_pass = config.ReadPassword(section, "ADMIN_PASS", admin_pass);

                config.ReleaseMutex();
            }

            return true;
        }

        public void SaveConfig()
        {
            if (config.LockMutex())
            {
                // config.Writexxxx(섹션, 키, 기본값)
                config.WriteString(section, "USER_NAME", user_name);
                config.WritePassword(section, "USER_PASS", user_pass);
                config.WriteString(section, "TEST_NAME", test_name);
                config.WritePassword(section, "TEST_PASS", test_pass);
                config.WriteString(section, "ADMIN_NAME", admin_name);
                config.WritePassword(section, "ADMIN_PASS", admin_pass);

                config.LastReadTime = config.LastWriteTime;

                config.ReleaseMutex();
            }

            return;
        }
    }
}
