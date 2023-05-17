using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_DATA_FLOOD_DAO : WB_DATA_DAO
    {
        public WB_DATA_FLOOD_DAO(MYSQL_T mysql)
        {
            this.mysql = mysql;
            this.table_code = "flood";
        }

        public new int INSERT_10min(WB_DATA_DTO dto)
        {
            WB_DATA_DTO data = dto.Clone() as WB_DATA_DTO;
            int rtv = 0;

            CREATE_10min(data);

            try
            {
                // 1분 데이터 조회 후 Bit OR 값 선정
                //string[] value_temp = SELECT_1min(data);
                //int bit_value = 0;
                //
                //for (int i = 0; i < 10; i++)
                //{
                //    if (byte.TryParse(value_temp[(data.Datatime.Minute / 10 * 10) + i], out byte value))
                //    {
                //        bit_value |= value;
                //    }
                //}
                //data.Value = $"{bit_value:D3}";

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

            // 10분 자료에서 Bit OR 값 구하기
            try
            {
                //string[] value_temp = SELECT_10min(data);
                //int bit_value = 0;
                //
                //for (int i = 0; i < 6; i++)
                //{
                //    if (byte.TryParse(value_temp[i], out byte value))
                //    {
                //        bit_value |= value;
                //    }
                //}
                //data.Value = $"{bit_value:D3}";

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
            string table, flood_table, water_table;
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
                flood_table = $"wb_flood1min_{settingTime:yyyy}";
                water_table = $"wb_water1min_{settingTime:yyyy}";
                regdate = $"{dto.Datatime:yyyy-MM-dd HH:mm:ss}";
                cd_dist_obsv = dto.Cd_dist_obsv;

                sub_query = $"SELECT * FROM {flood_table} WHERE CD_DIST_OBSV = '{cd_dist_obsv}' AND RegDate >= '{settingTime.AddDays(-2):yyyyMMdd}00' ORDER BY RegDate ASC";
                List<WB_DATA_DTO> flood_list = new List<WB_DATA_DTO>();
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
                        if (!string.IsNullOrEmpty(dataArr[i]))
                        {
                            WB_DATA_DTO data_dto = new WB_DATA_DTO()
                            {
                                Cd_dist_obsv = cd_dist_obsv,
                                Datatime = Convert.ToDateTime($"{year}-{month}-{day} {hour}:{minute}:00"),
                                Value = dataArr[i]
                            };
                            flood_list.Add(data_dto);
                        }
                    }
                }

                sub_query = $"SELECT * FROM {water_table} WHERE CD_DIST_OBSV = '{cd_dist_obsv}' AND RegDate >= '{settingTime.AddDays(-2):yyyyMMdd}00' ORDER BY RegDate ASC";
                List<WB_DATA_DTO> water_list = new List<WB_DATA_DTO>();
                dt = mysql.ExecuteReader(sub_query);
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
                            water_list.Add(data_dto);
                        };
                    }
                }

                string yester;
                string today;
                string now;
                {
                    if (flood_list.Exists(x => x.Datatime.ToString("yyyyMMdd") == settingTime.AddDays(-1).ToString("yyyyMMdd") && x.Value == "111"))
                    {
                        yester = "111";
                    }
                    else if (flood_list.Exists(x => x.Datatime.ToString("yyyyMMdd") == settingTime.AddDays(-1).ToString("yyyyMMdd") && x.Value == "011"))
                    {
                        yester = "011";
                    }
                    else if (flood_list.Exists(x => x.Datatime.ToString("yyyyMMdd") == settingTime.AddDays(-1).ToString("yyyyMMdd") && x.Value == "001"))
                    {
                        yester = "001";
                    }
                    else
                    {
                        yester = "000";
                    }

                    if (flood_list.Exists(x => x.Datatime.ToString("yyyyMMdd") == settingTime.ToString("yyyyMMdd") && x.Value == "111"))
                    {
                        today = "111";
                    }
                    else if (flood_list.Exists(x => x.Datatime.ToString("yyyyMMdd") == settingTime.ToString("yyyyMMdd") && x.Value == "011"))
                    {
                        today = "011";
                    }
                    else if (flood_list.Exists(x => x.Datatime.ToString("yyyyMMdd") == settingTime.ToString("yyyyMMdd") && x.Value == "001"))
                    {
                        today = "001";
                    }
                    else
                    {
                        today = "000";
                    }

                    now = flood_list.Last().Value;
                }

                double w_yester = water_list.FindAll(x => x.Datatime.ToString("yyyyMMdd") == settingTime.AddDays(-1).ToString("yyyyMMdd")).Max(x => double.Parse(x.Value));
                double w_today = water_list.FindAll(x => x.Datatime.ToString("yyyyMMdd") == settingTime.ToString("yyyyMMdd")).Max(x => double.Parse(x.Value));
                double w_now = double.Parse(water_list.Last().Value);

                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {table} ");
                    sb.Append("(CD_DIST_OBSV, RegDate, yester, today, now)");
                    sb.Append($" VALUES ('{cd_dist_obsv}', '{regdate}', '{yester}/{w_yester}', '{today}/{w_today}', '{now}/{w_now}') ");

                    sb.Append($"ON DUPLICATE KEY UPDATE ");
                    sb.Append($"yester = '{yester}/{w_yester}', ");
                    sb.Append($"today = '{today}/{w_today}', ");
                    sb.Append($"now = '{now}/{w_now}'");

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
