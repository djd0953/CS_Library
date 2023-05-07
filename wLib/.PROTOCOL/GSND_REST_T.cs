using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace wLib.PROTOCOL
{
    public class GSND_REST_T : RESTAPI_T
    {
        public string aToken { get; set; }

        public GSND_REST_T()
        {
            GSND_Rest_Setting();
        }

        public GSND_REST_T(string url)
        {
            this.url = url;

            GSND_Rest_Setting();
        }

        public GSND_REST_T(string url, string aToken)
        {
            this.url = url;
            this.aToken = aToken;

            GSND_Rest_Setting();
        }

        public void GSND_Rest_Setting()
        {
            this.port = "80";
            this.method = "POST";
            this.contenttype = "application/json";
            this.timeout = 5;
        }

        public string APIAccessToken(string id, string pw)
        {
            string url = $"{this.url}/c/v1/lo";
            this.body = $"{{\"dsCode\":\"{id}\",\"dsPwd\":\"{pw}\"}}";

            try
            {
                HttpWebRequest req = Create(url);
                req = Write(req);

                return Command(req);
            }
            catch
            {
                throw;
            }
        }

        public string APIReAccessToken(string id, string rToken)
        {
            string url = $"{this.url}/c/v1/rl/{id}";

            try
            {
                HttpWebRequest req = Create(url);
                req.Headers.Add("Authorization", $"Bearer {rToken}");
                req = Write(req);

                return Command(req);
            }
            catch
            {
                throw;
            }
        }

        public string GetEquipInfo(string id)
        {
            string url = $"{this.url}/c/v1/obsv/info/{id}";
            this.body = null;

            return SendHttpWebRequest(url);
        }

        public string SetEquipInfo(string id, string body)
        {
            string url = $"{this.url}/c/v1/obsv/insert/{id}";
            this.body = body;

            return SendHttpWebRequest(url);
        }

        public string EquipInfoUpdate(string id, string body)
        {
            string url = $"{this.url}/c/v1/obsv/update/{id}";
            this.body = body;

            return SendHttpWebRequest(url);
        }

        public string GetData(string id)
        {
            string url = $"{this.url}/c/v1/obsvrecd/insert/{id}";
            this.body = null;

            return SendHttpWebRequest(url);
        }

        public string SetData(string id, string body)
        {
            string url = $"{this.url}/c/v1/obsvrecd/insert/{id}";
            this.body = body;

            return SendHttpWebRequest(url);
        }

        private string SendHttpWebRequest(string url)
        {
            try
            {
                HttpWebRequest req = Create(url);
                req.Headers.Add("Authorization", $"Bearer {this.aToken}");
                req = Write(req);

                return Command(req);
            }
            catch
            {
                throw;
            }
        }
    }
}
