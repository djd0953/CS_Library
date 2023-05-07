﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public class XROSHOT_CONF : DB_INFO
    {
        // new CONFIG(string "설정 파일명", string "폴더명:기본값etc")
        CONFIG config = new CONFIG("CAST.ini");

        // 설정파일 항목([섹션])
        public string section;

        // 설정파일 항목(키=기본값)
        public bool used = false;
        public string comment = "DB(크로샷)";
        public override string Ip { get; set; } = "localhost";
        public override string Port { get; set; } = "3306";
        public override string Dbname { get; set; } = "";
        public override string Id { get; set; } = "";
        public override string Pw { get; set; } = "";
        public override string Charset { get; set; } = ""; // Using Default
        public override string Timeout { get; set; } = ""; // Mysql Default 15

        public string User { get; set; } = "";
        public string Callback { get; set; } = "";
        public string mstg_dir { get; set; } = "";
        public string mstg_file { get; set; } = "";

        public string strConn // 접속문자열 생성
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (Ip != "")
                    sb.Append(string.Format("SERVER={0};", Ip));
                if (Port != "")
                    sb.Append(string.Format("PORT={0};", Port));
                if (Dbname != "")
                    sb.Append(string.Format("Database={0};", Dbname));
                if (Id != "")
                    sb.Append(string.Format("Uid={0};", Id));
                if (Pw != "")
                    sb.Append(string.Format("Password={0};", Pw));
                if (Charset != "")
                    sb.Append(string.Format("Charset={0};", Charset));
                if (Timeout != "")
                    sb.Append(string.Format("Connection Timeout={0};", Timeout));

                return sb.ToString();
            }
        }

        public XROSHOT_CONF(string value = "XROSHOT")
        {
            section = value;

            // 초기값 설정
            switch (section)
            {
            case "XROSHOT": // XROHOST 서버
                used = false;
                comment = "DB(XROHOST)";
                Ip = "localhost";
                Dbname = "warning";
                Id = "WBEarly";
                Pw = "#woobosys@early!";
                Timeout = "5";
                break;
            }

            ReadConfig();
        }

        public bool ReLoad()
        {
            return ReadConfig(true);
        }

        public bool ReadConfig()
        {
            return ReadConfig(false);
        }
        public bool ReadConfig(bool isReset)
        {
            // 현재 파일의 기록시간 과 마지막 읽은 파일의 기록시간 비교
            if (config.LastWriteTime == config.LastReadTime)
            {
                // 파일의 변경이 없으면 false
                if (isReset == false)
                    return false;
            }

            // 동시접근제어 잠금(PC상의 모든 프로세스에서 하나만 접근 가능)
            if (config.LockMutex())
            {
                config.LastReadTime = config.LastWriteTime;

                // config.ReadXxxx(섹션, 키, 기본값)
                used = config.ReadBool(section, "USED", used);
                comment = config.ReadString(section, "COMMENT", comment);
                Ip = config.ReadString(section, "IP", Ip);
                Port = config.ReadString(section, "PORT", Port);
                Dbname = config.ReadString(section, "DBNAME", Dbname);
                Id = config.ReadString(section, "ID", Id);
                Pw = config.ReadPassword(section, "PW", Pw);
                Charset = config.ReadString(section, "CHARSET", Charset);
                Timeout = config.ReadString(section, "TIMEOUT", Timeout);

                User = config.ReadString(section, "USER", User);
                Callback = config.ReadString(section, "CALLBACK", Callback);
                mstg_dir = config.ReadString(section, "MSTG_DIR", mstg_dir);
                mstg_file = config.ReadString(section, "MSTG_FILE", mstg_file);

                // 동시접근제어 해제(PC상의 모든 프로세스에서 하나만 접근 가능)
                config.ReleaseMutex();
            }

            // 파일의 변경이 있으면(설정파일이 갱신 되었으면) true
            return true;
        }

        /// <summary>
        /// 특정 SECTION 조회
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ReadConfig(string value)
        {
            section = value;

            return ReadConfig();
        }

        public void SaveConfig()
        {
            // 동시접근제어 잠금(PC상의 모든 프로세스에서 하나만 접근 가능)
            if (config.LockMutex())
            {
                //섹션
                config.WriteBool(section, "USED", used);
                config.WriteString(section, "COMMENT", comment);
                config.WriteString(section, "IP", Ip);
                config.WriteString(section, "PORT", Port);
                config.WriteString(section, "DBNAME", Dbname);
                config.WriteString(section, "ID", Id);
                config.WritePassword(section, "PW", Pw);
                config.WriteString(section, "CHARSET", Charset);
                config.WriteString(section, "TIMEOUT", Timeout);

                config.WriteString(section, "USER", User);
                config.WriteString(section, "CALLBACK", Callback);
                config.WriteString(section, "MSTG_DIR", mstg_dir);
                config.WriteString(section, "MSTG_FILE", mstg_file);

                // 마지막 파일의 기록시간 변경
                config.LastReadTime = config.LastWriteTime;

                // 동시접근제어 해제(PC상의 모든 프로세스에서 하나만 접근 가능)
                config.ReleaseMutex();
            }

            return;
        }
    }

    public class RESTAPI_CONF
    {
        // new CONFIG(string "설정 파일명", string "폴더명:기본값etc")
        CONFIG config = new CONFIG("CAST.ini");

        // 설정파일 항목([섹션])
        public string section;

        // 설정파일 항목(키=기본값)
        public virtual bool used { get; set; } = false;
        public virtual string comment { get; set; } = "RESTAPI";
        public virtual string Ip { get; set; } = "localhost";
        public virtual string Port { get; set; } = "80";
        public virtual string Id { get; set; } = "";
        public virtual string Pw { get; set; } = "";
        public virtual string Token { get; set; } = "";
        public virtual string rToken { get; set; } = "";

        public virtual int Timeout { get; set; } = 3;
        public string TokenDate { get; set; } = "";
        public string rTokenDate { get; set; } = "";

        public RESTAPI_CONF(string value = "RESTAPI")
        {
            section = value;

            // 초기값 설정
            switch (section)
            {
            case "SAREST":
                used = false;
                comment = "RESTAPI";
                Ip = "localhost";
                Port = "8080";
                Id = "woobo";
                Pw = "woobo123";
                Token = "";
                TokenDate = DateTime.Now.AddMinutes(-1).ToString("yyyy-MM-dd HH:mm:ss");
                Timeout = 3;
                break;

            case "SGAPI":
                used = false;
                comment = "RESTAPI";
                Ip = "localhost";
                Port = "36302";
                Id = "wjogi";
                Token = "UCS000";
                Timeout = 60;
                break;

            case "AMN":
                used = false;
                comment = "RESTAPI";
                Ip = "127.0.0.1";
                Port = "9948";
                Id = "woobo";
                Pw = "woobo123!";
                Timeout = 3 * 1000;
                break;

            case "GSND":
                used = false;
                comment = "RESTAPI";
                Ip = "https://api.gsndbangjae.co.kr";
                Port = "80";
                Id = "4873010001";
                Pw = "2765";
                Token = "";
                rToken = "";
                TokenDate = DateTime.Now.AddMinutes(-62).ToString("yyyy-MM-dd HH:mm:ss");
                rTokenDate = DateTime.Now.AddHours(-24).ToString("yyyy-MM-dd HH:mm:ss");
                Timeout = 3 * 1000;
                break;
            }

            ReadConfig();
        }

        public bool ReLoad()
        {
            return ReadConfig(true);
        }

        public virtual bool ReadConfig()
        {
            return ReadConfig(false);
        }

        /// <summary>
        /// 특정 SECTION 조회
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool ReadConfig(string value)
        {
            section = value;

            return ReadConfig();
        }

        public virtual bool ReadConfig(bool isReset)
        {
            // 현재 파일의 기록시간 과 마지막 읽은 파일의 기록시간 비교
            if (config.LastWriteTime == config.LastReadTime)
            {
                // 파일의 변경이 없으면 false
                if (isReset == false)
                    return false;
            }

            // 동시접근제어 잠금(PC상의 모든 프로세스에서 하나만 접근 가능)
            if (config.LockMutex())
            {
                config.LastReadTime = config.LastWriteTime;

                // config.ReadXxxx(섹션, 키, 기본값)
                used = config.ReadBool(section, "USED", used);
                comment = config.ReadString(section, "COMMENT", comment);
                Ip = config.ReadString(section, "IP", Ip);
                Port = config.ReadString(section, "PORT", Port);
                Id = config.ReadString(section, "ID", Id);
                Pw = config.ReadPassword(section, "PW", Pw);
                Token = config.ReadString(section, "TOKEN", Token);
                rToken = config.ReadString(section, "RETOKEN", rToken);
                TokenDate = config.ReadString(section, "TOKEN DATE", TokenDate);
                rTokenDate = config.ReadString(section, "RETOKEN DATE", rTokenDate);
                Timeout = config.ReadInteger(section, "TIMEOUT", Timeout);

                // 동시접근제어 해제(PC상의 모든 프로세스에서 하나만 접근 가능)
                config.ReleaseMutex();
            }

            // 파일의 변경이 있으면(설정파일이 갱신 되었으면) true
            return true;
        }

        public virtual void SaveConfig()
        {
            // 동시접근제어 잠금(PC상의 모든 프로세스에서 하나만 접근 가능)
            if (config.LockMutex())
            {
                //섹션
                config.WriteBool(section, "USED", used);
                config.WriteString(section, "COMMENT", comment);
                config.WriteString(section, "IP", Ip);
                config.WriteString(section, "PORT", Port);
                config.WriteString(section, "ID", Id);
                config.WritePassword(section, "PW", Pw);
                config.WriteString(section, "TOKEN", Token);
                config.WriteString(section, "RETOKEN", rToken);
                config.WriteString(section, "TOKEN DATE", TokenDate);
                config.WriteString(section, "RETOKEN DATE", rTokenDate);
                config.WriteInteger(section, "TIMEOUT", Timeout);

                // 마지막 파일의 기록시간 변경
                config.LastReadTime = config.LastWriteTime;

                // 동시접근제어 해제(PC상의 모든 프로세스에서 하나만 접근 가능)
                config.ReleaseMutex();
            }

            return;
        }
    }
}
