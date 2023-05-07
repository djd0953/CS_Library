using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class EW_LOGGER_T : IDisposable
    {
        protected TCP_CLIENT client = new TCP_CLIENT();
        string ip;
        string port;

        public byte[] tx_buff = new byte[0];
        public byte[] rx_buff = new byte[0];

        private bool disposedValue;

        public WB_DATA_TYPE type = WB_DATA_TYPE.NONE;

        public void Connect(string ip, string port)
        {
            this.ip = ip;
            this.port = port;

            Connect();
        }

        public void Connect()
        {
            client.Connect(ip, int.Parse(port));
        }

        public void DisConnect()
        {
            client.DisConnect();
        }

        public DateTime GetTime()
        {
            EW_SEND_VO vo = new EW_SEND_VO("54A1");// T A1
            DateTime rtv;

            // SEND
            try
            {
                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                rx_buff = new byte[17];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    try
                    {
                        string temp = $"{rx_buff[7]:X2}{rx_buff[8]:X2}-{rx_buff[9]:X2}-{rx_buff[10]:X2} {rx_buff[11]:X2}:{rx_buff[12]:X2}:{rx_buff[13]:X2}";
                        rtv = Convert.ToDateTime(temp);
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public DateTime SetTime(DateTime datetime)
        {
            EW_SEND_VO vo = new EW_SEND_VO("54A2"); // T A2
            DateTime rtv;

            // SEND
            try
            {
                vo.Data = new byte[7];
                vo.Data[0] = (byte)Convert.ToInt32(datetime.Year.ToString().Substring(0, 2), 16);
                vo.Data[1] = (byte)Convert.ToInt32(datetime.Year.ToString().Substring(2, 2), 16);
                vo.Data[2] = (byte)Convert.ToInt32(datetime.Month.ToString(), 16);
                vo.Data[3] = (byte)Convert.ToInt32(datetime.Day.ToString(), 16);
                vo.Data[4] = (byte)Convert.ToInt32(datetime.Hour.ToString(), 16);
                vo.Data[5] = (byte)Convert.ToInt32(datetime.Minute.ToString(), 16);
                vo.Data[6] = (byte)Convert.ToInt32(datetime.Second.ToString(), 16);

                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                rx_buff = new byte[17];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    try
                    {
                        string temp = $"{rx_buff[7]:X2}{rx_buff[8]:X2}-{rx_buff[9]:X2}-{rx_buff[10]:X2} {rx_buff[11]:X2}:{rx_buff[12]:X2}:{rx_buff[13]:X2}";
                        rtv = Convert.ToDateTime(temp);
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public void Reboot()
        {
            EW_SEND_VO vo = new EW_SEND_VO("54A3"); // T A3

            // SEND
            try
            {
                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                rx_buff = new byte[10];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    ;
                }
                else
                {
                    throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return;
        }

        public DateTime GetUptime()
        {
            EW_SEND_VO vo = new EW_SEND_VO("54A5"); // T A5
            DateTime rtv;

            // SEND
            try
            {
                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                rx_buff = new byte[18];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    try
                    {
                        string temp = $"{rx_buff[8]:X2}{rx_buff[9]:X2}-{rx_buff[10]:X2}-{rx_buff[11]:X2} {rx_buff[12]:X2}:{rx_buff[13]:X2}:{rx_buff[14]:X2}";
                        rtv = Convert.ToDateTime(temp);
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public byte GetLoggerType()
        {
            EW_SEND_VO vo = new EW_SEND_VO("54A7"); // T A7
            byte rtv;

            // SEND
            try
            {
                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                rx_buff = new byte[11];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    try
                    {
                        rtv = rx_buff[7];
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public byte SetLoggerType(byte logger_type)
        {
            EW_SEND_VO vo = new EW_SEND_VO("54A8"); // T A8
            byte rtv;

            // SEND
            try
            {
                vo.Data = new byte[1];
                vo.Data[0] = logger_type;

                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                rx_buff = new byte[11];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length -1] == 0xFE)
                {
                    try
                    {
                        rtv = rx_buff[7];
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public byte[] GetEEPBank()
        {
            EW_SEND_VO vo = new EW_SEND_VO("54B1"); // T B1
            byte[] rtv = new byte[8];

            // SEND
            try
            {
                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                rx_buff = new byte[18];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    try
                    {
                        rtv[0] = rx_buff[7];
                        rtv[1] = rx_buff[8];
                        rtv[2] = rx_buff[9];
                        rtv[3] = rx_buff[10];
                        rtv[4] = rx_buff[11];
                        rtv[5] = rx_buff[12];
                        rtv[6] = rx_buff[13];
                        rtv[7] = rx_buff[14];
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public byte[] SetEEPBank(byte[] bank)
        {
            EW_SEND_VO vo = new EW_SEND_VO("54B2"); // T B1
            byte[] rtv = new byte[8];

            // SEND
            try
            {
                vo.Data = new byte[8];
                vo.Data[0] = bank[0];
                vo.Data[1] = bank[1];
                vo.Data[2] = bank[2];
                vo.Data[3] = bank[3];
                vo.Data[4] = bank[4];
                vo.Data[5] = bank[5];
                vo.Data[6] = bank[6];
                vo.Data[7] = bank[7];

                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                rx_buff = new byte[18];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    try
                    {
                        rtv[0] = rx_buff[7];
                        rtv[1] = rx_buff[8];
                        rtv[2] = rx_buff[9];
                        rtv[3] = rx_buff[10];
                        rtv[4] = rx_buff[11];
                        rtv[5] = rx_buff[12];
                        rtv[6] = rx_buff[13];
                        rtv[7] = rx_buff[14];
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public string Firmware_VERSION()
        {
            EW_SEND_VO vo = new EW_SEND_VO("54F1"); // T F1
            string rtv = "";

            // SEND
            try
            {
                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                rx_buff = new byte[18];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    try
                    {
                        rtv = Encoding.Default.GetString(rx_buff, 7, 8);
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public bool Firmware_START()
        {
            EW_SEND_VO vo = new EW_SEND_VO("54F2"); // T F2
            bool rtv = true;

            // SEND
            try
            {
                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                rx_buff = new byte[10];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {

                }
                else
                {
                    throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public int Firmware_WRITE(int length, int offset, int size, byte[] data)
        {
            EW_SEND_VO vo = new EW_SEND_VO("54F3"); // T F3
            int rtv;

            // SEND
            try
            {
                // Total Length
                vo.Data = new byte[4 + 4 + 4 + size];
                vo.Data[0] = (byte) ((length >> 24) & 0xFF);
                vo.Data[1] = (byte) ((length >> 16) & 0xFF);
                vo.Data[2] = (byte) ((length >> 8) & 0xFF);
                vo.Data[3] = (byte) ((length >> 0) & 0xFF);
                
                // OFFSET
                vo.Data[4] = (byte)((offset >> 24) & 0xFF);
                vo.Data[5] = (byte)((offset >> 16) & 0xFF);
                vo.Data[6] = (byte)((offset >> 8) & 0xFF);
                vo.Data[7] = (byte)((offset >> 0) & 0xFF);

                // SIZE
                vo.Data[8]  = (byte)((size >> 24) & 0xFF);
                vo.Data[9]  = (byte)((size >> 16) & 0xFF);
                vo.Data[10] = (byte)((size >> 8) & 0xFF);
                vo.Data[11] = (byte)((size >> 0) & 0xFF);

                // DATA
                Array.Copy(data, 0, vo.Data, 12, size);

                for (int i = 0; i < size; i++)
                {
                    //vo.Data[12 + i] = (byte)i;
                }

                tx_buff = vo.GetRaw();
                rtv = client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                rx_buff = new byte[22];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    int rx_offset = rx_buff[11] << 24 | rx_buff[12] << 16 | rx_buff[13] << 8 | rx_buff[14] << 0;
                    if (offset != rx_offset)
                    {
                        throw new Exception("파일 오프셋 오류");
                    }
                }
                else
                {
                    throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public bool Firmware_END()
        {
            EW_SEND_VO vo = new EW_SEND_VO("54F4"); // T F4
            bool rtv = true;

            // SEND
            try
            {
                tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                rx_buff = new byte[10];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {

                }
                else
                {
                    //throw new Exception("프로토콜 오류");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리형 상태(관리형 개체)를 삭제합니다.
                    client.Dispose();
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
                client = null;
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        ~EW_LOGGER_T()
        {
             // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
            Dispose(disposing: false);
        }

        void IDisposable.Dispose()
        {
            // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    // =====================================================================================================
    // | HEADER  |   VERSION    | COMMAND |                        DATA                     | CRC| FOOTER  |
    // =====================================================================================================
    // |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |
    // | FF | FA | 44 | 4C | 10 |    |    |    |    |    |    |    |    |    |    |    |    | 00 | FF | FE |
    // =====================================================================================================
    public class EW_SEND_VO
    {
        private byte[] header = new byte[2];
        public string Header
        {
            get
            {
                return string.Format("{0:X2}{1:X2}", header[0], header[1]);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                string temp2 = value.Substring(2, 2);

                header[0] = Convert.ToByte(temp1, 16);
                header[1] = Convert.ToByte(temp2, 16);
            }
        }

        private byte[] version = new byte[3];
        public string Version
        {
            get
            {
                return string.Format("{0:X2}{1:X2}{2:X2}", version[0], version[1], version[2]);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                string temp2 = value.Substring(2, 2);
                string temp3 = value.Substring(4, 2);

                version[0] = Convert.ToByte(temp1, 16);
                version[1] = Convert.ToByte(temp2, 16);
                version[2] = Convert.ToByte(temp3, 16);
            }
        }

        private byte[] command = new byte[2];
        public string Command
        {
            get
            {
                return string.Format("{0:X2}{1:X2}", command[0], command[1]);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                string temp2 = value.Substring(2, 2);

                command[0] = Convert.ToByte(temp1, 16);
                command[1] = Convert.ToByte(temp2, 16);
            }
        }

        private byte[] data = new byte[0];
        public byte[] Data
        {
            get
            {
                return data;
            }
            set
            {
                Array.Resize(ref data, value.Length);
                Array.Copy(value, data, value.Length);
            }
        }

        private byte[] crc = new byte[1];
        public string Crc
        {
            get
            {
                return string.Format("{0:X2}", crc[0]);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                crc[0] = Convert.ToByte(temp1, 16);
            }
        }

        private byte[] footer = new byte[2];
        public string Footer
        {
            get
            {
                return string.Format("{0:X2}{1:X2}", footer[0], footer[1]);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                string temp2 = value.Substring(2, 2);

                footer[0] = Convert.ToByte(temp1, 16);
                footer[1] = Convert.ToByte(temp2, 16);
            }
        }

        public EW_SEND_VO()
        {
            Header = "FFFA";
            Version = "444C10";

            Crc = "00";
            Footer = "FFFE";
        }

        public EW_SEND_VO(string cmd)
        {
            Header = "FFFA";
            Version = "444C10";

            Command = cmd;

            Crc = "00";
            Footer = "FFFE";
        }

        public byte[] GetRaw()
        {
            byte[] temp = new byte[header.Length + version.Length + command.Length + data.Length + crc.Length + footer.Length];
            int offset = 0;

            Array.Copy(header, 0, temp, 0, 2);
            offset += header.Length;

            Array.Copy(version, 0, temp, offset, version.Length);
            offset += version.Length;

            Array.Copy(command, 0, temp, offset, command.Length);
            offset += command.Length;

            Array.Copy(data, 0, temp, offset, data.Length);
            offset += data.Length;

            Array.Copy(crc, 0, temp, offset, crc.Length);
            offset += crc.Length;

            Array.Copy(footer, 0, temp, offset, footer.Length);
            offset += footer.Length;

            return temp;
        }
    }
}
