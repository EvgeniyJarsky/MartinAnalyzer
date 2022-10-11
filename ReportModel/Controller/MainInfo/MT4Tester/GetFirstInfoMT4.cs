using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.MainInfo.MT4Tester
{
    public static class GetFirstInfoMT4
    {
        /// <summary>
        /// Читаем файл и парсим имя символа
        /// меджик - он по умолчанию отсутствует поэтому присваиваем стразу "0"
        /// Дата первой сделки
        /// Дата последней сделки
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <returns>String[symbol, '0', startDate, endDate]</returns>
        public static Report_BL.ReportModel.FirstInfo GetSymbolDateMagic(string filePath)
        {
            string? line;
            string? symbol = String.Empty;
            DateTime startDate;
            DateTime endDate;

            /// <summary>
            /// Ключеая строка которая должна содержаться в
            /// строке отчета - там где есть имя символа
            /// В отчете содержиться несколько таких строк
            /// нам нужна первая
            /// </summary>
            const string keyLine = "<tr align=left><td colspan=2>";

            Report_BL.ReportModel.FirstInfo firstInfo =
                new Report_BL.ReportModel.FirstInfo(
                    filePath,
                    "MT4Tester",
                    new Dictionary<string, List<int>>(),
                    DateTime.MinValue,
                    DateTime.MinValue,
                    0);
                
            using (StreamReader? sr = new StreamReader(filePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    #region Определяем имя символа
                    // Если нашли строке с ключевыми символами
                    // и символ еще не определялся, т.е. равен пустой строке
                    if(line.Contains(keyLine) && symbol == String.Empty)
                    {
                        // Парсим имя символа.
                        symbol = 
                            Report_BL.Controller.Parser.MT4Tester.MA4TesterSymbolParse.SymbolParse(line);
                        // Если парсинг успешный
                        if (symbol != null)
                        {
                            firstInfo.DicSymbolMagic.Add(symbol, new List<int> { 0 });
                            continue;
                        }
                        // Иначе первичная инфрмация Null
                        else return null;
                    }
                    #endregion

                    #region Определим даты начала и конца теста
                    if (line.Contains("<tr align=left><td colspan=2>") && firstInfo.StartDate == DateTime.MinValue)
                        {

// "<tr align=left><td colspan=2>������</td><td colspan=4>15 ����� (M15)  2012.01.03 01:00 - 2020.02.20 01:45</td></tr>"
                            DateTime[]? date = new DateTime[2];
                            date = Report_BL.Controller.Parser.MT4Tester.MT4TesterDataParse.DateParse(line);
                            if (date == null)
                                return null;
                            else
                            {
                                firstInfo.StartDate = date[0];
                                firstInfo.EndDate   = date[1];
                                continue;
                            }
                         }
                    #endregion

                    #region Определяем начальный депозит
                    if (firstInfo.StartDeposit == 0)
                    {
                        if (line.Contains("</td><td></td><td align=right></td><td>"))
                        {
                            line = sr.ReadLine();
                            line = sr.ReadLine();

                            // TODO вынести парсинг в отдельную функцию
                            // line = "<tr align=left><td>Initial deposit</td><td align=right>10000.00</td><td></td><td align=right></td><td>Spread</td><td align=right>Variable</td></tr>"
                            firstInfo.StartDeposit = int.Parse(line.Split('>')[4].Split('<')[0].Split('.')[0]);
                        }
                    }
                    #endregion
                }
            }
            return firstInfo;
        }
    }
}
