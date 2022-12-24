using Report_BL.ReportModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Report_BL.Controller.GetDeals.TesterMT4
{
    public static class GetDealsMT4Tester
    {
        /// <summary>
        /// выбираем сделки из отчета и заносим их в писок DealsCollection
        /// </summary>
        /// <param name="report">Объект класса Report</param>
        public static void Get(NewReport report)
        {
            int digits = 0; //количество знаков после запятой
            List<int> buy = new List<int>();
            List<int> sell = new List<int>();
            string[] keywordsMT4 = { ">buy<", ">sell<", "t/p", "s/l", "close at stop", "close" };

            foreach (string line in File.ReadLines(report.FilePath))//перебираем все строки в файле отчета
            {
                foreach (string key in keywordsMT4)
                {
                    if (line.Contains(key))
                    {
                        string[] parseResult =
                            Report_BL.Controller.GetDeals.TesterMT4.ParseMT4Tester.ParseDealsMT4Tester(line, report.Symbol);
                        // Number|Symbol|Date|Buy_Sell|Direct|Lot|Price|Profit|Balance

                        //! Один раз встретилось тип сделки "close" - не знаю что это
                        // Если sell/buy = close - надо найти этот ордер и определить sell это или buy
                        if(parseResult[3] == "close")
                        {
                            foreach(var item in Report_BL.DataCollection.DealsCollection.dealsCollection)
                            {
                                if(item.Number == Convert.ToInt32(parseResult[0]))
                                {
                                    parseResult[3] = item.Buy_Sell;
                                    break;
                                }
                            }
                        }
                        
                        // определяем максимальное количество знаков после запятой
                        int currentDigit = Report_BL.Controller.GetDeals.CountDigits.Count(parseResult[6]);
                        if (digits < currentDigit)
                            digits = currentDigit;
                        
                        // заменим "t/p" на тип сделки
                        if (parseResult[3] == "buy")
                            buy.Add(Int16.Parse(parseResult[0]));
                        if (parseResult[3] == "sell")
                            sell.Add(Int16.Parse(parseResult[0]));
                        
                        #region Удалить сделки закрытые по причине окончания теста
                        // Если есть сделки закрыытые close at stop - их нужно удалить
                        // т.к. они закрыты не по стратегии и портят статистику
                        var col = Report_BL.DataCollection.DealsCollection.dealsCollection;
                        if(parseResult[3] == "close at stop")
                        {
                            foreach( var deal in Report_BL.DataCollection.DealsCollection.dealsCollection)
                            {
                                // Находим ордер с таким же номером как и текущий ордер который
                                // закрыт по close at stop
                                if(deal.Number == int.Parse(parseResult[0]))
                                {
                                    Report_BL.DataCollection.DealsCollection.dealsCollection.Remove(deal);
                                    break;
                                }
                            }
                        }
                        #endregion

                        if (parseResult[3] == "t/p" || parseResult[3] == "close at stop")
                        {
                            if (buy.Contains(Int16.Parse(parseResult[0])))
                                parseResult[3] = "buy";
                            else
                                parseResult[3] = "sell";
                        }
                        
                        Report_BL.DataCollection.DealsCollection.AddNewItem(parseResult);

                        report.Digits = digits;
                        break;
                    }
                }
            }
        }
    }
}
