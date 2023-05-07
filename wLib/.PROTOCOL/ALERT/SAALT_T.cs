using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using wLib.DB;

namespace wLib.PROTOCOL
{
    public class SAALT_T : HTTP_T
    {
        protected TCP_CLIENT client = new TCP_CLIENT();
        
        RESTAPI_CONF rest_conf = new RESTAPI_CONF("SAREST");

        public string SendProtocol(string type, MYSQL_T mysql = null, WB_BRDSEND_VO brdsend_vo = null)
        {
            DateTime NowDate = DateTime.Now;
            DateTime LastAPIKeyDate = Convert.ToDateTime(rest_conf.TokenDate);
            TimeSpan DateDiff = NowDate - LastAPIKeyDate;

            switch (type)
            {
                // 로그인
                case "login" :
                    // SA에서 최소 간격 1분 이상 요청
                    if (DateDiff.TotalMinutes > 1)
                    {
                        GetStartLine("POST", "/link/ibndBrct/apiLogin.json");
                        GetHeader($"auth_id:{rest_conf.Id}");
                        GetHeader($"auth_pw:{rest_conf.Pw}");
                        return process();
                    }
                    else return null;

                // 단말목록 요청
                case "list" :
                    if (DateDiff.TotalMinutes > 1)
                    {
                        GetStartLine("GET", $"/link/ibndBrct/retrieveTxmrList.json?api_key={rest_conf.Token}");
                        return process();
                    }
                    else return null;

                // 방송요청
                case "send" :
                    WB_EQUIP_DAO equip_dao = new WB_EQUIP_DAO(mysql);
                    WB_BRDLIST_DAO brdlist_dao = new WB_BRDLIST_DAO(mysql);
                    WB_BRDLIST_VO brdlist_vo = brdlist_dao.Select($"BCode = {brdsend_vo.Parm4}").SingleOrDefault();

                    string content = Uri.EscapeUriString(brdlist_vo.TTSContent);
                    string[] sa_equip_list = brdlist_vo.Cd_dist_obsv.Split(',');
                    string list = "";

                    foreach(string l in sa_equip_list)
                    {
                        WB_EQUIP_VO equip_vo = equip_dao.Select($"CD_DIST_OBSV = {l}").SingleOrDefault();
                        if( equip_vo.ConnModel == "SAALT_REST" )
                        {
                            if (list != "") list += ",";
                            list += l.Replace(" ", "");
                        }
                    }

                    GetStartLine("POST", $"/link/ibndBrct/insertBrctQue.json?brctMsg={content}&txmrIdList={list}");
                    GetHeader($"api_key:{rest_conf.Token}");
                    GetHeader($"Host:{rest_conf.Ip}:{rest_conf.Port}");
                    return process();

                // 방송결과
                case "result":
                    if (DateDiff.TotalMinutes > 1)
                    {
                        GetStartLine("GET", $"/link/ibndBrct/retrieveBrctQue.json?brctSeq={brdsend_vo.RetData}");
                        GetHeader($"api_key:{rest_conf.Token}");
                        return process();
                    }
                    else return null;

                // 방송 종료
                case "end":
                    GetStartLine("PUT", $"/link/ibndBrct/updateBrctQueState.json?brctSeq={brdsend_vo.RetData}&brctQueStateCd=S90");
                    GetHeader($"api_key:{rest_conf.Token}");
                    return process();
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
                    if (rs.AsyncWaitHandle.WaitOne(rest_conf.Timeout * 1000) == false)
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
    }
}
