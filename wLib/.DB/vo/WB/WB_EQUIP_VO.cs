using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_EQUIP_VO : ICloneable, IComparable<WB_EQUIP_VO>
    {
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}", Cd_dist_obsv, ConnModel);
            }
        }

        public string Key_gb_obsv
        {
            get
            {
                return string.Format("{0}_{1}", Cd_dist_obsv, gb_obsv);
            }
        }

        public string Key_sub_obsv
        {
            get
            {
                return string.Format("{0}_{1}", Cd_dist_obsv, sub_obsv);
            }
        }

        public string Cd_dist_obsv { get; set; }// PK, FK, index번호
        public string Nm_dist_obsv { get; set; } = ""; // 지역명

        public string ConnType { get; set; } // 접속방식(DB, LAN)
        public string ConnModel { get; set; } // FK, 프로토콜(EWWATER_LAN ... )
        public string ConnIp { get; set; } // IP
        public string ConnPort { get; set; } // PORT
        public string ConnPhone { get; set; } // PHONE

        public string LastDate { get; set; } // 마지막 성공한 통신시간
        public string LastStatus { get; set; } // 마지막 상태
        public string ErrorChk { get; set; } // 마지막 상태(실시간 현황)
        public string Use_YN { get; set; } // 사용유무
        public string RainBit { get; set; } // 단위변환
        public string DetCode { get; set; } // 방송장비 코드(방송, 전광판)
        public string Dtl_adres { get; set; }

        // WEATHERSI ADD COLUMN
        public string gb_obsv { get; set; } // 센서종류
        public string SubObCount { get; set; } // 변위 센서개수
        public string Lat { get; set; }
        public string Lon { get; set; }

        // TEMP COLUMN
        public int sub_obsv { get; set; }
        public string Data { get; set; }
        public DateTime logger_time { get; set; }
        public string LastStatus_ko
        {
            get
            {
                switch (LastStatus)
                {
                case "end":
                    return "완료";

                default:
                    return "";
                }
            }
        }

        public void SetData(WB_EQUIP_VO vo)
        {
            Cd_dist_obsv = vo.Cd_dist_obsv;
            Nm_dist_obsv = vo.Nm_dist_obsv;
            ConnType = vo.ConnType;
            ConnModel = vo.ConnModel;
            ConnIp = vo.ConnIp;
            ConnPort = vo.ConnPort;
            ConnPhone = vo.ConnPhone;
            LastDate = vo.LastDate;
            LastStatus = vo.LastStatus;
            ErrorChk = vo.ErrorChk;
            Use_YN = vo.Use_YN;
            RainBit = vo.RainBit;
            DetCode = vo.DetCode;
            Dtl_adres = vo.Dtl_adres;
            gb_obsv = vo.gb_obsv;
            SubObCount = vo.SubObCount;
            Lat = vo.Lat;
            Lon = vo.Lon;

            sub_obsv = vo.sub_obsv;
            Data = vo.Data;
            logger_time = vo.logger_time;
        }

        public object Clone()
        {
            WB_EQUIP_VO rtv = new WB_EQUIP_VO()
            {
                Cd_dist_obsv = Cd_dist_obsv,
                Nm_dist_obsv = Nm_dist_obsv,

                ConnType = ConnType,
                ConnModel = ConnModel,

                ConnIp = ConnIp,
                ConnPort = ConnPort,
                ConnPhone = ConnPhone,

                LastDate = LastDate,
                LastStatus = LastStatus,
                ErrorChk = ErrorChk,
                Use_YN = Use_YN,

                RainBit = RainBit,
                DetCode = DetCode,
                gb_obsv = gb_obsv,
                SubObCount = SubObCount,
                Lat = Lat,
                Lon = Lon,
                Dtl_adres = Dtl_adres,

                sub_obsv = sub_obsv,
                Data = Data,
        };

            return rtv;
        }

        public int CompareTo(WB_EQUIP_VO other)
        {
            return string.Compare(this.Cd_dist_obsv, other.Cd_dist_obsv);
        }
    }
}
