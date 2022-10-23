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
    public static class ReportCollection
    {
        /// <summary>
        /// Коллекция объектов NewReport - замена Report!!!!
        /// </summary>
        public static ObservableCollection<NewReport> newReport =
            new ObservableCollection<NewReport>();




        /// <summary>
        /// Коллекция объектов Report(отчетов)
        /// </summary>
        public static ObservableCollection<Report> report = new ObservableCollection<Report>();
       
        /// <summary>
        /// Добавить довый объект Report в коллекцию report
        /// </summary>
        /// <param name="param">Параметры</param>
        /// <param name="filePath">Путь к файлу</param>
        public static void AddNewItem(string[] param, string filePath)
        {
            report.Add(new Report()
            {
                ExpertName = param[0],
                FilePath = filePath,
                Curency = param[1],
                TimeFrame = param[2],
                TestPeriod = param[3],
                Deposit = param[4],
                Profit = param[5],
                DrawDown = param[6],
                ReportType = param[7],
                Magic = param[8]
            });
        }

        /// <summary>
        /// Очистить коллекцию
        /// </summary>
        public static void ClearReportCollection()
        {
            report.Clear();
        }
    }
}
