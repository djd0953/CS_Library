using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_DATA_DPLACE_DAO : WB_DATA_DAO
    {

        public WB_DATA_DPLACE_DAO(MYSQL_T mysql)
        {
            this.mysql = mysql;
            table_code = "dplace";
        }

        public new int INSERT_10min(WB_DATA_DTO dto)
        {
            WB_DATA_DTO data = dto.Clone() as WB_DATA_DTO;
            int rtv = 0;

            CREATE_10min(data);

            try
            {
                // 1분 데이터 조회 후 최대값 선정
                //string[] value_temp = SELECT_1min(data);
                //if (double.TryParse(data.Value, out double tmax_value) == true)
                //{
                //    for (int i = 0; i < 10; i++)
                //    {
                //        if (double.TryParse(value_temp[(data.Datatime.Minute / 10 * 10) + i], out double fValue))
                //        {
                //            if (Math.Abs(fValue) > Math.Abs(tmax_value))
                //            {
                //                tmax_value = fValue;
                //            }
                //        }
                //    }
                //
                //    data.Value = tmax_value.ToString("F3");
                //}
                //else data.Value = "null";

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

            try
            {
                // 10분 자료에서 최대값 구하기
                //string[] value_temp = SELECT_10min(data);
                //if (double.TryParse(dto.Value, out double data_value) == true)
                //{
                //    double tmax_value = double.Parse(data.Value);
                //
                //    for (int i = 0; i < 6; i++)
                //    {
                //        if (double.TryParse(value_temp[i], out double fVal))
                //        {
                //            if (Math.Abs(fVal) > Math.Abs(tmax_value))
                //            {
                //                tmax_value = fVal;
                //            }
                //        }
                //    }
                //    data.Value = tmax_value.ToString("F3");
                //}
                //else data.Value = "null";

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
            int sub_obsv;
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
                sub_obsv = int.TryParse(dto.Sub_obsv, out sub_obsv) ? sub_obsv : 1;

                sub_query = $"SELECT * FROM {data_table} WHERE CD_DIST_OBSV = '{cd_dist_obsv}' AND SUB_OBSV = '{sub_obsv}' AND RegDate >= '{settingTime.AddDays(-2):yyyyMMdd}00' ORDER BY RegDate ASC";
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

                    for (int i = 0; i < dataArr.Length; i++)
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

                double stand = 0;
                if (dto.standValue == null || double.TryParse(dto.standValue, out stand) == false)
                {
                    sub_query = $"SELECT MRMin FROM {data_table} WHERE CD_DIST_OBSV = '{cd_dist_obsv}' AND SUB_OBSV = '{sub_obsv}' ORDER BY RegDate ASC LIMIT 10";
                    dt = mysql.ExecuteReader(sub_query);
                    foreach (DataRow row in dt.Rows) 
                    {
                        if (dto.standValue != null) break;

                        string[] data = row["MRMin"].ToString().Split('/');
                        foreach (string minuteData in data)
                        {
                            if (double.TryParse(minuteData, out stand) && (stand != 0 || stand < 50000))
                            {
                                dto.standValue = minuteData;
                                break;
                            }
                        }
                    }
                }

                double yester = list.FindAll(x => x.Datatime.ToString("yyyyMMdd") == settingTime.AddDays(-1).ToString("yyyyMMdd")).Max(x => double.Parse(x.Value));
                double today = list.FindAll(x => x.Datatime.ToString("yyyyMMdd") == settingTime.ToString("yyyyMMdd")).Max(x => double.Parse(x.Value));
                double now = double.Parse(list.Last().Value);

                double speed;
                if (list.Exists(x => x.Datatime == settingTime.AddHours(-1)))
                {
                    speed = Math.Abs(double.Parse(list.Find(x => x.Datatime == settingTime.AddHours(-1)).Value) - now);
                }
                else
                {
                    speed = 0;
                }

                double change = Math.Abs((dto.standValue != null ? stand : now) - now);

                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {table} ");
                    sb.Append("(CD_DIST_OBSV, SUB_OBSV, RegDate, dplace_yester, dplace_today, dplace_now, dplace_speed, dplace_stand, dplace_change)");
                    sb.Append($" VALUES ('{cd_dist_obsv}', {sub_obsv}, '{regdate}', {yester}, {today}, {now}, {speed}, {(dto.standValue != null ? stand : now)}, {change}) ");

                    sb.Append($"ON DUPLICATE KEY UPDATE ");
                    sb.Append($"dplace_yester = {yester}, ");
                    sb.Append($"dplace_today = {today}, ");
                    sb.Append($"dplace_now = {now}, ");
                    sb.Append($"dplace_speed = {speed}, ");
                    sb.Append($"dplace_stand = {stand}, ");
                    sb.Append($"dplace_change = {change}, ");
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
