using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class CB_LOGGER_T : IDisposable
    {
        TCP_CLIENT tcp_client = new TCP_CLIENT();
        string ip;
        string port;

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
            tcp_client.Connect(ip, int.Parse(port));
        }

        public void DisConnect()
        {
            tcp_client.DisConnect();
        }

        public DateTime GetTime()
        {
            CB_SEND_VO send_vo = new CB_SEND_VO("WGT");

            // SEND
            byte[] tx_buff = send_vo.GetRaw();
            tcp_client.Send(tx_buff, tx_buff.Length);
#if DEBUG
            //Console.WriteLine(BitConverter.ToString(send_vo.Raw).Replace('-', ' '));
#endif

            // RECV
            CB_RETURN_VO recv_vo = new CB_RETURN_VO();

            tcp_client.Recv(recv_vo.Raw, recv_vo.Raw.Length);

            int year  = recv_vo.Data[ 0] << 8 | recv_vo.Data[ 1] << 0;
            int month = recv_vo.Data[ 2] << 8 | recv_vo.Data[ 3] << 0;
            int day   = recv_vo.Data[ 4] << 8 | recv_vo.Data[ 5] << 0;
            int hour  = recv_vo.Data[ 6] << 8 | recv_vo.Data[ 7] << 0;
            int min   = recv_vo.Data[ 8] << 8 | recv_vo.Data[ 9] << 0;
            int sec   = recv_vo.Data[10] << 8 | recv_vo.Data[11] << 0;

#if DEBUG
            Console.WriteLine("year: ", year);
            Console.WriteLine("month: ", month);
            Console.WriteLine("day: ", day);
            Console.WriteLine("hour: ", hour);
            Console.WriteLine("min: ", min);
            Console.WriteLine("sefc: ", sec);
#endif

            return new DateTime(year, month, day, hour, min, sec);
        }

        /// <summary>
        /// 우보 로거 시작일 출력
        /// </summary>
        public void SendWGI()
        {
            CB_SEND_VO vo = new CB_SEND_VO();
            vo.Header = "FAFB";
            vo.Version = "120101";
            vo.Time = DateTime.Now;
            vo.Command = "WGI";
            vo.Footer = "FFFE";

            byte[] buff = vo.GetRaw();
#if DEBUG
            Console.WriteLine(BitConverter.ToString(buff).Replace('-', ' '));
#endif
            tcp_client.Send(buff, buff.Length);
        }

        public DateTime RecvWGI()
        {
            CB_RETURN_VO vo = new CB_RETURN_VO();

            tcp_client.Recv(vo.Raw, vo.Raw.Length);
#if DEBUG
            Console.WriteLine(BitConverter.ToString(vo.Raw).Replace('-', ' '));
#endif

            int year = vo.Data[0] * 256 + vo.Data[1];
            int month = vo.Data[2] * 256 + vo.Data[3];
            int day = vo.Data[4] * 256 + vo.Data[5];
            int hour = vo.Data[6] * 256 + vo.Data[7];
            int min = vo.Data[8] * 256 + vo.Data[9];
            int sec = vo.Data[10] * 256 + vo.Data[11];
#if DEBUG
            Console.WriteLine("year: ", year);
            Console.WriteLine("month: ", month);
            Console.WriteLine("day: ", day);
            Console.WriteLine("hour: ", hour);
            Console.WriteLine("min: ", min);
            Console.WriteLine("sec: ", sec);
#endif

            return new DateTime(year + 2000, month, day, hour, min, sec);
        }

        /// <summary>
        /// 우보 로거 리셋
        /// </summary>
        public void SendWGR()
        {
            CB_SEND_VO vo = new CB_SEND_VO();
            vo.Header = "FAFB";
            vo.Version = "120101";
            vo.Command = "WGR";
            vo.Footer = "FFFE";

            byte[] buff = vo.GetRaw();
#if DEBUG
            Console.WriteLine(BitConverter.ToString(buff).Replace('-', ' '));
#endif
            tcp_client.Send(buff, buff.Length);
        }

        /// <summary>
        /// 우보 로거 리셋 응답
        /// </summary>
        public void RecvWGR()
        {
            CB_RETURN_VO vo = new CB_RETURN_VO();

            tcp_client.Recv(vo.Raw, vo.Raw.Length);
#if DEBUG
            Console.WriteLine(BitConverter.ToString(vo.Raw).Replace('-', ' '));
#endif
        }

        /*
        /// <summary>
        /// 우보 로거 시간 정보 요청
        /// </summary>
        public void SendWGT()
        {
            CB_SEND_VO vo = new CB_SEND_VO();
            vo.Header = "FAFB"; ;
            vo.Version = "120101";
            vo.Command = "WGT";
            vo.Footer = "FFFE";

            byte[] buff = vo.GetRaw();
#if DEBUG
            Console.WriteLine(BitConverter.ToString(buff).Replace('-', ' '));
#endif
            tcp_client.Send(buff, buff.Length);
        }

        /// <summary>
        /// 우보 로거 시간 정보 응답
        /// </summary>
        /// <returns></returns>
        public DateTime RecvWGT()
        {
            CB_RETURN_VO vo = new CB_RETURN_VO();

            tcp_client.Recv(vo.Raw, vo.Raw.Length);
#if DEBUG
            Console.WriteLine(BitConverter.ToString(vo.Raw).Replace('-', ' '));
#endif

            int year = vo.Data[0] * 256 + vo.Data[1];
            int month = vo.Data[2] * 256 + vo.Data[3];
            int day = vo.Data[4] * 256 + vo.Data[5];
            int hour = vo.Data[6] * 256 + vo.Data[7];
            int min = vo.Data[8] * 256 + vo.Data[9];
            int sec = vo.Data[10] * 256 + vo.Data[11];

#if DEBUG
            Console.WriteLine("year: ", year);
            Console.WriteLine("month: ", month);
            Console.WriteLine("day: ", day);
            Console.WriteLine("hour: ", hour);
            Console.WriteLine("min: ", min);
            Console.WriteLine("sefc: ", sec);
#endif

            return new DateTime(year, month, day, hour, min, sec);
        }

        */

        public void SendWST(DateTime datetime)
        {
            CB_SEND_VO vo = new CB_SEND_VO();
            vo.Header = "FAFB"; ;
            vo.Version = "120101";
            vo.Command = "WST";
            vo.Time = datetime;
            vo.Footer = "FFFE";

            byte[] buff = vo.GetRaw();
#if DEBUG
            Console.WriteLine(BitConverter.ToString(buff).Replace('-', ' '));
#endif
            tcp_client.Send(buff, buff.Length);
        }

        public void RecvWST()
        {
            CB_RETURN_VO vo = new CB_RETURN_VO();

            tcp_client.Recv(vo.Raw, vo.Raw.Length);
#if DEBUG
            Console.WriteLine(BitConverter.ToString(vo.Raw).Replace('-', ' '));
#endif
            return;
        }

        public void SendWGC()
        {
            CB_SEND_VO vo = new CB_SEND_VO();
            vo.Header = "FAFB"; ;
            vo.Version = "120101";
            vo.Command = "WGC";
            vo.Footer = "FFFE";

            byte[] buff = vo.GetRaw();
#if DEBUG
            Console.WriteLine(BitConverter.ToString(buff).Replace('-', ' '));
#endif
            tcp_client.Send(buff, buff.Length);
        }

        public void RecvWGC()
        {
            CB_RETURN_VO vo = new CB_RETURN_VO();

            tcp_client.Recv(vo.Raw, vo.Raw.Length);
#if DEBUG
            Console.WriteLine(BitConverter.ToString(vo.Raw).Replace('-', ' '));
#endif
            return;
        }

        public void SendWSC()
        {
            CB_SEND_VO vo = new CB_SEND_VO();
            vo.Header = "FAFB"; ;
            vo.Version = "120101";
            vo.Command = "WSC";
            vo.Footer = "FFFE";

            byte[] buff = vo.GetRaw();
#if DEBUG
            Console.WriteLine(BitConverter.ToString(buff).Replace('-', ' '));
#endif
            tcp_client.Send(buff, buff.Length);
        }

        public void RecvWSC()
        {
            CB_RETURN_VO vo = new CB_RETURN_VO();

            tcp_client.Recv(vo.Raw, vo.Raw.Length);
#if DEBUG
            Console.WriteLine(BitConverter.ToString(vo.Raw).Replace('-', ' '));
#endif
            return;
        }

        public void SendWGS(string type)
        {
            CB_SEND_VO vo = new CB_SEND_VO();
            vo.Header = "FAFB"; ;
            vo.Version = "120101";
            vo.Command = "WGS";
            //vo.Command_type = type;
            vo.Footer = "FFFE";

            byte[] buff = vo.GetRaw();
#if DEBUG
            Console.WriteLine(BitConverter.ToString(buff).Replace('-', ' '));
#endif
            tcp_client.Send(buff, buff.Length);
        }

        public void RecvWGS()
        {
            CB_RETURN_VO vo = new CB_RETURN_VO();

            tcp_client.Recv(vo.Raw, vo.Raw.Length);
#if DEBUG
            Console.WriteLine(BitConverter.ToString(vo.Raw).Replace('-', ' '));
#endif

#if DEBUG
            Console.WriteLine("D1: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 0)));
            Console.WriteLine("D2: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 2)));
            Console.WriteLine("D3: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 4)));
            Console.WriteLine("D4: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 6)));
            Console.WriteLine("D5: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 8)));
            Console.WriteLine("D6: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 10)));
            Console.WriteLine("D7: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 12)));
#endif
            return;
        }

        public void SendWCD(string type, DateTime datetime)
        {
            CB_SEND_VO vo = new CB_SEND_VO();
            vo.Header = "FAFB"; ;
            vo.Version = "120101";
            vo.Command = "WCD";
            //vo.Command_type = type;
            vo.Time = datetime;
            vo.Footer = "FFFE";

            byte[] buff = vo.GetRaw();
#if DEBUG
            Console.WriteLine(BitConverter.ToString(buff).Replace('-', ' '));
#endif
            tcp_client.Send(buff, buff.Length);
        }

        public UInt16 RecvWCD()
        {
            CB_RETURN_VO vo = new CB_RETURN_VO();

            tcp_client.Recv(vo.Raw, vo.Raw.Length);
#if DEBUG
            Console.WriteLine(BitConverter.ToString(vo.Raw).Replace('-', ' '));
#endif

#if DEBUG
            Console.WriteLine("D1: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 0)));
            Console.WriteLine("D2: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 2)));
            Console.WriteLine("D3: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 4)));
            Console.WriteLine("D4: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 6)));
            Console.WriteLine("D5: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 8)));
            Console.WriteLine("D6: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 10)));
            Console.WriteLine("D7: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 12)));
#endif

            return BitConverter.ToUInt16(ENDIAN.Swap(vo.Data, 0, 2), 0);
        }

        public void SendWGM(string type, DateTime time)
        {
            CB_SEND_VO vo = new CB_SEND_VO();
            vo.Header = "FAFB"; ;
            vo.Version = "120101";
            vo.Time = time;
            vo.Command = "WGM";
            //vo.Command_type = type;
            vo.Footer = "FFFE";

            byte[] buff = vo.GetRaw();
#if DEBUG
            Console.WriteLine(BitConverter.ToString(buff).Replace('-', ' '));
#endif
            tcp_client.Send(buff, buff.Length);
        }

        public void RecvWGM()
        {
            CB_RETURN_VO vo = new CB_RETURN_VO();

            tcp_client.Recv(vo.Raw, vo.Raw.Length);
#if DEBUG
            Console.WriteLine(BitConverter.ToString(vo.Raw).Replace('-', ' '));
#endif

#if DEBUG
            Console.WriteLine("D1: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 0)));
#endif
        }

        public void SendWGH(string type, DateTime time)
        {
            CB_SEND_VO vo = new CB_SEND_VO();
            vo.Header = "FAFB"; ;
            vo.Version = "120101";
            vo.Time = time;
            vo.Command = "WGH";
            //vo.Command_type = type;
            vo.Footer = "FFFE";

            byte[] buff = vo.GetRaw();
#if DEBUG
            Console.WriteLine(BitConverter.ToString(buff).Replace('-', ' '));
#endif
            tcp_client.Send(buff, buff.Length);
        }

        public void RecvWGH()
        {
            CB_RETURN_VO vo = new CB_RETURN_VO();

            tcp_client.Recv(vo.Raw, vo.Raw.Length);
#if DEBUG
            Console.WriteLine(BitConverter.ToString(vo.Raw).Replace('-', ' '));
#endif

#if DEBUG
            Console.WriteLine("D1: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 0)));
            Console.WriteLine("D2: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 2)));
            Console.WriteLine("D3: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 4)));
            Console.WriteLine("D4: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 6)));
            Console.WriteLine("D5: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 8)));
            Console.WriteLine("D6: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 10)));
            Console.WriteLine("D7: {0}", ENDIAN.Swap(BitConverter.ToInt16(vo.Data, 12)));
#endif
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리형 상태(관리형 개체)를 삭제합니다.
                    tcp_client.DisConnect();
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        // ~CB_LOGGER_T()
        // {
        //     // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public class CB_SEND_VO
    {
        /*
        public const int MAX_LENGTH = 41;

        public byte[] Raw = new byte[MAX_LENGTH];


        public string Command_str
        {
            get
            {
                return string.Format("{0:C}{1:C}{2:C}", Raw[15], Raw[16], Raw[17]);
            }
            set
            {
                char[] temp = value.ToCharArray(0, 3);

                Raw[15] = (byte)temp[0];
                Raw[16] = (byte)temp[1];
                Raw[17] = (byte)temp[2];
            }
        }

        public string Command_type
        {
            get
            {
                return string.Format("{0}", Raw[19]);

            }
            set
            {
                char[] temp = value.ToCharArray(0, 1);

                Raw[19] = (byte)temp[0];
            }
        }
        */
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


        private byte[] time = new byte[6];
        public DateTime Time
        {
            get
            {
                return new DateTime(2000 + time[0], time[1], time[2], time[3], time[4], time[5]);
            }
            set
            {
                time[0] = (byte)(value.Year - 2000);
                time[1] = (byte)value.Month;
                time[2] = (byte)value.Day;
                time[3] = (byte)value.Hour;
                time[4] = (byte)value.Minute;
                time[5] = (byte)value.Second;
            }
        }

        private byte[] password = new byte[2];
        public string Password
        {
            get
            {
                return string.Format("{0:X2}{1:X2}", password[0], password[1]);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                string temp2 = value.Substring(2, 2);

                password[0] = Convert.ToByte(temp1, 16);
                password[1] = Convert.ToByte(temp2, 16);
            }
        }

        private byte[] id = new byte[2];
        public string Id
        {
            get
            {
                return string.Format("{0:X2}{1:X2}", id[0], id[1]);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                string temp2 = value.Substring(2, 2);

                id[0] = Convert.ToByte(temp1, 16);
                id[1] = Convert.ToByte(temp2, 16);
            }
        }

        private byte[] command = new byte[3];
        public string Command
        {
            get
            {
                return string.Format("{0:X2}{1:X2}{2:X2}", command[0], command[1], command[2]);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                string temp2 = value.Substring(2, 2);
                string temp3 = value.Substring(4, 2);

                command[0] = Convert.ToByte(temp1, 16);
                command[1] = Convert.ToByte(temp2, 16);
                command[2] = Convert.ToByte(temp3, 16);
            }
        }

        private byte[] data = new byte[0];
        public byte[] Data
        {
            get
            {
                return data;
                //byte[] temp = new byte[12];
                //Array.Copy(Raw, 25, temp, 0, temp.Length);
                //
                //return temp;
            }
            set
            {
                Array.Resize(ref data, value.Length);
                Array.Copy(value, data, value.Length);
                //Array.Copy(value, 0, Raw, 25, 12);
            }
        }

        private byte[] crc = new byte[1];
        public string Crc
        {
            get
            {
                return string.Format("{0:X2}", crc[0]);
                // BitConverter.ToUInt16(Raw, 37);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                crc[0] = Convert.ToByte(temp1, 16);
                //Array.Copy(BitConverter.GetBytes(value), 0, Raw, 37, 2);
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

        public CB_SEND_VO()
        {
            Header = "FAFB";
            Version = "120101";
            
            Password = "0000";

            Crc = "00";
            Footer = "FFFE";
        }

        public CB_SEND_VO(string cmd)
        {
            Header = "FAFB";
            Version = "120101";

            Password = "0000";
            
            Command = cmd;

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

    public class CB_RETURN_VO
    {
        public const int MAX_LENGTH = 30;

        public byte[] Raw = new byte[MAX_LENGTH];

        public string Header
        {
            get
            {
                return string.Format("{0:X2}{1:X2}", Raw[0], Raw[1]);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                string temp2 = value.Substring(2, 2);

                Raw[0] = Convert.ToByte(temp1, 16);
                Raw[1] = Convert.ToByte(temp2, 16);
            }
        }

        public string Version
        {
            get
            {
                return string.Format("{0:X2}{1:X2}{2:X2}", Raw[2], Raw[3], Raw[4]);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                string temp2 = value.Substring(2, 2);
                string temp3 = value.Substring(4, 2);

                Raw[2] = Convert.ToByte(temp1, 16);
                Raw[3] = Convert.ToByte(temp2, 16);
                Raw[4] = Convert.ToByte(temp3, 16);
            }
        }

        public string Id
        {
            get
            {
                return string.Format("{0:X2}{1:X2}", Raw[5], Raw[6]);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                string temp2 = value.Substring(2, 2);

                Raw[5] = Convert.ToByte(temp1, 16);
                Raw[6] = Convert.ToByte(temp2, 16);
            }
        }

        public string Data_type
        {
            get
            {
                return string.Format("{0}", Raw[7]);

            }
            set
            {
                char[] temp = value.ToCharArray(0, 1);

                Raw[7] = (byte)temp[0];
            }
        }

        public string Data_result
        {
            get
            {
                return string.Format("{0:C}{1:C}{2:C}{3:C}", Raw[8], Raw[9], Raw[10], Raw[11]);
            }
            set
            {
                char[] temp = value.ToCharArray(0, 4);

                Raw[8] = (byte)temp[0];
                Raw[9] = (byte)temp[1];
                Raw[10] = (byte)temp[2];
                Raw[11] = (byte)temp[3];
            }
        }

        public byte[] Data
        {
            get
            {
                byte[] temp = new byte[14];
                Array.Copy(Raw, 12, temp, 0, 14);

                return temp;
            }
            set
            {
                Array.Copy(value, 0, Raw, 12, 14);
            }
        }

        public UInt16 Crc
        {
            get
            {
                return BitConverter.ToUInt16(Raw, 26);
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, Raw, 26, 2);
            }
        }

        public string End
        {
            get
            {
                return string.Format("{0:X2}{1:X2}", Raw[28], Raw[29]);
            }
            set
            {
                string temp1 = value.Substring(0, 2);
                string temp2 = value.Substring(2, 2);

                Raw[28] = Convert.ToByte(temp1, 16);
                Raw[29] = Convert.ToByte(temp2, 16);
            }
        }

        public void Clear()
        {
            Array.Clear(Raw, 0, MAX_LENGTH);
        }
    }
    

}
