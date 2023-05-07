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
    public class WB_ISUALERTGROUP_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_ISUALERTGROUP_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_isualertgroup";
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
                        sb.Append("`GName` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`AltCode` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`AdmSMS` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`FloodSMSAuto1` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`Auto1` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`Equip1` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`SMS1` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`FloodSMSAuto2` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`Auto2` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`Equip2` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`SMS2` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`FloodSMSAuto3` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`Auto3` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`Equip3` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`SMS3` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`FloodSMSAuto4` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`Auto4` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`Equip4` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`SMS4` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`AltDate` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`AltUse` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("PRIMARY KEY(`GCode`) USING BTREE)");
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

        public IEnumerable<WB_ISUALERTGROUP_VO> Select(string where = "1=1", string order = "AltCode", string limit = "1000")
        {
            List<WB_ISUALERTGROUP_VO> list = new List<WB_ISUALERTGROUP_VO>();

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
                        WB_ISUALERTGROUP_VO vo = new WB_ISUALERTGROUP_VO();
                        {
                            try
                            {
                                vo.GCode = DBNull.Value != row["GCode"] ? Convert.ToString(row["GCode"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.GName = DBNull.Value != row["GName"] ? Convert.ToString(row["GName"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.AltDate = DBNull.Value != row["AltDate"] ? Convert.ToString(row["AltDate"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.AltUse = DBNull.Value != row["AltDate"] ? Convert.ToString(row["AltUse"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.AdmSMS = DBNull.Value != row["AdmSMS"] ? Convert.ToString(row["AdmSMS"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.AltCode = DBNull.Value != row["AltCode"] ? Convert.ToString(row["AltCode"]) : null;
                            }
                            catch { }

                            for(int i  = 1; i <= 4; i++)
                            { 
                                try
                                {
                                    vo.FloodSMSAuto[i] = !string.IsNullOrEmpty(Convert.ToString(row[$"FloodSMSAuto{i}"])) ? Convert.ToString(row[$"FloodSMSAuto{i}"]) : null;
                                }
                                catch { }

                                try
                                {
                                    vo.Auto[i] = !string.IsNullOrEmpty(Convert.ToString(row[$"Auto{i}"])) ? Convert.ToString(row[$"Auto{i}"]) : null;
                                }
                                catch { }

                                try
                                {
                                    vo.Equip[i] = !string.IsNullOrEmpty(Convert.ToString(row[$"Equip{i}"])) ? Convert.ToString(row[$"Equip{i}"]) : null;
                                }
                                catch { }

                                try
                                {
                                    vo.SMS[i] = !string.IsNullOrEmpty(Convert.ToString(row[$"SMS{i}"])) ? Convert.ToString(row[$"SMS{i}"]) : null;
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
