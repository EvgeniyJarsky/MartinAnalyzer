using Report_BL.ReportModel;
using System.Collections.ObjectModel;

using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Report_BL.Controller.GetDeals.TesterMT4
{
    // Создаем дерево сеток, которое биндится с UI
    public static class GetDealsMT4Tester
    {
        /// <summary>
        /// выбираем сделки из отчета и заносим их в писок DealsCollection
        /// </summary>
        /// <param name="report">Объект класса Report</param>
        public static void Get(NewReport report)
        {
            int digits = 0; //количество знаков после запятой.
            List<float> buy = new List<float>(); // Нумерация сеток buy.
            List<float> sell = new List<float>();// Нумерация сеток sell.

            string[] keywordsMT4 = { ">buy<", ">sell<", "t/p", "s/l", "close at stop", "close" };

            int countGrid = 0;

            Report_BL.ReportModel.TreeViewClass sellGrid = new Report_BL.ReportModel.TreeViewClass();
            Report_BL.ReportModel.TreeViewClass buyGrid = new Report_BL.ReportModel.TreeViewClass();

            List<int> exeptNumberOrder = new List<int>();

            foreach (string line in File.ReadLines(report.FilePath))//перебираем все строки в файле отчета
            {
                
                foreach (string key in keywordsMT4)
                {
                    if (line.Contains(key))
                    {
                        var parseResult =
                            Report_BL.Controller.GetDeals.TesterMT4.ParseMT4Tester.ParseDealsMT4Tester(line, report.Symbol);
                        // Number|Symbol|Date|Buy_Sell|Direct|Lot|Price|Profit|Balance
                       
                        //! Один раз встретилось тип сделки "close" - не знаю что это
                        // Если sell/buy = close - надо найти этот ордер и определить sell это или buy
                        if(parseResult.sell_buy == "close")
                        {
                            foreach(var item in Report_BL.DataCollection.DealsCollection.dealsCollection)
                            {
                                if(item.Number == Convert.ToInt32(parseResult.orderNumber))
                                {
                                    parseResult.sell_buy = item.Buy_Sell;
                                    break;
                                }
                            }
                        }
                        
                        #region Определяем максимальное количество знаков после запятой
                            int currentDigit = Report_BL.Controller.GetDeals.CountDigits.Count(parseResult.price);
                            digits = Math.Max(digits, currentDigit);
                        #endregion
                        
                        // Запоминаем номера ордеров.
                        if (parseResult.sell_buy == "buy")
                            buy.Add(parseResult.orderNumber);
                        if (parseResult.sell_buy == "sell")
                            sell.Add(parseResult.orderNumber);

                        // Запомним номера ордеров которые закрыты по причине окончания теста
                        // что бы потом выкинуть их из дерева
                        if(parseResult.sell_buy == "close at stop")
                            exeptNumberOrder.Add(parseResult.orderNumber);
                        
                        #region Удалить сделки закрытые по причине окончания теста
                        // Если есть сделки закрыытые close at stop - их нужно удалить
                        // т.к. они закрыты не по стратегии и портят статистику
                        if(parseResult.sell_buy == "close at stop")
                        {
                            foreach( var deal in Report_BL.DataCollection.DealsCollection.dealsCollection)
                            {
                                // Находим ордер с таким же номером как и текущий ордер который
                                // закрыт по close at stop
                                if(deal.Number == parseResult.orderNumber)
                                {
                                    
                                    
                                    Report_BL.DataCollection.DealsCollection.dealsCollection.Remove(deal);
                                    break;
                                    // ordersToDeleteList.Add(deal);
                                }
                            }
                        }
                        #endregion

                        if (parseResult.sell_buy == "s/l" || parseResult.sell_buy == "t/p" || parseResult.sell_buy == "close at stop")
                        {
                            if (buy.Contains(parseResult.orderNumber) && parseResult.sell_buy != "close at stop")
                                parseResult.sell_buy = "buy";
                            else if(parseResult.sell_buy != "close at stop")
                                parseResult.sell_buy= "sell";
                        }
                        
                        if(parseResult.sell_buy != "close at stop")
                            Report_BL.DataCollection.DealsCollection.AddNewItem(parseResult);
                    }
                        report.Digits = digits;
                }
            }
        }
    
    
        public static void CreateNewGridTree(
            Report_BL.Controller.GetDeals.TesterMT4.ParseMT4Tester.Deal order,
            ref Report_BL.ReportModel.TreeViewClass Grid,
            ref int countGrid)
        {
            countGrid  += 1;

            Grid = new Report_BL.ReportModel.TreeViewClass();


            Grid.NumberGrid = countGrid;
            Grid.CountOrders++;
            Grid.Symbol = order.symbol;
            Grid.Sell_Buy = order.sell_buy;
            Grid.StartDate = order.dateAndTimeOfDeal;
            Grid.Lot += order.lot;
            //Grid.Profit += order.profit;

            var newOrder = new Report_BL.ReportModel.Order();

            newOrder.OpenDate = order.dateAndTimeOfDeal;
            newOrder.OpenPrice = order.price;
            newOrder.Lot = order.lot;
            newOrder.orderNumber = order.orderNumber;

            Grid.Orders.Add(newOrder);

            
        }

        public static void AddNewOrder(
            Report_BL.Controller.GetDeals.TesterMT4.ParseMT4Tester.Deal order,
            ref Report_BL.ReportModel.TreeViewClass Grid,
            ref int countGrid)
        {
            Grid.CountOrders++;
            Grid.StartDate = order.dateAndTimeOfDeal;
            Grid.Lot += order.lot;
            //Grid.Profit += order.profit;

            var newOrder = new Report_BL.ReportModel.Order();

            newOrder.OpenDate = order.dateAndTimeOfDeal;
            newOrder.OpenPrice = order.price;
            newOrder.Lot = order.lot;
            newOrder.orderNumber = order.orderNumber;

            Grid.Orders.Add(newOrder);
        }

        /// <summary>
        /// Добавим ордер закрытия в соответсвующую сетку
        /// </summary>
        /// <param name="order">Ордер закрытия</param>
        /// <param name="Grid">Сетка</param>
        /// <param name="countGrid"></param>
        public static void AddCloseOrder(
            Report_BL.Controller.GetDeals.TesterMT4.ParseMT4Tester.Deal order,
            ref Report_BL.ReportModel.TreeViewClass Grid,
            ref int countGrid)
            {
                // Найдем по номеру незакрытый ордер
                //! TODO Не учитываем частичное закрытие ордеров!!!!!!!!
                foreach(var _order in Grid.Orders)
                {
                    if(_order.orderNumber == order.orderNumber)
                    {
                        _order.CloseDate = order.dateAndTimeOfDeal;
                        _order.ClosePrice = order.price;
                        _order.Profit = order.profit;

                        // Grid.Profit += _order.Profit;
                        break;
                    }
                }
                // Если сетка закрыта то добавим ее в коллекцию и 
                // обнулим создадим пустой объект сетки
                if(IsGridClosed(ref Grid))
                {
                    Report_BL.DataCollection.TreeCollection.grid.Add(Grid);
                    Grid = new Report_BL.ReportModel.TreeViewClass();
                }
            }

            /// <summary>
            /// Проверка что сетка закрыта
            /// </summary>
            /// <param name="Grid">Сетка</param>
            /// <returns></returns>
            public static bool IsGridClosed(ref Report_BL.ReportModel.TreeViewClass Grid)
            {
                foreach(var order in Grid.Orders)
                {
                    if(order.ClosePrice == 0)
                        return false;
                }
                return true;
            }
    
    }
}

