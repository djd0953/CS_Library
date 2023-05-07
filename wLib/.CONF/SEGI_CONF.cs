using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace wLib
{
    public class SEGI_CONF : RESTAPI_CONF
    {
        // new CONFIG(string "설정 파일명", string "폴더명:기본값etc")
        CONFIG config = new CONFIG("SEGI.ini");

        // 설정파일 항목(키=기본값)
        public override bool used { get; set; } = false;
        public override string comment { get; set; } = "세기미래기술(REST)";

        public override string Ip { get; set; } = "127.0.0.1";
        public override string Port { get; set; } = "36302";
        public override string Id { get; set; } = "wjogi";
        public override string Key { get; set; } = "UCS000";

        public 

        SEGI_CONF(string value = "SEGI")
        {
            section = value;

            ReadConfig();
        }

        public override bool ReadConfig()
        {
            return ReadConfig(false);
        }
        public override bool ReadConfig(bool isReset)
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

                Ip = config.ReadString(section, "IP", Ip);
                Port = config.ReadString(section, "PORT", Port);
                Id = config.ReadString(section, "ID", Id);
                Key = config.ReadString(section, "KEY", Key);

                // 동시접근제어 해제(PC상의 모든 프로세스에서 하나만 접근 가능)
                config.ReleaseMutex();
            }

            // 파일의 변경이 있으면(설정파일이 갱신 되었으면) true
            return true;
        }

        public override void SaveConfig()
        {
            // 동시접근제어 잠금(PC상의 모든 프로세스에서 하나만 접근 가능)
            if (config.LockMutex())
            {
                //섹션
                config.WriteBool(section, "USED", used);
                config.WriteString(section, "COMMENT", comment);

                config.WriteString(section, "IP", Ip);
                config.WriteString(section, "PORT", Port);
                config.WriteString(section, "ID", Id);
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
