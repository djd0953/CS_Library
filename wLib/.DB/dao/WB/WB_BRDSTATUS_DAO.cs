using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_BRDSTATUS_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_BRDSTATUS_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_brdstatus";
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
                        sb.Append("`CD_DIST_OBSV` VARCHAR(10) NOT NULL COMMENT 'wb_equip.CD_DIST_OBSV', ");
                        sb.Append("`Volume` VARCHAR(50) NULL DEFAULT NULL, ");
                        sb.Append("`Output` VARCHAR(20) NULL DEFAULT NULL, ");
                        sb.Append("`Relay` VARCHAR(3) NULL DEFAULT NULL, ");
                        sb.Append("`Bell` VARCHAR(30) NULL DEFAULT NULL, ");
                        sb.Append("`LastSync` VARCHAR(10) NULL DEFAULT NULL, ");
                        sb.Append("`BStatus` VARCHAR(10) NULL DEFAULT NULL, ");
                        sb.Append("`UDate` VARCHAR(20) NULL DEFAULT NULL, ");
                        sb.Append("`dtmCreate` DATETIME NULL DEFAULT current_timestamp() COMMENT 'AUTO_CREATE', ");
                        sb.Append("`dtmUpdate` DATETIME NULL DEFAULT null ON UPDATE current_timestamp() COMMENT 'AUTO_UPDATE', ");
                        sb.Append("PRIMARY KEY (`CD_DIST_OBSV`) USING BTREE, ");
                        sb.Append("CONSTRAINT `FK_wb_brdstatus_wb_equip` FOREIGN KEY (`CD_DIST_OBSV`) REFERENCES `wb_equip` (`CD_DIST_OBSV`) ON UPDATE CASCADE ON DELETE CASCADE ");
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

        public IEnumerable<WB_BRDSTATUS_VO> Select(string where = "1=1", string order = "CD_DIST_OBSV", string limit = "1000")
        {
            List<WB_BRDSTATUS_VO> list = new List<WB_BRDSTATUS_VO>();

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
                        WB_BRDSTATUS_VO vo = new WB_BRDSTATUS_VO();
                        {
                            try
                            {
                                vo.Cd_dist_obsv = Convert.ToString(row["CD_DIST_OBSV"]);
                            }
                            catch { }

                            try
                            {
                                vo.Output = Convert.ToString(row["Output"]);
                            }
                            catch { }

                            try
                            {
                                vo.Volume = Convert.ToString(row["Volume"]);
                            }
                            catch { }

                            try
                            {
                                vo.Bell = Convert.ToString(row["Bell"]);
                            }
                            catch { }

                            try
                            {
                                vo.LastSync = Convert.ToString(row["LastSync"]);
                            }
                            catch { }

                            try
                            {
                                vo.Relay = Convert.ToString(row["Relay"]);
                            }
                            catch { }

                            try
                            {
                                vo.BStatus = Convert.ToString(row["BStatus"]);
                            }
                            catch { }

                            try
                            {
                                vo.UDate = Convert.ToString(row["UDate"]);
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

        public int Insert(WB_BRDSTATUS_VO vo, bool update = false)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table}(CD_DIST_OBSV, Output, Volume, Bell, LastSync, Relay, BStatus, UDate ) " +
                      $"VALUES('{vo.Cd_dist_obsv}', '{vo.Output}', '{vo.Volume}', '{vo.Bell}', '{vo.LastSync}', '{vo.Relay}', '{vo.BStatus}', '{vo.UDate}') ";

                if (update)
                {
                    sql += $"ON DUPLICATE KEY UPDATE " +
                           $"Output = '{vo.Output}', " +
                           $"Volume = '{vo.Volume}', " +
                           $"Bell = '{vo.Bell}', " +
                           $"LastSync = '{vo.LastSync}', " +
                           $"Relay = '{vo.Relay}', " +
                           $"BStatus = '{vo.BStatus}', " +
                           $"UDate = '{vo.UDate}' ";
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
