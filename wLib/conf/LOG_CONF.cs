using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class LOG_CONF
    {
        // new CONFIG(string "설정 파일명", string "폴더명:기본값etc")
        CONFIG config = new CONFIG("LOG.ini");

        // 설정파일 항목([섹션])
        public string section = "LOG";

        // 로그가 기록될 폴더명
        public string dir_name = "log";
        public string ext = "txt";
        /// <summary>
        /// verbose 0: 로그 미사용
        /// verbose 1: 로그 기록
        /// verbose 2: 로그 기록 + DEBUG 기록
        /// verbose 3: 로그 기록 + DEBUG 기록 + TRACE 기록
        /// </summary>
        public int verbose = 1;

        /// <summary>
        /// 내부버퍼에 저장 할 최대 줄 수
        /// </summary>
        public int max_lines = 100;

        public LOG_CONF()
        {
            ReadConfig();
        }

        public bool ReadConfig()
        {
            // 현재 파일의 기록시간 과 마지막 읽은 파일의 기록시간 비교
            if (config.LastWriteTime == config.LastReadTime)
            {
                // 파일의 변경이 없으면 false
                return false;
            }

            // 동시접근제어 잠금(PC상의 모든 프로세스에서 하나만 접근 가능)
            if (config.LockMutex())
            {
                // 마지막 읽은 파일의 기록시간 갱신
                config.LastReadTime = config.LastWriteTime;

                // config.Readxxxx(섹션, 키, 기본값)
                dir_name = config.ReadString(section, "DIR_NAME", dir_name);
                ext = config.ReadString(section, "EXT", ext);
                verbose = config.ReadInteger(section, "VERBOSE", verbose);
                max_lines = config.ReadInteger(section, "MAX_LINES", max_lines);

                // 동시접근제어 해제(PC상의 모든 프로세스에서 하나만 접근 가능)
                config.ReleaseMutex();
            }

            // 파일의 변경이 있으면(설정파일이 갱신 되었으면) true
            return true;
        }

        public void SaveConfig()
        {
            // 동시접근제어 잠금(PC상의 모든 프로세스에서 하나만 접근 가능)
            if (config.LockMutex())
            {
                config.WriteString(section, "DIR_NAME", dir_name);
                config.WriteString(section, "EXT", ext);
                config.WriteInteger(section, "VERBOSE", verbose);
                config.WriteInteger(section, "MAX_LINES", max_lines);

                // 마지막 파일의 기록시간 변경
                config.LastReadTime = config.LastWriteTime;

                // 동시접근제어 해제(PC상의 모든 프로세스에서 하나만 접근 가능)
                config.ReleaseMutex();
            }

            return;
        }
    }
}
