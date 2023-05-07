using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_DATA_SNOW_DAO : WB_DATA_DAO
    {
        public WB_DATA_SNOW_DAO(MYSQL_T mysql)
        {
            this.mysql = mysql;
            this.table_code = "snow";
        }

        public new int INSERT_10min(WB_DATA_DTO dto)
        {
            WB_DATA_DTO data = dto.Clone() as WB_DATA_DTO;
            int rtv = 0;

            CREATE_10min(data);

            try
            {
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
