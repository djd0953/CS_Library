using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_DISPLAY_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_DISPLAY_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_display";
        }

        public void CREATE()
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
                        sb.Append(@"`DisCode` INT(11) NOT NULL AUTO_INCREMENT,
	                                `CD_DIST_OBSV` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	                                `SaveType` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	                                `DisEffect` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	                                `DisSpeed` VARCHAR(10) NOT NULL COLLATE 'utf8_general_ci',
	                                `DisTime` INT(11) NOT NULL,
	                                `EndEffect` VARCHAR(10) NOT NULL COLLATE 'utf8_general_ci',
	                                `EndSpeed` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	                                `StrTime` VARCHAR(20) NOT NULL COLLATE 'utf8_general_ci',
	                                `EndTime` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	                                `Relay` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	                                `ViewImg` VARCHAR(150) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	                                `SendImg` VARCHAR(150) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	                                `HtmlData` VARCHAR(2000) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	                                `ViewOrder` INT(11) NULL DEFAULT NULL,
	                                `DisType` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	                                `Exp_YN` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	                                `RegDate` VARCHAR(20) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
                                    `dtmCreate` DATETIME NULL DEFAULT current_timestamp() COMMENT 'AUTO_CREATE',
	                                `dtmUpdate` DATETIME NULL DEFAULT NULL ON UPDATE current_timestamp() COMMENT 'AUTO_UPDATE',
	                                PRIMARY KEY (`DisCode`) USING BTREE,
	                                INDEX `FK_wb_display_wb_equip` (`CD_DIST_OBSV`) USING BTREE,
	                                CONSTRAINT `FK_wb_display_wb_equip` FOREIGN KEY (`CD_DIST_OBSV`) REFERENCES `wb_equip` (`CD_DIST_OBSV`) ON UPDATE CASCADE ON DELETE NO ACTION");
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

        public IEnumerable<WB_DISPLAY_VO> Select(string where = "1=1", string order = "DisCode", string limit = "1000")
        {
            List<WB_DISPLAY_VO> list = new List<WB_DISPLAY_VO>();

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
                        WB_DISPLAY_VO vo = new WB_DISPLAY_VO();
                        {
                            try
                            {
                                vo.DisCode = Convert.ToString(row["DisCode"]); // AUTO
                            }
                            catch { }

                            try
                            {
                                vo.CD_DIST_OBSV = !string.IsNullOrEmpty(Convert.ToString(row["CD_DIST_OBSV"])) ? Convert.ToString(row["CD_DIST_OBSV"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.SaveType = !string.IsNullOrEmpty(Convert.ToString(row["SaveType"])) ? Convert.ToString(row["SaveType"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.DisEffect = !string.IsNullOrEmpty(Convert.ToString(row["DisEffect"])) ? Convert.ToString(row["DisEffect"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.DisSpeed = !string.IsNullOrEmpty(Convert.ToString(row["DisSpeed"])) ? Convert.ToString(row["DisSpeed"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.DisTime = !string.IsNullOrEmpty(Convert.ToString(row["DisTime"])) ? Convert.ToString(row["DisTime"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.EndEffect = !string.IsNullOrEmpty(Convert.ToString(row["EndEffect"])) ? Convert.ToString(row["EndEffect"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.EndSpeed = !string.IsNullOrEmpty(Convert.ToString(row["EndSpeed"])) ? Convert.ToString(row["EndSpeed"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.StrTime = !string.IsNullOrEmpty(Convert.ToString(row["StrTime"])) ? Convert.ToString(row["StrTime"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.EndTime = !string.IsNullOrEmpty(Convert.ToString(row["EndTime"])) ? Convert.ToString(row["EndTime"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.Relay = !string.IsNullOrEmpty(Convert.ToString(row["Relay"])) ? Convert.ToString(row["Relay"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.ViewImg = !string.IsNullOrEmpty(Convert.ToString(row["ViewImg"])) ? Convert.ToString(row["ViewImg"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.SendImg = !string.IsNullOrEmpty(Convert.ToString(row["SendImg"])) ? Convert.ToString(row["SendImg"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.HtmlData = !string.IsNullOrEmpty(Convert.ToString(row["HtmlData"])) ? Convert.ToString(row["HtmlData"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.ViewOrder = !string.IsNullOrEmpty(Convert.ToString(row["ViewOrder"])) ? Convert.ToString(row["ViewOrder"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.DisType = !string.IsNullOrEmpty(Convert.ToString(row["DisType"])) ? Convert.ToString(row["DisType"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.Exp_YN = !string.IsNullOrEmpty(Convert.ToString(row["Exp_YN"])) ? Convert.ToString(row["Exp_YN"]) : null;
                            }
                            catch { }

                            try
                            {
                                vo.RegDate = !string.IsNullOrEmpty(Convert.ToString(row["RegDate"])) ? Convert.ToString(row["RegDate"]) : null;
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
