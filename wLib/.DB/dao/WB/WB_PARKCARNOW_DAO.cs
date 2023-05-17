using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_PARKCARNOW_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_PARKCARNOW_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_parkcarnow";
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
                        sb.Append("`GateDate` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`GateSerial` VARCHAR(4) NULL DEFAULT '1000' COMMENT 'LPR Gate Serial Code' COLLATE 'utf8_general_ci',");
                        sb.Append("`CarNum` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`CarNum_Img` MEDIUMTEXT NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`CarNum_Imgname` VARCHAR(50) NULL DEFAULT NULL COMMENT 'car num img filename' COLLATE 'utf8_general_ci',");
                        sb.Append("PRIMARY KEY(`idx`) USING BTREE )");
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

        public IEnumerable<WB_PARKCARNOW_VO> Select(string where = "1=1", string order = "idx", string limit = "1000")
        {
            List<WB_PARKCARNOW_VO> list = new List<WB_PARKCARNOW_VO>();

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
                        WB_PARKCARNOW_VO vo = new WB_PARKCARNOW_VO();
                        {
                            try
                            {
                                vo.idx = Convert.ToInt32(row["idx"]); // AUTO
                            }
                            catch { }

                            try
                            {
                                vo.GateDate = Convert.ToString(row["GateDate"]);
                            }
                            catch { }

                            try
                            {
                                vo.GateSerial = Convert.ToString(row["GateSerial"]);
                            }
                            catch { }

                            try
                            {
                                vo.CarNum = Convert.ToString(row["CarNum"]);
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
