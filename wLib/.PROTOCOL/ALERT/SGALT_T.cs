using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using wLib.DB;

namespace wLib.PROTOCOL
{
    public class SGALT_T
    {
        protected TCP_CLIENT client = new TCP_CLIENT();

        RESTAPI_CONF segi_conf = new RESTAPI_CONF("SGAPI");

        /// <summary>
        /// SEGI 방송 요청 프로토콜
        /// </summary>
        /// <param name="type">send(TTS 방송 요청), check(무음 점검 요청), result(방송 결과 요청), detail(방송 결과 자세히)</param>
        /// <param name="equip_vo">방송 할 단말기 정보를 갖는 Object (필수:NM_DIST_OBSV, ConnPhone, DetCode)</param>
        /// <param name="brdsend_vo">방송 정보를 갖는 Object (필수:Parm1, Parm2, Parm3 or RetData)</param>
        /// <returns>JsonText</returns>
        public string SendJson(string type, WB_EQUIP_VO equip_vo, WB_BRDSEND_VO brdsend_vo)
        {
            string inputData = "";
            if (!equip_vo.ConnPhone.Contains('-'))
            {
                int strLength;
                if (equip_vo.ConnPhone.Length > 10) strLength = 4;
                else strLength = 3;

                equip_vo.ConnPhone = $"{equip_vo.ConnPhone.Substring(0, 3)}-{equip_vo.ConnPhone.Substring(3, strLength)}-{equip_vo.ConnPhone.Substring((3 + strLength), 4)}";
            }

            switch (type)
            {
                // TTS 방송 요청
                case "send":
                    inputData = $@"{{ " +
                                $@"""REQUEST WORD"":""EXP REQST TERM TTS CAST"", " +
                                $@"""ORGAN CODE"":""{segi_conf.Token}"", " +
                                $@"""USER ID"":""{segi_conf.Id}"", " +
                                $@"""CAST TITLE"":""{brdsend_vo.Parm1}"", " +
                                $@"""CAST BODY"":""{brdsend_vo.Parm3}"", " +
                                $@"""PRE SILENCE"":""05"", " +
                                $@"""PST SILENCE"":""05"", " +
                                $@"""READ SPEED"":""02"", " +
                                $@"""REPETITIONS"":""0{brdsend_vo.Parm2}"", " +
                                $@"""CAST VOLUME"":""06"", " +
                                // DEBUG
                                //$@"""CAST VOLUME"":""00"", " +
                                $@"""CHIME YN"":""Y"", " +
                                $@"""DEST LIST"":[{{""TERM CODE"":""{equip_vo.DetCode}"", " +
                                $@"                 ""TERM NAME"":""{equip_vo.Nm_dist_obsv}"", " +
                                $@"                 ""TERM CDMA"":""{equip_vo.ConnPhone}"" " +
                                $@"               }} ]" +
                                $@"}}";
                    break;

                // 무음 점검 요청
                case "check":
                    string NowDate = DateTime.Now.ToString();
                    inputData = $@"{{" +
                                $@"""REQUEST WORD"":""EXP CHECK TERM REQST"", " +
                                $@"""ORGAN CODE"":""{segi_conf.Token}"", " +
                                $@"""USER ID"":""{segi_conf.Id}"", " +
                                $@"""CAST TITLE"":""Check_{NowDate}"", " +
                                $@"""DEST LIST"":[" +
                                $@"{{" +
                                $@"    ""TERM CODE"":""{equip_vo.DetCode}"", " +
                                $@"    ""TERM NAME"":""{equip_vo.Nm_dist_obsv}"", " +
                                $@"    ""TERM CDMA"":""{equip_vo.ConnPhone}""" +
                                $@"}} " +
                                $@"]}}";
                    break;

                // 방송결과
                case "result":
                    inputData = $@"{{" +
                                $@"""REQUEST WORD"":""EXP CAST ID RESULT"", " +
                                $@"""ORGAN CODE"":""{segi_conf.Token}"", " +
                                $@"""USER ID"":""{segi_conf.Id}"", " +
                                $@"""CAST ID"":""{brdsend_vo.RetData}""" +
                                $@"}}";
                    break;

                // 방송결과 자세히
                case "detail":
                    inputData = $@"{{" +
                                $@"""REQUEST WORD"":""EXP CAST ID DETAIL"", " +
                                $@"""ORGAN CODE"":""{segi_conf.Token}"", " +
                                $@"""USER ID"":""{segi_conf.Id}"", " +
                                $@"""CAST ID"":""{brdsend_vo.RetData}""" +
                                $@"}}";
                    break;
            }

            byte[] txbuff = Encoding.UTF8.GetBytes(inputData);
            return process(txbuff);
        }

        // Socket 통신!
        public string process(byte[] txbuff)
        {
            try
            {
                segi_conf.ReadConfig();
                client.Connect(segi_conf.Ip, int.Parse(segi_conf.Port));
                client.Send(txbuff, txbuff.Length);
                client.Send(Encoding.ASCII.GetBytes("\r\n"), 2);
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
                    if (rs.AsyncWaitHandle.WaitOne(segi_conf.Timeout * 1000) == false)
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
                catch (SocketException ex)
                {
                    throw ex;
                }
            }
            while (isRunning);

            byte[] rxBuff = ms.ToArray();
            string jsonText = Encoding.UTF8.GetString(rxBuff);

            return jsonText;   
        }
    }
}
