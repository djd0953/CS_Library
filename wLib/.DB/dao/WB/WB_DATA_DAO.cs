using System;
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
    public class WB_DATA_DAO
    {
        protected LOG_T log = LOG_T.Instance;
        protected MYSQL_T mysql;

        protected string table_code = "data";

        public WB_DATA_DAO()
        {

        }

        public WB_DATA_DAO(MYSQL_T mysql)
        {
            this.mysql = mysql;
        }

        public int CREATE_dis(WB_DATA_DTO dto)
        {
            string table = $"wb_{table_code}dis";
            string sql;
            int rtv = 0;

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
                        sb.Append("`CD_DIST_OBSV` VARCHAR(10) NOT NULL COMMENT '계측기 코드(wb_equip.CD_DIST_OBSV)', ");
                        sb.Append("`RegDate` VARCHAR(20) NOT NULL COMMENT '관측일시', ");
                        if (dto.Type == WB_DATA_TYPE.DPLACE)
                        {
                            sb.Append("`SUB_OBSV` INT(11) NOT NULL COMMENT '채널번호', ");
                        }

                        if (dto.Type == WB_DATA_TYPE.RAIN)
                        {
                            sb.Append("`rain_yester` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '전일값', ");
                            sb.Append("`rain_today` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '금일값', ");
                            sb.Append("`rain_hour` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '현재값', ");
                            sb.Append("`rain_month` DECIMAL(11,3) NULL DEFAULT NULL, ");
                            sb.Append("`rain_year` DECIMAL(11,3) NULL DEFAULT NULL, ");
                            sb.Append("`mov_1h` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '이동평균값 1시간', ");
                            sb.Append("`mov_2h` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '이동평균값 2시간', ");
                            sb.Append("`mov_3h` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '이동평균값 3시간', ");
                            sb.Append("`mov_6h` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '이동평균값 6시간', ");
                            sb.Append("`mov_12h` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '이동평균값 12시간', ");
                            sb.Append("`mov_24h` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '이동평균값 24시간', ");
                        }
                        else if (dto.Type == WB_DATA_TYPE.WATER)
                        {
                            sb.Append("`water_yester` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '전일값', ");
                            sb.Append("`water_today` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '금일값', ");
                            sb.Append("`water_now` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '현재값', ");
                        }
                        else if (dto.Type == WB_DATA_TYPE.DPLACE)
                        {
                            sb.Append("`dplace_yester` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '전일값', ");
                            sb.Append("`dplace_today` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '금일값', ");
                            sb.Append("`dplace_now` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '현재값', ");
                            sb.Append("`dplace_speed` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '절대값(현재값 - 1일 전 값(속도))', ");
                            sb.Append("`dplace_stand` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '설치 초기 값', ");
                            sb.Append("`dplace_change` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '절대값(현재값 - 설치 초기 값(누적))', ");

                        }
                        else if (dto.Type == WB_DATA_TYPE.SOIL)
                        {
                            sb.Append("`yester` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '전일값', ");
                            sb.Append("`today` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '금일값', ");
                            sb.Append("`now` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '현재값', ");
                        }
                        else if (dto.Type == WB_DATA_TYPE.SNOW)
                        {
                            sb.Append("`snow_yester` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '전일값', ");
                            sb.Append("`snow_today` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '금일값', ");
                            sb.Append("`snow_hour` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '현재값', ");
                        }
                        else if (dto.Type == WB_DATA_TYPE.TILT)
                        {
                            sb.Append("`yester` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '전일값', ");
                            sb.Append("`today` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '금일값', ");
                            sb.Append("`now` DECIMAL(11,3) NULL DEFAULT NULL COMMENT '현재값', ");
                        }
                        else if (dto.Type == WB_DATA_TYPE.FLOOD)
                        {
                            sb.Append("`yester` VARCHAR(15) NULL DEFAULT NULL COMMENT '전일값' COLLATE 'utf8_general_ci', ");
                            sb.Append("`today` VARCHAR(15) NULL DEFAULT NULL COMMENT '전일값' COLLATE 'utf8_general_ci', ");
                            sb.Append("`now` VARCHAR(15) NULL DEFAULT NULL COMMENT '전일값' COLLATE 'utf8_general_ci', ");
                        }

                        sb.Append("`dtmCreate` DATETIME DEFAULT current_timestamp() COMMENT 'AUTO_CREATE', ");
                        sb.Append("`dtmUpdate` DATETIME DEFAULT NULL ON UPDATE current_timestamp() COMMENT 'AUTO_UPDATE', ");
                        sb.Append($"PRIMARY KEY(`CD_DIST_OBSV`{(dto.Type == WB_DATA_TYPE.DPLACE ? ", `SUB_OBSV`" : "")}) USING BTREE,");
                        sb.Append($"INDEX `FK_{table}_wb_equip` (`CD_DIST_OBSV`) USING BTREE,");
                        sb.Append($"CONSTRAINT `FK_{table}_wb_equip` FOREIGN KEY(`CD_DIST_OBSV`) REFERENCES `wb_equip` (`CD_DIST_OBSV`) ON UPDATE CASCADE ON DELETE NO ACTION )");
                        sb.Append($"COLLATE = 'utf8_general_ci' ENGINE = InnoDB;");
                        sb.Append("COMMIT;");

                        sql = sb.ToString();
                    }

                    rtv = mysql.ExecuteNonQuery(sql);
                    if (rtv == -1)
                    {
                        log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table}): {sql}");
                    }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.{table})");
                    }
                }

            }
            catch (Exception ex)
            {
                log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table}): {ex.Message}");
            }

            return rtv;
        }

        public int CREATE_1min(WB_DATA_DTO dto)
        {
            string table = $"wb_{table_code}1min_{dto.Datatime:yyyy}";
            string sql;
            int rtv = 0;

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
                        sb.Append("`CD_DIST_OBSV` varchar(10) NOT NULL COMMENT '계측기 코드(wb_equip.CD_DIST_OBSV)', ");
                        if(dto.Type == WB_DATA_TYPE.DPLACE)
                        {
                            sb.Append("`SUB_OBSV` int(11) NOT NULL, ");
                        }
                        sb.Append("`RegDate` varchar(20) NOT NULL COMMENT '관측일시', ");
                        sb.Append("`MRMin` varchar(600) DEFAULT NULL COMMENT '관측값(00~59분)', ");
                        sb.Append("`dtmCreate` DATETIME DEFAULT current_timestamp() COMMENT 'AUTO_CREATE', ");
                        sb.Append("`dtmUpdate` DATETIME DEFAULT NULL ON UPDATE current_timestamp() COMMENT 'AUTO_UPDATE', ");
                        if (dto.Type == WB_DATA_TYPE.DPLACE)
                        {
                             sb.Append("PRIMARY KEY(`CD_DIST_OBSV`, `RegDate`, `SUB_OBSV`), ");
                        }
                        else sb.Append("PRIMARY KEY(`CD_DIST_OBSV`, `RegDate`), ");
                        sb.Append($"CONSTRAINT `FK_{table}` FOREIGN KEY (`CD_DIST_OBSV`) REFERENCES `wb_equip` (`CD_DIST_OBSV`) ON UPDATE CASCADE ON DELETE CASCADE");
                        sb.Append(") ENGINE = InnoDB COLLATE = 'utf8_general_ci';");
                        sb.Append("COMMIT;");

                        sql = sb.ToString();
                    }

                    rtv = mysql.ExecuteNonQuery(sql);
                    if (rtv == -1)
                    {
                        log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table}): {sql}");
                    }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.{table})");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table}): {ex.Message}");
                throw;
            }

            return rtv;
        }

        public int CREATE_10min(WB_DATA_DTO dto)
        {
            string table = $"wb_{table_code}10min_{dto.Datatime:yyyy}";
            string sql;
            int rtv = 0;

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
                        sb.Append("`CD_DIST_OBSV` VARCHAR(10) NOT NULL COMMENT '계측기 코드(wb_equip.CD_DIST_OBSV)', ");
                        if (dto.Type == WB_DATA_TYPE.DPLACE)
                        {
                            sb.Append("`SUB_OBSV` int(11) NOT NULL COMMENT '채널번호', ");
                        }
                        sb.Append("`RegDate` varchar(20) NOT NULL COMMENT '관측일시', ");

                        for(int i = 10; i <= 60; i += 10 )
                        {
                            if (dto.Type == WB_DATA_TYPE.FLOOD)
                            {
                                sb.Append($"`MR{i}` varchar(3) DEFAULT NULL COMMENT '관측값({i - 10:D02}~{i - 1:D02}분)', ");
                            }
                            else
                            {
                                sb.Append($"`MR{i}` decimal(11,3) DEFAULT NULL COMMENT '관측값({i-10:D02}~{i-1:D02}분)', ");
                            }
                        }

                        switch(dto.Type)
                        {
                            case WB_DATA_TYPE.RAIN:
                                sb.Append("`HourSum` decimal(11,3) DEFAULT NULL COMMENT '합산값(00~59분)', ");
                                break;
                            case WB_DATA_TYPE.WATER:
                                sb.Append("`HourMin` decimal(11,3) DEFAULT NULL COMMENT '최소값(00~59분)', ");
                                sb.Append("`HourMax` decimal(11,3) DEFAULT NULL COMMENT '최대값(00~59분)', ");
                                break;
                            case WB_DATA_TYPE.DPLACE:
                                sb.Append("`HourMax` decimal(11,3) DEFAULT NULL COMMENT '절대최대값(00~59분)', ");
                                break;
                            case WB_DATA_TYPE.SOIL:
                                sb.Append("`HourMax` decimal(11,3) DEFAULT NULL COMMENT '최대값(00~59분)', ");
                                break;
                            case WB_DATA_TYPE.SNOW:
                                sb.Append("`HourMax` decimal(11,3) DEFAULT NULL COMMENT '최대값(00~59분)', ");
                                break;
                            case WB_DATA_TYPE.TILT:
                                sb.Append("`HourMin` decimal(11,3) DEFAULT NULL COMMENT '최소값(00~59분)', ");
                                sb.Append("`HourMax` decimal(11,3) DEFAULT NULL COMMENT '최대값(00~59분)', ");
                                break;
                            case WB_DATA_TYPE.FLOOD:
                                sb.Append("`HourMax` varchar(3) DEFAULT NULL COMMENT '최대값(00~59분)', ");
                                break;
                        }

                        sb.Append("`dtmCreate` DATETIME DEFAULT current_timestamp(), ");
                        sb.Append("`dtmUpdate` DATETIME DEFAULT NULL ON UPDATE current_timestamp(), ");
                        if (dto.Type == WB_DATA_TYPE.DPLACE)
                        {
                            sb.Append("PRIMARY KEY(`CD_DIST_OBSV`, `RegDate`, `SUB_OBSV`), ");
                        }
                        else sb.Append("PRIMARY KEY(`CD_DIST_OBSV`, `RegDate`), ");
                        sb.Append($"CONSTRAINT `FK_{table}` FOREIGN KEY (`CD_DIST_OBSV`) REFERENCES `wb_equip` (`CD_DIST_OBSV`) ON UPDATE CASCADE ON DELETE CASCADE");
                        sb.Append(") ENGINE = InnoDB DEFAULT CHARSET = utf8;");
                        sb.Append("COMMIT;");

                        sql = sb.ToString();
                    }

                    rtv = mysql.ExecuteNonQuery(sql);
                    if (rtv == -1)
                    {
                        log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table}): {sql}");
                    }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.{table})");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table}): {ex.Message}");
                throw;
            }

            return rtv;
        }

        public int CREATE_1hour(WB_DATA_DTO dto)
        {
            string table = $"wb_{table_code}1hour";
            string sql;
            int rtv = 0;

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
                        sb.Append("`CD_DIST_OBSV` varchar(10) NOT NULL COMMENT '계측기 코드(wb_equip.CD_DIST_OBSV)', ");
                        if (dto.Type == WB_DATA_TYPE.DPLACE)
                        {
                            sb.Append("`SUB_OBSV` int(11) NOT NULL, ");
                        }
                        sb.Append("`RegDate` varchar(20) NOT NULL COMMENT '관측일시', ");

                        for (int i = 1; i <= 24; i++)
                        {
                            if (dto.Type == WB_DATA_TYPE.FLOOD)
                            {
                                sb.Append($"`MR{i}` varchar(3) DEFAULT NULL, ");
                            }
                            else
                            {
                                sb.Append($"`MR{i}` decimal(11,3) DEFAULT NULL, ");
                            }
                        }

                        switch(dto.Type)
                        {
                            case WB_DATA_TYPE.RAIN:
                                sb.Append("`DaySum` decimal(11,3) DEFAULT NULL, ");
                                break;
                            case WB_DATA_TYPE.WATER:
                                sb.Append("`DayMin` decimal(11,3) DEFAULT NULL, ");
                                sb.Append("`DayMax` decimal(11,3) DEFAULT NULL, ");
                                break;
                            case WB_DATA_TYPE.DPLACE:
                                sb.Append("`DayMax` decimal(11,3) DEFAULT NULL, ");
                                break;
                            case WB_DATA_TYPE.SOIL:
                                sb.Append("`DayMax` decimal(11,3) DEFAULT NULL, ");
                                break;
                            case WB_DATA_TYPE.SNOW:
                                sb.Append("`DayMax` decimal(11,3) DEFAULT NULL, ");
                                break;
                            case WB_DATA_TYPE.TILT:
                                sb.Append("`DayMin` decimal(11,3) DEFAULT NULL, ");
                                sb.Append("`DayMax` decimal(11,3) DEFAULT NULL, ");
                                break;
                            case WB_DATA_TYPE.FLOOD:
                                sb.Append("`DayMax` varchar(3) DEFAULT NULL, ");
                                break;
                        }
                        sb.Append("`dtmCreate` DATETIME DEFAULT current_timestamp(), ");
                        sb.Append("`dtmUpdate` DATETIME DEFAULT NULL ON UPDATE current_timestamp(), ");
                        if (dto.Type == WB_DATA_TYPE.DPLACE)
                        {
                            sb.Append("PRIMARY KEY(`CD_DIST_OBSV`, `RegDate`, `SUB_OBSV`), ");
                        }
                        else sb.Append("PRIMARY KEY(`CD_DIST_OBSV`, `RegDate`), ");
                        sb.Append($"CONSTRAINT `FK_{table}` FOREIGN KEY (`CD_DIST_OBSV`) REFERENCES `wb_equip` (`CD_DIST_OBSV`) ON UPDATE CASCADE ON DELETE CASCADE");
                        sb.Append(") ENGINE = InnoDB DEFAULT CHARSET = utf8;");
                        sb.Append("COMMIT;");

                        sql = sb.ToString();
                    }

                    rtv = mysql.ExecuteNonQuery(sql);
                    if (rtv == -1)
                    {
                        log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table}): {sql}");
                    }
                    else
                    {
                        log.Info($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 성공({mysql.Ip}:{mysql.Port}.{table})");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info(LOG_TYPE.UI, $"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성 실패({mysql.Ip}:{mysql.Port}.{table}): {ex.Message}");
                throw;
            }

            return rtv;
        }

        public string[] SELECT_1min(WB_DATA_DTO dto)
        {
            // TABLE
            string table;
            // PK
            string cd_dist_obsv, regdate;
            // FK
            string sub_obsv;
            // COLUMN
            string column;
            // SQL
            string sql;

            string[] rtv;

            try
            {
                // CREATE SQL
                {
                    table = $"wb_{table_code}1min_{dto.Datatime:yyyy}";
                    cd_dist_obsv = dto.Cd_dist_obsv;
                    regdate = $"{dto.Datatime:yyyyMMddHH}";
                    sub_obsv = dto.Sub_obsv;
                    column = "MRmin";

                    // sql
                    sql = $"SELECT {column} " +
                          $"FROM {table} " +
                          $"WHERE cd_dist_obsv = '{cd_dist_obsv}' AND RegDate = '{regdate}' ";

                    if (dto.Type == WB_DATA_TYPE.DPLACE && sub_obsv != "")
                    {
                        sql += $"AND sub_obsv = '{sub_obsv}'";
                    }
                }

                string result = mysql.ExecuteScalar<string>(sql);
                if (result == string.Empty || result == null)
                {
                    // 없을 경우 생성
                    rtv = new string[60];
                }
                else
                {
                    // 있을 경우 업데이트
                    rtv = result.Split('/');
                    if (rtv.Length != 60)
                    {
                        // 기존 데이터를 신뢰할 수 없으므로 지우고 새로 생성
                        rtv = new string[60];
                    }
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public int INSERT_1min(WB_DATA_DTO dto)
        {
            // TABLE
            string table;
            // PK
            string cd_dist_obsv, regdate;
            // FK
            string sub_obsv;
            // COLUMN
            string column, value;
            // SQL
            string sql;

            int rtv;

            CREATE_1min(dto);

            // 신규 데이터 조회 후 입력
            try
            {
                string[] value_temp = SELECT_1min(dto);
                value_temp[dto.Datatime.Minute] = dto.Value;

                value = string.Join("/", value_temp);
            }
            catch
            {
                throw;
            }

            // CREATE SQL
            try
            {
                table = $"wb_{table_code}1min_{dto.Datatime:yyyy}";
                cd_dist_obsv = dto.Cd_dist_obsv;
                regdate = $"{dto.Datatime:yyyyMMddHH}";
                sub_obsv = dto.Sub_obsv;
                column = "MRmin";

                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {table} (cd_dist_obsv, RegDate, ");
                    if (dto.Type == WB_DATA_TYPE.DPLACE && sub_obsv != "")
                    { 
                        sb.Append("sub_obsv, ");
                    }
                    sb.Append($"{column}) ");

                    sb.Append("VALUES");
                    sb.Append($"('{cd_dist_obsv}', '{regdate}', ");
                    if (dto.Type == WB_DATA_TYPE.DPLACE && sub_obsv != "")
                    {
                        sb.Append($"{sub_obsv}, ");
                    }
                    sb.Append($"'{value}') ");

                    sb.Append($"ON DUPLICATE KEY UPDATE ");
                    sb.Append($"{column} = '{value}' ");

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

        public string[] SELECT_10min(WB_DATA_DTO dto)
        {
            // TABLE
            string table;
            // PK
            string cd_dist_obsv, regdate;
            // FK
            string sub_obsv;
            // SQL
            string sql;

            string[] rtv = new string[6];

            try
            {
                // CREATE SQL
                {
                    table = $"wb_{table_code}10min_{dto.Datatime:yyyy}";
                    cd_dist_obsv = dto.Cd_dist_obsv;
                    regdate = $"{dto.Datatime:yyyyMMddHH}";
                    sub_obsv = dto.Sub_obsv;

                    // SQL
                    sql = $"SELECT MR10, MR20, MR30, MR40, MR50, MR60 " +
                          $"FROM {table} " +
                          $"WHERE cd_dist_obsv = '{cd_dist_obsv}' AND regdate = '{regdate}' ";

                    if (dto.Type == WB_DATA_TYPE.DPLACE && sub_obsv != "")
                    {
                        sql += $"AND sub_obsv = '{sub_obsv}' ";
                    }
                }

                object result = mysql.ExecuteReader(sql);
                if (result == null || result == DBNull.Value)
                {
                    // 없을 경우
                }
                else
                {
                    // 있을 경우
                    System.Data.DataTable dt = result as System.Data.DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        rtv = dt.Select().First().ItemArray.Select(x => Convert.ToString(x)).ToArray();
                    }
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public virtual int INSERT_10min(WB_DATA_DTO dto)
        {
            // TABLE
            string table;
            // PK
            string cd_dist_obsv, regdate;
            // FK
            string sub_obsv;
            // COLUMN
            string column;
            // VALUE
            string value;

            // SQL
            string sql;

            int rtv;

            CREATE_10min(dto);

            try
            {
                // CREATE SQL
                table = $"wb_{table_code}10min_{dto.Datatime:yyyy}";
                cd_dist_obsv = dto.Cd_dist_obsv;
                regdate = $"{dto.Datatime:yyyyMMddHH}";
                sub_obsv = dto.Sub_obsv;
                column = $"MR{(dto.Datatime.Minute / 10) * 10 + 10}";
                value = dto.Value;

                if (value == "")
                    value = "NULL";
                
                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {table} (cd_dist_obsv, RegDate, ");
                    if (dto.Type == WB_DATA_TYPE.DPLACE && sub_obsv != "")
                    {
                        sb.Append($"sub_obsv, ");
                    }
                    sb.Append($"{column}) ");

                    sb.Append($"VALUES('{cd_dist_obsv}', '{regdate}', ");
                    if (dto.Type == WB_DATA_TYPE.DPLACE && sub_obsv != "")
                    {
                        sb.Append($"{sub_obsv}, ");
                    }

                    if (dto.Type == WB_DATA_TYPE.FLOOD)
                    {
                        value = $"'{value}'";
                    }

                    sb.Append($"{value}) ");

                    sb.Append($"ON DUPLICATE KEY UPDATE ");
                    sb.Append($"{column} = {value} ");

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

        public string SELECT_10min_hourSum(WB_DATA_DTO dto)
        {
            return SELECT_10min_column(dto, "hourSum");
        }

        public string SELECT_10min_hourMin(WB_DATA_DTO dto)
        {
            return SELECT_10min_column(dto, "hourMin");
        }

        public string SELECT_10min_hourMax(WB_DATA_DTO dto)
        {
            return SELECT_10min_column(dto, "hourMax");
        }

        private string SELECT_10min_column(WB_DATA_DTO dto, string _column)
        {
            // TABLE
            string table;
            // PK
            string cd_dist_obsv, regdate;
            // FK
            string sub_obsv;
            // COLUMN
            string column;
            // SQL
            string sql;

            string rtv;

            // CREATE SQL
            try
            {
                table = $"wb_{table_code}10min_{dto.Datatime:yyyy}";
                cd_dist_obsv = dto.Cd_dist_obsv;
                regdate = $"{dto.Datatime:yyyyMMddHH}";
                sub_obsv = dto.Sub_obsv;
                column = _column;

                sql = $"SELECT {column} " +
                      $"FROM {table} " +
                      $"WHERE cd_dist_obsv = '{cd_dist_obsv}' AND regdate = '{regdate}' ";

                if (dto.Type == WB_DATA_TYPE.DPLACE && sub_obsv != "")
                {
                    sql += $"AND sub_obsv = '{sub_obsv}'";
                }

                string result = mysql.ExecuteScalar<string>(sql);
                if (result == null || result == string.Empty)
                {
                    rtv = null;
                }
                else
                {
                    rtv = result;
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public int UPDATE_10min_calc(WB_DATA_DTO dto)
        {
            // TABLE
            string table;
            // PK
            string cd_dist_obsv, regdate;
            // FK
            string sub_obsv;
            // VALUE
            string value_hourSum;
            string value_hourMin, value_hourTMin;
            string value_hourMax, value_hourTMax;
            string value_hourOr, value_hourAnd;
            // SQL
            string sql;

            int rtv;

            try
            {
                table = $"wb_{table_code}10min_{dto.Datatime:yyyy}";
                cd_dist_obsv = dto.Cd_dist_obsv;
                regdate = $"{dto.Datatime:yyyyMMddHH}";
                sub_obsv = dto.Sub_obsv;

                string[] value_temp = SELECT_10min(dto);
                if (double.TryParse(dto.Value, out double data_value) == false)
                    return 0;

                double min_value = data_value;
                double tmin_value = data_value;
                double max_value = data_value;
                double tmax_value = data_value;
                double sum_value = 0.0;
                byte and_value = 0;
                byte or_value = 0;

                for (int i = 0; i < 6; i++)
                {
                    if (double.TryParse(value_temp[i], out double fValue))
                    {
                        // SUM
                        sum_value += fValue;

                        // MIN
                        if (fValue < min_value)
                        {
                            min_value = fValue;
                        }

                        // MAX
                        if (fValue > max_value)
                        {
                            max_value = fValue;
                        }

                        // TMIN
                        if ( Math.Abs(fValue) < Math.Abs(tmin_value))
                        {
                            tmin_value = fValue;
                        }

                        // TMAX
                        if (Math.Abs(fValue) > Math.Abs(tmax_value))
                        {
                            tmax_value = fValue;
                        }
                    }

                    if (byte.TryParse(value_temp[i], out byte bValue))
                    {
                        // AND
                        and_value &= (byte)bValue;

                        // OR
                        or_value |= (byte)bValue;
                    }
                }

                value_hourSum = sum_value.ToString("F3");
                value_hourMin = min_value.ToString("F3");
                value_hourMax = max_value.ToString("F3");
                value_hourTMin = tmin_value.ToString("F3");
                value_hourTMax = tmax_value.ToString("F3");
                value_hourAnd = and_value.ToString("D3");
                value_hourOr = or_value.ToString("D3");

                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"UPDATE {table} ");
                    sb.Append($"SET ");
                    switch(dto.Type)
                    {
                        case WB_DATA_TYPE.RAIN:
                            sb.Append($"hourSum = {value_hourSum} ");
                            break;
                        case WB_DATA_TYPE.WATER:
                            sb.Append($"hourMin = {value_hourMin}, ");
                            sb.Append($"hourMax = {value_hourMax} ");
                            break;
                        case WB_DATA_TYPE.DPLACE:
                            sb.Append($"hourMax = {value_hourTMax} ");
                            break;
                        case WB_DATA_TYPE.SOIL:
                            sb.Append($"hourMax = {value_hourMax} ");
                            break;
                        case WB_DATA_TYPE.SNOW:
                            sb.Append($"hourMax = {value_hourMax} ");
                            break;
                        case WB_DATA_TYPE.TILT:
                            sb.Append($"hourMin = {value_hourMin}, ");
                            sb.Append($"hourMax = {value_hourMax} ");
                            break;
                        case WB_DATA_TYPE.FLOOD:
                            sb.Append($"hourMin = {value_hourAnd}, ");
                            sb.Append($"hourMax = {value_hourOr}");
                            return 0;
                    }
                    sb.Append($"WHERE cd_dist_obsv = '{cd_dist_obsv}' AND regdate = '{regdate}' ");

                    if (dto.Type == WB_DATA_TYPE.DPLACE && sub_obsv != "")
                    {
                        sb.Append($"AND sub_obsv = '{sub_obsv}'");
                    }

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

        public string[] SELECT_1hour(WB_DATA_DTO dto)
        {
            // TABLE
            string table;
            // PK
            string cd_dist_obsv, regdate;
            // FK
            string sub_obsv;
            // SQL
            string sql;

            string[] rtv = new string[24];

            try
            {
                // CREATE SQL
                {
                    table = $"wb_{table_code}1hour";
                    cd_dist_obsv = dto.Cd_dist_obsv;
                    regdate = $"{dto.Datatime:yyyyMMdd}";
                    sub_obsv = dto.Sub_obsv;

                    // SQL
                    sql = $"SELECT MR1, MR2, MR3, MR4, MR5, MR6, MR7, MR8, MR9, MR10, MR11, MR12, MR13, MR14, MR15, MR16, MR17, MR18, MR19, MR20, MR21, MR22, MR23, MR24 " +
                          $"FROM {table} " +
                          $"WHERE cd_dist_obsv = '{cd_dist_obsv}' AND regdate = '{regdate}' ";

                    if (dto.Type == WB_DATA_TYPE.DPLACE && sub_obsv != "")
                    {
                        sql += $"AND sub_obsv = '{sub_obsv}'";
                    }
                }

                object result = mysql.ExecuteReader(sql);
                if (result == null || result == DBNull.Value)
                {
                    // 없을 경우
                }
                else
                {
                    // 있을 경우
                    System.Data.DataTable dt = result as System.Data.DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        rtv = dt.Select().First().ItemArray.Select(x => Convert.ToString(x)).ToArray();
                    }
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public string SELECT_1hour_daySum(WB_DATA_DTO dto)
        {
            return SELECT_1hour_column(dto, "daySum");
        }

        public string SELECT_1hour_dayMin(WB_DATA_DTO dto)
        {
            return SELECT_1hour_column(dto, "dayMin");
        }

        public string SELECT_1hour_dayMax(WB_DATA_DTO dto)
        {
            return SELECT_1hour_column(dto, "dayMax");
        }

        private string SELECT_1hour_column(WB_DATA_DTO dto, string _column)
        {
            // TABLE
            string table;
            // PK
            string cd_dist_obsv, regdate;
            // FK
            string sub_obsv;
            // COLUMN
            string column;
            // SQL
            string sql;

            string rtv;

            // CREATE SQL
            try
            {
                table = $"wb_{table_code}1hour";
                cd_dist_obsv = dto.Cd_dist_obsv;
                regdate = $"{dto.Datatime:yyyyMMdd}";
                sub_obsv = dto.Sub_obsv;
                column = _column;

                sql = $"SELECT {column} " +
                      $"FROM {table} " +
                      $"WHERE cd_dist_obsv = '{cd_dist_obsv}' AND regdate = '{regdate}' ";

                if (dto.Type == WB_DATA_TYPE.DPLACE && sub_obsv != "")
                {
                    sql += $"AND sub_obsv = '{sub_obsv}'";
                }

                string result = mysql.ExecuteScalar<string>(sql);
                if (result == null || result == string.Empty)
                {
                    rtv = null;
                }
                else
                {
                    rtv = result;
                }
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public int INSERT_1hour(WB_DATA_DTO dto)
        {
            // TABLE
            string table;
            // PK
            string cd_dist_obsv, regdate;
            // FK
            string sub_obsv;
            // COLUMN
            string column;
            string value;
            // SQL
            string sql;

            int rtv;

            CREATE_1hour(dto);

            // 1시간 데이터 입력
            try
            {
                table = $"wb_{table_code}1hour";
                cd_dist_obsv = dto.Cd_dist_obsv;
                regdate = $"{dto.Datatime:yyyyMMdd}";
                sub_obsv = dto.Sub_obsv;
                column = $"MR{dto.Datatime.Hour + 1}";
                value = dto.Value;

                if (value == "")
                    value = "NULL";

                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {table} (cd_dist_obsv, regDate, ");
                    if (dto.Type == WB_DATA_TYPE.DPLACE && sub_obsv != "")
                    {
                        sb.Append($"sub_obsv, ");
                    }
                    sb.Append($"{column}) ");

                    sb.Append($"VALUES('{cd_dist_obsv}', '{regdate}', ");
                    if (dto.Type == WB_DATA_TYPE.DPLACE && sub_obsv != "")
                    {
                        sb.Append($"{sub_obsv}, ");
                    }

                    if (dto.Type == WB_DATA_TYPE.FLOOD)
                    {
                        value = $"'{value}'";
                    }

                    sb.Append($"{value}) ");

                    sb.Append($"ON DUPLICATE KEY UPDATE ");
                    sb.Append($"{column} = {value} ");

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

        public int UPDATE_1hour_calc(WB_DATA_DTO dto)
        {
            // TABLE
            string table;
            // PK
            string cd_dist_obsv, regdate;
            // FK
            string sub_obsv;
            // VALUE
            string value_daySum;
            string value_dayMin, value_dayTMin;
            string value_dayMax, value_dayTMax;
            string value_dayAnd, value_dayOr;
            // SQL
            string sql;

            int rtv;

            // 1시간 자료에서 합계값 구하기
            try
            {
                table = $"wb_{table_code}1hour";
                cd_dist_obsv = dto.Cd_dist_obsv;
                regdate = $"{dto.Datatime:yyyyMMdd}";
                sub_obsv = dto.Sub_obsv;

                if (double.TryParse(dto.Value, out double data_value) == false)
                    return 0;

                string[] value_temp = SELECT_1hour(dto);
                double min_value = data_value;
                double tmin_value = data_value;
                double max_value = data_value;
                double tmax_value = data_value;
                double sum_value = 0.0;
                byte and_value = 0;
                byte or_value = 0;

                for (int i = 0; i < 24; i++)
                {

                    if (double.TryParse(value_temp[i], out double fValue))
                    {
                        // MIN
                        if (fValue < min_value)
                        {
                            min_value = fValue;
                        }

                        // MAX
                        if (fValue > max_value)
                        {
                            max_value = fValue;
                        }

                        // TMIN
                        if (Math.Abs(fValue) < Math.Abs(tmin_value))
                        {
                            tmin_value = fValue;
                        }

                        // TMAX
                        if (Math.Abs(fValue) > Math.Abs(tmax_value))
                        {
                            tmax_value = fValue;
                        }

                        sum_value += fValue;
                    }

                    if (byte.TryParse(value_temp[i], out byte bValue))
                    {
                        // AND
                        and_value &= bValue;

                        // OR
                        or_value |= bValue;
                    }
                }

                value_daySum = sum_value.ToString("F3");
                value_dayMin = min_value.ToString("F3");
                value_dayMax = max_value.ToString("F3");
                value_dayTMin = tmin_value.ToString("F3");
                value_dayTMax = tmax_value.ToString("F3");
                value_dayAnd = and_value.ToString("D3");
                value_dayOr = or_value.ToString("D3");

                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"UPDATE {table} ");
                    sb.Append($"SET ");
                    switch(dto.Type)
                    {
                        case WB_DATA_TYPE.RAIN:
                            sb.Append($"daySum = {value_daySum} ");
                            break;
                        case WB_DATA_TYPE.WATER:
                            sb.Append($"dayMin = {value_dayMin}, ");
                            sb.Append($"dayMax = {value_dayMax} ");
                            break;
                        case WB_DATA_TYPE.DPLACE:
                            sb.Append($"dayMax = {value_dayTMax} ");
                            break;
                        case WB_DATA_TYPE.SOIL:
                            sb.Append($"dayMax = {value_dayMax} ");
                            break;
                        case WB_DATA_TYPE.SNOW:
                            sb.Append($"dayMax = {value_dayMax} ");
                            break;
                        case WB_DATA_TYPE.TILT:
                            sb.Append($"dayMin = {value_dayMin}, ");
                            sb.Append($"dayMax = {value_dayMax} ");
                            break;
                        case WB_DATA_TYPE.FLOOD:
                            sb.Append($"dayMin = {value_dayAnd} ");
                            sb.Append($"dayMax = {value_dayOr} ");
                            return 0;
                    }
                    sb.Append($"WHERE cd_dist_obsv = '{cd_dist_obsv}' AND regdate = '{regdate}' ");

                    if (dto.Type == WB_DATA_TYPE.DPLACE && sub_obsv != "")
                    {
                        sb.Append($"AND sub_obsv = '{sub_obsv}'");
                    }

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

        public string SELECT_dis(WB_DATA_DTO dto, string rainTime = null)
        {
            // TABLE
            string table;
            // PK
            string cd_dist_obsv;
            // FK
            string sub_obsv;
            // COLUMN
            string column;
            // SQL
            string sql;

            string rtv;

            try
            {
                // CREATE SQL
                {
                    if (dto.Type == WB_DATA_TYPE.RAIN && rainTime == null)
                    {
                        throw new Exception("RainTime is Null");
                    }

                    table = $"wb_{table_code}dis";
                    cd_dist_obsv = dto.Cd_dist_obsv;
                    sub_obsv = dto.Sub_obsv;

                    switch (dto.Type)
                    {
                        case WB_DATA_TYPE.RAIN:
                            column = $"mov_{rainTime}h";
                            break;
                        case WB_DATA_TYPE.WATER:
                            column = "water_now";
                            break;
                        case WB_DATA_TYPE.DPLACE:
                            column = "CONCAT(dplace_change,'/',dplace_speed)";
                            break;
                        case WB_DATA_TYPE.SOIL:
                            column = "now";
                            break;
                        case WB_DATA_TYPE.SNOW:
                            column = "snow_hour";
                            break;
                        case WB_DATA_TYPE.TILT:
                            column = "now";
                            break;
                        case WB_DATA_TYPE.FLOOD:
                            column = "now";
                            break;
                        default:
                            throw new Exception();
                    }

                    // SQL
                    sql = $"SELECT {column} " +
                          $"FROM {table} " +
                          $"WHERE CD_DIST_OBSV = '{cd_dist_obsv}' AND RegDate >= '{dto.Datatime.AddMinutes(-2).ToString("yyyy-MM-dd HH:mm")}:00'";

                    if (dto.Type == WB_DATA_TYPE.DPLACE && sub_obsv != "")
                    {
                        sql += $"AND sub_obsv = '{sub_obsv}'";
                    }
                }

                rtv = mysql.ExecuteScalar<string>(sql);
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public virtual int Insert_dis(WB_DATA_DTO dto)
        {
            // TABLE
            string table, data_table;
            // PK
            string cd_dist_obsv, regdate;
            // SQL
            string sql, sub_query;

            int rtv;
            DateTime settingTime = dto.Datatime;

            CREATE_dis(dto);

            // 1시간 데이터 입력
            try
            {
                table = $"wb_{table_code}dis";
                data_table = $"wb_{table_code}1min_{settingTime:yyyy}";
                regdate = $"{dto.Datatime:yyyy-MM-dd HH:mm:ss}";
                cd_dist_obsv = dto.Cd_dist_obsv;

                sub_query = $"SELECT * FROM {data_table} WHERE CD_DIST_OBSV = '{cd_dist_obsv}' AND RegDate >= '{settingTime.AddDays(-2):yyyyMMdd}00' ORDER BY RegDate ASC";
                List<WB_DATA_DTO> list = new List<WB_DATA_DTO>();
                DataTable dt = mysql.ExecuteReader(sub_query);
                foreach (DataRow row in dt.Rows)
                {
                    // WeatherSI Program이 MRMin 데이터를 믿을 수 있는 데이터로 바꿨다고 가정하고 로직 구성
                    string[] dataArr = row["MRMin"].ToString().Split('/');
                    string year = row["RegDate"].ToString().Substring(0, 4);
                    string month = row["RegDate"].ToString().Substring(4, 2);
                    string day = row["RegDate"].ToString().Substring(6, 2);
                    string hour = row["RegDate"].ToString().Substring(8, 2);

                    for(int i = 0; i < dataArr.Length; i++) 
                    {
                        string minute = i.ToString("D2");
                        if (double.TryParse(dataArr[i], out double value)) 
                        {
                            WB_DATA_DTO data_dto = new WB_DATA_DTO()
                            {
                                Cd_dist_obsv = cd_dist_obsv,
                                Datatime = Convert.ToDateTime($"{year}-{month}-{day} {hour}:{minute}:00"),
                                Value = value.ToString()
                            };
                            list.Add(data_dto);
                        };
                    }
                }

                double yester = list.FindAll(x => x.Datatime.ToString("yyyyMMdd") == settingTime.AddDays(-1).ToString("yyyyMMdd")).Max( x => double.Parse(x.Value));
                double today = list.FindAll(x => x.Datatime.ToString("yyyyMMdd") == settingTime.ToString("yyyyMMdd")).Max( x => double.Parse(x.Value));
                string now = list.Last().Value.ToString();

                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {table} ");
                    
                    if (dto.Type == WB_DATA_TYPE.WATER)
                    {
                        sb.Append("(CD_DIST_OBSV, RegDate, water_yester, water_today, water_now)");
                    }
                    else if (dto.Type == WB_DATA_TYPE.SNOW)
                    {
                        sb.Append("(CD_DIST_OBSV, RegDate, snow_yester, snow_today, snow_hour)");
                    }
                    else
                    {
                        sb.Append("(CD_DIST_OBSV, RegDate, yester, today, now)");
                    }

                    sb.Append($" VALUES ('{cd_dist_obsv}', '{regdate}', {yester}, {today}, {now}) ");
                    sb.Append($"ON DUPLICATE KEY UPDATE ");

                    if (dto.Type == WB_DATA_TYPE.WATER)
                    {
                        sb.Append($"water_yester = {yester}, ");
                        sb.Append($"water_today = {today}, ");
                        sb.Append($"water_now = {now}");
                    }
                    else if (dto.Type == WB_DATA_TYPE.SNOW)
                    {
                        sb.Append($"snow_yester = {yester}, ");
                        sb.Append($"snow_today = {today}, ");
                        sb.Append($"snow_hour = {now}");
                    }
                    else
                    {
                        sb.Append($"yester = {yester}, ");
                        sb.Append($"today = {today}, ");
                        sb.Append($"now = {now}");
                    }

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

