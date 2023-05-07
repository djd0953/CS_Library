using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_BRDSTATUS_DTMF_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_BRDSTATUS_DTMF_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_brdstatus_dtmf";
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
                        sb.Append("`SendCode` int NOT NULL COMMENT 'wb_equip.CD_DIST_OBSV', ");
                        sb.Append("`CD_DIST_OBSV` VARCHAR(10) NOT NULL COMMENT 'wb_equip.CD_DIST_OBSV', ");
                        sb.Append("``BrdStatus` VARCHAR(1) NULL DEFAULT NULL, ");
                        sb.Append("`AmpPower` VARCHAR(20) NULL DEFAULT NULL, ");
                        sb.Append("`Speaker` VARCHAR(3) NULL DEFAULT NULL, ");
                        sb.Append("`DCPower` VARCHAR(30) NULL DEFAULT NULL, ");
                        sb.Append("`RFPower` VARCHAR(10) NULL DEFAULT NULL, ");
                        sb.Append("`AmpLevel` VARCHAR(10) NULL DEFAULT NULL, ");
                        sb.Append("`DCLevel` VARCHAR(20) NULL DEFAULT NULL, ");
                        sb.Append("`RetDate` VARCHAR(20) NULL DEFAULT NULL, ");
                        sb.Append("`dtmCreate` DATETIME NULL DEFAULT current_timestamp() COMMENT 'AUTO_CREATE', ");
                        sb.Append("`dtmUpdate` DATETIME NULL DEFAULT null ON UPDATE current_timestamp() COMMENT 'AUTO_UPDATE', ");
                        sb.Append("PRIMARY KEY (`SendCode`) USING BTREE, ");
                        sb.Append("INDEX `CD_DIST_OBSV` (`CD_DIST_OBSV`) USING BTREE, ");
                        sb.Append("CONSTRAINT `FK_wb_brdstatus_dtmf_wb_equip` FOREIGN KEY (`CD_DIST_OBSV`) REFERENCES `wb_equip` (`CD_DIST_OBSV`) ON UPDATE CASCADE ON DELETE CASCADE ");
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

        public IEnumerable<WB_BRDSTATUS_DTMF_VO> Select(string where = "1=1", string order = "CD_DIST_OBSV", string limit = "1000")
        {
            List<WB_BRDSTATUS_DTMF_VO> list = new List<WB_BRDSTATUS_DTMF_VO>();

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
                        WB_BRDSTATUS_DTMF_VO vo = new WB_BRDSTATUS_DTMF_VO();
                        {
                            try
                            {
                                vo.SendCode = Convert.ToString(row["SendCode"]);
                            }
                            catch { }

                            try
                            {
                                vo.Cd_dist_obsv = Convert.ToString(row["CD_DIST_OBSV"]);
                            }
                            catch { }

                            try
                            {
                                vo.BrdStatus = Convert.ToString(row["BrdStatus"]);
                            }
                            catch { }

                            try
                            {
                                vo.AmpPower = Convert.ToString(row["AmpPower"]);
                            }
                            catch { }

                            try
                            {
                                vo.Speaker = Convert.ToString(row["Speaker"]);
                            }
                            catch { }

                            try
                            {
                                vo.DCPower = Convert.ToString(row["DCPower"]);
                            }
                            catch { }

                            try
                            {
                                vo.RFPower = Convert.ToString(row["RFPower"]);
                            }
                            catch { }

                            try
                            {
                                vo.AmpLevel = Convert.ToString(row["AmpLevel"]);
                            }
                            catch { }

                            try
                            {
                                vo.DCLevel = Convert.ToString(row["DCLevel"]);
                            }
                            catch { }

                            try
                            {
                                vo.RetDate = Convert.ToString(row["RetDate"]);
                            }
                            catch { }
                        };

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

        public int Insert(WB_BRDSTATUS_DTMF_VO vo, bool update = false)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table}(SendCode, CD_DIST_OBSV, BrdStatus, AmpPower, Speaker, DCPower, RFPower, AmpLevel, DCLevel, RetDate) " +
                      $"VALUES('{vo.SendCode}', '{vo.Cd_dist_obsv}', '{vo.BrdStatus}', '{vo.AmpPower}', '{vo.Speaker}', '{vo.DCPower}', '{vo.RFPower}', '{vo.AmpLevel}', '{vo.DCLevel}', '{vo.RetDate}') ";

                if (update)
                {
                    sql += $"ON DUPLICATE KEY UPDATE " +
                           $"CD_DIST_OBSV = '{vo.Cd_dist_obsv}', "+
                           $"BrdStatus = '{vo.BrdStatus}', " +
                           $"Volume = '{vo.AmpPower}', " +
                           $"Speaker = '{vo.Speaker}', " +
                           $"DCPower = '{vo.DCPower}', " +
                           $"RFPower = '{vo.RFPower}', " +
                           $"AmpLevel = '{vo.AmpLevel}', " +
                           $"DCLevel = '{vo.DCLevel}' " +
                           $"RetDate = '{vo.RetDate}'";
                }

                rtv = base.Insert(sql);
            }
            catch
            {
                throw;
            }

            return rtv;
        }
    }
}
