using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wLib.DB
{
    public class WB_SENDMESSAGE_VO
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
        public string SCode { get; set; }
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
        public string LastStatus_ko
        {
            get
            {
                switch (SendStatus.ToLower())
                {
                case "start":
                    return "전송시작";
                case "ing":
                    return "전송중";
                case "end":
                case "ok":
                    return "전송완료";
                case "error":
                    return "전송오류";
                case "fail":
                    return "전송실패";
                default:
                    return SendStatus;
                }
            }            
        }

        public void SetData(WB_SENDMESSAGE_VO vo)
        {
            MsgCode = vo.MsgCode;
            SCode = vo.SCode;
            PhoneNum = vo.PhoneNum;
            SendMessage = vo.SendMessage;
            SendStatus = vo.SendStatus;
            RegDate = vo.RegDate;
            RetDate = vo.RetDate;
        }
    }
}
