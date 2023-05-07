using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class WB_DATA_EXTERN_DAO : DAO_T
    {
        public WB_DATA_EXTERN_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "wb_data_extern";
        }

        public void Create()
        {
            string sql;

            try
            {
                sql = $"SHOW TABLES LIKE '{table}'";
                if (mysql.ExecuteScalar(sql) == null)
                {
                    // 테이블 생성
                    sql = $"CREATE TABLE `{table}` " +
                          "(" +
                          "`No` INT(11) NOT NULL AUTO_INCREMENT, " +
                          "	`Col1` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col2` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col3` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col4` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col5` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col6` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col7` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col8` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col9` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col10` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col11` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col12` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col13` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col14` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col15` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col16` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col17` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col18` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col19` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col20` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col21` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col22` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col23` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col24` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col25` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col26` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col27` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col28` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col29` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	`Col30` VARCHAR(10) NULL DEFAULT NULL, " +
                          "	PRIMARY KEY (`No`) USING BTREE" +
                          ") ENGINE = InnoDB DEFAULT CHARSET = utf8;" +
                          "COMMIT;";
#if DEBUG
                    Console.WriteLine($"{GetType().Name}::{MethodBase.GetCurrentMethod().Name}(): 테이블 생성({mysql.Ip}:{mysql.Port}.{table})");
#endif
                    mysql.ExecuteNonQuery(sql);
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<WB_DATA_EXTERN_VO> Select(string where = "1=1", string order = "DataTime", string limit = "1000")
        {
            List<WB_DATA_EXTERN_VO> list = new List<WB_DATA_EXTERN_VO>();

            try
            {
                base.where = where;
                base.order = order;
                base.limit = limit;

                DataTable dt = base.Select();
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        try
                        {
                            WB_DATA_EXTERN_VO vo = new WB_DATA_EXTERN_VO();
                            for (int i = 0; i < row.ItemArray.Length - 1; i++)
                            {
                                vo.cols.Add(Convert.ToString(row[$"Col{(i + 1)}"]));
                            }

                            list.Add(vo);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return list;
        }

        public int Insert(WB_DATA_EXTERN_VO vo)
        {
            string sql;
            int rtv;

            StringBuilder columns = new StringBuilder();
            columns.Append("DataTime, ");
            for(int i = 0; i < vo.cols.Count; i++)
            {
                columns.Append(string.Format("col{0}", i + 1));

                if (i < vo.cols.Count -1)
                {
                    columns.Append(", ");
                }
            }

            StringBuilder values = new StringBuilder();
            try
            {
                for (int i = 0; i < vo.cols.Count; i++)
                {
                    values.Append($"'{vo.cols[i].ToString()}'");

                    if (i < vo.cols.Count - 1)
                    {
                        values.Append(", ");
                    }
                }

                sql = $"INSERT INTO {table}({columns.ToString()}) " +
                      $"VALUES({values.ToString()}) ";

                rtv = base.Insert(sql);
            }
            catch
            {
                throw;
            }

            return rtv;
        }
    }
}
