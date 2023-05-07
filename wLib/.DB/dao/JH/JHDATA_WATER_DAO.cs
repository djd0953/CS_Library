using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHDATA_WATER_DAO : JHDATA_DAO
    {
        public JHDATA_WATER_DAO(MYSQL_T mysql)
        {
            this.mysql = mysql;
            this.table_code = "water";
        }

        public new int INSERT(WB_DATA_DTO dto)
        {
            // TABLE
            string table;
            // PK
            string JHAreaCode, JHDate;
            // COLUMN
            string column;
            string value;
            // SQL
            string sql;

            int rtv;

            CREATE(dto);

            try
            {
                table = $"jh{table_code}";
                JHAreaCode = dto.Cd_dist_obsv;
                JHDate = $"{dto.Datatime:yyyyMMdd}";
                column = $"JHHour{dto.Datatime.Hour + 1}";
                value = dto.Value;

                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {table} (JHAreaCode, JHDate, {column}) ");
                    sb.Append($"VALUES('{JHAreaCode}', '{JHDate}', {value}) ");
                    sb.Append($"ON DUPLICATE KEY UPDATE ");
                    sb.Append($"{column} = IF({value} > {column}, {value}, {column})");

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
