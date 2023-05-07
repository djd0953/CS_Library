using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wLib;
using wLib.DB;

namespace TEST
{
    class Program
    {
        static TEST test = new TEST();

        static void Main(string[] args)
        {
            Console.WriteLine("hello world!");

            test.test1();

            Console.ReadKey();
        }

        class TEST
        {
            public void test1()
            {
                string[] temp = new string[60];
                temp[10] = "0.00";

                string rtv = string.Join("/", temp);
                //Console.WriteLine(rtv);

                using (MYSQL_T mysql = new MYSQL_T(new DB_CONF("LOCAL")))
                {
                    mysql.Open();

                    WB_DATA_DTO dto = new WB_DATA_DTO();
                    dto.Datatime = DateTime.Now;
                    dto.Sub_obsv = "1";
                    dto.Value = "-10.25";
                    
                    WB_DATA_RAIN_DAO rain_dao = new WB_DATA_RAIN_DAO(mysql);
                    dto.Type = WB_DATA_TYPE.RAIN;
                    rain_dao.INSERT_1min(dto);
                    rain_dao.INSERT_10min(dto);
                    rain_dao.INSERT_1hour(dto);

                    WB_DATA_WATER_DAO water_dao = new WB_DATA_WATER_DAO(mysql);
                    dto.Type = WB_DATA_TYPE.WATER;
                    water_dao.INSERT_1min(dto);
                    water_dao.INSERT_10min(dto);
                    water_dao.INSERT_1hour(dto);

                    WB_DATA_DPLACE_DAO dplace_dao = new WB_DATA_DPLACE_DAO(mysql);
                    dto.Type = WB_DATA_TYPE.DPLACE;
                    dplace_dao.INSERT_1min(dto);
                    dplace_dao.INSERT_10min(dto);
                    dplace_dao.INSERT_1hour(dto);

                    WB_DATA_SOIL_DAO soil_dao = new WB_DATA_SOIL_DAO(mysql);
                    dto.Type = WB_DATA_TYPE.SOIL;
                    soil_dao.INSERT_1min(dto);
                    soil_dao.INSERT_10min(dto);
                    soil_dao.INSERT_1hour(dto);

                    WB_DATA_TILT_DAO tilt_dao = new WB_DATA_TILT_DAO(mysql);
                    dto.Type = WB_DATA_TYPE.TILT;
                    tilt_dao.INSERT_1min(dto);
                    tilt_dao.INSERT_10min(dto);
                    tilt_dao.INSERT_1hour(dto);

                    WB_DATA_SNOW_DAO snow_dao = new WB_DATA_SNOW_DAO(mysql);
                    dto.Type = WB_DATA_TYPE.SNOW;
                    snow_dao.INSERT_1min(dto);
                    snow_dao.INSERT_10min(dto);
                    snow_dao.INSERT_1hour(dto);

                    WB_DATA_FLOOD_DAO flood_dao = new WB_DATA_FLOOD_DAO(mysql);
                    dto.Type = WB_DATA_TYPE.FLOOD;
                    dto.Value = "000";
                    flood_dao.INSERT_1min(dto);
                    flood_dao.INSERT_10min(dto);
                    flood_dao.INSERT_1hour(dto);
                }

                Console.WriteLine("End Program.");
            }
        }
    }
}
