using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class SDK_VMS_SEND_VO
    {
        //(USER_ID, NOW_DATE, SEND_DATE, DEST_COUNT, DEST_INFO, CALLBACK, SUBJECT, MSG_SUBTYPE, RELISTEN_COUNT,  ATTACH_FILE, SEND_STATUS) " +
        public string Key
        {
            get
            {
                return MSG_ID;
            }
        }

        public string MSG_ID { get; set; }
        public string USER_ID { get; set; }
        public string NOW_DATE { get; set; }
        public string SEND_DATE { get; set; }
        public string DEST_COUNT { get; set; }
        public string DEST_INFO { get; set; }
        public string CALLBACK { get; set; }
        public string SUBJECT { get; set; }
        public string MSG_SUBTYPE { get; set; }
        public string RELISTEN_COUNT { get; set; }
        public string ATTACH_FILE { get; set; }
        public string RESERVED1 { get; set; }
        public string RESERVED2 { get; set; }
        public string RESERVED3 { get; set; }
        public string RESERVED4 { get; set; }
        public string RESERVED5 { get; set; }
        public string SEND_STATUS { get; set; }
        public string SEND_COUNT { get; set; }
        public string SEND_RESULT { get; set; }
    }
}
