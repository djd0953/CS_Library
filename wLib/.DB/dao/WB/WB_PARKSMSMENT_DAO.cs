using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_PARKSMSMENT_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_PARKSMSMENT_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_parksmsment";
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
                        sb.Append("`SMSMentCode` INT(11) NOT NULL AUTO_INCREMENT,");
                        sb.Append("`Title` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`Content` VARCHAR(58) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("PRIMARY KEY(`SMSMentCode`) USING BTREE )");
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

        public IEnumerable<WB_PARKSMSMENT_VO> Select(string where = "1=1", string order = "SMSMentCode", string limit = "1000")
        {
            List<WB_PARKSMSMENT_VO> list = new List<WB_PARKSMSMENT_VO>();

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
                        WB_PARKSMSMENT_VO vo = new WB_PARKSMSMENT_VO();
                        {
                            try
                            {
                                vo.SMSMentCode = Convert.ToString(row["MentCode"]); // AUTO
                            }
                            catch { }

                            try
                            {
                                vo.Title = Convert.ToString(row["Title"]); // AUTO
                            }
                            catch { }

                            try
                            {
                                vo.Content = Convert.ToString(row["Content"]); // AUTO
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
    }
}
