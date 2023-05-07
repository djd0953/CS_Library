using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class NMS_EQUIP_VO : ICloneable, IComparable<NMS_EQUIP_VO>
    {
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}", JHACode, Cd_dist_obsv);
            }
        }

        public string JHACode { get; set; }// PK, index번호
        public string Cd_dist_obsv { get; set; }// PK, index번호
        public string Sub_obsv { get; set; }// PK, index번호

        public string Nm_dist_obsv { get; set; } = ""; // 지역명
        public string ConnModel { get; set; } // FK, 프로토콜(EWWATER_LAN ... )

        public string ConnPhone { get; set; } // PHONE
        public string ConnIp { get; set; } // IP
        public string ConnPort { get; set; } // PORT
        public string LoggerTime { get; set; } // 로거 시각


        public string LastStatus { get; set; } // 마지막 상태
        public string LastDate { get; set; } // 마지막 성공한 통신시간
        public string Data { get; set; }

        public string Gb_obsv { get; set; }
        public string RainBit { get; set; } // 단위변환
        public string Lat { get; set; }
        public string Lon { get; set; }

        public object Clone()
        {
            NMS_EQUIP_VO rtv = new NMS_EQUIP_VO()
            {
                JHACode = JHACode,
                Cd_dist_obsv = Cd_dist_obsv,
                Sub_obsv = Sub_obsv,

                Nm_dist_obsv = Nm_dist_obsv,
                ConnModel = ConnModel,

                ConnPhone = ConnPhone,
                ConnIp = ConnIp,
                ConnPort = ConnPort,
                LoggerTime = LoggerTime,

                LastStatus = LastStatus,
                LastDate = LastDate,
                Data = Data,

                Gb_obsv = Gb_obsv,
                RainBit = RainBit,
                Lat = Lat,
                Lon = Lon
            };

            return rtv;
        }

        public int CompareTo(NMS_EQUIP_VO other)
        {
            return string.Compare(this.Key, other.Key);
        }
    }
}
