using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class TX700_T : IDisposable
    {
        COMPORT_T serial = new COMPORT_T();
        public string cid = "";

        private bool disposedValue;

        LOG_T log = LOG_T.Instance;

        public TX700_T()
        {

        }

        public TX700_T(COMPORT_INFO info)
        {
            serial.comport_info.Port_name = info.Port_name;
            serial.comport_info.Baud_rate = info.Baud_rate;
            serial.comport_info.Parity_bits = info.Parity_bits;
            serial.comport_info.Data_bits = info.Data_bits;
            serial.comport_info.Stop_bits = info.Stop_bits;
        }

        public TX700_T(string Port_name, int Baud_rate, int Parity_bits, int Data_bits, int Stop_bits)
        {
            serial.comport_info.Port_name = Port_name;
            serial.comport_info.Baud_rate = Baud_rate;
            serial.comport_info.Parity_bits = Parity_bits;
            serial.comport_info.Data_bits = Data_bits;
            serial.comport_info.Stop_bits = Stop_bits;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리형 상태(관리형 개체)를 삭제합니다.
                    if (serial.IsOpen == true)
                        serial.Close();
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        ~TX700_T()
        {
            // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void Open(COMPORT_INFO info)
        {
            serial.comport_info.Port_name = info.Port_name;
            serial.comport_info.Baud_rate = info.Baud_rate;
            serial.comport_info.Parity_bits = info.Parity_bits;
            serial.comport_info.Data_bits = info.Data_bits;
            serial.comport_info.Stop_bits = info.Stop_bits;

            this.Open();
        }

        public void Open()
        {
            serial.Open();

            serial.ReadTimeOut = serial.WriteTimeout = 2000;
            serial.Encoding = Encoding.GetEncoding("ks_c_5601-1987");
        }

        public void Close()
        {
            serial.Close();
        }

        private string GetResult()
        {
            string rtv = "";
            string rx;

            while ((rx = serial.ReadLine()) != null)
            {
                //log.WriteLine($"{serial.comport_info.Port_name}({cid}): {rx}");
                //rx = Encoding.UTF8.GetString(Encoding.Convert(Encoding.GetEncoding("euc-kr"), Encoding.UTF8, Encoding.GetEncoding("euc-kr").GetBytes(rx)));
                rtv += $">> {rx}\r\n";
            }

            return rtv;
        }

        public string GetModel()
        {
            serial.Write("AT+GMM\r");
            
            return GetResult();
        }

        public string GetVersion()
        {
            serial.Write("AT+GMR\r");

            return GetResult();
        }

        public string GetCid()
        {
            serial.Write("AT+CNUM\r");

            return GetResult();
        }

        public string GetNetwork()
        {
            serial.Write("AT+CREG?\r");

            return GetResult();
        }

        public string GetIP()
        {
            serial.Write("AT+CGPADDR=1\r");

            return GetResult();
        }

        public string GetSMS_LIST()
        {
            serial.Write("AT+CMGL=\"ALL\"\r");

            return GetResult();
        }

        public string GetSMS_TEXT(int idx)
        {
            if (idx < 0 || idx > 99)
            {
                throw new Exception("입력범위 초과");
            }

            serial.Write($"<AT+CMGR={idx}\r>");

            return GetResult();
        }

        public string RemoveSMS(int idx)
        {
            if (idx < 0 || idx > 99)
            {
                throw new Exception("입력범위 초과");
            }

            serial.Write($"<AT+CMGD={idx}\r>");

            return GetResult();
        }

        public string GetTime()
        {
            serial.Write($"AT$$TIME?\r");

            return GetResult();
        }

        public string GetNumber()
        {
            string rtv = "";
            string rx;

            serial.Write("AT+CNUM\r");
            while ((rx = serial.ReadLine()) != null)
            {
                //log.WriteLine($"{serial.comport_info.Port_name}({cid}): {rx}");

                // TODO
                // +CNUM: ,"+821229988864",145
                if (rx.StartsWith("+CNUM") && rx.Split(',').Length == 3)
                {
                    rtv = rx.Split(',')[1].Replace('"', ' ').Trim().Replace("+82", "0");
                    break;
                }
            }

            if (rtv == "")
            {
                throw new Exception("CID 조회실패");
            }
            else
            {
                cid = rtv;
            }

            return rtv;
        }

        public void SetNumber(string number)
        {
            string rx;

            serial.Write($"AT+CMGS=\"{number}\"\r");
            while ((rx = serial.ReadLine()) != null)
            {
                if (rx == "")
                    continue;

                //log.WriteLine($"{serial.comport_info.Port_name}({cid}): {rx}");

                // TODO
                if (rx.StartsWith("AT+CMGS"))
                {
                    //break;
                }
            }
        }

        public string SendDTMF(string value)
        {
            string rx;

            serial.Write($"AT+VTS={value}\r");
            while ((rx = serial.ReadLine()) != null)
            {
                if (rx == "")
                    continue;

                // TODO
                if (rx.StartsWith("OK"))
                {
                    break;
                }
            }

            return rx;
        }

        public COMPORT_T SendCall(string number)
        {
            string phoneNumber = number.Replace("+82", "").Replace("-", "").Trim();
            string rx;

            serial.Write($"ATD{phoneNumber};\r");
            while ((rx = serial.ReadLine()) != null)
            {
                if (rx == "")
                    continue;

                if (rx.StartsWith("OK"))
                {
                    return serial;
                }
            }

            return null;
        }

        public string HangUp()
        {
            string rx;

            serial.Write("AT+CHUP\r");
            while ((rx = serial.ReadLine()) != null)
            {
                if (rx == "")
                    continue;

                if (rx.StartsWith("OK"))
                {
                    break;
                }
            }

            return rx;
        }

        public void SendText(string text)
        {
            serial.Write(text + "\x1A");

            string rx;
            while ((rx = serial.ReadLine()) != null)
            {
                if (rx == "")
                    continue;

                //log.WriteLine($"{serial.comport_info.Port_name}({cid}): " + Encoding.UTF8.GetString(Encoding.Convert(Encoding.GetEncoding("euc-kr"), Encoding.UTF8, Encoding.GetEncoding("euc-kr").GetBytes(rx))));

                // TODO
                //log.WriteLine("<< " + Encoding.Default.GetString(Encoding.Convert(Encoding.GetEncoding("euc-kr"), Encoding.GetEncoding("utf-8"), Encoding.GetEncoding("euc-kr").GetBytes(rx))));
                //if (rx.StartsWith("AT+CMGS"))
                //{
                //break;
                //}
            }

            //*/
        }

        public string SendMessage(string number, string text)
        {
            string rtv = "";
            string rx;
            string phoneNumber = number.Replace("+82", "").Replace("-", "").Trim();

            serial.Write($"AT+CMGS=\"{phoneNumber}\"\r");
            while ((rx = serial.ReadLine()) != null)
            {
                if (rx.StartsWith("AT+CMGS"))
                {
                    rtv += rx;
                    //log.WriteLine(rx);
                    break;
                }
            }

            serial.Write(text);
            serial.Write("\x1A");
            rtv += GetResult();
            return rtv; 
        }

        public void SendBroadCast(string number, string text)
        {
            //string send_message = "[WB,TTS,]";
            //this.SendText(text);
            
        }

        public void SendTTS(string number, string text)
        {
            int page_curr = 1;
            int page_total = 1;
            int repeat = 1;
            string rx;

            this.SetNumber(number);


            // TODO
            byte[] bodyByte = Encoding.Default.GetBytes($"[WB,TTS,{cid},{repeat},{page_curr},{page_total},\"{text}\"]");
            
            this.SendText(text);

            /*

            byte[] sendByte = new byte[bodyByte.Length + endByte.Length];
            Array.Copy(bodyByte, 0, sendByte, 0, bodyByte.Length);
            Array.Copy(endByte, 0, sendByte, bodyByte.Length, endByte.Length);

            log.WriteLine("> " + Encoding.UTF8.GetString(sendByte));
            serial.Write(sendByte, 0, sendByte.Length);

            while ((rx = serial.ReadLine()) != null)
            {
                log.WriteLine("<< " + Encoding.Default.GetString(Encoding.Convert(Encoding.GetEncoding("euc-kr"), Encoding.GetEncoding("utf-8"), Encoding.GetEncoding("euc-kr").GetBytes(rx))));
                //if (rx.StartsWith("AT+CMGS"))
                //{
                //break;
                //}
            }
            */
        }


        
       
    }
}
