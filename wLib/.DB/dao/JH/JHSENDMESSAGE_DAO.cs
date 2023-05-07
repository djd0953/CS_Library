using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB.dao
{
    public class JHSENDMESSAGE_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public JHSENDMESSAGE_DAO(MYSQL_T mysql)
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
                        sb.Append(" `PhoneNum` VARCHAR(20) NULL DEFAULT NULL COMMENT '수신번호', ");
                        sb.Append(" `SendMessage` VARCHAR(200) NULL DEFAULT NULL, ");
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
    }
}
