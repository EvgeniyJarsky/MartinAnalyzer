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
            param.Add(new Info() { Value = report.ExpertName, Parametr          = "Expert name" });
            param.Add(new Info() { Value = report.Symbol, Parametr              = "Symbol" });
            param.Add(new Info() { Value = report.TimeFrame, Parametr           = "Timeframe" });
            param.Add(new Info() { Value = report.TradePeriod, Parametr         = "Test period" });
            param.Add(new Info() { Value = report.Deposit.ToString(), Parametr  = "Deposit" });
            param.Add(new Info() { Value = report.Profit.ToString(), Parametr   = "Profit" });
            param.Add(new Info() { Value = report.DrawDown.ToString(), Parametr = "DrawDown" });
        }
        
        /// <summary>
        /// Очистить список
        /// </summary>
        public static void ClearItem()
        {
            param.Clear();
        }
    }
}
