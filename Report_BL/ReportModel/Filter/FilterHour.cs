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
        public bool Hour_0 {get; set;} = true;
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
        

        public bool[] hourFilterMassive()
        {
            return new bool[24]
            {
                Hour_0,
                Hour_1,
                Hour_2,
                Hour_3,
                Hour_4,
                Hour_5,
                Hour_6,
                Hour_7,
                Hour_8,
                Hour_9,
                Hour_10,
                Hour_11,
                Hour_12,
                Hour_13,
                Hour_14,
                Hour_15,
                Hour_16,
                Hour_17,
                Hour_18,
                Hour_19,
                Hour_20,
                Hour_21,
                Hour_22,
                Hour_23
            };
        }  

    }
}