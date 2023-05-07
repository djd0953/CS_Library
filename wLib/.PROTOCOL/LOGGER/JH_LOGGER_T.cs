using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class JH_LOGGER_T : IDisposable
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
            JH_SEND_VO vo = new JH_SEND_VO("54A1"); // T A1
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
                rx_buff = new byte[16];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    try
                    {
                        string temp = $"{rx_buff[6]:X2}{rx_buff[7]:X2}-{rx_buff[8]:X2}-{rx_buff[9]:X2} {rx_buff[10]:X2}:{rx_buff[11]:X2}:{rx_buff[12]:X2}";
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
            JH_SEND_VO vo = new JH_SEND_VO("54A2"); // T A2
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

                byte[] tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                byte[] rx_buff = new byte[16];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    try
                    {
                        string temp = $"{rx_buff[6]:X2}{rx_buff[7]:X2}-{rx_buff[8]:X2}-{rx_buff[9]:X2} {rx_buff[10]:X2}:{rx_buff[11]:X2}:{rx_buff[12]:X2}";
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

        public List<FLOOD_DATA_VO> GetData_now()
        {
            JH_SEND_VO vo = new JH_SEND_VO("44A1"); // D A1
            List<FLOOD_DATA_VO> rtv = new List<FLOOD_DATA_VO>();
            
            try
            {
                // SEND
                byte[] tx_buff = vo.GetRaw();
                client.Send(tx_buff);
            }
            catch
            {
                throw;
            }

            // RECV
            try
            {
                byte[] rx_buff = new byte[19];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0xFF && rx_buff[1] == 0xFA && rx_buff[rx_buff.Length - 2] == 0xFF && rx_buff[rx_buff.Length - 1] == 0xFE)
                {
                    try
                    {
                        FLOOD_DATA_VO data = new FLOOD_DATA_VO();
                        data.floodData = string.Format("{0}{1}{2}", (char)rx_buff[7], (char)rx_buff[8], (char)rx_buff[9]);
                        data.waterData = int.Parse(string.Format("{0}{1}{2}{3}{4}", (char)rx_buff[11], (char)rx_buff[12], (char)rx_buff[13], (char)rx_buff[14], (char)rx_buff[15])).ToString();

                        rtv.Add(data);
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

        /// <summary>
        /// 최근값 요청
        /// </summary>
        public void SendDA1()
        {
            JH_SEND_VO vo = new JH_SEND_VO();
            vo.Header = "FFFA";
            vo.Version = "4633";
            vo.Command = "44A1";
            vo.Crc = "00";
            vo.Footer = "FFFE";

            byte[] buff = vo.GetRaw();
#if DEBUG
            Console.WriteLine(BitConverter.ToString(buff).Replace('-', ' '));
#endif
            client.Send(buff, buff.Length);
        }

        public FLOOD_DATA_VO RecvDA1()
        {
            FLOOD_DATA_VO rtv = new FLOOD_DATA_VO();

            byte[] buff = new byte[19];

            client.Recv(buff, buff.Length);
#if DEBUG
            Console.WriteLine(BitConverter.ToString(buff).Replace('-', ' '));
#endif

            if (buff[0] == 0xFF && buff[1] == 0xFA && buff[17] == 0xFF && buff[18] == 0xFE)
            {
                rtv.floodData = string.Format("{0}{1}{2}", (char)buff[7], (char)buff[8], (char)buff[9]);
                rtv.waterData = int.Parse(string.Format("{0}{1}{2}{3}{4}", (char)buff[11], (char)buff[12], (char)buff[13], (char)buff[14], (char)buff[15])).ToString();
            }
            else
            {
                throw new Exception("프로토콜 오류");
            }
#if DEBUG
            Console.WriteLine("FLOOD_DATA: {0}", rtv.floodData);
            Console.WriteLine("WATER_DATA: {0}", rtv.waterData);
#endif
            return rtv;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리형 상태(관리형 개체)를 삭제합니다.
                    client?.Dispose();
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        // ~EW_LOGGER_T()
        // {
        //     // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    // =====================================================================================================
    // | HEADER  | VERSION | COMMAND |                          DATA                        | CRC| FOOTER  |
    // =====================================================================================================
    // |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |    |
    // | FF | FA | 46 | 33 |    |    |    |    |    |    |    |    |    |    |    |    |    | 00 | FF | FE |
    // =====================================================================================================

    public class JH_SEND_VO
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

        private byte[] version = new byte[2];
        public string Version
        {
            get
            {
                return string.Format("{0:X2}{1:X2}", version[0], version[1]);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                string temp2 = value.Substring(2, 2);

                version[0] = Convert.ToByte(temp1, 16);
                version[1] = Convert.ToByte(temp2, 16);
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

        public JH_SEND_VO()
        {
            Header = "FFFA";
            Version = "4633";

            Crc = "00";
            Footer = "FFFE";
        }

        public JH_SEND_VO(string cmd)
        {
            Header = "FFFA";
            Version = "4633";
            
            Command = cmd;

            Crc = "00";
            Footer = "FFFE";
        }

        public byte[] GetRaw()
        {
            byte[] temp = new byte[header.Length + version.Length + command.Length + data.Length + crc.Length + footer.Length];
            int offset = 0;

            Array.Copy(header, 0, temp, 0, 2);

            offset = header.Length;
            Array.Copy(version, 0, temp, offset, version.Length);

            offset = header.Length + version.Length;
            Array.Copy(command, 0, temp, offset, command.Length);

            offset = header.Length + version.Length + command.Length;
            Array.Copy(data, 0, temp, offset, data.Length);

            offset = header.Length + version.Length + command.Length + data.Length;
            Array.Copy(crc, 0, temp, offset, crc.Length);

            offset = header.Length + version.Length + command.Length + data.Length + crc.Length;
            Array.Copy(footer, 0, temp, offset, footer.Length);

            return temp;
        }
    }

    public class JH_RETURN_VO
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

        private byte[] version = new byte[2];
        public string Version
        {
            get
            {
                return string.Format("{0:X2}{1:X2}", version[0], version[1]);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                string temp2 = value.Substring(2, 2);

                version[0] = Convert.ToByte(temp1, 16);
                version[1] = Convert.ToByte(temp2, 16);
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

        public byte[] GetRaw()
        {
            byte[] temp = new byte[header.Length + version.Length + command.Length + data.Length + crc.Length + footer.Length];
            int offset;

            offset = 0;
            Array.Copy(header, 0, temp, 0, 2);

            offset = header.Length;
            Array.Copy(version, 0, temp, offset, version.Length);

            offset = header.Length + version.Length;
            Array.Copy(command, 0, temp, offset, command.Length);

            offset = header.Length + version.Length + command.Length;
            Array.Copy(data, 0, temp, offset, data.Length);

            offset = header.Length + version.Length + command.Length + data.Length;
            Array.Copy(crc, 0, temp, offset, crc.Length);

            offset = header.Length + version.Length + command.Length + data.Length + crc.Length;
            Array.Copy(footer, 0, temp, offset, footer.Length);

            return temp;
        }
    }

    public class FLOOD_DATA_VO
    {
        public string floodData;
        public string waterData;

    }
}
