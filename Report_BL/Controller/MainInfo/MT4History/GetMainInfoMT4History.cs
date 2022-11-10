using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportModel.Controller.MainInfo
{
    public class GetMainInfoMT4History
    {
        public static string[] Get(string filePath)
        {
            #region Поля
            /// <summary>
            /// Имя робота
            /// </summary>
            string expertName = String.Empty;

            /// <summary>
            /// Символ
            /// </summary>
            string curencyName = String.Empty;
            /// <summary>
            /// Таймфрейм
            /// </summary>
            string timeFrame = String.Empty;

            /// <summary>
            /// Период тестирования
            /// </summary>
            string testPeriod = String.Empty;

            /// <summary>
            /// Начальный депозит
            /// </summary>
            string startDeposit = String.Empty;

            /// <summary>
            /// Суммарная прибыль
            /// </summary>
            string profit = String.Empty;

            /// <summary>
            /// Максимальная просадка
            /// </summary>
            string drawDown = String.Empty;

            /// <summary>
            /// Тип отчета - определен сразу
            /// </summary>
            string reportType = "MT4Tester";

            /// <summary>
            /// Меджик равен 0 т.к. в отчете торги одной парой
            /// </summary>
            string magic = "0";
            #endregion

            // Вспомогательные переменные
            string startTime = String.Empty;
            string endTime = String.Empty;
            string? line = String.Empty;
            string magics = String.Empty;
            string symbols = String.Empty;
            // Словарь для запоминания какой символ с каким меджиком
            // есть в отчете
            var dictionaryMagicSymbol = new Dictionary<string, string>();

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
                    string[] lineArray = line.Split(new string[] {"</td>"}, StringSplitOptions.None);
                    string openDate = lineArray[1].Split('>')[1];
                    string orderType = lineArray[2].Split('>')[1];
                    string lot = lineArray[3].Split('>')[1];
                    string symbol = lineArray[4].Split('>')[1].ToUpper();
                    string openPrice = lineArray[5].Split('>')[1];
                    string closeDate = lineArray[8].Split('>')[1];
                    string closePrice = lineArray[9].Split('>')[1];
                    string commision = lineArray[10].Split('>')[1];
                    string taxes = lineArray[11].Split('>')[1];
                    string swap = lineArray[12].Split('>')[1];
                    profit = lineArray[13].Split('>')[1];
                    string newMagic = line.Split(new string[] {"title=\"#"}, StringSplitOptions.None)[1].Split(' ')[0];
                    // Запоминаем начало даты начала отчета и конца
                    // TODO Пара может торговаться не с начала отчета!!!
                    if (startTime == String.Empty)
                        startTime = openDate;
                    endTime = closeDate;
                    // Если такого меджика или символа еще не было - запомним его
                    if (!magic.Contains(newMagic) || !symbol.Contains(symbol))
                    {
                        dictionaryMagicSymbol.Add(symbol, newMagic);
                    }
                }
                // Здесь вызвать окно с выбором символа и меджика
                
                
                return new string[] {"bbbn"};
            }
        }
    }
}
