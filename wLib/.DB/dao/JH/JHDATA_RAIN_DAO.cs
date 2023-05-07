using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class JHDATA_RAIN_DAO : JHDATA_DAO
    {
        public JHDATA_RAIN_DAO(MYSQL_T mysql)
        {
            this.mysql = mysql;
            this.table_code = "rain";
        }
    }
}
