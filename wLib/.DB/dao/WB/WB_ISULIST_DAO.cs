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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Markup;

namespace wLib.DB
{
    public class WB_ISULIST_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_ISULIST_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_isulist";
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
                        sb.Append("`IsuCode` INT(11) NOT NULL AUTO_INCREMENT,");
                        sb.Append("`GCode` INT(11) NULL DEFAULT NULL,");
                        sb.Append("`IsuKind` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`IsuSrtAuto` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`IsuSrtDate` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`IsuEndAuto` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`IsuEndDate` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`Occur` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`Equip` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`SMS` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`IStatus` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`Send` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("`HAOK` VARCHAR(10) NULL DEFAULT 'E' COLLATE 'utf8_general_ci',");
                        sb.Append("PRIMARY KEY(`IsuCode`) USING BTREE,");
                        sb.Append("INDEX `GCode_IsuKind_IsuSrtDate` (`GCode`, `IsuKind`, `IsuSrtDate`) USING BTREE,");
                        sb.Append("CONSTRAINT `FK_wb_isulist_wb_isualertgroup` FOREIGN KEY(`GCode`) REFERENCES `wb_isualertgroup` (`GCode`) ON UPDATE CASCADE ON DELETE SET NULL)");
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

        public IEnumerable<WB_ISULIST_VO> Select(string where = "1=1", string order = "IsuCode", string limit = "1000")
        {
            List<WB_ISULIST_VO> list = new List<WB_ISULIST_VO>();

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
                        WB_ISULIST_VO vo = new WB_ISULIST_VO();
                        {
                            try
                            {
                                vo.IsuCode = !string.IsNullOrEmpty(Convert.ToString(row["IsuCode"])) ? Convert.ToString(row["IsuCode"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.GCode = !string.IsNullOrEmpty(Convert.ToString(row["GCode"])) ? Convert.ToString(row["GCode"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.IsuKind= !string.IsNullOrEmpty(Convert.ToString(row["IsuKind"])) ? Convert.ToString(row["IsuKind"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.IsuSrtAuto = !string.IsNullOrEmpty(Convert.ToString(row["IsuSrtAuto"])) ? Convert.ToString(row["IsuSrtAuto"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.IsuSrtDate = !string.IsNullOrEmpty(Convert.ToString(row["IsuSrtDate"])) ? Convert.ToString(row["IsuSrtDate"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.IsuEndAuto = !string.IsNullOrEmpty(Convert.ToString(row["IsuEndAuto"])) ? Convert.ToString(row["IsuEndAuto"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.IsuEndDate = !string.IsNullOrEmpty(Convert.ToString(row["IsuEndDate"])) ? Convert.ToString(row["IsuEndDate"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.Occur = !string.IsNullOrEmpty(Convert.ToString(row["Occur"])) ? Convert.ToString(row["Occur"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.Equip = !string.IsNullOrEmpty(Convert.ToString(row["Equip"])) ? Convert.ToString(row["Equip"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.SMS = !string.IsNullOrEmpty(Convert.ToString(row["SMS"])) ? Convert.ToString(row["SMS"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.IStatus = !string.IsNullOrEmpty(Convert.ToString(row["IStatus"])) ? Convert.ToString(row["IStatus"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.Send = !string.IsNullOrEmpty(Convert.ToString(row["Send"])) ? Convert.ToString(row["Send"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.HAOK = !string.IsNullOrEmpty(Convert.ToString(row["HAOK"])) ? Convert.ToString(row["HAOK"]) : null;
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

        public int Insert(WB_ISULIST_VO vo)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table}" +
                      $"(GCode, IsuKind, IsuSrtAuto, IsuSrtDate, IsuEndAuto, IsuEndDate, Occur, Equip, SMS, IStatus, Send, HAOK) " +
                      $"VALUES({vo.GCode}, '{vo.IsuKind}', '{vo.IsuSrtAuto}', '{vo.IsuSrtDate}', '{vo.IsuEndAuto}', '{vo.IsuEndDate}', '{vo.Occur}', '{vo.Equip}', '{vo.SMS}', '{vo.IStatus}', '{vo.Send}', '{vo.HAOK}') ";

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
