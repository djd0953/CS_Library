using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class JHDATA_DTO : ICloneable
    {
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}", Cd_dist_obsv, Sub_obsv);
            }
        }

        public virtual WB_DATA_TYPE Type { get; set; } = WB_DATA_TYPE.NONE;
        public string Cd_dist_obsv { get; set; }
        public string Sub_obsv { get; set; }
        public DateTime Datatime { get; set; } = new DateTime();
        public string Value { get; set; }

        public JHDATA_DTO()
        {

        }

        public JHDATA_DTO(WB_DATA_TYPE type, string cd_dist_obsv, string sub_obsv, DateTime datatime, string value)
        {
            Type = type;
            Cd_dist_obsv = cd_dist_obsv;
            Sub_obsv = sub_obsv;
            Datatime = datatime;
            Value = value;
        }

        public object Clone()
        {
            return new JHDATA_DTO(Type, Cd_dist_obsv, Sub_obsv, Datatime, Value);
        }
    }
}
