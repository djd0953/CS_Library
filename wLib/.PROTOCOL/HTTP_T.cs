using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace wLib
{
    public class HTTP_T
    {
        public string startLine { get; set; } = "";
        public string header { get; set; } = "";
        public string body { get; set; } = "";
        public string httpProtocol { get; set; }

        public void GetStartLine(string method, string address = "/")
        {
            this.startLine = $"{method.ToUpper()} {address} HTTP/1.1\r\n";
            
            // HTTP 기본 Header
            {
                this.header = "Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9\r\n";
                this.header += "Accept-Language: ko-KR,ko;q=0.9,en-US;q=0.8,en;q=0.7\r\n";
                this.header += "Accept-Encoding:gzip,deflate\r\n";
                this.header += "Connection:keep-alive\r\n";
            }
        }

        public void GetHeader(string header)
        {
            this.header += $"{header}\r\n";
        }

        public void GetBody(string body) 
        {
            this.body += $"{body}";
        }

        // HTTP PROTOCOL을 byte로 변환
        public byte[] txbuff()
        {
            if (this.body.Length > 0)
            {
                GetHeader($"Content-Length:{Encoding.UTF8.GetBytes(this.body).Length}");
            }

            this.httpProtocol = this.startLine;
            this.httpProtocol += this.header;
            this.httpProtocol += "\r\n";
            this.httpProtocol += this.body;

            return Encoding.UTF8.GetBytes(this.httpProtocol);
        }
    }

    public class RESTAPI_T : IDisposable
    {
        System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();

        public string url { get; set; }
        public string port { get; set; }

        public string method { get; set; }
        public string contenttype { get; set; }
        public int timeout { get; set; } = 5;
        public string body { get; set; }

        private bool disposedValue;

        public async Task<bool> Connect(string url)
        {
            this.url = url;

            return await Connect();
        }

        public async Task<bool> Connect()
        {
            bool rtv;

            try
            {
                await client.ConnectAsync(this.url, int.Parse(this.port));
                rtv = client.Client.Connected;
            }
            catch
            {
                rtv = false;
            }

            return rtv;
        }

        protected HttpWebRequest Create(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                request.Method = this.method;
                request.ContentType = this.contenttype;
                request.ReadWriteTimeout = this.timeout;
            }
            catch
            {
                throw;
            }

            return request;
        }

        protected HttpWebRequest Write(HttpWebRequest request)
        {
            try
            {
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

        protected string Command(HttpWebRequest request)
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
        ~RESTAPI_T()
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
