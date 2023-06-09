﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_ISUMENT_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_ISUMENT_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_isument";
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
	                    sb.Append("`MentCode` INT(11) NOT NULL AUTO_INCREMENT,");
	                    sb.Append("`BrdMent1` VARCHAR(500) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`BrdMent2` VARCHAR(500) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`BrdMent3` VARCHAR(500) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`BrdMent4` VARCHAR(500) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`DisMent1` VARCHAR(500) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`DisMent2` VARCHAR(500) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`DisMent3` VARCHAR(500) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`DisMent4` VARCHAR(500) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`SMSMent1` VARCHAR(500) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`SMSMent2` VARCHAR(500) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`SMSMent3` VARCHAR(500) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`SMSMent4` VARCHAR(500) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("PRIMARY KEY(`MentCode`) USING BTREE");
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

        public WB_ISUMENT_VO Select(string where = "MentCode = 1", string order = "MentCode", string limit = "1")
        {
            WB_ISUMENT_VO rtv = new WB_ISUMENT_VO();

            try
            {
                Create();
                DefaultInsert();

                base.where = where;
                base.order = order;
                base.limit = limit;

                DataTable dt = base.Select();
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        rtv = new WB_ISUMENT_VO();
                        {
                            try
                            {
                                rtv.MentCode = Convert.ToString(row["MentCode"]); // AUTO
                            }
                            catch { }

                            for(int i = 1; i <= 4; i++)
                            {
                                try
                                {
                                    rtv.BrdMent[i] = !string.IsNullOrEmpty(Convert.ToString(row[$"BrdMent{i}"])) ? Convert.ToString(row[$"BrdMent{i}"]) : null;
                                }
                                catch { }

                                try
                                {
                                    rtv.DisMent[i] = !string.IsNullOrEmpty(Convert.ToString(row[$"DisMent{i}"])) ? Convert.ToString(row[$"DisMent{i}"]) : null;
                                }
                                catch { }

                                try
                                {
                                    rtv.SMSMent[i] = !string.IsNullOrEmpty(Convert.ToString(row[$"SMSMent{i}"])) ? Convert.ToString(row[$"SMSMent{i}"]) : null;
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public void DefaultInsert()
        {
            string sql;

            try
            {
                sql = $"SELECT * FROM {table} WHERE MentCode = 1";
                DataTable dt = mysql.ExecuteReader(sql);
                if (dt == null)
                {
                    sql = $"INSERT INTO {table} (MentCode) VALUES (1)";
                    if (mysql.ExecuteNonQuery(sql) == -1) { }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): Ment Insert 성공 ({mysql.Ip}:{mysql.Port}.{table})");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): Ment Insert 실패({mysql.Ip}:{mysql.Port}.{table}): {ex.Message}");
            }
        }
    }
}
