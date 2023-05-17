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
    public class NDMS_BOARD_DAO : DAO_T
    {
        protected LOG_T log = LOG_T.Instance;

        public NDMS_BOARD_DAO()
        {

        }

        public NDMS_BOARD_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "TCM_FLUD_BOARD";
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
                        sb.Append("`CD_DIST_BOARD` INT(4) NOT NULL COMMENT '전광판 순번',");
                        sb.Append("`NM_DIST_BOARD` VARCHAR(100) NULL DEFAULT NULL COMMENT '전광판 명칭' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`COMM_STTUS` VARCHAR(1) NULL DEFAULT NULL COMMENT '통신 상태' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`MSG_BOARD` VARCHAR(100) NULL DEFAULT NULL COMMENT '표출 메시지' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`LAT` DECIMAL(10, 7) NULL DEFAULT NULL COMMENT '위도',");
                        sb.Append("`LON` DECIMAL(10, 7) NULL DEFAULT NULL COMMENT '경도',");
                        sb.Append("`RM` VARCHAR(1000) NULL DEFAULT NULL COMMENT '비고' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`USE_YN` CHAR(1) NOT NULL COMMENT '사용여부' COLLATE 'utf8mb4_general_ci',");
                        sb.Append("`RGSDE` DATETIME NOT NULL DEFAULT current_timestamp() COMMENT '최종등록일시',");
                        sb.Append("`UPDDE` DATETIME NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT '최종수정일시',");
                        sb.Append("PRIMARY KEY(`FLCODE`, `CD_DIST_BOARD`) USING BTREE,");
                        sb.Append("INDEX `FK_TCM_FLUD_BOARD_wb_equip` (`CD_DIST_BOARD`) USING BTREE,");
                        sb.Append("CONSTRAINT `FK_TCM_FLUD_BOARD_wb_equip` FOREIGN KEY(`CD_DIST_BOARD`) REFERENCES `wb_equip` (`NDMS_DIST_OBSV`) ON UPDATE CASCADE ON DELETE CASCADE )");
                        sb.Append("COMMENT = '전광판 정보' COLLATE = 'utf8mb4_general_ci' ENGINE = InnoDB;");

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

        public int Insert(NDMS_BOARD_VO vo)
        {

            // COLUMN
            string column, value;
            // SQL
            string sql;

            int rtv;

            Create();

            try
            {
                column = "FLCODE, CD_DIST_BOARD, NM_DIST_BOARD, LAT, LON, USE_YN";

                {
                    List<string> _temp_value = new List<string>
                    {
                        $"'{vo.FlCode}'",
                        $"{vo.Cd_dist_board}",
                        $"'{vo.Nm_dist_board}'",
                        $"'{vo.Lat}'",
                        $"'{vo.Lon}'",
                        $"'{vo.Use_YN}'"
                    };

                    if (!string.IsNullOrEmpty(vo.Comm_sttus)) { _temp_value.Add($"'{vo.Comm_sttus}'"); column += ", COMM_STTUS"; }
                    if (!string.IsNullOrEmpty(vo.Msg_board)) { _temp_value.Add($"'{vo.Msg_board}'"); column += ", MSG_BOARD"; }

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
                    sb.Append($"NM_DIST_BOARD = '{vo.Nm_dist_board}', ");
                    sb.Append($"LAT = {vo.Lat}, ");
                    sb.Append($"LON = {vo.Lon}, ");
                    sb.Append($"USE_YN = '{vo.Use_YN}', ");
                    sb.Append($"COMM_STTUS = '{vo.Comm_sttus}', ");
                    sb.Append($"MSG_BOARD = '{vo.Msg_board}'");

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

