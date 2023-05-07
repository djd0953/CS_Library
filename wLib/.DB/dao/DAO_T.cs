using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class DAO_T
    {
        protected MYSQL_T mysql;
        public string table;

        public string value;
        public string where;
        public string order;
        public string limit;

        public DAO_T()
        {

        }

        public DAO_T(MYSQL_T mysql)
        {
            this.mysql = mysql;
        }

        private void QueryInit()
        {
            value = string.Empty;
            where = string.Empty;
            order = string.Empty;
            limit = string.Empty;
        }

        public int Insert(string sql)
        {
            int rtv;

            try
            {
                mysql.Open();

                rtv = mysql.ExecuteNonQuery(sql);
                
            }
            catch
            {
                throw;
            }
            finally
            {
                QueryInit();
            }

            return rtv;
        }

        public DataTable Select()
        {
            DataTable dt;

            try
            {
                mysql.Open();

                string sql = $"SELECT * " +
                             $"FROM {table} ";

                if (where != string.Empty)
                    sql += $"WHERE {where} ";
                if (order != string.Empty)
                    sql += $"ORDER BY {order} ";
                if (limit != string.Empty)
                    sql += $"LIMIT {limit} ";

                dt = Select(sql);
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public DataTable Select(string sql)
        {
            DataTable dt;

            try
            {
                mysql.Open();

                dt = mysql.ExecuteReader(sql);
                QueryInit();
            }
            catch
            {
                throw;
            }

            return dt;
        }


        public int Update()
        {
            string sql;
            int rtv;

            if (value == string.Empty || where == string.Empty)
            {
                throw new Exception($"SQL Error: value: {value}, where: {where}");
            }

            try
            {
                
                sql = $"UPDATE {table} " +
                      $"SET {value} " +
                      $"WHERE {where}";

                rtv = Update(sql);
            }
            catch
            {
                throw;
            }

            return rtv;
        }

        public int Update(string value, string where)
        {
            this.value = value;
            this.where = where;

            return Update();
        }

        public int Update(string sql)
        {
            int rtv;

            try
            {
                mysql.Open();

                rtv = mysql.ExecuteNonQuery(sql);
            }
            catch
            {
                throw;
            }
            finally
            {
                QueryInit();
            }

            return rtv;
        }
    }
}
