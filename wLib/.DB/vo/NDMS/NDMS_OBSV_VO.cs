using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class NDMS_OBSV_VO
    {
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}", Dscode, Cd_dist_obsv);
            }
        }

        public string Dscode { get; set; } // 법정동코드(5자) + 구분코드(1자) + 일련번호(4자)
        public string Cd_dist_obsv { get; set; }// PK, FK, index번호
        public string gb_obsv { get; set; } // 센서종류
        public string Nm_dist_obsv { get; set; } = ""; // 지역명
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
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string RM { get; set; }
        public string Use_YN { get; set; } // 사용유무
        public string table_name { get; set; } = "TCM_COU_DD_OBSV";
        public string table_comment { get; set; } = "재해위험개선지구";

        // NDMS COLUMN
        public string Ndms_dist_obsv { get; set; } // NDMS CD_DIST_OBSV Mapping

        public void SetData(NDMS_OBSV_VO vo)
        {
            Dscode = vo.Dscode;
            Cd_dist_obsv = vo.Cd_dist_obsv;
            gb_obsv = vo.gb_obsv;
            Nm_dist_obsv = vo.Nm_dist_obsv;
            Bdong_cd = vo.Bdong_cd;
            Mntn_adres_at = vo.Mntn_adres_at;
            Mlnm = vo.Mlnm;
            Aulnm = vo.Aulnm;
            Dtl_adres = vo.Dtl_adres;
            Rdnmadr_cd = vo.Rdnmadr_cd;
            Rn_dtl_adres = vo.Rn_dtl_adres;
            Spo_No_cd = vo.Spo_No_cd;
            Orgn_cd = vo.Orgn_cd;
            Lat = vo.Lat;
            Lon = vo.Lon;
            RM = vo.RM;
            Use_YN = vo.Use_YN;
        }
    }
}
