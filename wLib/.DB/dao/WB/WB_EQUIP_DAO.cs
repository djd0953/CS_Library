using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using wLib;

namespace wLib.DB
{
    public class WB_EQUIP_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_EQUIP_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_equip";
        }

        public void Create()
        {
            string sql;

            try
            {
                sql = $"SHOW TABLES LIKE '{table}'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    // 테이블 생성
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append($"CREATE TABLE `{table}` ");
                        sb.Append("( ");
                        sb.Append(@"`DSCODE` CHAR(10) NULL DEFAULT NULL COMMENT '재해코드' COLLATE 'utf8_general_ci', `CD_DIST_OBSV` VARCHAR(10) NOT NULL COMMENT '장비번호' COLLATE 'utf8_general_ci', `NM_DIST_OBSV` VARCHAR(30) NULL DEFAULT NULL COMMENT '장비명칭' COLLATE 'utf8_general_ci', `ConnType` VARCHAR(10) NULL DEFAULT NULL COMMENT '통신형태' COLLATE 'utf8_general_ci', `ConnModel` VARCHAR(20) NULL DEFAULT NULL COMMENT '프로토콜' COLLATE 'utf8_general_ci', `ConnPhone` VARCHAR(20) NULL DEFAULT NULL COMMENT '전화번호' COLLATE 'utf8_general_ci', `ConnIP` VARCHAR(20) NULL DEFAULT NULL COMMENT 'IP 번호(000.000.000.000)' COLLATE 'utf8_general_ci', `ConnPort` VARCHAR(10) NULL DEFAULT NULL COMMENT 'PORT 번호' COLLATE 'utf8_general_ci', `LastStatus` VARCHAR(10) NULL DEFAULT NULL COMMENT '마지막상태' COLLATE 'utf8_general_ci', `LastDate` VARCHAR(20) NULL DEFAULT NULL COMMENT '마지막시간(성공시)' COLLATE 'utf8_general_ci', `ErrorChk` INT(11) NULL DEFAULT '5' COMMENT '오류횟수(0:오류)', `GB_OBSV` CHAR(2) NULL DEFAULT NULL COMMENT '장비구분코드' COLLATE 'utf8_general_ci', `USE_YN` CHAR(2) NULL DEFAULT NULL COMMENT '사용유무(\'1\', \'0\')' COLLATE 'utf8_general_ci', `RainBit` DOUBLE NULL DEFAULT NULL COMMENT '데이터 배율', `SubOBCount` INT(11) NULL DEFAULT NULL COMMENT '센서 갯수', `DetCode` INT(11) NULL DEFAULT NULL COMMENT '방송장비 번호', `SeeLevelUse` VARCHAR(10) NULL DEFAULT NULL COMMENT '절대수위 사용유무' COLLATE 'utf8_general_ci', `LAT` DOUBLE NULL DEFAULT NULL COMMENT '위도', `LON` DOUBLE NULL DEFAULT NULL COMMENT '경도', `DTL_ADRES` VARCHAR(100) NULL DEFAULT NULL COMMENT '주소' COLLATE 'utf8_general_ci', `EType` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci', `SizeX` INT(11) NULL DEFAULT NULL, `SizeY` INT(11) NULL DEFAULT NULL, `URL` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci', `EComment` VARCHAR(400) NULL DEFAULT NULL COLLATE 'utf8_general_ci', `BDONG_CD` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci', `MNTN_ADRES_AT` CHAR(1) NULL DEFAULT NULL COLLATE 'utf8_general_ci', `MLNM` VARCHAR(4) NULL DEFAULT NULL COLLATE 'utf8_general_ci', `AULNM` VARCHAR(4) NULL DEFAULT NULL COLLATE 'utf8_general_ci', `RDNMADR_CD` VARCHAR(25) NULL DEFAULT NULL COLLATE 'utf8_general_ci', `RN_DTL_ADRES` VARCHAR(300) NULL DEFAULT NULL COLLATE 'utf8_general_ci', `SPO_NO_CD` VARCHAR(12) NULL DEFAULT NULL COLLATE 'utf8_general_ci', `ORGN_CD` CHAR(7) NULL DEFAULT NULL COLLATE 'utf8_general_ci', `DT_REGT` VARCHAR(14) NULL DEFAULT NULL COLLATE 'utf8_general_ci', `DT_UPT` VARCHAR(14) NULL DEFAULT NULL COLLATE 'utf8_general_ci', PRIMARY KEY (`CD_DIST_OBSV`) USING BTREE");
                        sb.Append(")");
                        sb.Append("COLLATE='utf8_general_ci' ");
                        sb.Append("ENGINE=InnoDB; ");
                        sb.Append("COMMIT;");

                        sql = sb.ToString();
                    }

                    mysql.ExecuteNonQuery(sql);

                    log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.{table})");
                }
            }
            catch (Exception ex)
            {
                log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table}): {ex.Message}");
                throw;
            }
        }

        public List<WB_EQUIP_VO> GetAreaList()
        {
            return Select(
                   "ConnModel IN('EWGATE_LAN') " +
                   "AND Use_YN IN('Y', '1')"
                   ).ToList();
        }

        public IEnumerable<WB_EQUIP_VO> Select(string where = "1=1", string order = "CD_DIST_OBSV", string limit = "1000")
        {
            List<WB_EQUIP_VO> list = new List<WB_EQUIP_VO>();

            // 표준
            // 01: 강우량계
            // 02: 수위계
            // 03: 변위계
            // 04: 토양함수비계
            // 05: 거리측정기
            // 06: 적설계
            // 07: 지하수위계
            // 08: 경사계
            // 09: 간극수압계
            // 10: 진동계
            // 11: 지중경사계
            // 12: 하중계
            // 13: 구조물경사계
            // 14: GNSS(GPS)
            // 15: 지표변위계
            // 16: 조위계

            // 20: 침수

            // 비표준 WOOBO
            // 17: 방송
            // 18: 전광판
            // 19: CCTV
            // 20: 차단기
            // 21: 침수

            try
            {
                base.where = where;
                base.order = order;
                base.limit = limit;

                DataTable dt = base.Select();
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        WB_EQUIP_VO vo = new WB_EQUIP_VO();
                        {
                            try
                            {
                                vo.Cd_dist_obsv = Convert.ToString(row["CD_DIST_OBSV"]);
                            }
                            catch { }

                            try
                            {
                                vo.Nm_dist_obsv = Convert.ToString(row["NM_DIST_OBSV"]);
                            }
                            catch { }

                            try
                            {
                                vo.ConnType = Convert.ToString(row["ConnType"]);
                            }
                            catch { }

                            try
                            {
                                vo.ConnModel = Convert.ToString(row["ConnModel"]);
                            }
                            catch { }

                            try
                            {
                                vo.ConnPhone = Convert.ToString(row["ConnPhone"]);
                            }
                            catch { }

                            try
                            {
                                vo.ConnIp = Convert.ToString(row["ConnIP"]);
                            }
                            catch { }

                            try
                            {
                                vo.ConnPort = Convert.ToString(row["ConnPort"]);
                            }
                            catch { }

                            try
                            {
                                if (row["LastDate"] is DateTime)
                                {
                                    vo.LastDate = Convert.ToDateTime(row["LastDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else vo.LastDate = Convert.ToString(row["LastDate"]);
                            }
                            catch { }

                            try
                            {
                                vo.LastStatus = Convert.ToString(row["LastStatus"]);
                            }
                            catch { }

                            try
                            {
                                vo.gb_obsv = Convert.ToString(row["GB_OBSV"]);
                            }
                            catch { }

                            try
                            {
                                vo.Use_YN = Convert.ToString(row["USE_YN"]);
                            }
                            catch { }

                            try
                            {
                                vo.RainBit = Convert.ToString(row["rainbit"]);
                            }
                            catch { }

                            try
                            {
                                vo.Lat = Convert.ToString(row["Lat"]);
                            }
                            catch { }
                        
                            try
                            {
                                vo.Lon = Convert.ToString(row["Lon"]);
                            }
                            catch { }

                            try
                            {
                                vo.DetCode = Convert.ToString(row["DetCode"]);
                            }
                            catch { }

                            try
                            {
                                vo.SubObCount = Convert.ToString(row["SubOBCount"]);
                            }
                            catch { }

                            try
                            {
                                vo.Data = Convert.ToString(row["Data"]);
                            }
                            catch { vo.Data = ""; }

                            try
                            {
                                vo.Dtl_adres = Convert.ToString(row["DTL_ADRES"]);
                            }
                            catch { }
                        }
                        list.Add(vo);
                    }
                }
            }
            catch
            {
                throw;
            }

            return list;
        }
    }
}
