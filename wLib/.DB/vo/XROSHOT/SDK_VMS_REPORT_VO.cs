using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{

    public class SDK_VMS_REPORT_VO
    {
        public string Key
        {
            get
            {
                return MSG_ID;
            }
        }

        public string MSG_ID { get; set; }
        public string JOB_ID { get; set; }
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
        public string SUCC_COUNT { get; set; }
        public string FAIL_COUNT { get; set; }
    }
}
