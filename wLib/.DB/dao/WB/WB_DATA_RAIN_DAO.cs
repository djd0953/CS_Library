using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_DATA_RAIN_DAO : WB_DATA_DAO
    {
        public WB_DATA_RAIN_DAO(MYSQL_T mysql)
        {
            this.mysql = mysql;
            this.table_code = "rain";
        }

        public new int INSERT_10min(WB_DATA_DTO dto)
        {
            WB_DATA_DTO data = dto.Clone() as WB_DATA_DTO;
            int rtv = 0;

            CREATE_10min(data);

            try
            {
                // 1분 데이터 조회 후 합계값 선정
                string[] value_temp = SELECT_1min(data);
                double sum_value = 0.0;

                for (int i = 0; i < 10; i++)
                {
                    if (double.TryParse(value_temp[(data.Datatime.Minute / 10 * 10) + i], out double fValue))
                    {
                        sum_value += fValue;
                    }
                }

                data.Value = sum_value.ToString("F3");

                rtv = base.INSERT_10min(data);
                if (rtv > 0)
                {
                    base.UPDATE_10min_calc(data);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): {ex.Message}");
#endif
                throw;
            }

            return rtv;
        }

        public new int INSERT_1hour(WB_DATA_DTO dto)
        {
            WB_DATA_DTO data = dto.Clone() as WB_DATA_DTO;
            int rtv = 0;

            CREATE_1hour(data);

            // 10분 자료에서 합계값 구하기
            try
            {
                string[] value_temp = SELECT_10min(data);
                double sum_value = 0;

                for (int i = 0; i < 6; i++)
                {
                    if (double.TryParse(value_temp[i], out double fVal))
                    {
                        sum_value += fVal;
                    }
                }

                data.Value = sum_value.ToString("F3");

                rtv = base.INSERT_1hour(data);
                if (rtv > 0)
                {
                    base.UPDATE_1hour_calc(data);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): {ex.Message}");
#endif
                throw;
            }

            return rtv;
        }

        public override int Insert_dis(WB_DATA_DTO dto)
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
                    string y = row["RegDate"].ToString().Substring(0, 4);
                    string m = row["RegDate"].ToString().Substring(4, 2);
                    string d = row["RegDate"].ToString().Substring(6, 2);
                    string h = row["RegDate"].ToString().Substring(8, 2);

                    for (int i = 0; i < dataArr.Length; i++)
                    {
                        string min = i.ToString("D2");
                        if (double.TryParse(dataArr[i], out double value))
                        {
                            WB_DATA_DTO data_dto = new WB_DATA_DTO()
                            {
                                Cd_dist_obsv = cd_dist_obsv,
                                Datatime = Convert.ToDateTime($"{y}-{m}-{d} {h}:{min}:00"),
                                Value = value.ToString()
                            };
                            list.Add(data_dto);
                        };
                    }
                }

                sub_query = $"SELECT SUM(DaySum) AS `Month`, CD_DIST_OBSV, Left(RegDate, 6) AS `Date` FROM wb_{table_code}1hour GROUP BY `Date` HAVING CD_DIST_OBSV = '{cd_dist_obsv}' AND `Date` = '{settingTime:yyyyMM}'";
                var res = mysql.ExecuteScalar(sub_query);
                string month = res.ToString();

                sub_query = $"SELECT SUM(DaySum) AS `Year`, CD_DIST_OBSV, Left(RegDate, 4) AS `Date` FROM wb_{table_code}1hour GROUP BY `Date` HAVING CD_DIST_OBSV = '{cd_dist_obsv}' AND `Date` = '{settingTime:yyyy}'";
                res = mysql.ExecuteScalar(sub_query);
                string year = res.ToString();

                double yester = list.FindAll(x => x.Datatime.ToString("yyyyMMdd") == settingTime.AddDays(-1).ToString("yyyyMMdd")).Sum(x => double.Parse(x.Value));
                double today = list.FindAll(x => x.Datatime.ToString("yyyyMMdd") == settingTime.ToString("yyyyMMdd")).Sum(x => double.Parse(x.Value));
                string now = list.Last().Value.ToString();

                double mov1h = list.FindAll(x => x.Datatime >= settingTime.AddHours(-1)).Sum(x => double.Parse(x.Value));
                double mov2h = list.FindAll(x => x.Datatime >= settingTime.AddHours(-2)).Sum(x => double.Parse(x.Value));
                double mov3h = list.FindAll(x => x.Datatime >= settingTime.AddHours(-3)).Sum(x => double.Parse(x.Value));
                double mov6h = list.FindAll(x => x.Datatime >= settingTime.AddHours(-6)).Sum(x => double.Parse(x.Value));
                double mov12h = list.FindAll(x => x.Datatime >= settingTime.AddHours(-12)).Sum(x => double.Parse(x.Value));
                double mov24h = list.FindAll(x => x.Datatime >= settingTime.AddHours(-24)).Sum(x => double.Parse(x.Value));

                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {table} ");
                    sb.Append("(CD_DIST_OBSV, RegDate, rain_yester, rain_today, rain_hour, rain_month, rain_year, mov_1h, mov_2h, mov_3h, mov_6h, mov_12h, mov_24h)");
                    sb.Append($" VALUES ('{cd_dist_obsv}', '{regdate}', {yester}, {today}, {now}, {month}, {year}, {mov1h}, {mov2h}, {mov3h}, {mov6h}, {mov12h}, {mov24h}) ");

                    sb.Append($"ON DUPLICATE KEY UPDATE ");
                    sb.Append($"rain_yester = {yester}, ");
                    sb.Append($"rain_today = {today}, ");
                    sb.Append($"rain_hour = {now}, ");
                    sb.Append($"rain_month = {month}, ");
                    sb.Append($"rain_year = {year}, ");
                    sb.Append($"mov_1h = {mov1h}, ");
                    sb.Append($"mov_2h = {mov2h}, ");
                    sb.Append($"mov_3h = {mov3h}, ");
                    sb.Append($"mov_3h = {mov3h}, ");
                    sb.Append($"mov_6h = {mov6h}, ");
                    sb.Append($"mov_12h = {mov12h}, ");
                    sb.Append($"mov_24h = {mov24h}, ");
                    sb.Append($"RegDate = '{settingTime:yyyy-MM-dd HH:mm:ss}'");

                    sql = sb.ToString();
                }

                    rtv = mysql.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): {ex.Message}");
#endif
                throw;
            }

            return rtv;
        }
    }
}
