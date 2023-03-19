using Report_BL.ReportModel;
using System.Collections.ObjectModel;

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
                        
                        // определяем максимальное количество знаков после запятой
                        int currentDigit = Report_BL.Controller.GetDeals.CountDigits.Count(parseResult.price);
                        digits = Math.Max(digits, currentDigit);
                        
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
                        var col = Report_BL.DataCollection.DealsCollection.dealsCollection;
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
                                }
                            }
                        }
                        #endregion

                        if (parseResult.sell_buy == "s/l" || parseResult.sell_buy == "t/p" || parseResult.sell_buy == "close at stop")
                        {
                            if (buy.Contains(parseResult.orderNumber))
                                parseResult.sell_buy = "buy";
                            else
                                parseResult.sell_buy= "sell";
                        }
                        
                        Report_BL.DataCollection.DealsCollection.AddNewItem(parseResult);

                        /*
                        **********************************************************************
                        **********************************************************************
                        */
                        if(parseResult.sell_buy == "buy")
                        {
                            if(buyGrid.CountOrders == 0)// значит открытых сеток sell нет
                            {
                                CreateNewGridTree(
                                    parseResult,
                                    ref buyGrid,
                                    ref countGrid);
                            }
                            else // если сетка sell уже строиться
                            {
                                // Если это открытие позиции
                                if(parseResult.direct == "open")
                                {
                                    AddNewOrder(
                                    parseResult,
                                    ref buyGrid,
                                    ref countGrid);
                                }
                                // Если это закрытие позиции
                                else
                                {
                                    AddCloseOrder(
                                    parseResult,
                                    ref buyGrid,
                                    ref countGrid);
                                }
                            }
                        }
                        if(parseResult.sell_buy == "sell")
                        {
                            if(sellGrid.CountOrders == 0)// значит открытых сеток sell нет
                            {
                                CreateNewGridTree(
                                    parseResult,
                                    ref sellGrid,
                                    ref countGrid);
                            }
                            else // если сетка sell уже строиться
                            {
                                // Если это открытие позиции
                                if(parseResult.direct == "open")
                                {
                                    AddNewOrder(
                                    parseResult,
                                    ref sellGrid,
                                    ref countGrid);
                                }
                                // Если это закрытие позиции
                                else
                                {
                                    AddCloseOrder(
                                    parseResult,
                                    ref sellGrid,
                                    ref countGrid);
                                }
                            }
                        }
                    }
                        
                        /*
                        **********************************************************************
                        **********************************************************************
                        */
                        report.Digits = digits;
                        //break;
                    }
            }

            #region Удалим сетки ордера которых закрыты из-за окончания теста
            var grid_ = new ObservableCollection<Report_BL.ReportModel.TreeViewClass>();

            foreach(var grid in Report_BL.DataCollection.TreeCollection.grid)
            {
                foreach(var order in grid.Orders)
                {
                    if(exeptNumberOrder.Contains(order.orderNumber))
                    {
                        grid_.Add(grid);
                        break;
                    }
                }
            }
            foreach(var grid in grid_)
                Report_BL.DataCollection.TreeCollection.grid.Remove(grid);
            #endregion
            


            #region Сортировка пузырьком дерева сеток
            for(int i=0; i<Report_BL.DataCollection.TreeCollection.grid.Count(); i++)
            {
                for(int j = i+1; j<Report_BL.DataCollection.TreeCollection.grid.Count(); j++)
                {
                    if(Report_BL.DataCollection.TreeCollection.grid[i].NumberGrid > Report_BL.DataCollection.TreeCollection.grid[j].NumberGrid)
                    {
                        var temp = Report_BL.DataCollection.TreeCollection.grid[i];
                        Report_BL.DataCollection.TreeCollection.grid[i] = Report_BL.DataCollection.TreeCollection.grid[j];
                        Report_BL.DataCollection.TreeCollection.grid[j] = temp;
                    }
                }

                #region Округляем цены, находим кол-во пунктов до ТП, считаем время жизни сетки
                double averageClosePrice = 0;
                double priceSell = double.MinValue;
                double priceBuy = double.MaxValue;
                double startPrice = -1;
                foreach(var order in Report_BL.DataCollection.TreeCollection.grid[i].Orders)
                {
                    // Округляем цены
                    order.OpenPrice = Math.Round(order.OpenPrice, report.Digits, MidpointRounding.AwayFromZero);
                    order.ClosePrice = Math.Round(order.ClosePrice, report.Digits, MidpointRounding.AwayFromZero);
                    
                    // считаем время жизни сетки
                    if(Report_BL.DataCollection.TreeCollection.grid[i].StartDate == DateTime.MinValue)
                        Report_BL.DataCollection.TreeCollection.grid[i].StartDate = order.OpenDate;
                    Report_BL.DataCollection.TreeCollection.grid[i].EndDate = order.CloseDate;

                    // Сложим все цены закрытия ордеров что бы потом найти среднее значение
                    averageClosePrice += order.ClosePrice;

                    // в зависимости от типа сетки sell/buy определим крайнюю цену что бы потом найти кол-во 
                    // пунктов до ТП
                    if(Report_BL.DataCollection.TreeCollection.grid[i].Sell_Buy == "sell")
                    {
                        if(order.OpenPrice > priceSell)
                            priceSell = order.OpenPrice;
                    }
                    else
                    {
                        if(order.OpenPrice < priceBuy)
                            priceBuy = order.OpenPrice;
                    }
                    // Определим цену открытия первого ордера что бы посчитать длину сетки
                    if(startPrice < 0)
                        startPrice = order.OpenPrice;
                }
                averageClosePrice = Math.Round(averageClosePrice/Report_BL.DataCollection.TreeCollection.grid[i].Orders.Count(), report.Digits, MidpointRounding.AwayFromZero);
                int digit_ = 1000;
                if(report.Digits == 5)
                    digit_ = 10000;
                if(Report_BL.DataCollection.TreeCollection.grid[i].Sell_Buy == "sell")
                {
                    Report_BL.DataCollection.TreeCollection.grid[i].PointsToTP = 
                    Convert.ToInt32(Math.Round((priceSell-averageClosePrice), 5, MidpointRounding.AwayFromZero)*digit_);

                    Report_BL.DataCollection.TreeCollection.grid[i].GridLenght = 
                    Convert.ToInt32((priceSell - startPrice)*digit_); 
                }
                else
                {
                    Report_BL.DataCollection.TreeCollection.grid[i].PointsToTP = 
                    Convert.ToInt32(Math.Round((averageClosePrice - priceBuy), 5, MidpointRounding.AwayFromZero)*digit_);

                    Report_BL.DataCollection.TreeCollection.grid[i].GridLenght = 
                    Convert.ToInt32((startPrice - priceBuy)*digit_);
                }
                
                #endregion

            }
            #endregion


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

