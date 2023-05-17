using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace wLib.DB
{
    public class NDMS_WAL_DAO :DAO_T
    {
        protected LOG_T log = LOG_T.Instance;

        public NDMS_WAL_DAO()
        {

        }

        public NDMS_WAL_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "TCM_FLUD_WAL";
        }

        public int Create()
        {
            string sql;
            int rtv = 0;

            try
            {
                sql = $"SHOW TABLES LIKE '{table}'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append($"CREATE TABLE `{table}` (");
                        sb.Append("`FLCODE` CHAR(10) NOT NULL COMMENT '침수지점 코드' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`CD_DIST_WAL` INT(4) NOT NULL COMMENT '전광판 순번',");
                        sb.Append("`NM_DIST_WAL` VARCHAR(100) NULL DEFAULT NULL COMMENT '전광판 명칭' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`GB_WAL` VARCHAR(1) NULL DEFAULT NULL COMMENT '수집 유형' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`LAST_COLCT_DE` VARCHAR(14) NULL DEFAULT NULL COMMENT '최종수집일시' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`LAST_COLCT_WAL` DECIMAL(5, 3) NULL DEFAULT NULL COMMENT '최종수집수위',");
                        sb.Append("`LAT` DECIMAL(10, 7) NULL DEFAULT NULL COMMENT '위도',");
                        sb.Append("`LON` DECIMAL(10, 7) NULL DEFAULT NULL COMMENT '경도',");
                        sb.Append("`RM` VARCHAR(1000) NULL DEFAULT NULL COMMENT '비고' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`USE_YN` CHAR(1) NOT NULL COMMENT '사용여부' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`RGSDE` DATETIME NOT NULL DEFAULT current_timestamp() COMMENT '최종등록일시',");
                        sb.Append("`UPDDE` DATETIME NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT '최종수정일시',");
                        sb.Append("PRIMARY KEY(`FLCODE`, `CD_DIST_WAL`) USING BTREE,");
                        sb.Append("INDEX `FK_TCM_FLUD_WAL_wb_equip` (`CD_DIST_WAL`) USING BTREE,");
                        sb.Append("CONSTRAINT `FK_TCM_FLUD_WAL_wb_equip` FOREIGN KEY(`CD_DIST_WAL`) REFERENCES `wb_equip` (`NDMS_DIST_OBSV`) ON UPDATE CASCADE ON DELETE CASCADE )");
                        sb.Append("COMMENT = '수위측정소 정보' COLLATE = 'utf8mb4_general_ci' ENGINE = InnoDB;");

                        sql = sb.ToString();
                    }
                    rtv = 0;
                    rtv = mysql.ExecuteNonQuery(sql);
                    if (rtv == -1)
                    {
                        log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.OBSV): {sql}");
                    }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.OBSV)");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}): {ex.Message}");
                throw;
            }

            return rtv;
        }

        public int Insert(NDMS_WAL_VO vo)
        {

            // COLUMN
            string column, value;
            // SQL
            string sql;

            int rtv;

            Create();

            try
            {
                column = "FLCODE, CD_DIST_WAL, NM_DIST_WAL, GB_WAL, LAT, LON, USE_YN";

                {
                    List<string> _temp_value = new List<string>
                    {
                        $"'{vo.FlCode}'",
                        $"{vo.Cd_dist_wal}",
                        $"'{vo.Nm_dist_wal}'",
                        $"'{vo.Gb_wal}'",
                        $"'{vo.Lat}'",
                        $"'{vo.Lon}'",
                        $"'{vo.Use_YN}'"
                    };

                    if (!string.IsNullOrEmpty(vo.Last_colct_de)) { _temp_value.Add($"'{vo.Last_colct_de}'"); column += ", LAST_COLCT_DE"; }
                    if (!string.IsNullOrEmpty(vo.Last_colct_wal)) { _temp_value.Add($"{vo.Last_colct_wal}"); column += ", LAST_COLCT_WAL"; }

                    value = string.Join(",", _temp_value);
                }

                //CREATE SQL
                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {table} ");
                    sb.Append($"({column})");
                    sb.Append(" VALUES ");
                    sb.Append($"({value})");
                    sb.Append(" ON DUPLICATE KEY UPDATE ");
                    sb.Append($"NM_DIST_WAL = '{vo.Nm_dist_wal}', ");
                    sb.Append($"GB_WAL = '{vo.Gb_wal}', ");
                    sb.Append($"LAT = {vo.Lat}, ");
                    sb.Append($"LON = {vo.Lon}, ");
                    sb.Append($"USE_YN = '{vo.Use_YN}', ");
                    sb.Append($"LAST_COLCT_DE = '{vo.Last_colct_de}', ");
                    sb.Append($"LAST_COLCT_WAL = {vo.Last_colct_wal}");

                    sql = sb.ToString();
                }

                rtv = mysql.ExecuteNonQuery(sql);
            }
            catch
            {
                throw;
            }

            return rtv;
        }
    }
}

