using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace wLib
{
    public class HTTP
    {
        public string startLine { get; set; } = "";
        public string header { get; set; } = "";
        public string body { get; set; } = "";

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
    }
}
