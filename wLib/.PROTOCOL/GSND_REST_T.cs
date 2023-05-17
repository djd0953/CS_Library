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

        /// <summary>
        /// Access Token 재발급
        /// </summary>
        /// <param name="id">dsCode</param>
        /// <param name="pw">dsPwd</param>
        /// <returns>
        /// {
        ///     "resultCode": "101",
        ///     "body": {
        ///         "atoken": "eyJh...",
        ///     }
        /// }
        /// or
        /// {
        ///     "resultCode": "216",
        ///     "resultMessage": "ID 또는 비밀번호를 확인 바랍니다."
        /// }
        /// </returns>
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

        /// <summary>
        /// Access Token 발급
        /// </summary>
        /// <param name="id">dsCode</param>
        /// <param name="rToken">Http Header의 Access Token을 Refresh Token으로 대체하여 전송</param>
        /// <returns>
        /// {
        ///     "resultCode": "101",
        ///     "body": {
        ///         "atoken": "eyJh...",
        ///         "rtoken": "eyJh..."
        ///     }
        /// }
        /// or
        /// {
        ///     "resultCode": "216",
        ///     "resultMessage": "ID 또는 비밀번호를 확인 바랍니다."
        /// }
        /// </returns>
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

        /// <summary>
        /// 계측기 정보 조회
        /// </summary>
        /// <param name="id">dsCode</param>
        /// <returns>
        /// {
        ///     "resultCode": "101",
        ///     "body": [
        ///         {
        ///             "dsCode": "4812000001",
        ///             "cdDistObsv": 1,
        ///             "gbObsv": "01",
        ///             "nmDistObsv": "강우량계1",
        ///             "bdongCd": "4812110200",
        ///             "mntnAdresAt": "N",
        ///             "mlnm": "412",
        ///             "aulnm": "5",
        ///             "dtlAdres": "경상남도 창원시 의창구 도계동 00번지",
        ///             "rdnmadrCd": "112304115227",
        ///             "rnDtlAdres": "경상남도 창원시 의창구 00번길",
        ///             "spoNoCd": "다사54755034",
        ///             "orgnCd": "5310055",
        ///             "lat": 35.0000000,
        ///             "lon": 128.1200000,
        ///             "rm": "비고",
        ///             "useYn": "Y",
        ///             "rgsDe": "2022-08-30 09:54:42",
        ///             "updDe": "2022-08-30 09:57:10"
        ///         },
        ///         {
        ///             "dsCode": "4812000001",
        ///             "cdDistObsv": 2,
        ///             "gbObsv": "01",
        ///             "nmDistObsv": "강우량계2",
        ///             "bdongCd": "4812110200",
        ///             "mntnAdresAt": "N",
        ///             "mlnm": "412",
        ///             "aulnm": "5",
        ///             "dtlAdres": "경상남도 창원시 의창구 도계동 00번지",
        ///             "rdnmadrCd": "112304115227",
        ///             "rnDtlAdres": "경상남도 창원시 의창구 00번길",
        ///             "spoNoCd": "다사54755034",
        ///             "orgnCd": "5310055",
        ///             "lat": 35.0000000,
        ///             "lon": 128.1200000,
        ///             "rm": "비고",
        ///             "useYn": "Y",
        ///             "rgsDe": "2022-08-30 09:57:02",
        ///             "updDe": "2022-08-30 09:57:02"
        ///         }
        ///     ]
        /// }
        /// </returns>
        public string GetEquipInfo(string id)
        {
            string url = $"{this.url}/c/v1/obsv/info/{id}";
            this.body = null;

            return SendHttpWebRequest(url);
        }


        /// <summary>
        /// 계측기 정보 등록
        /// </summary>
        /// <param name="id">dsCode</param>
        /// <param name="body">
        /// {
        ///     "cdDistObsv": 2,
        ///     "gbObsv": "01",
        ///     "nmDistObsv": "강우량계2",
        ///     "bdongCd": "4812110200",
        ///     "mntnAdresAt": "N",
        ///     "mlnm": "412",
        ///     "aulnm": "5",
        ///     "dtlAdres": "경상남도 창원시 의창구 도계동 00번지",
        ///     "rdnmadrCd": "112304115227",
        ///     "rnDtlAdres": "경상남도 창원시 의창구 00번길",
        ///     "spoNoCd": "다사54755034",
        ///     "orgnCd": "5310055",
        ///     "lat": 35.0000000,
        ///     "lon": 128.1200000,
        ///     "rm": "비고",
        ///     "useYn": "Y" 
        /// }
        /// </param>
        /// <returns>
        /// {
        ///     "resultCode": "101"
        /// }
        /// or
        /// {
        ///     "resultCode": "201",
        ///     "resultMessage": "파라미터가 적합하지 않습니다.",
        ///     "body": [
        ///         "계측기 구분 은(는) 필수입력"
        ///     ]
        /// }
        /// </returns>
        public string SetEquipInfo(string id, string body)
        {
            string url = $"{this.url}/c/v1/obsv/insert/{id}";
            this.body = body;

            return SendHttpWebRequest(url);
        }

        /// <summary>
        /// 계측기 정보 수정
        /// </summary>
        /// <param name="id">dsCode</param>
        /// <param name="body">
        /// {
        ///     "cdDistObsv": 2,
        ///     "gbObsv": "01",
        ///     "nmDistObsv": "강우량계2",
        ///     "bdongCd": "4812110200",
        ///     "mntnAdresAt": "N",
        ///     "mlnm": "412",
        ///     "aulnm": "5",
        ///     "dtlAdres": "경상남도 창원시 의창구 도계동 00번지",
        ///     "rdnmadrCd": "112304115227",
        ///     "rnDtlAdres": "경상남도 창원시 의창구 00번길",
        ///     "spoNoCd": "다사54755034",
        ///     "orgnCd": "5310055",
        ///     "lat": 35.0000000,
        ///     "lon": 128.1200000,
        ///     "rm": "비고",
        ///     "useYn": "Y" 
        /// }
        /// </param>
        /// <returns>
        /// {
        ///     "resultCode": "101"
        /// }
        /// or
        /// {
        ///     "resultCode": "201",
        ///     "resultMessage": "파라미터가 적합하지 않습니다.",
        ///     "body": [
        ///         "계측기 구분 은(는) 필수입력"
        ///     ]
        /// }
        /// </returns>
        public string EquipInfoUpdate(string id, string body)
        {
            string url = $"{this.url}/c/v1/obsv/update/{id}";
            this.body = body;

            return SendHttpWebRequest(url);
        }

        /// <summary>
        /// 데이터 기록 조회
        /// </summary>
        /// <param name="id">dsCode</param>
        /// <returns>
        /// {
        ///     "resultCode": "101",
        ///     "body": [
        ///         {
        ///             "dsCode": "4812000001",
        ///             "cdDistObsv": "1",
        ///             "gbObsv": "01",
        ///             "obsrDe": "20221010091900",
        ///             "obsrGb": "RF",
        ///             "obsrValue": 0.000,
        ///             "obsrValueTxt": null,
        ///             "rgsDe": "2022-10-10 14:41:26",
        ///             "updDe": "2022-10-10 14:41:26"
        ///         },
        ///         {
        ///             "dsCode": "4812000001",
        ///             "cdDistObsv": "2",
        ///             "gbObsv": "01",
        ///             "obsrDe": "20221100091900",
        ///             "obsrGb": "RF",
        ///             "obsrValue": 0.000,
        ///             "obsrValueTxt": null,
        ///             "rgsDe": "2022-10-10 14:41:26",
        ///             "updDe": "2022-10-10 14:41:26"
        ///         }
        ///     ]
        /// }
        /// or
        /// {
        ///     "resultCode": "219",
        ///     "resultMessage": "조회된 데이타가 없습니다."
        /// }
        /// </returns>
        public string GetData(string id)
        {
            string url = $"{this.url}/c/v1/obsvrecd/info/{id}";
            this.body = null;

            return SendHttpWebRequest(url);
        }

        /// <summary>
        /// 계측 기록 등록
        /// </summary>
        /// <param name="id">dsCode</param>
        /// <param name="body">
        /// [
        ///     {
        ///     "cdDistObsv": 1,
        ///     "obsrDe":"20220822090900",
        ///     "obsrGb":"RF",
        ///     "obsrValue":0.1
        ///     },
        ///     {
        ///     "cdDistObsv": 2,
        ///     "obsrDe":"20220822090900",
        ///     "obsrGb":"RF",    
        ///     "obsrValue":0.12
        ///     }
        /// ]
        /// </param>
        /// <returns>
        /// {
        ///     "resultCode": "101"
        /// }
        /// or
        /// {
        ///     "resultCode": "201",
        ///     "resultMessage": "파라미터가 적합하지 않습니다.",
        ///     "body": [
        ///         "계측기 구분 은(는) 필수입력"
        ///     ]
        /// }
        /// </returns>
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
