using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_PARKSMSLIST_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_PARKSMSLIST_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_parksmslist";
        }

        public void Create()
        {
            try
            {
                string sql = $"SHOW TABLES LIKE '{table}'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    // 테이블 생성
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append($"CREATE TABLE `{table}` (");
                        sb.Append("`idx` INT(11) NOT NULL AUTO_INCREMENT,");
                        sb.Append("`CarNum` VARCHAR(15) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`CarPhone` VARCHAR(15) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`SMSContent` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`RegDate` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`EndDate` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`SendStatus` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`SendType` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("PRIMARY KEY(`idx`) USING BTREE)");
                        sb.Append("COLLATE = 'utf8_general_ci'");
                        sb.Append("ENGINE = InnoDB;");

                        sql = sb.ToString();
                    }

                    if (mysql.ExecuteNonQuery(sql) == -1)
                    {

                    }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.{table})\n{{ {sql} }}");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table}): {ex.Message}");
            }
        }

        public IEnumerable<WB_PARKSMSLIST_VO> Select(string where = "1=1", string order = "idx", string limit = "1000")
        {
            List<WB_PARKSMSLIST_VO> list = new List<WB_PARKSMSLIST_VO>();

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
                        WB_PARKSMSLIST_VO vo = new WB_PARKSMSLIST_VO();
                        {
                            try
                            {
                                vo.idx = Convert.ToInt16(row["idx"]); // AUTO
                            }
                            catch { }

                            try
                            {
                                vo.CarNum = Convert.ToString(row["CarNum"]);
                            }
                            catch { }

                            try
                            {
                                vo.CarPhone = Convert.ToString(row["CarPhone"]);
                            }
                            catch { }

                            try
                            {
                                vo.SMSContent = Convert.ToString(row["SMSContent"]);
                            }
                            catch { }

                            try
                            {
                                vo.RegDate = Convert.ToString(row["RegDate"]);
                            }
                            catch { }

                            try
                            {
                                vo.EndDate = Convert.ToString(row["EndDate"]);
                            }
                            catch { }

                            try
                            {
                                vo.SendStatus = Convert.ToString(row["SendStatus"]);
                            }
                            catch { }

                            try
                            {
                                vo.SendType = Convert.ToString(row["SendType"]);
                            }
                            catch { }

                            list.Add(vo);
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

        public int Insert(WB_PARKSMSLIST_VO vo)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table}" +
                      $"(CarNum, SMSContent, RegDate, SendStatus, SendType) " +
                      $"VALUES('{vo.CarNum}', '{vo.SMSContent}', '{vo.RegDate}', '{vo.SendStatus}', '{vo.SendType}') ";

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
