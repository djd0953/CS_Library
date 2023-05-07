﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace wLib.DB
{
    public class WB_GATESTATUS_DAO : DAO_T
    {
        LOG_T log = LOG_T.Instance;

        public WB_GATESTATUS_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_gatestatus";
        }

        public IEnumerable<WB_GATESTATUS_VO> Select(string where = "1=1", string order = "CD_DIST_OBSV", string limit = "1000")
        {
            List<WB_GATESTATUS_VO> list = new List<WB_GATESTATUS_VO>();

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
                        try
                        {
                            WB_GATESTATUS_VO vo = new WB_GATESTATUS_VO();
                            {
                                try
                                {
                                    vo.Cd_dist_obsv = Convert.ToString(row["CD_DIST_OBSV"]);
                                }
                                catch { }

                                try
                                {
                                    vo.RegDate = Convert.ToString(row["RegDate"]);
                                }
                                catch { }

                                try
                                {
                                    vo.Gate = Convert.ToString(row["Gate"]);
                                }
                                catch { }
                            };

                            list.Add(vo);
                        }
                        catch
                        {
                            continue;
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

        public int Insert(WB_GATESTATUS_VO vo, bool update = false)
        {
            string sql;
            int rtv;

            try
            {
                sql = $"INSERT INTO {table}(CD_DIST_OBSV, RegDate, Gate ) " +
                      $"VALUES('{vo.Cd_dist_obsv}', '{vo.RegDate}', '{vo.Gate}') ";

                if (update)
                {
                    sql += $"ON DUPLICATE KEY UPDATE " +
                           $"RegDate = '{vo.RegDate}', " +
                           $"Gate = '{vo.Gate}' ";
                }

                rtv = base.Insert(sql);
            }
            catch
            {
                throw;
            }

            return rtv;
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
                        sb.Append("(");
	                    sb.Append("`CD_DIST_OBSV` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`RegDate` VARCHAR(19) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
	                    sb.Append("`Gate` VARCHAR(10) NULL DEFAULT NULL COLLATE 'utf8_general_ci',");
                        sb.Append("INDEX `FK_wb_gatestatus_wb_equip` (`CD_DIST_OBSV`) USING BTREE,");
                        sb.Append("CONSTRAINT `FK_wb_gatestatus_wb_equip` FOREIGN KEY(`CD_DIST_OBSV`) REFERENCES `parking`.`wb_equip` (`CD_DIST_OBSV`) ON UPDATE CASCADE ON DELETE CASCADE");
                        sb.Append(")");
                        sb.Append("COLLATE = 'utf8_general_ci'");
                        sb.Append("ENGINE = InnoDB;");

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
    }
}
