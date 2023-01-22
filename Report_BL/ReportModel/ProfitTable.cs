using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.ReportModel
{
    /// <summary>
    /// Класс таблицы прибыли по месяцам
    /// </summary>
    public class ProfitTable
    {
        /// <summary>
        /// Номер года
        /// </summary>
        public string YearVal { get; set; }
        /// <summary>
        /// Прибыль за январь
        /// </summary>
        double januaryProfit = 0;
        public double JanuaryProfit { get { return Math.Round(januaryProfit, 2, MidpointRounding.AwayFromZero); } set { januaryProfit = value; } }
        /// <summary>
        /// Прибыль за Февраль
        /// </summary>
        double februaryProfit = 0;
        public double FebruaryProfit { get { return Math.Round(februaryProfit, 2, MidpointRounding.AwayFromZero); } set { februaryProfit = value; } }
        /// <summary>
        /// Прибыль за Март
        /// </summary>
        double marchProfit = 0;
        public double MarchProfit { get { return Math.Round(marchProfit, 2, MidpointRounding.AwayFromZero); } set { marchProfit = value; } }
        /// <summary>
        /// Прибыль за Апрель
        /// </summary>
        double aprilProfit = 0;
        public double AprilProfit { get { return Math.Round(aprilProfit, 2, MidpointRounding.AwayFromZero); } set { aprilProfit = value; } }
        /// <summary>
        /// Прибыль за Май
        /// </summary>
        double mayProfit = 0;
        public double MayProfit { get { return Math.Round(mayProfit, 2, MidpointRounding.AwayFromZero); } set { mayProfit = value; } }
        /// <summary>
        /// Прибыль за Июнь
        /// </summary>
        double juneProfit = 0;
        public double JuneProfit { get { return Math.Round(juneProfit, 2, MidpointRounding.AwayFromZero); } set { juneProfit = value; } }
        /// <summary>
        /// Прибыль за Июль
        /// </summary>
        double julyProfit = 0;
        public double JulyProfit { get { return Math.Round(julyProfit, 2, MidpointRounding.AwayFromZero); } set { julyProfit = value; } }
        /// <summary>
        /// Прибыль за Август
        /// </summary>
        double augustProfit = 0;
        public double AugustProfit { get { return Math.Round(augustProfit, 2, MidpointRounding.AwayFromZero); } set { augustProfit = value; } }
        /// <summary>
        /// Прибыль за Сентябрь
        /// </summary>
        double septemberProfit = 0;
        public double SeptemberProfit { get { return Math.Round(septemberProfit, 2, MidpointRounding.AwayFromZero); } set { septemberProfit = value; } }
        /// <summary>
        /// Прибыль за Октябрь
        /// </summary>
        double octoberProfit = 0;
        public double OctoberProfit { get { return Math.Round(octoberProfit, 2, MidpointRounding.AwayFromZero); } set { octoberProfit = value; } }
        /// <summary>
        /// Прибыль за Ноябрь
        /// </summary>
        double novemberProfit = 0;
        public double NovemberProfit { get { return Math.Round(novemberProfit, 2, MidpointRounding.AwayFromZero); } set { novemberProfit = value; } }
        /// <summary>
        /// Декабрь
        /// </summary>
        double decemberProfit = 0;
        public double DecemberProfit { get { return Math.Round(decemberProfit, 2, MidpointRounding.AwayFromZero); } set { decemberProfit = value; } }
        
        /// <summary>
        /// Сумарная прибыль за год
        /// </summary>
        double sumProfit = 0;
        public double SumProfit
        {
            get {return sumProfit;}
            set {sumProfit = value;}
        }

        /// <summary>
        /// Средняя прибыль за месяц
        /// </summary>
        double averageProfit = 0;
        public double AverageProfit
        {
            get {return averageProfit;}
            set {averageProfit = value;}
        }

        public double GetSum()
        {
            double rez = 
                    januaryProfit+
                    februaryProfit+
                    marchProfit+
                    aprilProfit+
                    mayProfit+
                    juneProfit+
                    julyProfit+
                    augustProfit+
                    septemberProfit+
                    octoberProfit+
                    novemberProfit+
                    decemberProfit;

                return Math.Round(rez, 2, MidpointRounding.AwayFromZero);
        }

        public double GetAverageValue()
        {
            int countMonths = 0;
                double rez = 0;
                #region Подсчет месяцев в которых были торги - не нулевая прибыль
                if(this.januaryProfit != 0)
                    countMonths++;
                if(this.februaryProfit != 0)
                    countMonths++;
                if(this.marchProfit != 0)
                    countMonths++;
                if(this.aprilProfit != 0)
                    countMonths++;
                if(this.mayProfit != 0)
                    countMonths++;
                if(this.juneProfit != 0)
                    countMonths++;
                if(this.julyProfit != 0)
                    countMonths++;
                if(this.augustProfit != 0)
                    countMonths++;
                if(this.septemberProfit != 0)
                    countMonths++;
                if(this.octoberProfit != 0)
                    countMonths++;
                if(this.novemberProfit != 0)
                    countMonths++;
                if(this.decemberProfit != 0)
                    countMonths++;
                #endregion
                if(countMonths != 0)
                    rez = Math.Round(this.GetSum()/countMonths, 2, MidpointRounding.AwayFromZero);
                return rez;
        }
    }
}
