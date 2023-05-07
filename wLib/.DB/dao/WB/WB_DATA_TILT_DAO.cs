using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_DATA_TILT_DAO : WB_DATA_DAO
    {
        public WB_DATA_TILT_DAO(MYSQL_T mysql)
        {
            this.mysql = mysql;
            this.table_code = "tilt";
        }

        public new int INSERT_10min(WB_DATA_DTO dto)
        {
            WB_DATA_DTO data = dto.Clone() as WB_DATA_DTO;
            int rtv = 0;

            CREATE_10min(dto);

            try
            {
                // 1분 데이터 조회 후 최대값 선정
                //string[] value_temp = SELECT_1min(dto);
                //double tmax_value = double.Parse(dto.Value);
                //
                //for (int i = 0; i < 10; i++)
                //{
                //    if (double.TryParse(value_temp[(dto.Datatime.Minute / 10 * 10) + i], out double fValue))
                //    {
                //        if (Math.Abs(fValue) > Math.Abs(tmax_value))
                //        {
                //            tmax_value = fValue;
                //        }
                //    }
                //}
                //dto.Value = tmax_value.ToString("F3");

                rtv = base.INSERT_10min(dto);
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
                //double tmax_value = double.Parse(data.Value);
                //
                //for (int i = 0; i < 6; i++)
                //{
                //    if (double.TryParse(value_temp[i], out double fVal))
                //    {
                //        if (Math.Abs(fVal) > Math.Abs(tmax_value))
                //        {
                //            tmax_value = fVal;
                //        }
                //    }
                //}
                //data.Value = tmax_value.ToString("F3");

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
    }

}
