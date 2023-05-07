using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHSENDMESSAGE_VO
    {
        /// <summary>
        /// KEY: MsgCode
        /// </summary>
        public string Key
        {
            get
            {
                return MsgCode;
            }
        }

        /// <summary>
        /// PK: 자동증가
        /// </summary>
        public string MsgCode { get; set; } // AUTO PK
        /// <summary>
        /// 수신번호 EX) "012-1234-5678"
        /// </summary>
        public string PhoneNum { get; set; }
        /// <summary>
        /// 발신 메시지
        /// </summary>
        public string SendMessage { get; set; }
        /// <summary>
        /// 전송 상태 {"start", "ing", "OK", "Error", "fail"}
        /// </summary>
        public string SendStatus { get; set; }

        /// <summary>
        /// 등록시간 "0000-00-00 00:00:00"
        /// </summary>
        public string RegDate { get; set; }

        /// <summary>
        /// 처리시간 "0000-00-00 00:00:00"
        /// </summary>
        public string RetDate { get; set; }

        public void SetData(WB_SENDMESSAGE_VO vo)
        {
            MsgCode = vo.MsgCode;
            PhoneNum = vo.PhoneNum;
            SendMessage = vo.SendMessage;
            SendStatus = vo.SendStatus;
            RegDate = vo.RegDate;
            RetDate = vo.RetDate;
        }
    }
}
