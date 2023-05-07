using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace wLib.DB
{
    public class WB_SMSUSER_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_SMSUSER_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_smsuser";
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
	                    sb.Append("`GCode` INT(11) NOT NULL AUTO_INCREMENT,");
	                    sb.Append("`GMCode` INT(11) NULL DEFAULT NULL,");
	                    sb.Append("`GSCode` INT(11) NULL DEFAULT NULL,");
	                    sb.Append("`UName` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`Organ` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`Division` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`Fax` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`Phone` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`UPosition` VARCHAR(30) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`Sex` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`Address` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`Commeet` VARCHAR(400) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("PRIMARY KEY(`GCode`) USING BTREE");
                        sb.Append(")");
                        sb.Append("COLLATE = 'utf8_general_ci'");
                        sb.Append("ENGINE = InnoDB;");
                        sb.Append("COMMIT;");

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

        public IEnumerable<WB_SMSUSER_VO> Select(string where = "1=1", string order = "GCode", string limit = "1000")
        {
            List<WB_SMSUSER_VO> list = new List<WB_SMSUSER_VO>();

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
                        WB_SMSUSER_VO vo = new WB_SMSUSER_VO();
                        {
                            try
                            {
                                vo.GCode = Convert.ToString(row["GCode"]); // AUTO
                            }
                            catch { }

                            try
                            {
                                vo.UName = Convert.ToString(row["UName"]);
                            }
                            catch { }

                            try
                            {
                                vo.Division = Convert.ToString(row["Division"]);
                            }
                            catch { }

                            try
                            {
                                vo.Phone = Convert.ToString(row["Phone"]);
                            }
                            catch { }

                            try
                            {
                                vo.UPosition = Convert.ToString(row["UPosition"]);
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
