using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_BRDLIST_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_BRDLIST_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_brdlist";
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
                        sb.Append(@"`BCode` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'AUTO_PK', `CD_DIST_OBSV` VARCHAR(200) NULL DEFAULT NULL COMMENT 'CD_DIST_OBSV[] 구분자(\',\')', `Title` VARCHAR(100) NULL DEFAULT NULL COMMENT '제목', `BType` VARCHAR(10) NULL DEFAULT NULL COMMENT '방송타입(""general"", ""reserve"", ""level1-4"")', `BrdType` VARCHAR(10) NULL DEFAULT NULL COMMENT '멘트타입(""alert"", ""tts"")', `AltMent` VARCHAR(10) NULL DEFAULT NULL COMMENT 'BrdType(""alert"") 방송내용(0-9)', `TTSContent` VARCHAR(200) NULL DEFAULT NULL COMMENT 'BrdType(""tts"") 방송내용', `RevType` VARCHAR(10) NULL DEFAULT NULL COMMENT '예약구분(""now"", ""reserve"", ""reserved"")', `BrdDate` VARCHAR(20) NULL DEFAULT NULL COMMENT '송출시간', `BRepeat` VARCHAR(10) NULL DEFAULT NULL COMMENT '반복횟수(1-9)', `IsuCode` INT(11) NULL DEFAULT NULL COMMENT 'wb_isulist.IsuCode', `RegDate` VARCHAR(20) NULL DEFAULT NULL COMMENT '등록시간', `dtmCreate` DATETIME NULL DEFAULT current_timestamp(), `dtmUpdate` DATETIME NULL DEFAULT NULL ON UPDATE current_timestamp(), PRIMARY KEY (`BCode`) USING BTREE");
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

        public IEnumerable<WB_BRDLIST_VO> Select(string where = "1=1", string order = "BCode", string limit = "1000")
        {
            List<WB_BRDLIST_VO> list = new List<WB_BRDLIST_VO>();

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
                        try
                        {
                            WB_BRDLIST_VO vo = new WB_BRDLIST_VO();

                            try
                            {
                                vo.BCode = Convert.ToString(row["BCode"]); // AUTO
                            }
                            catch { }

                            try
                            {
                                vo.Cd_dist_obsv = Convert.ToString(row["CD_DIST_OBSV"]);
                            }
                            catch { }

                            try
                            {
                                vo.Title = Convert.ToString(row["Title"]);
                            }
                            catch { }

                            try
                            {
                                vo.BType = Convert.ToString(row["BType"]);
                                
                            }
                            catch { }

                            try
                            {
                                vo.BrdType = Convert.ToString(row["BrdType"]);
                            }
                            catch { }

                            try
                            {
                                vo.AltMent = Convert.ToString(row["AltMent"]);
                            }
                            catch { }

                            try
                            {
                                vo.TTSContent = Convert.ToString(row["TTSContent"]);
                            }

                            catch { }

                            try
                            {
                                vo.RevType = Convert.ToString(row["RevType"]);
                            }
                            catch { }

                            try
                            {
                                vo.BrdDate = Convert.ToString(row["BrdDate"]);
                            }
                            catch { }

                            try
                            {
                                vo.BRepeat = Convert.ToString(row["BRepeat"]);
                            }
                            catch { }

                            try
                            {
                                vo.RegDate = Convert.ToString(row["RegDate"]);
                            }
                            catch { }

                            try
                            {
                                vo.IsuCode = Convert.ToString(row["IsuCode"]);
                            }
                            catch { }


                            list.Add(vo);
                        }
                        catch
                        {
                            continue;
                        }
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
