using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class CHANNEL_CONF
    {
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}", obsv, sub_obsv);
            }
        }

        public string Key_sub_obsv
        {
            get
            {
                if (sub_obsv == "")
                {
                    return $"{obsv}_1";
                }
                
                return $"{obsv}_{sub_obsv}";
            }
        }

        public int offset { get; set; } = 0;
        public string name { get; set; } = "";
        public string obsv { get; set; } = "";
        public string sub_obsv { get; set; } = "";
        public string gb_obsv { get; set; } = "";
        public string unit { get; set; } = "";

        public string QUERY_WHERE { get; set; } = " 1 = 1 ";
    }
}
