using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_BRDSEND_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_BRDSEND_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_brdsend";
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
                        sb.Append("`SendCode` INT(11) NOT NULL AUTO_INCREMENT, ");
                        sb.Append("`CD_DIST_OBSV` VARCHAR(4) NULL DEFAULT NULL COLLATE 'utf8_general_ci', ");
                        sb.Append("`RCMD` VARCHAR(5) NULL DEFAULT NULL COLLATE 'utf8_general_ci', ");
                        sb.Append("`Parm1` VARCHAR(50) NOT NULL COLLATE 'utf8_general_ci', ");
                        sb.Append("`Parm2` VARCHAR(20) NOT NULL COLLATE 'utf8_general_ci', ");
                        sb.Append("`Parm3` VARCHAR(200) NOT NULL COLLATE 'utf8_general_ci', ");
                        sb.Append("`Parm4` VARCHAR(50) NOT NULL COLLATE 'utf8_general_ci', ");
                        sb.Append("`BStatus` VARCHAR(10) NOT NULL COLLATE 'utf8_general_ci', ");
                        sb.Append("`RegDate` DATETIME NULL DEFAULT NULL, ");
                        sb.Append("`RetData` VARCHAR(100) NULL DEFAULT NULL COLLATE 'utf8_general_ci', ");
                        sb.Append("`RetDate` DATETIME NULL DEFAULT NULL, ");
                        sb.Append("`dtmCreate` DATETIME NULL DEFAULT current_timestamp(), ");
                        sb.Append("`dtmUpdate` DATETIME NULL DEFAULT null ON UPDATE current_timestamp(), ");
                        sb.Append("PRIMARY KEY (`SendCode`) USING BTREE, ");
                        sb.Append("INDEX `idx` (`RegDate`) USING BTREE ");
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

        public IEnumerable<WB_BRDSEND_VO> Select(string where = "1=1", string order = "SendCode", string limit = "1000")
        {
            List<WB_BRDSEND_VO> list = new List<WB_BRDSEND_VO>();

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
                        WB_BRDSEND_VO vo = new WB_BRDSEND_VO();
                        {
                            try
                            {
                                vo.SendCode = Convert.ToString(row["SendCode"]); // AUTO
                            }
                            catch { }

                            try
                            {
                                vo.Cd_dist_obsv = Convert.ToString(row["CD_DIST_OBSV"]);
                            }
                            catch { }

                            try
                            {
                                vo.Rcmd = Convert.ToString(row["Rcmd"]);
                            }
                            catch { }

                            try
                            {
                                vo.Parm1 = Convert.ToString(row["Parm1"]);
                            }
                            catch { }
                            
                            try
                            {
                                vo.Parm2 = Convert.ToString(row["Parm2"]);
                            }
                            catch { }

                            try
                            {
                                vo.Parm3 = Convert.ToString(row["Parm3"]);
                            }
                            catch { }

                            try
                            {
                                vo.Parm4 = Convert.ToString(row["Parm4"]);
                            }
                            catch { }

                            try
                            {
                                if (row["RegDate"] is DateTime)
                                {
                                    vo.RegDate = Convert.ToDateTime(row["RegDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else vo.RegDate = Convert.ToString(row["RegDate"]);
                            }
                            catch { }

                            try
                            {
                                vo.BStatus = Convert.ToString(row["BStatus"]);
                            }
                            catch { }

                            try
                            {
                                if (row["RetDate"] is DateTime)
                                {
                                    vo.RetDate = Convert.ToDateTime(row["RetDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else vo.RetDate = Convert.ToString(row["RetDate"]);
                            }
                            catch { }

                            try
                            {
                                vo.RetData = Convert.ToString(row["RetData"]);
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

        // TODO RetDate
        public int Insert(WB_BRDSEND_VO vo)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table}" +
                      $"(CD_DIST_OBSV, RCMD, Parm1, Parm2, Parm3, Parm4, RegDate, BStatus, RetData) " +
                      $"VALUES('{vo.Cd_dist_obsv}', '{vo.Rcmd}', '{vo.Parm1}', '{vo.Parm2}', '{vo.Parm3}', '{vo.Parm4}', '{vo.RegDate}', '{vo.BStatus}', '{vo.RetData}') ";

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
