using Report_BL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Пути к файлам
            // Путь к файлу отчета МТ4 тестер
            //string filePath = "F:\\!Coding\\C#\\MartinAnalyzer\\ReportExamples\\EURJPY_ENG.htm";

            //Путь к файлу МТ4 History
            string filePath = "F:\\!Coding\\C#\\MartinAnalyzer\\ReportExamples\\MT4_History.htm";
            #endregion

            #region тип отчета и билд МТ4
            // определяем тип отчета
            string reportType = Report_BL.Controller.MainInfo.DetectReportType.GetReportType(filePath);
            Console.WriteLine($"Тип отчета -  {reportType}");

            // определяем билд отчета МТ4
            if (reportType == "MT4Tester")
            {
                int buildVer = Report_BL.Controller.MainInfo.DetectReportType.GetBuildMT4(filePath);
                Console.WriteLine($"Билд отчета {buildVer}");
            }
            else Console.WriteLine("Билд определяется только для МТ4 Tester");
            #endregion

            // Парсим весь отчет
            Report_BL.Controller.MainInfo.MainInfo.GetMainInfo(filePath);

            #region Парсинг периода тестирования МТ4 тестера
            Report_BL.ReportModel.FirstInfo ssd =
            Report_BL.Controller.MainInfo.MT4Tester.GetFirstInfoMT4.GetSymbolDateMagic(filePath);
            
            Console.WriteLine($"Кол-во символов {ssd.DicSymbolMagic.Count()}");

            foreach (string symbol in ssd.DicSymbolMagic.Keys)
                Console.WriteLine(symbol);
            foreach (List<int> magic in ssd.DicSymbolMagic.Values)
                Console.WriteLine($"Меджик - {magic[0]}");
            
            Console.WriteLine($"Time start {ssd.StartDate}");
            Console.WriteLine($"Time end {ssd.EndDate}");
            #endregion

            #region Получим первичную информацию
            var dicFirstInfo = new Dictionary<string, Func<string, Report_BL.ReportModel.FirstInfo>>();
            
            // MT4Tester
            dicFirstInfo.Add(
                "MT4Tester",
                new Func<string, Report_BL.ReportModel.FirstInfo>
                (Report_BL.Controller.MainInfo.MT4Tester.GetFirstInfoMT4.GetSymbolDateMagic));
            // MT4History
            dicFirstInfo.Add(
                "MT4History",
                new Func<string, Report_BL.ReportModel.FirstInfo>
                (Report_BL.Controller.MainInfo.MT4History.GetFirstInfoMT4History.GetSymbolDateMagic));

            // Получим первичную информацию по отчету
            try
            {
                var firstInfo = dicFirstInfo[reportType](filePath);
            }
            catch(Exception e)
            {
                // Здесь надо завершить выполнение программы - ошибка чтения информации
                Console.WriteLine("Отчет не определен файл не добавляем");
                Console.WriteLine(e);
            }
            #endregion

            #region Тест добавления нового символа и меджика
            var dic = new Dictionary<string, List<int>>();
            dic.Add("EURUSD", new List<int> { 123, 321 });
            dic.Add("GBPUSD", new List<int> { 123, 456 });

            string newSymbol = "EURUSD";
            int newMagic = 789;

            var newDic = Report_BL.Controller.MainInfo.AddNewSymbolMagicToDic.Add(dic, newSymbol, newMagic);
            #endregion

            Console.ReadLine();
        }
    }
}
