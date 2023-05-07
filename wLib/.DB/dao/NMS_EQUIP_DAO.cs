using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class NMS_EQUIP_DAO : DAO_T
    {
        public NMS_EQUIP_DAO(MYSQL_T mysql)
        {
            base.mysql = mysql;
            base.table = "nms_equip";
        }

        public IEnumerable<NMS_EQUIP_VO> Select(string where = "1=1", string order = "JHACode AND CD_DIST_OBSV", string limit = "1000")
        {
            List<NMS_EQUIP_VO> list = new List<NMS_EQUIP_VO>();

            // 표준
            // 01: 강우량계
            // 02: 수위계
            // 03: 변위계
            // 04: 토양함수비계
            // 05: 거리측정기
            // 06: 적설계
            // 07: 지하수위계
            // 08: 경사계
            // 09: 간극수압계
            // 10: 진동계
            // 11: 지중경사계
            // 12: 하중계
            // 13: 구조물경사계
            // 14: GNSS(GPS)
            // 15: 지표변위계
            // 16: 조위계

            // 비표준
            // 17: 방송
            // 18: 전광판
            // 19: CCTV
            // 20: 차단기
            // 21: 침수

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
                        NMS_EQUIP_VO vo = new NMS_EQUIP_VO();

                        try
                        {
                            vo.JHACode = Convert.ToString(row["JHACode"]);
                        }
                        catch { }

                        try
                        {

                            vo.Cd_dist_obsv = Convert.ToString(row["CD_DIST_OBSV"]);
                        }
                        catch { }

                        try
                        {

                            vo.Sub_obsv = Convert.ToString(row["SUB_OBSV"]);
                        }
                        catch { }

                        try
                        {
                            vo.Nm_dist_obsv = Convert.ToString(row["NM_DIST_OBSV"]);
                        }
                        catch { }

                        try
                        {
                            vo.ConnModel = Convert.ToString(row["ConnModel"]);
                        }
                        catch { }

                        try
                        {
                            vo.ConnPhone = Convert.ToString(row["ConnPhone"]);
                        }
                        catch { }

                        try
                        {
                            vo.ConnIp = Convert.ToString(row["ConnIP"]);
                        }
                        catch { }

                        try
                        {
                            vo.ConnPort = Convert.ToString(row["ConnPort"]);
                        }
                        catch { }

                        try
                        {
                            vo.LoggerTime = Convert.ToString(row["LoggerTime"]);
                        }
                        catch { }

                        try
                        {
                            vo.LastStatus = Convert.ToString(row["LastStatus"]);
                        }
                        catch { }

                        try
                        {
                            if (row["LastDate"] is DateTime)
                            {
                                vo.LastDate = Convert.ToDateTime(row["LastDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            else vo.LastDate = Convert.ToString(row["LastDate"]);
                        }
                        catch { }

                        try
                        {
                            vo.Data = Convert.ToString(row["Data"]);
                        }
                        catch { }

                        try
                        {
                            vo.Gb_obsv = Convert.ToString(row["GB_OBSV"]);
                        }
                        catch { }

                        try
                        {
                            vo.RainBit = Convert.ToString(row["RainBit"]);
                        }
                        catch { }

                        try
                        {
                            vo.Lat = Convert.ToString(row["LAT"]);
                        }
                        catch { }

                        try
                        {
                            vo.Lon = Convert.ToString(row["LON"]);
                        }
                        catch { }

                        list.Add(vo);
                    }
                }
            }
            catch
            {
                throw;
            }

            return list;
        }

        public int INSERT(NMS_EQUIP_VO vo)
        {
            // SQL
            string sql;

            int rtv;

            // CREATE SQL
            try
            {
                StringBuilder sb = new StringBuilder();
                {
                    sb.Append($"INSERT INTO {table} (JHACode, cd_dist_obsv, sub_obsv, Nm_dist_obsv, ConnModel, ConnPhone, ConnIp, ConnPort, LoggerTime, LastStatus, LastDate, Data, Gb_obsv, RainBit, Lat, Lon) ");

                    sb.Append("VALUES");
                    sb.Append($"('{vo.JHACode}', '{vo.Cd_dist_obsv}', '{vo.Sub_obsv}', '{vo.Nm_dist_obsv}', '{vo.ConnModel}', '{vo.ConnPhone}', '{vo.ConnIp}', '{vo.ConnPort}', '{vo.LoggerTime}', '{vo.LastStatus}', '{vo.LastDate}', '{vo.Data}', '{vo.Gb_obsv}', '{vo.RainBit}', '{vo.Lat}', '{vo.Lon}') ");

                    sb.Append($"ON DUPLICATE KEY UPDATE ");
                    sb.Append($"Nm_dist_obsv = '{vo.Nm_dist_obsv}', ");
                    sb.Append($"ConnModel = '{vo.ConnModel}', ");
                    sb.Append($"ConnPhone = '{vo.ConnPhone}', ");
                    sb.Append($"ConnIp = '{vo.ConnIp}', ");
                    sb.Append($"ConnPort = '{vo.ConnPort}', ");
                    sb.Append($"LoggerTime = '{vo.LoggerTime}', ");
                    sb.Append($"LastStatus = '{vo.LastStatus}', ");
                    sb.Append($"LastDate = '{vo.LastDate}', ");
                    sb.Append($"Data = '{vo.Data}', ");
                    sb.Append($"Gb_obsv = '{vo.Gb_obsv}', ");
                    sb.Append($"RainBit = '{vo.RainBit}', ");
                    sb.Append($"Lat = '{vo.Lat}', ");
                    sb.Append($"Lon = '{vo.Lon}' ");

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
