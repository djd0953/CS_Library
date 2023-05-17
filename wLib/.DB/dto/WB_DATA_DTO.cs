using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public enum WB_DATA_TYPE { NONE = 0, RAIN = 1, WATER = 2, DPLACE = 3, SOIL = 4, SNOW = 6, TILT = 8, FLOOD = 21 };

    public class WB_DATA_DTO : ICloneable
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

        // DPLACE TEMP COLUMN
        public string standValue { get; set; }

        public WB_DATA_DTO()
        {

        }

        public WB_DATA_DTO(WB_DATA_TYPE type, string cd_dist_obsv, string sub_obsv, DateTime datatime, string value, string standvalue = null)
        {
            Type = type;
            Cd_dist_obsv = cd_dist_obsv;
            Sub_obsv = sub_obsv;
            Datatime = datatime;
            Value = value;
            standValue = standValue;
        }

        public object Clone()
        {
            return new WB_DATA_DTO(Type, Cd_dist_obsv, Sub_obsv, Datatime, Value, standValue);
        }
    }
}
