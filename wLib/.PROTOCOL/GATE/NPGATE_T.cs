using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace wLib.PROTOCOL
{
    public class NPGATE_T : IDisposable
    {
        LOG_T log = LOG_T.Instance;

        System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();

        string ip;
        string port = "4098";

        public string method { get; set; }
        public string contenttype { get; set; }
        public int timeout { get; set; } = 5;
        public string body { get; set; }

        private bool disposedValue;

        public NPGATE_T()
        {
            this.method = "POST";
            this.contenttype = "application/json";
            this.timeout = 5;
            this.body = "{\r\n    \"CMD\":\"OK\"\r\n}";
        }

        public NPGATE_T(string ip, string port)
        {
            this.ip = ip;
            this.port = port;

            this.method = "POST";
            this.contenttype = "application/json";
            this.timeout = 5;
            this.body = "{\r\n    \"CMD\":\"OK\"\r\n}";
        }

        public async Task<bool> Connect(string ip, string port)
        {
            this.ip = ip;
            this.port = port;

            return await Connect();
        }

        public async Task<bool> Connect()
        {
            bool rtv;

            try
            {
                await client.ConnectAsync(this.ip, int.Parse(this.port));
                rtv = client.Client.Connected;
            }
            catch
            {
                rtv = false;
            }

            return rtv;
        }

        public string Open()
        {
            string url = $"http://{this.ip}:{this.port}/nxlprs/v1.0/bars/open";
            log.WriteLine($"{url}");

            HttpWebRequest req = Create(url);

            return Command(req);
        }

        public string UpLock()
        {
            string url = $"http://{this.ip}:{this.port}/nxlprs/v1.0/bars/lock";
            log.WriteLine($"{url}");

            HttpWebRequest req = Create(url);

            return Command(req);
        }

        public string OpenUnLock()
        {
            string url = $"http://{this.ip}:{this.port}/nxlprs/v1.0/bars/unlock";
            log.WriteLine($"{url}"); 
            
            HttpWebRequest req = Create(url);

            return Command(req);
        }

        public string Close()
        {
            string url = $"http://{this.ip}:{this.port}/nxlprs/v1.0/bars/close";
            log.WriteLine($"{url}"); 
            
            HttpWebRequest req = Create(url);

            return Command(req);
        }

        public string CloseLock()
        {
            string url = $"http://{this.ip}:{this.port}/nxlprs/v1.0/bars/closelock";
            log.WriteLine($"{url}"); 
            
            HttpWebRequest req = Create(url);

            return Command(req);
        }

        public string CloseUnLock()
        {
            string url = $"http://{this.ip}:{this.port}/nxlprs/v1.0/bars/closeunlock";
            log.WriteLine($"{url}"); 
            
            HttpWebRequest req = Create(url);

            return Command(req);
        }

        private HttpWebRequest Create(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                request.Method = this.method;
                request.ContentType = this.contenttype;
                request.ReadWriteTimeout = this.timeout;

                StreamWriter reqStr = new StreamWriter(request.GetRequestStream());
                reqStr.Write(this.body);
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
        ~NPGATE_T()
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
