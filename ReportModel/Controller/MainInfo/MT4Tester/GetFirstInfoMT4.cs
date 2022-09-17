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
            string symbol = String.Empty;
            DateTime startDate;
            DateTime endDate;

            Report_BL.ReportModel.FirstInfo firstInfo =
                new Report_BL.ReportModel.FirstInfo(
                    new Dictionary<string, List<int>>(),
                    DateTime.MinValue,
                    DateTime.MinValue,
                    0);
                
            using (StreamReader? sr = new StreamReader(filePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    #region Определяем имя символа
                    if (symbol == String.Empty)
                    {
                        symbol = 
                            Report_BL.Controller.Parser.MT4Tester.MA4TesterSymbolParse.SymbolParse(line);
                        
                        // Если имя символа прочитано - запоминаем его
                        if(symbol != String.Empty)
                        {
                            firstInfo.DicSymbolMagic.Add(symbol,new List<int>{ 0 });
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    #endregion

                    #region Определим даты начала и конца теста
                    if (line.Contains("<tr align=left><td colspan=2>"))
                        {
                            // "<tr align=left><td colspan=2>������</td><td colspan=4>15 ����� (M15)  2012.01.03 01:00 - 2020.02.20 01:45</td></tr>"
                            try
                            {
                                // TODO вынести парсинг в отдельную функцию
                                startDate = DateTime.Parse(
                                line.Split('(')[1].Split(')')[1].Trim().Split('<')[0].Split('-')[0]
                                );
                            // TODO вынести парсинг в отдельную функцию
                                endDate = DateTime.Parse(
                                    line.Split('(')[1].Split(')')[1].Trim().Split('<')[0].Split('-')[1]
                                    );
                                firstInfo.StartDate = startDate;
                                firstInfo.EndDate = endDate;
                            }
                            catch(FormatException e)
                            {
                                //Console.WriteLine(e.Message);
                                continue;
                            }
                            break;
                        }
                    #endregion

                    #region Определяем начальный депозит
                    if (firstInfo.StartDeposit == 0)
                    {
                        if (line.Contains("</td><td></td><td align=right></td><td>"))
                        {
                            line = sr.ReadLine();
                            // TODO вынести парсинг в отдельную функцию
                            firstInfo.StartDeposit = int.Parse(line.Split('>')[4].Split('<')[0]);
                        }
                    }
                    #endregion
                }
            }
            return firstInfo;
        }
    }
}
