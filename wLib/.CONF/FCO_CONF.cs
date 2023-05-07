using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wLib
{
    public class FCO_CONF
    {
        // new CONFIG(string "설정 파일명", string "폴더명:기본값etc")
        CONFIG config = new CONFIG("FCO.ini");

        // 설정파일 항목([섹션])
        public string section;

        // 설정파일 항목(키=기본값)
        public bool used { get; set; } = false;
        public string comment { get; set; } = "홍수통제소";

        public string Url { get; set; } = "http://api.hrfco.go.kr";
        public string Key { get; set; } = "34467655-4C98-4AB0-8F32-DE5115D630CE";

        public FCO_CONF(string value = "FCO")
        {
            section = value;

            ReadConfig();
        }

        public bool ReLoad()
        {
            return ReadConfig(true);
        }

        public bool ReadConfig()
        {
            return ReadConfig(false);
        }

        public bool ReadConfig(bool isReset)
        {
            // 현재 파일의 기록시간 과 마지막 읽은 파일의 기록시간 비교
            if (config.LastWriteTime == config.LastReadTime)
            {
                // 파일의 변경이 없으면 false
                if (isReset == false)
                    return false;
            }

            // 동시접근제어 잠금(PC상의 모든 프로세스에서 하나만 접근 가능)
            if (config.LockMutex())
            {
                config.LastReadTime = config.LastWriteTime;

                // config.ReadXxxx(섹션, 키, 기본값)
                used = config.ReadBool(section, "USED", used);
                comment = config.ReadString(section, "COMMENT", comment);
                Url = config.ReadString(section, "URL", Url);
                Key = config.ReadString(section, "KEY", Key);

                // 동시접근제어 해제(PC상의 모든 프로세스에서 하나만 접근 가능)
                config.ReleaseMutex();
            }

            // 파일의 변경이 있으면(설정파일이 갱신 되었으면) true
            return true;
        }

        public virtual void SaveConfig()
        {
            // 동시접근제어 잠금(PC상의 모든 프로세스에서 하나만 접근 가능)
            if (config.LockMutex())
            {
                //섹션
                config.WriteBool(section, "USED", used);
                config.WriteString(section, "COMMENT", comment);
                config.WriteString(section, "URL", Url);
                config.WriteString(section, "KEY", Key);

                // 마지막 파일의 기록시간 변경
                config.LastReadTime = config.LastWriteTime;

                // 동시접근제어 해제(PC상의 모든 프로세스에서 하나만 접근 가능)
                config.ReleaseMutex();
            }

            return;
        }
    }
}
