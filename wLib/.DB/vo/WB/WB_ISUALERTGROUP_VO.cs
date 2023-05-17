using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace wLib.DB
{
    public class WB_ISUALERTGROUP_VO
    {
        public string Key
        {
            get
            {
                return GCode;
            }
        }

        public string GCode { get; set; } // AUTO_PK
        public string GName{ get; set; }
        public string AltDate { get; set; }
        public string AltUse { get; set; }

        public string AdmSMS{ get; set; }
        public List<string> AdmSMSList
        {
            get
            {
                List<string> rtv = new List<string>();
                string[] AdmSMSs = AdmSMS.Split(new char[] { ',' });
                foreach (string s in AdmSMSs) 
                {
                    if (s.Length > 0) 
                    {
                        rtv.Add(s);
                    }
                }

                return rtv;
            }
        }
        public string AltCode{ get; set; }
        public string[] AltCodeList
        {
            get
            {
                string[] rtv = AltCode.Split(',');
                return rtv;
            }
        }
        
        public string[] FloodSMSAuto { get; set; } = new string[5];
        public string[] Auto { get; set; } = new string[5];
        public string[] Equip { get; set; } = new string[5];
        public List<string>[] EquipList
        {
            get
            {
                List<string>[] rtv = new List<string>[5];
                for (int i = 1; i <= 4; i++)
                {
                    rtv[i] = new List<string>();
                    if (Equip[i] != null)
                    {
                        string[] equips = Equip[i].Split(',');
                        foreach (string e in equips)
                        {
                            if (e.Length > 0)
                            {
                                rtv[i].Add(e);
                            }
                        }
                    }
                }

                return rtv;
            }
        }
        public string[] SMS{ get; set; } = new string[5];
        public List<string>[] SMSList
        {
            get
            {
                List<string>[] rtv = new List<string>[5];
                for (int i = 1; i <= 4; i++)
                {
                    rtv[i] = new List<string>();
                    if (SMS[i] != null)
                    {
                        string[] SMSs = SMS[i].Split(',');
                        foreach (string e in SMSs)
                        {
                            if (e.Length > 0)
                            {
                                rtv[i].Add(e);
                            }
                        }
                    }
                }

                return rtv;
            }
        }

        // TEMP COLUMN
        public string IsuCode { get; set; }
        public int NowLevel { get; set; }
        public string AlertDate { get; set; }
        public string retreat { get; set; } = null;
        public void SetData(WB_ISUALERTGROUP_VO vo)
        {
            GCode = vo.GCode;
            GName = vo.GName;
            AltCode = vo.AltCode;
            AltDate = vo.AltDate;
            AltUse = vo.AltUse;
            AdmSMS = vo.AdmSMS;

            FloodSMSAuto = vo.FloodSMSAuto;
            Auto = vo.Auto;
            Equip = vo.Equip;
            SMS = vo.SMS;

            IsuCode = vo.IsuCode;
            NowLevel = vo.NowLevel;
            AlertDate = vo.AlertDate;
        }
    }
}
