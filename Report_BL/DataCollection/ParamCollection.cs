using Report_BL.ReportModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.DataCollection
{
    /// <summary>
    /// Коллекции для сбора итоговых данных
    /// </summary>
    public static class ParamentrsCollection
    {
        /// <summary>
        /// Коллекция объектов Info(состоит из 2-х значений - парамет и значение)
        /// </summary>
        public static ObservableCollection<Info> param = new ObservableCollection<Info>();

        /// <summary>
        /// При выборе отчета заполняем param новыми объектами что бы потом отобразить их
        /// </summary>
        /// <param name="report"> Объект класса Report</param>
        public static void AddNewItem(NewReport report)
        {
            if(report.ExpertName != null)
                param.Add(new Info() { Value = report.ExpertName, Parametr = "Название робота" });
            param.Add(new Info() { Value = report.Symbol, Parametr = "Валютная пара" });
            if(report.TimeFrame != null)
                param.Add(new Info() { Value = report.TimeFrame, Parametr = "Таймфрейм" });


            int months = GetMonths(report.StartDate, report.EndDate);
            int years = months/12;
            int months_ = months%12;

            string text_ = $"({years} {YearOrYears(years)} и {months_} {MonthOrMonths(months_)})";
            
            string period = $"{report.StartDate:d} - {report.EndDate:d}";

            string rezPeriod = $"{period} {text_}";
            param.Add(new Info() { Value = rezPeriod, Parametr = "Период тестирования" });



            if(report.Deposit !=0)
                param.Add(new Info() { Value = report.Deposit.ToString() + " $", Parametr  = "Начальный депозит" });
            if(report.Profit != 0)
                param.Add(new Info() { Value = report.Profit.ToString() + " $", Parametr   = "Прибыль" });
            if(report.DrawDown != 0)
                param.Add(new Info() { Value = report.DrawDown.ToString() + " $", Parametr = "Максимальная просадка" });
        
            // Считаем рентабельность
            if(report.Deposit != 0 && report.Profit != 0 && report.DrawDown != 0)
            {
                // months = GetMonths(report.StartDate, report.EndDate);
                if(months != 0)
                {
                    double monthProfitability = Math.Round((report.Profit/months)/report.Deposit*100,2);
                    double yearProfitability = Math.Round(monthProfitability*12,2);
                    param.Add(new Info() { Value = $"{monthProfitability}% (в месяц) или {yearProfitability}% (в год)", Parametr = "Рентабельность" });
                    double coefProfitability = Math.Round(((report.Deposit/100)*yearProfitability)/report.DrawDown, 2); 
                    param.Add(new Info() { Value = $"{coefProfitability}", Parametr = "Коэффициент рентабельности" });
                }
            }
            
        }
        
        // Кол-во месяцев между датами
        private static int GetMonths(DateTime startDate, DateTime endDate)
        {
            return(endDate.Month-startDate.Month) + 12*(endDate.Year-startDate.Year);
        }
        
        /// <summary>
        /// Очистить список
        /// </summary>
        public static void ClearItem()
        {
            param.Clear();
        }

        private static string YearOrYears(int years)
        {
            if(years.ToString().EndsWith('1'))
                return "год";
            if(years.ToString().EndsWith('2') || years.ToString().EndsWith('3') || years.ToString().EndsWith('4'))
                return "года";
            return "лет"; 
        }

        private static string MonthOrMonths(int month)
        {
            if(month.ToString().EndsWith('1'))
                return "месяц";
            if(month.ToString().EndsWith('2') || month.ToString().EndsWith('3') || month.ToString().EndsWith('4'))
                return "месяца";
            return "месяцев"; 
        }
    }
}
