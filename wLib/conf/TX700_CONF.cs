using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class TX700_CONF : COMPORT_INFO
    {
        CONFIG config = new CONFIG("TX700.ini");

        // 설정파일 항목([섹션])
        public string section;

        // 설정파일 항목(키=기본값)
        public bool used = true;

        public override string Port_name { get; set; } = "COM2";
        public override int Baud_rate { get; set; } = 115200;
        public override int Parity_bits { get; set; } = 0;
        public override int Data_bits { get; set; } = 8;
        public override int Stop_bits { get; set; } = 1;

        public TX700_CONF(string section = "TX700")
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
                used = config.ReadBool(section, "USED", used);

                Port_name = config.ReadString(section, "PORT_NAME", Port_name);
                Baud_rate = config.ReadInteger(section, "BAUD_RATE", Baud_rate);
                Parity_bits = config.ReadInteger(section, "PARITY_BITS", Parity_bits);
                Data_bits = config.ReadInteger(section, "DATA_BITS", Data_bits);
                Stop_bits = config.ReadInteger(section, "STOP_BITS", Stop_bits);

                config.ReleaseMutex();
            }

            return true;
        }

        public void SaveConfig()
        {
            if (config.LockMutex())
            {
                // config.Writexxxx(섹션, 키, 기본값)
                config.WriteBool(section, "USED", used);

                config.WriteString(section, "PORT_NAME", Port_name);
                config.WriteInteger(section, "BAUD_RATE", Baud_rate);
                config.WriteInteger(section, "PARITY_BITS", Parity_bits);
                config.WriteInteger(section, "DATA_BITS", Data_bits);
                config.WriteInteger(section, "STOP_BITS", Stop_bits);

                config.LastReadTime = config.LastWriteTime;
                config.ReleaseMutex();
            }

            return;
        }
    }
}
