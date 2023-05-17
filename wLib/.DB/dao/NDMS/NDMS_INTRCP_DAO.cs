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
    public class NDMS_INTRCP_DAO : DAO_T
    {
        protected LOG_T log = LOG_T.Instance;

        public NDMS_INTRCP_DAO()
        {

        }

        public NDMS_INTRCP_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "TCM_FLUD_CAR_INTRCP";
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
                        sb.Append("`CD_DIST_INTRCP` INT(4) NOT NULL COMMENT '차량제어기 순번',");
                        sb.Append("`NM_DIST_INTRCP` VARCHAR(100) NULL DEFAULT NULL COMMENT '차량제어기 명칭' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`GB_INTRCP` VARCHAR(1) NULL DEFAULT NULL COMMENT '진출입유형' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`MOD_INTRCP` VARCHAR(1) NULL DEFAULT NULL COMMENT '재난 시 차단기 모드' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`COMM_STTUS` VARCHAR(1) NULL DEFAULT NULL COMMENT '통신 상태' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`INTRCP_STTUS` VARCHAR(1) NULL DEFAULT NULL COMMENT '차단기 상태' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`LAT` DECIMAL(10, 7) NULL DEFAULT NULL COMMENT '위도',");
                        sb.Append("`LON` DECIMAL(10, 7) NULL DEFAULT NULL COMMENT '경도',");
                        sb.Append("`RM` VARCHAR(1000) NULL DEFAULT NULL COMMENT '비고' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`USE_YN` CHAR(1) NOT NULL COMMENT '사용여부' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`RGSDE` DATETIME NOT NULL DEFAULT current_timestamp() COMMENT '최종등록일시',");
                        sb.Append("`UPDDE` DATETIME NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT '최종수정일시',");
                        sb.Append("PRIMARY KEY(`FLCODE`, `CD_DIST_INTRCP`) USING BTREE,");
                        sb.Append("INDEX `FK_tcm_flud_car_intrcp_wb_equip` (`CD_DIST_INTRCP`) USING BTREE,");
                        sb.Append("CONSTRAINT `FK_tcm_flud_car_intrcp_wb_equip` FOREIGN KEY(`CD_DIST_INTRCP`) REFERENCES `wb_equip` (`NDMS_DIST_OBSV`) ON UPDATE CASCADE ON DELETE CASCADE)");
                        sb.Append("COMMENT = '차량제어기 정보'");
                        sb.Append("COLLATE = 'utf8mb4_general_ci' ENGINE = InnoDB;");

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

        public int Insert(NDMS_INTRCP_VO vo)
        {

            // COLUMN
            string column, value;
            // SQL
            string sql;

            int rtv;

            Create();

            try
            {
                column = "FLCODE, CD_DIST_INTRCP, NM_DIST_INTRCP, LAT, LON, USE_YN";

                {
                    List<string> _temp_value = new List<string>
                    {
                        $"'{vo.FlCode}'",
                        $"{vo.Cd_dist_intrcp}",
                        $"'{vo.Nm_dist_intrcp}'",
                        $"'{vo.Lat}'",
                        $"'{vo.Lon}'",
                        $"'{vo.Use_YN}'"
                    };

                    if (!string.IsNullOrEmpty(vo.Gb_intrcp)) { _temp_value.Add($"'{vo.Gb_intrcp}'"); column += ", GB_INTRCP"; }
                    if (!string.IsNullOrEmpty(vo.mod_intrcp)) { _temp_value.Add($"'{vo.mod_intrcp}'"); column += ", MOD_INTRCP"; }
                    if (!string.IsNullOrEmpty(vo.Comm_sttus)) { _temp_value.Add($"'{vo.Comm_sttus}'"); column += ", COMM_STTUS"; }
                    if (!string.IsNullOrEmpty(vo.intrcp_sttus)) { _temp_value.Add($"'{vo.intrcp_sttus}'"); column += ", INTRCP_STTUS"; }

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
                    sb.Append($"NM_DIST_INTRCP = '{vo.Nm_dist_intrcp}', ");
                    sb.Append($"LAT = {vo.Lat}, ");
                    sb.Append($"LON = {vo.Lon}, ");
                    sb.Append($"USE_YN = '{vo.Use_YN}', ");
                    sb.Append($"GB_INTRCP = '{vo.Gb_intrcp}', ");
                    sb.Append($"MOD_INTRCP = '{vo.mod_intrcp}', ");
                    sb.Append($"COMM_STTUS = '{vo.Comm_sttus}', ");
                    sb.Append($"INTRCP_STTUS = '{vo.intrcp_sttus}'");

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

