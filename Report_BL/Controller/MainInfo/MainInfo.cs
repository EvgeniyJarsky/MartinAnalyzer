using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.MainInfo
{
    public static class MainInfo
    {
        /// <summary>
        /// Определяем тип файла и в зависимости от типа создаем объект Report и добавляем его 
        /// в коллекцию report
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        public static void GetMainInfo(string filePath)
        {
            // Определяем тип файла отчета
            string reportType = DetectReportType.GetReportType(filePath);

            if (reportType == "MT4Tester")
            {
                string[] param = MainInfoMT4Tester.Get(filePath);
                DataCollection.ReportCollection.AddNewItem(param, filePath);
            }
        }
    }
}
