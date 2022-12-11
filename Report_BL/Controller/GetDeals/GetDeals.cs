using Report_BL.ReportModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.GetDeals
{
    /// <summary>
    /// Получаем сделки и запоминаем их
    /// </summary>
    public class GetDeals
    {
        public delegate void delegateTest(NewReport report);

        public static void Get(Report_BL.ReportModel.NewReport report)
        {
            // Если тип отчета МТ4 из тестера
            if (report.ReportType == "MT4Tester")
            {
                // Создаем список сделок List 
                Report_BL.Controller.GetDeals.TesterMT4.GetDealsMT4Tester.Get(report);

                // Читаем БД
                //Report_BL.SQL_Work.ReadDB.ReadDBExampl(report);

                // Создаем базу данных
                Report_BL.SQL_Work.CreateDB_MT4Tester.Create_DB(report);


                

                
            }
        }
    }
}
