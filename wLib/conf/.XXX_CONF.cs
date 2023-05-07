using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    /// <summary>
    /// CONF 클래스 양식
    /// </summary>
    public class XXX_CONF
    {
        CONFIG config = new CONFIG("XXX.ini");

        // 설정파일 항목([섹션])
        private string section;

        #region /* USER CODE BEGIN */
        // 설정파일 항목(키=기본값)
        string key_string = "";
        int key_int = 0;
        string key_password = "";
        DateTime key_datetime = new DateTime();
        #endregion /* USER CODE END */

        public XXX_CONF(string value)
        {
            section = value;

            ReadConfig();
        }

        /// <summary>
        /// 설정파일을 읽습니다.
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
                #region /* USER CODE BEGIN */

                key_string = config.ReadString(section, "KEY_STRING", key_string);
                key_int = config.ReadInteger(section, "KEY_INT", key_int);
                key_password = config.ReadPassword(section, "KEY_PASSWORD", key_password);
                key_datetime = config.ReadDateTime(section, "KEY_DATETIME", key_datetime);

                #endregion /* USER CODE END */

                config.ReleaseMutex();
            }

            return true;
        }

        /// <summary>
        /// 설정파일을 저장합니다.
        /// </summary>
        public void SaveConfig()
        {
            if (config.LockMutex())
            {
                // 변수 = config.Writexxxx(섹션, 키, 기본값);
                #region /* USER CODE BEGIN */

                config.WriteString(section, "KEY_STRING", key_string);
                config.WriteInteger(section, "KEY_INT", key_int);
                config.WritePassword(section, "KEY_PASSWORD", key_password);
                config.WriteDateTime(section, "KEY_DATETIME", key_datetime);

                #endregion /* USER CODE END */

                config.LastReadTime = config.LastWriteTime;

                config.ReleaseMutex();
            }
        }

    }
}
