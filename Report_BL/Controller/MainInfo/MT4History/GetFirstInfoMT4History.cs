using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.MainInfo.MT4History
{
    public static class GetFirstInfoMT4History
    {
        /// <summary>
        /// Получаем первичную информацию с отчета МТ4History
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Report_BL.ReportModel.FirstInfo GetSymbolDateMagic(string filePath)
        {
            string? line;
            string symbol;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            int magic;

            Report_BL.ReportModel.FirstInfo firstInfo =
                new Report_BL.ReportModel.FirstInfo(
                    filePath,
                    "MT4History",
                    new Dictionary<string, List<int>>(),
                    DateTime.MinValue,
                    DateTime.MinValue,
                    0);
            using (StreamReader sr = new StreamReader(filePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    // Если достигли этой строки то дальше только
                    // открытые позиции - выходим из чтения файла
                    if (line.Contains("Open Trades:"))
                        break;
                    // Эти строчки пропускаем.
                    if (line.Contains("Summary trade result"))
                        continue;
                    // Строки с отмененными ордерами пропускаем.
                    if (line.Contains("cancelled"))
                        continue;
                    if(line.Contains("sell") || line.Contains("buy"))
                    {
                        string[] lineArray = line.Split(new string[] { "</td>" }, StringSplitOptions.None);
                        
                        // TODO Надо проверять парсинг строк!!!!!!!!!!!!!!! 
                        // Запоминаем первую дату сделки
                        if (endDate == DateTime.MinValue)
                            endDate = DateTime.Parse(lineArray[8].Split('>')[1]);

                        // Запоминаем дату последнего закрытого ордера
                        startDate = DateTime.Parse(lineArray[1].Split('>')[1]);

                        // Определяем символ
                        symbol = lineArray[4].Split('>')[1].ToUpper();
                       
                        // Определяем меджик
                        magic = Int32.Parse(line.Split(new string[] { "title=\"#" }, StringSplitOptions.None)[1].Split(' ')[0]);
                        
                        // Добовляем символ и меджик при необходимости
                        firstInfo.DicSymbolMagic =
                            Report_BL.Controller.MainInfo.AddNewSymbolMagicToDic.Add(
                                firstInfo.DicSymbolMagic,
                                symbol,
                                magic);
                    }
                }
                firstInfo.StartDate = startDate;
                firstInfo.EndDate = endDate;
            }
            return firstInfo;
        }

    }
}
