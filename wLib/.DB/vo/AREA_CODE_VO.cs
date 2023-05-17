﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib.DB
{
    public class AREA_CODE_T
    {
        public class AREA_CODE
        {
            public string Code { get; set; }
            public string DoName { get; set; }
            public string Name { get; set; }

            public string FullName
            {
                get
                {
                    return $"{DoName} {Name}({Code})";
                }
            }

            public AREA_CODE(string code, string doname, string name)
            {
                Code = code;
                DoName = doname;
                Name = name;
            }
        };

        public AREA_CODE[] Items = new AREA_CODE[]
        {
            new AREA_CODE("0", "", "서울특별시"),
            new AREA_CODE("1", "", "광주광역시"),
            new AREA_CODE("2", "", "대구광역시"),
            new AREA_CODE("3", "", "대전광역시"),
            new AREA_CODE("4", "", "부산광역시"),
            new AREA_CODE("5", "", "세종특별자치시"),
            new AREA_CODE("6", "", "울산광역시"),
            new AREA_CODE("7", "", "인천광역시"),
            new AREA_CODE("8", "제주도", "서귀포시"),
            new AREA_CODE("9", "제주도", "제주시"),
            new AREA_CODE("10", "강원도", "강릉시"),
            new AREA_CODE("11", "강원도", "고성군"),
            new AREA_CODE("12", "강원도", "동해시"),
            new AREA_CODE("13", "강원도", "삼척시"),
            new AREA_CODE("14", "강원도", "속초시"),
            new AREA_CODE("15", "강원도", "양구군"),
            new AREA_CODE("16", "강원도", "양양군"),
            new AREA_CODE("17", "강원도", "영월군"),
            new AREA_CODE("18", "강원도", "원주시"),
            new AREA_CODE("19", "강원도", "인제군"),
            new AREA_CODE("20", "강원도", "정선군"),
            new AREA_CODE("21", "강원도", "철원군"),
            new AREA_CODE("22", "강원도", "춘천시"),
            new AREA_CODE("23", "강원도", "태백시"),
            new AREA_CODE("24", "강원도", "평창군"),
            new AREA_CODE("25", "강원도", "홍천군"),
            new AREA_CODE("26", "강원도", "화천군"),
            new AREA_CODE("27", "강원도", "횡성군"),
            new AREA_CODE("28", "경기도", "가평군"),
            new AREA_CODE("29", "경기도", "고양시"),
            new AREA_CODE("30", "경기도", "과천시"),
            new AREA_CODE("31", "경기도", "광명시"),
            new AREA_CODE("32", "경기도", "광주시"),
            new AREA_CODE("33", "경기도", "구리시"),
            new AREA_CODE("34", "경기도", "군포시"),
            new AREA_CODE("35", "경기도", "김포시"),
            new AREA_CODE("36", "경기도", "남양주시"),
            new AREA_CODE("37", "경기도", "동두천시"),
            new AREA_CODE("38", "경기도", "부천시"),
            new AREA_CODE("39", "경기도", "성남시"),
            new AREA_CODE("40", "경기도", "수원시"),
            new AREA_CODE("41", "경기도", "시흥시"),
            new AREA_CODE("42", "경기도", "안산시"),
            new AREA_CODE("43", "경기도", "안성시"),
            new AREA_CODE("44", "경기도", "안양시"),
            new AREA_CODE("45", "경기도", "양주시"),
            new AREA_CODE("46", "경기도", "양평군"),
            new AREA_CODE("47", "경기도", "여주시"),
            new AREA_CODE("48", "경기도", "연천군"),
            new AREA_CODE("49", "경기도", "오산시"),
            new AREA_CODE("50", "경기도", "용인시"),
            new AREA_CODE("51", "경기도", "의왕시"),
            new AREA_CODE("52", "경기도", "의정부시"),
            new AREA_CODE("53", "경기도", "이천시"),
            new AREA_CODE("54", "경기도", "파주시"),
            new AREA_CODE("55", "경기도", "평택시"),
            new AREA_CODE("56", "경기도", "포천시"),
            new AREA_CODE("57", "경기도", "하남시"),
            new AREA_CODE("58", "경기도", "화성시"),
            new AREA_CODE("59", "경상남도", "거제시"),
            new AREA_CODE("60", "경상남도", "거창군"),
            new AREA_CODE("61", "경상남도", "고성군"),
            new AREA_CODE("62", "경상남도", "김해시"),
            new AREA_CODE("63", "경상남도", "남해군"),
            new AREA_CODE("64", "경상남도", "밀양시"),
            new AREA_CODE("65", "경상남도", "사천시"),
            new AREA_CODE("66", "경상남도", "산청군"),
            new AREA_CODE("67", "경상남도", "양산시"),
            new AREA_CODE("68", "경상남도", "의령군"),
            new AREA_CODE("69", "경상남도", "진주시"),
            new AREA_CODE("70", "경상남도", "창녕군"),
            new AREA_CODE("71", "경상남도", "창원시"),
            new AREA_CODE("72", "경상남도", "통영시"),
            new AREA_CODE("73", "경상남도", "하동군"),
            new AREA_CODE("74", "경상남도", "함안군"),
            new AREA_CODE("75", "경상남도", "함양군"),
            new AREA_CODE("76", "경상남도", "합천군"),
            new AREA_CODE("77", "경상북도", "경산시"),
            new AREA_CODE("78", "경상북도", "경주시"),
            new AREA_CODE("79", "경상북도", "고령군"),
            new AREA_CODE("80", "경상북도", "구미시"),
            new AREA_CODE("81", "경상북도", "군위군"),
            new AREA_CODE("82", "경상북도", "김천시"),
            new AREA_CODE("83", "경상북도", "문경시"),
            new AREA_CODE("84", "경상북도", "봉화군"),
            new AREA_CODE("85", "경상북도", "상주시"),
            new AREA_CODE("86", "경상북도", "성주군"),
            new AREA_CODE("87", "경상북도", "안동시"),
            new AREA_CODE("88", "경상북도", "영덕군"),
            new AREA_CODE("89", "경상북도", "영양군"),
            new AREA_CODE("90", "경상북도", "영주시"),
            new AREA_CODE("91", "경상북도", "영천시"),
            new AREA_CODE("92", "경상북도", "예천시"),
            new AREA_CODE("93", "경상북도", "울릉군"),
            new AREA_CODE("94", "경상북도", "울진군"),
            new AREA_CODE("95", "경상북도", "의성군"),
            new AREA_CODE("96", "경상북도", "청도군"),
            new AREA_CODE("97", "경상북도", "청송군"),
            new AREA_CODE("98", "경상북도", "칠곡군"),
            new AREA_CODE("99", "경상북도", "포항시"),
            new AREA_CODE("100", "전라남도", "강진군"),
            new AREA_CODE("101", "전라남도", "고흥군"),
            new AREA_CODE("102", "전라남도", "곡성군"),
            new AREA_CODE("103", "전라남도", "광양시"),
            new AREA_CODE("104", "전라남도", "구례군"),
            new AREA_CODE("105", "전라남도", "나주시"),
            new AREA_CODE("106", "전라남도", "담양군"),
            new AREA_CODE("107", "전라남도", "목포시"),
            new AREA_CODE("108", "전라남도", "무안군"),
            new AREA_CODE("109", "전라남도", "보성군"),
            new AREA_CODE("110", "전라남도", "순천시"),
            new AREA_CODE("111", "전라남도", "신안군"),
            new AREA_CODE("112", "전라남도", "여수시"),
            new AREA_CODE("113", "전라남도", "영광군"),
            new AREA_CODE("114", "전라남도", "영암군"),
            new AREA_CODE("115", "전라남도", "완도군"),
            new AREA_CODE("116", "전라남도", "장성군"),
            new AREA_CODE("117", "전라남도", "장흥군"),
            new AREA_CODE("118", "전라남도", "진도군"),
            new AREA_CODE("119", "전라남도", "함평군"),
            new AREA_CODE("120", "전라남도", "해남군"),
            new AREA_CODE("121", "전라남도", "화순군"),
            new AREA_CODE("122", "전라북도", "고창군"),
            new AREA_CODE("123", "전라북도", "군산시"),
            new AREA_CODE("124", "전라북도", "김제시"),
            new AREA_CODE("125", "전라북도", "남원시"),
            new AREA_CODE("126", "전라북도", "무주군"),
            new AREA_CODE("127", "전라북도", "부안군"),
            new AREA_CODE("128", "전라북도", "순창군"),
            new AREA_CODE("129", "전라북도", "완주군"),
            new AREA_CODE("130", "전라북도", "익산시"),
            new AREA_CODE("131", "전라북도", "임실군"),
            new AREA_CODE("132", "전라북도", "장수군"),
            new AREA_CODE("133", "전라북도", "전주시"),
            new AREA_CODE("134", "전라북도", "정읍시"),
            new AREA_CODE("135", "전라북도", "진안군"),
            new AREA_CODE("136", "충청남도", "계룡시"),
            new AREA_CODE("137", "충청남도", "공주시"),
            new AREA_CODE("138", "충청남도", "금산군"),
            new AREA_CODE("139", "충청남도", "논산시"),
            new AREA_CODE("140", "충청남도", "당진시"),
            new AREA_CODE("141", "충청남도", "보령시"),
            new AREA_CODE("142", "충청남도", "부여군"),
            new AREA_CODE("143", "충청남도", "서산시"),
            new AREA_CODE("144", "충청남도", "서천군"),
            new AREA_CODE("145", "충청남도", "아산시"),
            new AREA_CODE("146", "충청남도", "예산군"),
            new AREA_CODE("147", "충청남도", "천안시"),
            new AREA_CODE("148", "충청남도", "청양군"),
            new AREA_CODE("149", "충청남도", "태안군"),
            new AREA_CODE("150", "충청남도", "홍성군"),
            new AREA_CODE("151", "충청북도", "괴산군"),
            new AREA_CODE("152", "충청북도", "단양군"),
            new AREA_CODE("153", "충청북도", "보은군"),
            new AREA_CODE("154", "충청북도", "영동군"),
            new AREA_CODE("155", "충청북도", "옥천군"),
            new AREA_CODE("156", "충청북도", "음성군"),
            new AREA_CODE("157", "충청북도", "제천시"),
            new AREA_CODE("158", "충청북도", "증평군"),
            new AREA_CODE("159", "충청북도", "진천시"),
            new AREA_CODE("160", "충청북도", "청주시"),
            new AREA_CODE("161", "충청북도", "충주시"),
            //new AREA_CODE("162", "", ""),
            //new AREA_CODE("163", "", ""),
            //new AREA_CODE("164", "", ""),
            //new AREA_CODE("165", "", ""),
            //new AREA_CODE("166", "", ""),
            //new AREA_CODE("167", "", ""),
            //new AREA_CODE("168", "", ""),
            //new AREA_CODE("169", "", ""),
            //new AREA_CODE("170", "", ""),
            //new AREA_CODE("171", "", ""),
            //new AREA_CODE("172", "", ""),
            //new AREA_CODE("173", "", ""),
            //new AREA_CODE("174", "", ""),
            //new AREA_CODE("175", "", ""),
            //new AREA_CODE("176", "", ""),
            //new AREA_CODE("177", "", ""),
            //new AREA_CODE("178", "", ""),
            //new AREA_CODE("179", "", ""),
            //new AREA_CODE("180", "", ""),
            //new AREA_CODE("181", "", ""),
            //new AREA_CODE("182", "", ""),
            //new AREA_CODE("183", "", ""),
            //new AREA_CODE("184", "", ""),
            //new AREA_CODE("185", "", ""),
            //new AREA_CODE("186", "", ""),
            //new AREA_CODE("187", "", ""),
            //new AREA_CODE("188", "", ""),
            //new AREA_CODE("189", "", ""),
            //new AREA_CODE("190", "", ""),
            //new AREA_CODE("191", "", ""),
            //new AREA_CODE("192", "", ""),
            //new AREA_CODE("193", "", ""),
            //new AREA_CODE("194", "", ""),
            //new AREA_CODE("195", "", ""),
            //new AREA_CODE("196", "", ""),
            //new AREA_CODE("197", "", ""),
            //new AREA_CODE("198", "", ""),
            //new AREA_CODE("199", "", ""),
            //new AREA_CODE("200", "", ""),
            //new AREA_CODE("201", "", ""),
            //new AREA_CODE("202", "", ""),
            //new AREA_CODE("203", "", ""),
            //new AREA_CODE("204", "", ""),
            //new AREA_CODE("205", "", ""),
            //new AREA_CODE("206", "", ""),
            //new AREA_CODE("207", "", ""),
            //new AREA_CODE("208", "", ""),
            //new AREA_CODE("209", "", ""),
            //new AREA_CODE("210", "", ""),
            //new AREA_CODE("211", "", ""),
            //new AREA_CODE("212", "", ""),
            //new AREA_CODE("213", "", ""),
            //new AREA_CODE("214", "", ""),
            //new AREA_CODE("215", "", ""),
            //new AREA_CODE("216", "", ""),
            //new AREA_CODE("217", "", ""),
            //new AREA_CODE("218", "", ""),
            //new AREA_CODE("219", "", ""),
            //new AREA_CODE("220", "", ""),
            //new AREA_CODE("221", "", ""),
            //new AREA_CODE("222", "", ""),
            //new AREA_CODE("223", "", ""),
            //new AREA_CODE("224", "", ""),
            //new AREA_CODE("225", "", ""),
            //new AREA_CODE("226", "", ""),
            //new AREA_CODE("227", "", ""),
            //new AREA_CODE("228", "", ""),
            //new AREA_CODE("229", "", ""),
            //new AREA_CODE("230", "", ""),
            //new AREA_CODE("231", "", ""),
            //new AREA_CODE("232", "", ""),
            //new AREA_CODE("233", "", ""),
            //new AREA_CODE("234", "", ""),
            //new AREA_CODE("235", "", ""),
            //new AREA_CODE("236", "", ""),
            //new AREA_CODE("237", "", ""),
            //new AREA_CODE("238", "", ""),
            //new AREA_CODE("239", "", ""),
            //new AREA_CODE("240", "", ""),
            //new AREA_CODE("241", "", ""),
            //new AREA_CODE("242", "", ""),
            //new AREA_CODE("243", "", ""),
            //new AREA_CODE("244", "", ""),
            //new AREA_CODE("245", "", ""),
            //new AREA_CODE("246", "", ""),
            //new AREA_CODE("247", "", ""),
            //new AREA_CODE("248", "", ""),
            //new AREA_CODE("249", "", ""),
            //new AREA_CODE("250", "", ""),
            //new AREA_CODE("251", "", ""),
            //new AREA_CODE("252", "", ""),
            //new AREA_CODE("253", "", ""),
            //new AREA_CODE("254", "", ""),
            new AREA_CODE("255", "우보재난", "시스템")
        };
        
        public int Length
        {
            get { return Items.Length; }
        }

        public string this[int index]
        {
            get
            {
                return Items[index].Code;
            }
        }

        public string this[string index]
        {
            get
            { 
                return Items.First(x=>x.Code == index).Name;
            }

        }
    }
}
