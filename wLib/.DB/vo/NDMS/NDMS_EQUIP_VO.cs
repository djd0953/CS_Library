using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class NDMS_EQUIP_VO : ICloneable, IComparable<NDMS_EQUIP_VO>
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

        // WEATHERSI ADD COLUMN
        public string gb_obsv { get; set; } // 센서종류
        public string Lat { get; set; }
        public string Lon { get; set; }

        // TEMP COLUMN
        public int sub_obsv { get; set; }
        public string Data { get; set; }
        public DateTime logger_time { get; set; }

        // NDMS COLUMN
        public string Dscode { get; set; } // 법정동코드(5자) + 구분코드(1자) + 일련번호(4자)
        public string Ndms_dist_obsv { get; set; } // NDMS CD_DIST_OBSV Mapping
        public string Bdong_cd { get; set; } // 법정동코드
        public string Mntn_adres_at { get; set; } // 산주소여부 (Y/N)
        // 본번/부번 : ex)상대원동 333-7 중 본번은 '333' 부번은 '7'
        public string Mlnm { get; set; } // 본번  ex)상대원동 333-7 중 본번은 '333'
        public string Aulnm { get; set; } // 부번  ex)상대원동 333-7 중 부번은 '7'
        public string Dtl_adres { get; set; } // 상세주소
        public string Rdnmadr_cd { get; set; } // 도로명주소코드
        public string Rn_dtl_adres { get; set; } // 도로명상세주소
        public string Spo_No_cd { get; set; } // 도로명상세주소
        public string Orgn_cd { get; set; } // 기관코드

        public void SetData(NDMS_EQUIP_VO vo)
        {
            Cd_dist_obsv = vo.Cd_dist_obsv;
            Ndms_dist_obsv = vo.Ndms_dist_obsv;
            Nm_dist_obsv = vo.Nm_dist_obsv;
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
            gb_obsv = vo.gb_obsv;
            Lat = vo.Lat;
            Lon = vo.Lon;

            sub_obsv = vo.sub_obsv;
            Data = vo.Data;
            logger_time = vo.logger_time;

            Dscode = vo.Dscode;
            Bdong_cd = vo.Bdong_cd;
            Mntn_adres_at = vo.Mntn_adres_at;
            Mlnm = vo.Mlnm;
            Aulnm = vo.Aulnm;
            Dtl_adres = vo.Dtl_adres;
            Rdnmadr_cd = vo.Rdnmadr_cd;
            Rn_dtl_adres = vo.Rn_dtl_adres;
            Spo_No_cd = vo.Spo_No_cd;
            Orgn_cd = vo.Orgn_cd;
        }

        public object Clone()
        {
            NDMS_EQUIP_VO rtv = new NDMS_EQUIP_VO()
            {
                Cd_dist_obsv = Cd_dist_obsv,
                Ndms_dist_obsv = Ndms_dist_obsv,
                Nm_dist_obsv = Nm_dist_obsv,

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
                Lat = Lat,
                Lon = Lon,

                sub_obsv = sub_obsv,
                Data = Data,

                Dscode = Dscode,
                Bdong_cd = Bdong_cd,
                Mntn_adres_at = Mntn_adres_at,
                Mlnm = Mlnm,
                Aulnm = Aulnm,
                Dtl_adres = Dtl_adres,
                Rdnmadr_cd = Rdnmadr_cd,
                Rn_dtl_adres = Rn_dtl_adres,
                Spo_No_cd = Spo_No_cd,
                Orgn_cd = Orgn_cd
        };

            return rtv;
        }

        public int CompareTo(NDMS_EQUIP_VO other)
        {
            return string.Compare(this.Cd_dist_obsv, other.Cd_dist_obsv);
        }
    }
}
