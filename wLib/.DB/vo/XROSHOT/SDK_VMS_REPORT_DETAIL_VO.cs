using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class SDK_VMS_REPORT_DETAIL_VO
    {
        //(USER_ID, NOW_DATE, SEND_DATE, DEST_COUNT, DEST_INFO, CALLBACK, SUBJECT, MSG_SUBTYPE, RELISTEN_COUNT,  ATTACH_FILE, SEND_STATUS) " +
        public string Key
        {
            get
            {
                return $"{MSG_ID}_{SUBJOB_ID}";
            }
        }

        public string MSG_ID { get; set; }
        public string SUBJOB_ID { get; set; }

        public string USER_ID { get; set; }
        public string SEND_DATE { get; set; }
        public string DEST_NAME { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string REPLIED_FILE { get; set; }
    }
}
