using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class EXT_DATA_CONF
    {
        CONFIG config = new CONFIG("EXT_DATA.ini");

        // 설정파일 항목([섹션])
        public string section;

        // 설정파일 항목(키=기본값)
        public bool used = false;

        public DB_INFO db_info = new DB_INFO();
        public string db_table { get; set; } = "data";

        public int channel_count = 0;
        public CHANNEL_CONF[] channel = new CHANNEL_CONF[0];

        public EXT_DATA_CONF(string section = "EXT")
        {
            this.section = section;

            ReadConfig();
        }

        public bool ReadConfig()
        {
            string temp;

            if (config.LastWriteTime == config.LastReadTime)
                return false;

            if (config.LockMutex())
            {
                config.LastReadTime = config.LastWriteTime;

                // config.Readxxxx(섹션, 키, 기본값)
                used = config.ReadBool(section, "USED", used);

                // DB_INFO
                db_info.Ip = config.ReadString(section, "IP", db_info.Ip);
                db_info.Port = config.ReadString(section, "PORT", db_info.Port);
                db_info.Dbname = config.ReadString(section, "DBNAME", db_info.Dbname);
                db_info.Id = config.ReadString(section, "ID", db_info.Id);
                db_info.Pw = config.ReadPassword(section, "PW", db_info.Pw);
                db_info.Charset = config.ReadString(section, "CHARSET", db_info.Charset);
                db_info.Timeout = config.ReadString(section, "TIMEOUT", db_info.Timeout);

                // DB_PATH
                db_table = config.ReadString(section, "DB_TABLE", db_table);

                // CHANNEL
                channel_count = config.ReadInteger(section, "CHANNEL_COUNT", channel_count);
                Array.Resize(ref channel, channel_count);

                for (int i = 0; i < channel_count; i++)
                {
                    if (channel[i] == null)
                        channel[i] = new CHANNEL_CONF();

                    int idx = i + 1;

                    temp = string.Format("CHANNEL_{0}_NAME", idx);
                    channel[i].name = config.ReadString(section, temp, channel[i].name);

                    temp = string.Format("CHANNEL_{0}_OFFSET", idx);
                    channel[i].offset = config.ReadInteger(section, temp, channel[i].offset);

                    temp = string.Format("CHANNEL_{0}_OBSV", idx);
                    channel[i].obsv = config.ReadString(section, temp, channel[i].obsv);

                    temp = string.Format("CHANNEL_{0}_SUB_OBSV", idx);
                    channel[i].sub_obsv = config.ReadString(section, temp, channel[i].sub_obsv);

                    temp = string.Format("CHANNEL_{0}_GB_OBSV", idx);
                    channel[i].gb_obsv = config.ReadString(section, temp, channel[i].gb_obsv);

                    temp = string.Format("CHANNEL_{0}_UNIT", idx);
                    channel[i].unit = config.ReadString(section, temp, channel[i].unit);
                }

                config.ReleaseMutex();
            }

            return true;
        }

        public void SaveConfig()
        {
            string temp;

            if (config.LockMutex())
            {
                // config.Writexxxx(섹션, 키, 기본값)
                config.WriteBool(section, "USED", used);

                // DB_INFO
                config.WriteString(section, "IP", db_info.Ip);
                config.WriteString(section, "PORT", db_info.Port);
                config.WriteString(section, "DBNAME", db_info.Dbname);
                config.WriteString(section, "ID", db_info.Id);
                config.WritePassword(section, "PW", db_info.Pw);
                config.WriteString(section, "CHARSET", db_info.Charset);
                config.WriteString(section, "TIMEOUT", db_info.Timeout);

                config.WriteString(section, "DB_TABLE", db_table);

                // CHANNEL
                config.WriteInteger(section, "CHANNEL_COUNT", channel_count);
                for (int i = 0; i < channel.Length; i++)
                {
                    int idx = i + 1;

                    temp = string.Format("CHANNEL_{0}_NAME", idx);
                    config.WriteString(section, temp, channel[i].name);

                    temp = string.Format("CHANNEL_{0}_OFFSET", idx);
                    config.WriteInteger(section, temp, channel[i].offset);

                    temp = string.Format("CHANNEL_{0}_OBSV", idx);
                    config.WriteString(section, temp, channel[i].obsv);

                    temp = string.Format("CHANNEL_{0}_SUB_OBSV", idx);
                    config.WriteString(section, temp, channel[i].sub_obsv);

                    temp = string.Format("CHANNEL_{0}_GB_OBSV", idx);
                    config.WriteString(section, temp, channel[i].gb_obsv);

                    temp = string.Format("CHANNEL_{0}_UNIT", idx);
                    config.WriteString(section, temp, channel[i].unit);
                }

                config.LastReadTime = config.LastWriteTime;

                config.ReleaseMutex();
            }
        }
    }
}
