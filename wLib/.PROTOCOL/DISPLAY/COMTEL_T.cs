using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class EWDPL_T : IDisposable
    {
        protected TCP_CLIENT client = new TCP_CLIENT();
        string ip;
        string port;

        public byte[] tx_buff = new byte[0];
        public byte[] rx_buff = new byte[0];

        private bool disposedValue;

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

        /// <summary>
        /// 전원 상태조회 명령 0x01
        /// </summary>
        public void GetStatus_Power()
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("01");

            //SEND
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

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {
                        string temp = "";

                    }
                    catch
                    {
                        throw new Exception("프로토콜 오류");
                    }
                }

            }
            catch
            {

            }
        }

        /// <summary>
        /// 밝기설명 명령 0x08
        /// </summary>
        public void SetBrightness(byte[] data)
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("08");

            // SEND
            try
            {
                vo.Body = new byte[24];
                Array.Copy(data, vo.Body, 24);

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
                rx_buff = new byte[31];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {

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
        }

        /// <summary>
        /// 시각정보 요청 0x51
        /// </summary>
        public DateTime GetTime()
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("51");
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
                rx_buff = new byte[13];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {
                        string temp = $"20{rx_buff[5]:D2}-{rx_buff[6]:D2}-{rx_buff[7]:D2} {rx_buff[8]:D2}:{rx_buff[9]:D2}:{rx_buff[10]:D2}";
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

        /// <summary>
        /// 시각정보 설정 0x52
        /// </summary>
        public DateTime SetTime()
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("52");
            DateTime rtv;

            // SEND
            try
            {
                DateTime dateTime = DateTime.Now;
                vo.Body = new byte[6];
                vo.Body[0] = (byte)(dateTime.Year - 2000);
                vo.Body[1] = (byte)dateTime.Month;
                vo.Body[2] = (byte)dateTime.Day;
                vo.Body[3] = (byte)dateTime.Hour;
                vo.Body[4] = (byte)dateTime.Minute;
                vo.Body[5] = (byte)dateTime.Second;
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
                rx_buff = new byte[13];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {
                        string temp = $"20{rx_buff[5]:D2}-{rx_buff[6]:D2}-{rx_buff[7]:D2} {rx_buff[8]:D2}:{rx_buff[9]:D2}:{rx_buff[10]:D2}";
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

        /// <summary>
        /// 릴레이 상태 요청
        /// </summary>
        public byte[] GetStatus_Relay()
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("53");
            byte[] rtv = new byte[4];

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

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {
                        rtv[0] = rx_buff[5];
                        rtv[1] = rx_buff[6];
                        rtv[2] = rx_buff[7];
                        rtv[3] = rx_buff[8];
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
        /// 릴레이 상태 설정
        /// </summary>
        public void SetStatus_Relay(byte[] data)
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("54");
            byte[] rtv = new byte[4];

            // SEND
            try
            {
                vo.Body = new byte[4];
                vo.Body[0] = data[0];
                vo.Body[1] = data[1];
                vo.Body[2] = data[2];
                vo.Body[3] = data[3];

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

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {
                        rtv[0] = rx_buff[5];
                        rtv[1] = rx_buff[6];
                        rtv[2] = rx_buff[7];
                        rtv[3] = rx_buff[8];
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

            return;
        }

        /// <summary>
        /// 감지센서 상태 요청 0x55
        /// </summary>
        public void GetStatus_Sensor()
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("55");

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
                rx_buff = new byte[7];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {

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

            return;

        }

        /// <summary>
        /// 감지센서 상태 설정 0x56
        /// </summary>
        public void SetStatus_Sensor(byte[] data)
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("56");

            // SEND
            try
            {
                vo.Body = new byte[4];
                vo.Body[0] = data[0];
                vo.Body[1] = data[1];
                vo.Body[2] = data[2];
                vo.Body[3] = data[3];

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
                rx_buff = new byte[7];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {

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

            return;
        }

        /// <summary>
        /// 리셋 스케쥴 요청 0x57
        /// </summary>
        public void GetResetSchedule()
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("57");

            // SEND
            try
            {
                vo.Body = new byte[4];
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
                rx_buff = new byte[14];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {

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

            return;
        }

        /// <summary>
        /// 리셋 스케쥴 설정 0x58
        /// </summary>
        public void SetResetSchedule(byte data)
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("58");

            // SEND
            try
            {
                vo.Body = new byte[1];
                vo.Body[0] = data;
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
                rx_buff = new byte[9];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {

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

            return;
        }

        // 시스템 리셋 0x59
        public void Reset()
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("59");

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
                rx_buff = new byte[7];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {

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

            return;
        }

        /// <summary>
        /// 평시모드 설정 0x09
        /// </summary>
        public int SetNormalMode()
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("09");
            int rtv;

            // SEND
            try
            {
                vo.Body = new byte[1];
                vo.Body[0] = 00;
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
                rx_buff = new byte[8];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {
                        rtv = rx_buff[5];
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
        /// 긴급모드 설정 0x09
        /// </summary>
        public int SetEmergencyMode()
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("09");
            int rtv;

            // SEND
            try
            {
                vo.Body = new byte[1];
                vo.Body[0] = 01;
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
                rx_buff = new byte[8];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {
                        rtv = rx_buff[5];
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

        public void GetMode()
        {
            
        }

        /// <summary>
        /// 평시 문구 전체 삭제 0x19
        /// </summary>
        public void ResetNormalImage()
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("19");

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
                rx_buff = new byte[7];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {
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

            return;
        }

        /// <summary>
        /// 긴급 문구 전체 삭제 0x1A
        /// </summary>
        public void ResetEmergencySenario()
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("1A");

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
                rx_buff = new byte[7];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {
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

            return;
        }

        /// <summary>
        /// 평시 문구 추가 0x21
        /// </summary>
        public void AddNormalText()
        {

        }

        /// <summary>
        /// 평시 이미지 추가 0x22
        /// </summary>
        public void AddNormalImage()
        {

        }

        /// <summary>
        /// 평시 이미지 다운로드 0x23
        /// </summary>
        public void DownloadNormalImage()
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("23");
            int rtv;

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
                rx_buff = new byte[8];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {
                        rtv = rx_buff[5];
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

            return;
        }
        
        /// <summary>
        /// 평시 문구 추가 0x31
        /// </summary>
        public void AddEmergencyText()
        {

        }

        /// <summary>
        /// 평시 이미지 추가 0x32
        /// </summary>
        public void AddEmergencyImage()
        {

        }

        /// <summary>
        /// 긴급 이미지 다운로드 0x33
        /// </summary>
        public int DownloadEmergencyImage()
        {
            COMTEL_SEND_VO vo = new COMTEL_SEND_VO("33");
            int rtv;

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
                rx_buff = new byte[8];
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 2] == 0x03)
                {
                    try
                    {
                        rtv = rx_buff[5];
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


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리형 상태(관리형 개체)를 삭제합니다.
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        // ~EWDPL_T()
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

    public class COMTEL_SEND_VO
    {
        private byte[] stx = new byte[1];
        public string Stx
        {
            get => string.Format("{0:X2}", stx);
            set
            {
                stx[0] = Convert.ToByte(value, 16);
            }
        }

        private byte[] count = new byte[1];
        public string Count
        {
            get => string.Format("{0:X2}", count);
            set
            {
                count[0] = Convert.ToByte(value, 16);
            }
        }

        private byte[] cmd = new byte[1];
        public string Cmd
        {
            get => string.Format("{0:X2}", cmd);
            set
            {
                cmd[0] = Convert.ToByte(value, 16);
            }
        }

        private byte[] length = new byte[2];
        public string Length
        {
            get => string.Format("{0:X2}", Convert.ToInt16(length));
            set
            {
                int len = Convert.ToInt16(value);

                length[0] = (byte)(len >> 8);
                length[1] = (byte)(len >> 0);
            }
        }

        private byte[] body = new byte[0];
        public byte[] Body
        {
            get => body;
            set
            {
                Array.Resize(ref body, value.Length);
                Array.Copy(value, body, value.Length);
            }
        }

        private byte[] etx = new byte[1];
        public string Etx
        {
            get => string.Format("{0:X2}", etx);
            set
            {
                etx[0] = Convert.ToByte(value, 16);
            }
        }

        private byte[] crc = new byte[1];
        public string Crc
        {
            get => string.Format("{0:X2}", crc);
            set
            {
                crc[0] = Convert.ToByte(value, 16);
            }
        }

        public COMTEL_SEND_VO()
        {
            Stx = "02";
            Count = "C0";

            Etx = "03";
            Crc = "00";
        }

        public COMTEL_SEND_VO(string cmd)
        {
            Stx = "02";
            Count = "C0";

            Cmd = cmd;

            Etx = "03";
            Crc = "00";
        }

        public byte[] GetRaw()
        {
            byte[] temp = new byte[stx.Length + count.Length + cmd.Length + length.Length + body.Length + etx.Length +  crc.Length];
            int offset = 0;

            Array.Copy(stx, 0, temp, offset, stx.Length);
            offset += stx.Length;

            Array.Copy(count, 0, temp, offset, count.Length);
            offset += count.Length;

            Array.Copy(cmd, 0, temp, offset, cmd.Length);
            offset += cmd.Length;

            Length = $"{temp.Length}";
            Array.Copy(length, 0, temp, offset, length.Length);
            offset += length.Length;

            Array.Copy(body, 0, temp, offset, body.Length);
            offset += body.Length;

            Array.Copy(etx, 0, temp, offset, etx.Length);
            offset += etx.Length;

            Array.Copy(crc, 0, temp, offset, crc.Length);
            offset += crc.Length;

            return temp;
        }
    }
}
