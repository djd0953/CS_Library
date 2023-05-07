using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace wLib.PROTOCOL
{
    public class AMNGATE_T : IDisposable
    {
        RESTAPI_CONF amn_conf = new RESTAPI_CONF("AMN");
        TCP_CLIENT client = new TCP_CLIENT();

        string ip;
        string port;

        private bool disposedValue;

        public string url { get; set; }
        public string method { get; set; }
        public string contenttype { get; set; }
        public int timeout { get; set; }
        public string authorization { get; set; }

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

        public string Status(string IP)
        {
            // 사용할 수 없음
            // 비어있는 0번 인덱스를 상태값으로 받을 수 있는지 질문에 아마노측에서 사용할 수 없다고 말함
            HttpWebRequest req = Create("0", IP);
            return Command(req);
        }

        public string Open(string IP)
        {
            HttpWebRequest req = Create("1", IP);
            return Command(req);
        }

        public string Close(string IP)
        {
            HttpWebRequest req = Create("2", IP);
            return Command(req);
        }

        public string UpLock(string IP)
        {
            HttpWebRequest req = Create("3", IP);
            return Command(req);
        }

        public string OpenUnLock(string IP)
        {
            HttpWebRequest req = Create("4", IP);
            return Command(req);
        }


        public AMNGATE_T()
        {
            this.ip = amn_conf.Ip;
            this.port = amn_conf.Port;

            this.url = $"http://{ip}:{port}/interop/remoteGateControl.do";
            this.method = "POST";
            this.contenttype = "application/json";
            this.timeout = amn_conf.Timeout;
            this.authorization = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes($"{amn_conf.Id}:{amn_conf.Pw}"));
        }

        public AMNGATE_T(string ip, string port = "9948")
        {
            this.port = amn_conf.Port;

            this.url = $"http://{ip}:{port}/interop/remoteGateControl.do";
            this.method = "POST";
            this.contenttype = "application/json";
            this.timeout = amn_conf.Timeout;
            this.authorization = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes($"{amn_conf.Id}:{amn_conf.Pw}"));
        }

        private HttpWebRequest Create(string action, string remoteIP)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                request.Method = this.method;
                request.ContentType = this.contenttype;
                request.ReadWriteTimeout = this.timeout;
                string body = $"{{\"Authorization\":\"{this.authorization}\",\"actionType\":\"{action}\",\"remoteIP\":\"{remoteIP}\"}}";
                

                StreamWriter reqStr = new StreamWriter(request.GetRequestStream());
                reqStr.Write(body);
                reqStr.Flush();
                reqStr.Close();
            }
            catch
            {
                throw;
            }

            return request;
        }

        private string Command(HttpWebRequest request)
        {
            string retVal;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader respStream = new StreamReader(response.GetResponseStream());
                retVal = respStream.ReadToEnd();
                respStream.Close();
            }
            catch
            {
                throw;
            }

            return retVal;
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
        ~AMNGATE_T()
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
}
