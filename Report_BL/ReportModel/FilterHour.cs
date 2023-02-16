using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.ReportModel
{
    public class FilterHour
    {
        public bool Hour_1 {get; set;} = true;
        public bool Hour_2 {get; set;} = true;
        public bool Hour_3 {get; set;} = true;
        public bool Hour_4 {get; set;} = true;
        public bool Hour_5 {get; set;} = true;
        public bool Hour_6 {get; set;} = true;
        public bool Hour_7 {get; set;} = true;
        public bool Hour_8 {get; set;} = true;
        public bool Hour_9 {get; set;} = true;
        public bool Hour_10 {get; set;} = true;
        public bool Hour_11 {get; set;} = true;
        public bool Hour_12 {get; set;} = true;
        public bool Hour_13 {get; set;} = true;
        public bool Hour_14 {get; set;} = true;
        public bool Hour_15 {get; set;} = true;
        public bool Hour_16 {get; set;} = true;
        public bool Hour_17 {get; set;} = true;
        public bool Hour_18 {get; set;} = true;
        public bool Hour_19 {get; set;} = true;
        public bool Hour_20 {get; set;} = true;
        public bool Hour_21 {get; set;} = true;
        public bool Hour_22 {get; set;} = true;
        public bool Hour_23 {get; set;} = true;
        public bool Hour_24 {get; set;} = true;

        public sbyte[] hourFilterMassive()
        {
            var rez = new sbyte[24];
            Array.Fill<sbyte>(rez, -100);

            if(Hour_1) rez[0] = 1;
            if(Hour_2) rez[1] = 2;
            if(Hour_3) rez[2] = 3;
            if(Hour_4) rez[3] = 4;
            if(Hour_5) rez[4] = 5;
            if(Hour_6) rez[5] = 6;
            if(Hour_7) rez[6] = 7;
            if(Hour_8) rez[7] = 8;
            if(Hour_9) rez[8] = 9;
            if(Hour_10) rez[9] = 10;
            if(Hour_11) rez[10] = 11;
            if(Hour_12) rez[11] = 12;
            if(Hour_13) rez[12] = 13;
            if(Hour_14) rez[13] = 14;
            if(Hour_15) rez[14] = 15;
            if(Hour_16) rez[15] = 16;
            if(Hour_17) rez[16] = 17;
            if(Hour_18) rez[17] = 18;
            if(Hour_19) rez[18] = 19;
            if(Hour_20) rez[19] = 20;
            if(Hour_21) rez[20] = 21;
            if(Hour_22) rez[21] = 22;
            if(Hour_23) rez[22] = 23;
            if(Hour_24) rez[23] = 24;

            return rez;
        }  

    }
}