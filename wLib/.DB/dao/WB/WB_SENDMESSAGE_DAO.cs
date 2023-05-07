using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_SENDMESSAGE_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_SENDMESSAGE_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_sendmessage";
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
                        sb.Append(" `MsgCode` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'AUTO_PK', ");
                        sb.Append(" `SCode` INT(11) NULL DEFAULT NULL COMMENT 'wb_smslist.SCode', ");
                        sb.Append(" `PhoneNum` VARCHAR(20) NULL DEFAULT NULL COMMENT '수신번호', ");
                        sb.Append(" `SendMessage` VARCHAR(200) NULL DEFAULT NULL COMMENT 'wb_smslist.SMSContent', ");
                        sb.Append(" `SendStatus` VARCHAR(10) NULL DEFAULT NULL COMMENT '발신상태(start, ing, OK, fail, Error)', ");
                        sb.Append(" `RegDate` VARCHAR(20) NULL DEFAULT NULL COMMENT '등록시간', ");
                        sb.Append(" `RetDate` DATETIME NULL DEFAULT NULL COMMENT '처리시간', ");
                        sb.Append(" `dtmCreate` DATETIME NULL DEFAULT current_timestamp() COMMENT 'AUTO_CREATE', ");
                        sb.Append(" `dtmUpdate` DATETIME NULL DEFAULT NULL ON UPDATE current_timestamp() COMMENT 'AUTO_UPDATE', ");
                        sb.Append(" PRIMARY KEY (`MsgCode`) USING BTREE, ");
                        sb.Append(" INDEX `idx` (`RegDate`) USING BTREE ");
                        sb.Append(")");
                        sb.Append("COLLATE='utf8_general_ci' ");
                        sb.Append("ENGINE=InnoDB; ");
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
                throw;
            }
        }

        public IEnumerable<WB_SENDMESSAGE_VO> Select(string where = "1=1", string order = "MsgCode", string limit = "1000")
        {
            List<WB_SENDMESSAGE_VO> list = new List<WB_SENDMESSAGE_VO>();

            try
            {
                base.where = where;
                base.order = order;
                base.limit = limit;

                DataTable dt = base.Select();
                if (dt != null)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        WB_SENDMESSAGE_VO vo = new WB_SENDMESSAGE_VO();
                        {
                            try
                            {
                                vo.MsgCode = Convert.ToString(row["MsgCode"]); // AUTO
                            }
                            catch { }

                            try
                            {
                                vo.SCode = Convert.ToString(row["SCode"]);
                            }
                            catch { }

                            try
                            {
                                vo.PhoneNum = Convert.ToString(row["PhoneNum"]);
                            }
                            catch { }

                            try
                            {
                                vo.SendMessage = Convert.ToString(row["SendMessage"]);
                            }
                            catch { }

                            try
                            {
                                vo.SendStatus = Convert.ToString(row["SendStatus"]);
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
                                if (row["RetDate"] is DateTime)
                                {
                                    vo.RetDate = Convert.ToDateTime(row["RetDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else vo.RetDate = Convert.ToString(row["RetDate"]);
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

        public int Insert(WB_SENDMESSAGE_VO vo)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table} " +
                      $"(PhoneNum, SendMessage, SendStatus, RegDate) " +
                      $"VALUES('{vo.PhoneNum}', '{vo.SendMessage}', '{vo.SendStatus}', '{vo.RegDate}') ";

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
