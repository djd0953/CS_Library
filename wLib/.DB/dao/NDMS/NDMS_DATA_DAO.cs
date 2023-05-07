using MySqlX.XDevAPI.Relational;
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
using System.Windows.Media.Converters;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace wLib.DB
{
    public class NDMS_DATA_DAO
    {
        protected LOG_T log = LOG_T.Instance;
        protected MYSQL_T mysql;

        protected string table_code = "data";

        public NDMS_DATA_DAO()
        {

        }

        public NDMS_DATA_DAO(MYSQL_T mysql)
        {
            this.mysql = mysql;
        }

        public int CREATE_SENSOR(string table_type)
        {
            string table_name;
            string table_comment;
            string sql;
            int rtv = 0;

            try
            {
                switch (table_type)
                {
                    case "district":
                        table_name = "TCM_COU_DD_OBSV";
                        table_comment = "재해위험개선지구";
                        break;
                    case "reservoir":
                        table_name = "TCM_COU_DR_OBSV";
                        table_comment = "위험저수지";
                        break;
                    case "slope":
                        table_name = "TCM_COU_DS_OBSV";
                        table_comment = "급경사지";
                        break;
                    case "facilities":
                        table_name = "TCM_COU_DF_OBSV";
                        table_comment = "시설물안전";
                        break;
                    default:
                        throw new Exception(table_type);
                }

                sql = $"SHOW TABLES LIKE '{table_name}'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    // 테이블 생성
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append($"CREATE TABLE `{table_name}` (");
                        sb.Append("`DSCODE` CHAR(10) NOT NULL COMMENT '재해위험지구코드/시설물 코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`CD_DIST_OBSV` INT(4) NOT NULL COMMENT '계측기 순번',");
                        sb.Append("`GB_OBSV` CHAR(2) NULL DEFAULT NULL COMMENT '계측기 구분' COLLATE 'utf8_general_ci',");
                        sb.Append("`NM_DIST_OBSV` VARCHAR(100) NULL DEFAULT NULL COMMENT '계측기 명' COLLATE 'utf8_general_ci',");
                        sb.Append("`BDONG_CD` VARCHAR(10) NULL DEFAULT NULL COMMENT '법정동코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`MNTN_ADRES_AT` CHAR(1) NULL DEFAULT NULL COMMENT '산주소여부' COLLATE 'utf8_general_ci',");
                        sb.Append("`MLNM` VARCHAR(4) NULL DEFAULT NULL COMMENT '본번' COLLATE 'utf8_general_ci',");
                        sb.Append("`AULNM` VARCHAR(4) NULL DEFAULT NULL COMMENT '부번' COLLATE 'utf8_general_ci',");
                        sb.Append("`DTL_ADRES` VARCHAR(100) NULL DEFAULT NULL COMMENT '상세주소' COLLATE 'utf8_general_ci',");
                        sb.Append("`RDNMADR_CD` VARCHAR(25) NULL DEFAULT NULL COMMENT '도로명주소코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`RN_DTL_ADRES` VARCHAR(300) NULL DEFAULT NULL COMMENT '도로명상세주소' COLLATE 'utf8_general_ci',");
                        sb.Append("`SPO_NO_CD` VARCHAR(12) NULL DEFAULT NULL COMMENT '국가지점번호' COLLATE 'utf8_general_ci',");
                        sb.Append("`ORGN_CD` CHAR(7) NULL DEFAULT NULL COMMENT '기관코드' COLLATE 'utf8_general_ci',");
                        sb.Append("`LAT` DECIMAL(10, 7) NULL DEFAULT NULL COMMENT '위도',");
                        sb.Append("`LON` DECIMAL(10, 7) NULL DEFAULT NULL COMMENT '경도',");
                        sb.Append("`RM` VARCHAR(1000) NULL DEFAULT NULL COMMENT '비고' COLLATE 'utf8_general_ci',");
                        sb.Append("`USE_YN` CHAR(1) NULL DEFAULT NULL COMMENT '사용여부' COLLATE 'utf8_general_ci',");
                        sb.Append("`RGSDE` DATETIME NULL DEFAULT current_timestamp() COMMENT '최초등록일시',");
                        sb.Append("`UPDDE` DATETIME NULL DEFAULT NULL ON UPDATE current_timestamp() COMMENT '최종수정일시',");
                        sb.Append("PRIMARY KEY(`DSCODE`, `CD_DIST_OBSV`) USING BTREE,");
                        sb.Append("INDEX `FK_tcm_cou_dd_obsv_wb_equip` (`CD_DIST_OBSV`) USING BTREE,");
                        sb.Append("CONSTRAINT `FK_tcm_cou_dd_obsv_wb_equip` FOREIGN KEY (`CD_DIST_OBSV`) REFERENCES `wb_equip` (`NDMS_DIST_OBSV`) ON UPDATE CASCADE ON DELETE CASCADE");
                        sb.Append(")");
                        sb.Append($"COMMENT = '{table_comment} 계측·관측 센서 기본 정보' COLLATE = 'utf8_general_ci' ENGINE = InnoDB;");

                        sql = sb.ToString();
                    }

                    rtv = mysql.ExecuteNonQuery(sql);
                    if (rtv == -1)
                    {
                        log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table_name}): {sql}");
                    }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.{table_name})");
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

        public int CREATE_DATA(string table_type)
        {
            string table_name;
            string table_comment;
            string sql;
            int rtv = 0;

            try
            {
                switch (table_type)
                {
                    case "district":
                        table_name = "TCM_COU_DNGR_DISTRICT";
                        table_comment = "재해위험개선지구";
                        break;
                    case "reservoir":
                        table_name = "TCM_COU_DNGR_RESERVOIR";
                        table_comment = "위험저수지";
                        break;
                    case "slope":
                        table_name = "TCM_COU_DNGR_SLOPE";
                        table_comment = "급경사지";
                        break;
                    case "facilities":
                        table_name = "TCM_COU_DNGR_FACILITIES";
                        table_comment = "시설물안전";
                        break;
                    default:
                        throw new Exception(table_type);
                }

                sql = $"SHOW TABLES LIKE '{table_name}'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    // 테이블 생성
                    StringBuilder sb = new StringBuilder();
                    {
                        sb.Append($"CREATE TABLE `{table_name}` (");
	                    sb.Append("`DSCODE` CHAR(10) NOT NULL COMMENT '재해위험지구코드/시설물 코드' COLLATE 'utf8_general_ci',");
	                    sb.Append("`CD_DIST_OBSV` INT(4) NOT NULL COMMENT '계측기 순번',");
	                    sb.Append("`GB_OBSV` CHAR(2) NULL DEFAULT NULL COMMENT '계측기 구분' COLLATE 'utf8_general_ci',");
	                    sb.Append("`OBSR_DTTM` VARCHAR(14) NOT NULL COMMENT '관측일시' COLLATE 'utf8_general_ci',");
	                    sb.Append("`OBSR_GB` VARCHAR(2) NOT NULL COMMENT '관측값구분' COLLATE 'utf8_general_ci',");
	                    sb.Append("`OBSR_VALUE` VARCHAR(50) NULL DEFAULT NULL COMMENT '관측값' COLLATE 'utf8_general_ci',");
	                    sb.Append("`RGSDE` DATETIME NULL DEFAULT current_timestamp() COMMENT '최초등록일시',");
	                    sb.Append("`UPDDE` DATETIME NULL DEFAULT NULL ON UPDATE current_timestamp() COMMENT '최종수정일시',");
	                    sb.Append("PRIMARY KEY (`DSCODE`, `CD_DIST_OBSV`, `OBSR_DTTM`, `OBSR_GB`) USING BTREE,");
	                    sb.Append("CONSTRAINT `FK_tcm_cou_dngr_district_tcm_cou_dd_obsv` FOREIGN KEY (`DSCODE`, `CD_DIST_OBSV`) REFERENCES `tcm_cou_dd_obsv` (`DSCODE`, `CD_DIST_OBSV`) ON UPDATE CASCADE ON DELETE CASCADE");
	                    sb.Append(")");
                        sb.Append($"COMMENT = '{table_comment} 센싱 계측·관측 정보'");
                        sb.Append("COLLATE = 'utf8_general_ci' ENGINE = InnoDB;");

                        sql = sb.ToString();
                    }

                    rtv = mysql.ExecuteNonQuery(sql);
                    if (rtv == -1)
                    {
                        log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table_name}): {sql}");
                    }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.{table_name})");
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

        public int INSERT_DATA(NDMS_DATA_VO vo, string table_type)
        {

            // TABLE
            string table;
            // COLUMN
            string column, value;
            // SQL
            string sql;

            int rtv;

            CREATE_DATA(table_type);

            try
            {
                switch (table_type)
                {
                    case "district":
                        table = "TCM_COU_DNGR_DISTRICT";
                        break;
                    case "reservoir":
                        table = "TCM_COU_DNGR_RESERVOIR";
                        break;
                    case "slope":
                        table = "TCM_COU_DNGR_SLOPE";
                        break;
                    case "facilities":
                        table = "TCM_COU_DNGR_FACILITIES";
                        break;
                    default:
                        throw new Exception(table_type);
                }

                column = "DSCODE, CD_DIST_OBSV, GB_OBSV, OBSR_DTTM, OBSR_GB, OBSR_VALUE";

                {
                    List<string> _temp_value = new List<string>
                    {
                        $"'{vo.Dscode}'",
                        $"{vo.Cd_dist_obsv}",
                        $"'{vo.Gb_obsv}'",
                        $"'{vo.Obsr_dttm}'",
                        $"'{vo.Obsr_gb}'",
                        $"'{vo.Obsr_value}'"
                    };

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
                    sb.Append($"OBSR_VALUE = '{vo.Obsr_value}'");

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

        public int INSERT_EQUIP_INFO(NDMS_EQUIP_VO vo, string table_type)
        {
            // TABLE
            string table;
            // SQL
            string sql;

            int rtv;

            try
            {
                switch (table_type.ToLower())
                {
                    case "district":
                        table = "TCM_COU_DD_OBSV";
                        break;
                    case "reservoir":
                        table = "TCM_COU_DR_OBSV";
                        break;
                    case "slope":
                        table = "TCM_COU_SS_OBSV";
                        break;
                    case "facilities":
                        table = "TCM_COU_FC_OBSV";
                        break;
                    default:
                        throw new Exception(table_type);
                }

                //CREATE SQL
                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {table}");
                    sb.Append($"(DSCODE, CD_DIST_OBSV, GB_OBSV, NM_DIST_OBSV, BDONG_CD, MNTN_ADRES_AT, MLNM, AULNM, DTL_ADRES, RDNMADR_CD, RN_DTL_ADRES, SPO_NO_CD, ORGN_CD, LAT, LON, USE_YN)");
                    sb.Append(" VALUES ");
                    sb.Append("(");
                    {
                        if (string.IsNullOrEmpty(vo.Dscode) || string.IsNullOrEmpty(vo.Ndms_dist_obsv) || string.IsNullOrEmpty(vo.gb_obsv) || string.IsNullOrEmpty(vo.Nm_dist_obsv))
                        {
                            log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 장비 정보 업데이트 실패 : DSCODE || NDMS_DIST_OBSV || GB_OBSV || NM_DIST_OBSV IS NULL");
                            throw new Exception();
                        }

                        sb.Append($"'{vo.Dscode}',");
                        sb.Append($"'{vo.Ndms_dist_obsv}',");
                        sb.Append($"'{vo.gb_obsv}',");
                        sb.Append($"'{vo.Nm_dist_obsv}',");

                        sb.Append(!string.IsNullOrEmpty(vo.Bdong_cd) ? $"'{vo.Bdong_cd}'," : "NULL,");
                        sb.Append(!string.IsNullOrEmpty(vo.Mntn_adres_at) ? $"'{vo.Mntn_adres_at}'," : "NULL,");
                        sb.Append(!string.IsNullOrEmpty(vo.Mlnm) ? $"'{vo.Mlnm}'," : "NULL,");
                        sb.Append(!string.IsNullOrEmpty(vo.Aulnm) ? $"'{vo.Aulnm}'," : "NULL,");
                        sb.Append(!string.IsNullOrEmpty(vo.Dtl_adres) ? $"'{vo.Dtl_adres}'," : "NULL,");
                        sb.Append(!string.IsNullOrEmpty(vo.Rdnmadr_cd) ? $"'{vo.Rdnmadr_cd}'," : "NULL,");
                        sb.Append(!string.IsNullOrEmpty(vo.Rn_dtl_adres) ? $"'{vo.Rn_dtl_adres}'," : "NULL,");
                        sb.Append(!string.IsNullOrEmpty(vo.Spo_No_cd) ? $"'{vo.Spo_No_cd}'," : "NULL,");
                        sb.Append(!string.IsNullOrEmpty(vo.Orgn_cd) ? $"'{vo.Orgn_cd}'," : "NULL,");
                        sb.Append(!string.IsNullOrEmpty(vo.Lat) ? $"'{vo.Lat}'," : "NULL,");
                        sb.Append(!string.IsNullOrEmpty(vo.Lon) ? $"'{vo.Lon}'," : "NULL,");
                    }
                    sb.Append("'1')");
                    sb.Append(" ON DUPLICATE KEY UPDATE ");
                    sb.Append($"DSCODE = '{vo.Dscode}',");
                    sb.Append($"CD_DIST_OBSV = '{vo.Ndms_dist_obsv}',");
                    sb.Append($"GB_OBSV = '{vo.gb_obsv}',");
                    sb.Append($"NM_DIST_OBSV = '{vo.Nm_dist_obsv}'");
                    sb.Append(!string.IsNullOrEmpty(vo.Bdong_cd) ? $", BDONG_CD = '{vo.Bdong_cd}'" : $", BDONG_CD = '{vo.Dscode.Substring(0, 5)}',");

                    if (!string.IsNullOrEmpty(vo.Dtl_adres)) sb.Append($", DTL_ADRES = '{vo.Dtl_adres}'");
                    if (!string.IsNullOrEmpty(vo.Lat)) sb.Append($", LAT = '{vo.Lat}'");
                    if (!string.IsNullOrEmpty(vo.Lon)) sb.Append($", LON = '{vo.Lon}'");

                    sql = sb.ToString();
                }

                rtv = mysql.ExecuteNonQuery(sql);
                if (rtv == -1)
                {
                    log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 장비 정보 업데이트 실패 : {sql}");
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }
    }
}

