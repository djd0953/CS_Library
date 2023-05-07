using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.PROTOCOL
{
    public class STGATE_T : IDisposable
    {
        TCP_CLIENT client = new TCP_CLIENT();
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
        /// 차단기 시간 요청
        /// </summary>
        /// <returns>TIMESYN#yyyy-mm-dd hh:mm:ss</returns>
        public string TimeReq()
        {
            // TX > TIMEREQ
            // RX > TIMESYN#2021-12-27 12:54:23

            string txStr = "TIMEREQ";
            string rxStr = "TIMESYN#yyyy-mm-dd hh:mm:ss";

            return Encoding.UTF8.GetString(Command(txStr, rxStr));
        }

        /// <summary>
        /// 차단기 시간 설정
        /// </summary>
        /// <returns>TIMESYN#yyyy-mm-dd hh:mm:ss</returns>
        public string TimeSyn()
        {
            // TX> TIMESYN#2021-12-27 12:54:23 
            // RX> TIMESYN#2021-12-27 12:54:23

            string txStr = $"TIMESYN#{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
            string rxStr = "TIMESYN#yyyy-mm-dd hh:mm:ss";

            return Encoding.UTF8.GetString(Command(txStr, rxStr));
        }

        /// <summary>
        /// 차단기 상태 요청
        /// </summary>
        /// <returns>BARCTRL#GATEUP#UPLOCK#yyyy-mm-dd hh:mm:ss</returns>
        public string BarCtrl()
        {
            // TX> BARCTRL 
            // RX> BARCTRL#GATEUP#UPLOCK#2021-12-27 12:54:23

            string txStr = "BARCTRL";
            string rxStr = "BARCTRL#GATEUP#UPLOCK#yyyy-mm-dd hh:mm:ss";

            return Encoding.UTF8.GetString(Command(txStr, rxStr));
        }

        /// <summary>
        /// 차단기 상태 제어(열림 고정)
        /// </summary>
        /// <returns>BARCTRL#GATEUP#UPLOCK#yyyy-mm-dd hh:mm:ss</returns>
        public string UpLock()
        {
            // TX> BARCTRL#UPLOCK
            // RX> BARCTRL#GATEUP#UPLOCK#2021-12-27 12:54:23

            string txStr = "BARCTRL#UPLOCK";
            string rxStr = "BARCTRL#GATEUP#UPLOCK#yyyy-mm-dd hh:mm:ss";

            return Encoding.UTF8.GetString(Command(txStr, rxStr));
        }

        /// <summary>
        /// 차단기 상태 제어(닫힘 고정)
        /// </summary>
        /// <returns>BARCTRL#GATEUP#UPLOCK#yyyy-mm-dd hh:mm:ss</returns>
        public string DownLock()
        {
            // TX> BARCTRL#DOWNLOCK
            // RX> BARCTRL#GATEDOWN#DOWNLOCK#2021-12-27 12:54:23

            string txStr = "BARCTRL#DOWNLOCK";
            string rxStr = "BARCTRL#GATEUP#DOWNLOCK#yyyy-mm-dd hh:mm:ss";

            return Encoding.UTF8.GetString(Command(txStr, rxStr));
        }

        /// <summary>
        /// 차단기 상태 제어(루프에 의한 자동동작)
        /// </summary>
        /// <returns>BARCTRL#GATEUP#AUTO#yyyy-mm-dd hh:mm:ss</returns>
        public string Auto()
        {
            // TX> BARCTRL#AUTO
            // RX> BARCTRL#GATEUP#AUTO#2021-12-27 12:54:23

            string txStr = "BARCTRL#AUTO";
            string rxStr = "BARCTRL#GATEUP#AUTO#yyyy-mm-dd hh:mm:ss";

            return Encoding.UTF8.GetString(Command(txStr, rxStr));
        }

        /// <summary>
        /// 차단기 상태 제어(출차만 가능)
        /// </summary>
        /// <returns>BARCTRL#GATEDOWN#EMGOUT#yyyy-mm-dd hh:mm:ss</returns>
        public string EmgOut()
        {
            // TX> BARCTRL#EMGOUT
            // RX> BARCTRL#GATEDOWN#EMGOUT#2021-12-27 12:54:23

            string txStr = "BARCTRL#EMGOUT";
            string rxStr = "BARCTRL#GATEDOWN#EMGOUT#yyyy-mm-dd hh:mm:ss";

            return Encoding.UTF8.GetString(Command(txStr, rxStr));
        }

        /// <summary>
        /// 전광판 문구 수정
        /// </summary>
        /// <param name="place">0:상단|1:하단</param>
        /// <param name="txt">문구</param>
        /// <param name="color">0:빨강|1:녹색|2:노랑|3:파랑|4:보라|5:하늘|6:하양</param>
        /// <returns>SCRTEXT#동작여부 (0:성공|1:실패)</returns>
        public string ScrText(int place, string txt, int color)
        {
            // TX> SCRTEXT#문구위치(0:상단, 1:하단)#문구(15자이내 텍스트)#색상(0:빨강, 1:녹색, 2:노랑, 3:파랑, 4:보라,5:하늘, 6:하양)
            // RX> SCRTEXT#동작여부(0:동작성공, 1:동작실패)

            string txStr = $"SCRTEXT#{place}#{txt}#{color}";
            string rxStr = "SCRTEXT#0";

            return Encoding.UTF8.GetString(Command(txStr, rxStr));
        }

        private byte[] Command(string txStr, string rxStr)
        {
            STSEND_VO vo = new STSEND_VO();
            byte[] rtv;
            int rxStrLength = Encoding.UTF8.GetBytes(rxStr).Length;

            // SEND
            try
            {
                vo.Data = new byte[Encoding.UTF8.GetBytes(txStr).Length];
                vo.Data = Encoding.UTF8.GetBytes(txStr);

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
                rx_buff = new byte[ 1 + rxStrLength + 1 ]; //0x02 + body + 0x03
                client.Recv(rx_buff);

                if (rx_buff[0] == 0x02 && rx_buff[rx_buff.Length - 1] == 0x03)
                {
                    rtv = new byte[rxStrLength];
                    Array.Copy(rx_buff, 1, rtv, 0, rxStrLength);
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
                    client.Dispose();
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
                client = null;
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        ~STGATE_T()
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

    public class STSEND_VO
    {
        private byte[] header = new byte[1];
        public string Header
        {
            get
            {
                return string.Format("{0:X2}", header[0]);
            }
            set
            {
                header[0] = Convert.ToByte(value, 16);
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

        private byte[] footer = new byte[1];
        public string Footer
        {
            get
            {
                return string.Format("{0:X2}", footer[0]);
            }
            set
            {
                footer[0] = Convert.ToByte(value, 16);
            }
        }

        public STSEND_VO()
        {
            Header = "02";
            Footer = "03";
        }

        public byte[] GetRaw()
        {
            byte[] temp = new byte[header.Length + data.Length + footer.Length];
            int offset = 0;

            Array.Copy(header, 0, temp, 0, header.Length);
            offset += header.Length;

            Array.Copy(data, 0, temp, offset, data.Length);
            offset += data.Length;

            Array.Copy(footer, 0, temp, offset, footer.Length);
            // offset += footer.Length;

            return temp;
        }
    }
}
