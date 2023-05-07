using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_ISUMENT_VO
    {
        public string Key
        {
            get
            {
                return MentCode;
            }
        }

        public string MentCode { get; set; } // AUTO_PK
        public string[] BrdMent { get; set; } = new string[6];
        public List<string> BrdMentList
        {
            get
            {
                List<string> rtv = new List<string>();
                for (int i = 1; i <= 4; i++)
                {
                    if (BrdMent[i] != null)
                    {
                        rtv.Add(BrdMent[i]);
                    }
                }

                return rtv;
            }
        }
        public string[] DisMent { get; set; } = new string[6];
        public List<string> DisMentList
        {
            get
            {
                List<string> rtv = new List<string>();
                for (int i = 1; i <= 4; i++)
                {
                    if (DisMent[i] != null)
                    {
                        rtv.Add(DisMent[i]);
                    }
                }

                return rtv;
            }
        }
        public string[] SMSMent { get; set; } = new string[6];
        public List<string> SMSMentList
        {
            get
            {
                List<string> rtv = new List<string>();
                for (int i = 1; i <= 4; i++)
                {
                    if (SMSMent[i] != null)
                    {
                        rtv.Add(SMSMent[i]);
                    }
                }

                return rtv;
            }
        }
    }
}
