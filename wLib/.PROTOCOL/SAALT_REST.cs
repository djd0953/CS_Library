using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class SAALT_REST : HTTP
    {
        protected TCP_CLIENT client = new TCP_CLIENT();
        public string httpProtocol { get; set; }
        
        RESTAPI_CONF rest_conf = new RESTAPI_CONF();

        public string SendProtocol(string t)
        {
            DateTime NowDate = DateTime.Now;
            DateTime LastAPIKeyDate = Convert.ToDateTime(rest_conf.ApiTime);
            TimeSpan DateDiff = LastAPIKeyDate - NowDate;

            switch (t)
            {
                // 로그인
                case "login" :
                    // SA에서 15분이 지나면 API KEY가 폐기처리 된다 하여 15분마다 수정
                    if (DateDiff.TotalMinutes > 15)
                    {
                        GetStartLine("POST", "/link/ibndBrct/apiLogin.json");
                        GetHeader($"auth_id:{rest_conf.Id}");
                        GetHeader($"auth_pw:{rest_conf.Pw}");
                        return process();
                    }
                    break;

                // 단말목록 요청
                case "list" :
                    if( DateDiff.TotalMinutes > 1 )
                    {
                        GetStartLine("GET", $"/link/ibndBrct/retrieveTxmrList.json?api_key={rest_conf.Api}");
                        return process();
                    }
                    break;

                // 방송요청
                case "send" :
                    break;
                // 방송결과
                case "result":
                    break;
                // 방송 종료
                case "end":
                    GetHeader($"api_key:{rest_conf.Api}");
                    if( this.startLine != "") return process();
                    break;
            }

            return null;
        }

        // Socket 통신!
        public string process()
        {
            try
            {
                rest_conf.ReadConfig();
                client.Connect(rest_conf.Ip, int.Parse(rest_conf.Port));
                client.Send(txbuff());
            }
            catch
            {
                throw;
            }

            MemoryStream ms = new MemoryStream();
            bool isRunning = true;
            int rtv;

            do
            {
                try
                {
                    byte[] temp = new byte[1024];
                    IAsyncResult rs = client.clnt_sock.BeginReceive(temp, 0, 1024, SocketFlags.None, null, null);
                    if (rs.AsyncWaitHandle.WaitOne(3000) == false)
                    {
                        throw new SocketException(10060);
                    }

                    rtv = client.clnt_sock.EndReceive(rs);
                    if (rtv == 0)
                    {
                        break;
                    }

                    if (temp[rtv - 1] == '\n' && temp[rtv - 2] == '\r')
                    {
                        rtv -= 2;
                        isRunning = false;
                    }

                    ms.Write(temp, 0, rtv);
                }
                catch(SocketException ex)
                {
                    throw ex;
                }
            }
            while (isRunning);

            byte[] rxBuff = ms.ToArray();
            string httpResponse = Encoding.UTF8.GetString(rxBuff);
            string[] res = httpResponse.Split('\n');

            foreach (string s in res) 
            {
                if (s.StartsWith("{"))
                {
                    return s;
                }
            }

            return null;
        }

        // 방송 요청 StartLine 설정
        public void SendBroadCastMessage(string content, string list)
        {
            content = Uri.EscapeUriString(content);
            list = list.Replace(" ", "");

            GetStartLine("POST", $"/?brctMsg={content}&txmrIdList={list}");
        }

        // 방송 결과 StartLine 설정
        public void ResultBroadCast(string seq)
        {
            GetStartLine("GET", $"/link/ibndBrct/retrieveBrctQue.json?brctSeq={seq}");
        }

        // 방송 종료 StartLine 설정
        public void EndBroadCast(string seq)
        {
            GetStartLine("PUT", $"/link/ibndBrct/updateBrctQueState.json?brctSeq={seq}&brctQueStateCd=S90");
        }

        // HTTP PROTOCOL을 byte로 변환
        public byte[] txbuff()
        {
            if (this.body.Length > 0)
            {
                GetHeader($"Content-Length:{Encoding.UTF8.GetBytes(this.body).Length}");
            }

            GetHeader($"Host:{rest_conf.Ip}:{rest_conf.Port}");

            this.httpProtocol = this.startLine;
            this.httpProtocol += this.header;
            this.httpProtocol += "\r\n";
            this.httpProtocol += this.body;

            return Encoding.UTF8.GetBytes(this.httpProtocol);
        }
    }
}
