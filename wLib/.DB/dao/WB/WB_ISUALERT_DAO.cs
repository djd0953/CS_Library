using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace wLib.DB
{
    public class WB_ISUALERT_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_ISUALERT_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_isualert";
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
                        sb.Append("`AltCode` INT(11) NOT NULL AUTO_INCREMENT,");
                        sb.Append("`CD_DIST_OBSV` INT(11) NULL DEFAULT NULL,");
                        sb.Append("`EquType` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`RainTime` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`L1Use` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`L1Std` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`L2Use` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`L2Std` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`L3Use` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`L3Std` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`L4Use` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`L4Std` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`NowType` INT(11) NULL DEFAULT NULL,");
                        sb.Append("`ChkCount` INT(11) NULL DEFAULT NULL,");
                        sb.Append("PRIMARY KEY(`AltCode`) USING BTREE)");
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

        public IEnumerable<WB_ISUALERT_VO> Select(string where = "1=1", string order = "AltCode", string limit = "1000")
        {
            List<WB_ISUALERT_VO> list = new List<WB_ISUALERT_VO>();

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
                        WB_ISUALERT_VO vo = new WB_ISUALERT_VO();
                        {
                            try
                            {
                                vo.AltCode = !string.IsNullOrEmpty(Convert.ToString(row["AltCode"])) ? Convert.ToString(row["AltCode"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.Cd_dist_obsv = !string.IsNullOrEmpty(Convert.ToString(row["CD_DIST_OBSV"])) ? Convert.ToString(row["CD_DIST_OBSV"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.EquType= !string.IsNullOrEmpty(Convert.ToString(row["EquType"])) ? Convert.ToString(row["EquType"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.RainTime = !string.IsNullOrEmpty(Convert.ToString(row["RainTime"])) ? Convert.ToString(row["RainTime"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.NowType = !string.IsNullOrEmpty(Convert.ToString(row["NowType"])) ? Convert.ToInt16(row["NowType"]) : 0;
                            }
                            catch { }

                            try
                            {
                                vo.ChkCount = !string.IsNullOrEmpty(Convert.ToString(row["ChkCount"])) ? Convert.ToInt16(row["ChkCount"]) : 0;
                            }
                            catch { }

                            for(int i  = 1; i <= 4; i++)
                            {
                                try
                                {
                                    vo.Use[i] = !string.IsNullOrEmpty(Convert.ToString(row[$"L{i}Use"])) ? Convert.ToString(row[$"L{i}Use"]) : null;
                                }
                                catch { }

                                try
                                {
                                    vo.Std[i] = !string.IsNullOrEmpty(Convert.ToString(row[$"L{i}Std"])) ? Convert.ToString(row[$"L{i}Std"]) : null;
                                }
                                catch { }
                            }

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
