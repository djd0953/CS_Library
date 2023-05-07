using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace wLib
{
    public class KWATER_API_T
    {
        LOG_T log = LOG_T.Instance;
        FCO_CONF fco_conf = new FCO_CONF();


        public List<DATA_WATER_VO> GetData_10min(DateTime datatime)
        {
            List<DATA_WATER_VO> data = new List<DATA_WATER_VO>();
            XmlDocument xml = new XmlDocument();

            string url = fco_conf.Url;
            string key = fco_conf.Key;
            //string code = "3012675";

            try
            {
                url = $"{fco_conf.Url}/{fco_conf.Key}/waterlevel/list/10M/";
                xml.Load(url);

                // 데이터 호출
                XmlNodeList waterList = xml.SelectNodes("/entities/content/Waterlevel");
                foreach (XmlElement news in waterList)
                {
                    try
                    {
                        log.WriteLine(" - 자료수신: {0}", news["ymdhm"].InnerText);

                        String wl = news["wl"].InnerText; // 특보 코드1
                        String ymdhm = news["ymdhm"].InnerText; // 특보 코드1
                        double value = 0;

                        if (wl == "-")
                        {
                            value = 0;
                        }
                        else
                        {
                            if (wl[0] == '.')
                            {
                                wl = "0" + wl;
                            }

                            value = Convert.ToDouble(wl) * 100;
                        }

                        //String nowTime = getColumnsName(ymdhm.Substring(8, 2), "WB");
                        double sendData = value;// / rainBit;
                        String JHDate = ymdhm.Substring(0, 4) + "-" + ymdhm.Substring(4, 2) + "-" + ymdhm.Substring(6, 2) + " " + ymdhm.Substring(8, 2) + ":00:00";
                    }
                    catch (Exception ex)
                    {
                        log.Warning(ex.Message);
                        continue;
                    }
                }

                log.WriteLine(" - 수집완료");
            }
            catch
            {
                throw;
            }

            return data;
        }
    }
}
