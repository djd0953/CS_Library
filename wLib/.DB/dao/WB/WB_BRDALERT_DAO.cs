using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_BRDALERT_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_BRDALERT_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_brdalert";
        }

        // TODO 
        // 미확인
        public void Create()
        {
            string sql;
            int rtv = 0;

            try
            {
                sql = $"SHOW TABLES LIKE '{table}'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append($"CREATE TABLE `{table}` ");
                        sb.Append("(");
                        sb.Append(" `AltCode` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'AUTO_PK', ");
                        sb.Append(" `Title` VARCHAR(200) NULL DEFAULT NULL, ");
                        sb.Append("	`Content` VARCHAR(400) NULL DEFAULT NULL, ");
                        sb.Append("	`CStatus` VARCHAR(10) NULL DEFAULT NULL, ");
                        sb.Append("PRIMARY KEY (`AltCode`), ");
                        sb.Append(") ENGINE = InnoDB DEFAULT CHARSET = utf8; ");
                        sb.Append("COMMIT;");

                        sql = sb.ToString();
                    }

                    rtv = mysql.ExecuteNonQuery(sql);

                    if (rtv == -1)
                        log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table}): {sql}");
                    else log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.{table})");

                }
            }
            catch (Exception ex)
            {
                log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 에러({mysql.Ip}:{mysql.Port}.{table}): {ex.Message}");
                throw;
            }
        }

        public IEnumerable<WB_BRDALERT_VO> Select(string where = "1=1", string order = "AltCode", string limit = "1000")
        {
            List<WB_BRDALERT_VO> list = new List<WB_BRDALERT_VO>();

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
                        WB_BRDALERT_VO vo = new WB_BRDALERT_VO();
                        {
                            try
                            {
                                vo.AltCode = Convert.ToString(row["AltCode"]); // AUTO
                            }
                            catch { }

                            try
                            {
                                vo.Title = Convert.ToString(row["Title"]);
                            }
                            catch { }

                            try
                            {
                                vo.Content = Convert.ToString(row["Content"]);
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

        public int Insert(WB_BRDALERT_VO vo)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table}" +
                      $"(Cd_dist_obsv, Cid, CStatus, RegDate) " +
                      $"VALUES('{vo.Title}', '{vo.Content}') ";

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
