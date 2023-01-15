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
        public int YearVal { get; set; }
        /// <summary>
        /// Прибыль за январь
        /// </summary>
        double januaryProfit = 0;
        public double JanuaryProfit { get { return Math.Round(januaryProfit, 2, MidpointRounding.AwayFromZero); } set { januaryProfit = value; } }
        /// <summary>
        /// Прибыль за Февраль
        /// </summary>
        double february = 0;
        public double FebruaryProfit { get { return Math.Round(february, 2, MidpointRounding.AwayFromZero); } set { february = value; } }
        /// <summary>
        /// Прибыль за Март
        /// </summary>
        double march = 0;
        public double MarchProfit { get { return Math.Round(march, 2, MidpointRounding.AwayFromZero); } set { march = value; } }
        /// <summary>
        /// Прибыль за Апрель
        /// </summary>
        double april = 0;
        public double AprilProfit { get { return Math.Round(april, 2, MidpointRounding.AwayFromZero); } set { april = value; } }
        /// <summary>
        /// Прибыль за Май
        /// </summary>
        double may = 0;
        public double MayProfit { get { return Math.Round(may, 2, MidpointRounding.AwayFromZero); } set { may = value; } }
        /// <summary>
        /// Прибыль за Июнь
        /// </summary>
        double june = 0;
        public double JuneProfit { get { return Math.Round(june, 2, MidpointRounding.AwayFromZero); } set { june = value; } }
        /// <summary>
        /// Прибыль за Июль
        /// </summary>
        double july = 0;
        public double JulyProfit { get { return Math.Round(july, 2, MidpointRounding.AwayFromZero); } set { july = value; } }
        /// <summary>
        /// Прибыль за Август
        /// </summary>
        double august = 0;
        public double AugustProfit { get { return Math.Round(august, 2, MidpointRounding.AwayFromZero); } set { august = value; } }
        /// <summary>
        /// Прибыль за Сентябрь
        /// </summary>
        double september = 0;
        public double SeptemberProfit { get { return Math.Round(september, 2, MidpointRounding.AwayFromZero); } set { september = value; } }
        /// <summary>
        /// Прибыль за Октябрь
        /// </summary>
        double october = 0;
        public double OctoberProfit { get { return Math.Round(october, 2, MidpointRounding.AwayFromZero); } set { october = value; } }
        /// <summary>
        /// Прибыль за Ноябрь
        /// </summary>
        double november = 0;
        public double NovemberProfit { get { return Math.Round(november, 2, MidpointRounding.AwayFromZero); } set { november = value; } }
        /// <summary>
        /// Декабрь
        /// </summary>
        double december = 0;
        public double DecemberProfit { get { return Math.Round(december, 2, MidpointRounding.AwayFromZero); } set { december = value; } }
        //! TODO посчитать прямо в классе
        /// <summary>
        /// Средняя прибыль за месяц
        /// </summary>
        public double AverageProfit { get; set; }
        /// <summary>
        /// Сумарная прибыль за год
        /// </summary>
        public double SumProfit { get; set; }
    }
}
