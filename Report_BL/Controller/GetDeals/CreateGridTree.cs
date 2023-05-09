using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Report_BL.ReportModel;
using System.Collections.ObjectModel;

namespace Report_BL.Controller.GetDeals
{
    public static class GridTree
    {
        public static void CreateGridTree(NewReport report)
        {
            var sellGrid = new Report_BL.ReportModel.TreeViewClass();
            var buyGrid = new Report_BL.ReportModel.TreeViewClass();
            int counGrid = 0;

            Report_BL.DataCollection.TreeCollection.grid.Clear();


            foreach(var order in Report_BL.DataCollection.DealsCollection.dealsCollection)
            {
                if(order.Buy_Sell == "sell")
                {
                    if(sellGrid.CountOrders == 0) // Если новая сетка
                    {
                        counGrid++;
                        sellGrid.NumberGrid = counGrid;
                        sellGrid.Sell_Buy   = order.Buy_Sell;
                        sellGrid.Symbol     = order.Symbol;
                        sellGrid.StartDate = order.Date;
                    }
                    if(order.Direct == "open")
                    {
                        sellGrid.CountOrders++;

                        var newOrder = new Order();

                        newOrder.orderNumber = order.Number;
                        newOrder.OpenDate = order.Date;
                        newOrder.OpenPrice = order.Price;
                        newOrder.Lot = order.Lot;

                        sellGrid.Lot += newOrder.Lot;

                        sellGrid.Orders.Add(newOrder);
                    }
                    else // Иначе это закрытие позиции
                    {
                        // Найдем ордер с нужным номером который закрывается
                        foreach(var _order in sellGrid.Orders)
                        {
                            if(_order.orderNumber == order.Number)
                            {
                                _order.CloseDate = order.Date;
                                _order.ClosePrice = order.Price;
                                _order.Profit = order.Profit;
                            }
                            // Если сетка закрыта
                            if(IsGridClosed(sellGrid))
                            {
                                sellGrid.EndDate = _order.CloseDate;
                                sellGrid.GridLenght = GetGridLenght(report,sellGrid);
                                Report_BL.DataCollection.TreeCollection.grid.Add(sellGrid);
                                sellGrid = new Report_BL.ReportModel.TreeViewClass();

                                break;

                            }
                        }
                    }
                    
                }
                else // Это ордер buy
                {
                    if(buyGrid.CountOrders == 0) // Если новая сетка
                    {
                        counGrid++;
                        buyGrid.NumberGrid = counGrid;
                        buyGrid.Sell_Buy   = order.Buy_Sell;
                        buyGrid.Symbol     = order.Symbol;
                        buyGrid.StartDate  = order.Date;
                    }

                    if(order.Direct == "open")
                    {
                        buyGrid.CountOrders++;
                        var newOrder = new Order();

                        newOrder.orderNumber = order.Number;
                        newOrder.OpenDate = order.Date;
                        newOrder.OpenPrice = order.Price;
                        newOrder.Lot = order.Lot;

                        buyGrid.Lot += newOrder.Lot;

                        buyGrid.Orders.Add(newOrder);
                    }
                    else // Иначе это закрытие позиции
                    {
                        // Найдем ордер с нужным номером который закрывается
                        foreach(var _order in buyGrid.Orders)
                        {
                            if(_order.orderNumber == order.Number)
                            {
                                _order.CloseDate = order.Date;
                                _order.ClosePrice = order.Price;
                                _order.Profit = order.Profit;
                            }
                            // Если сетка закрыта
                            if(IsGridClosed(buyGrid))
                            {
                                buyGrid.EndDate = _order.CloseDate;

                                buyGrid.GridLenght = GetGridLenght(report,buyGrid);
                                Report_BL.DataCollection.TreeCollection.grid.Add(buyGrid);
                                buyGrid = new Report_BL.ReportModel.TreeViewClass();
                                break;
                            }
                        }
                    }
                }
            }


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

                // #region Округляем цены, находим кол-во пунктов до ТП, считаем время жизни сетки
                double averageClosePrice = 0;
                double priceSell = double.MinValue;
                double priceBuy = double.MaxValue;
                double startPrice = -1;
                foreach(var order in Report_BL.DataCollection.TreeCollection.grid[i].Orders)
                {
                    // Округляем цены
                    order.OpenPrice = (float)Math.Round(order.OpenPrice, report.Digits, MidpointRounding.AwayFromZero);
                    order.ClosePrice = (float)Math.Round(order.ClosePrice, report.Digits, MidpointRounding.AwayFromZero);
                    
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

                //TODO DELETE
                if(Report_BL.DataCollection.TreeCollection.grid[i].Orders.Count() > 1)
                {
                    int d = 0;
                }


                averageClosePrice = Math.Round(averageClosePrice/Report_BL.DataCollection.TreeCollection.grid[i].Orders.Count(), report.Digits, MidpointRounding.AwayFromZero);
                int digit_ = 1000;
                if(report.Digits == 5)
                    digit_ = 100000;
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

                    var tp = Report_BL.DataCollection.TreeCollection.grid[i].PointsToTP;
                    var lenght_ = Report_BL.DataCollection.TreeCollection.grid[i].GridLenght;
                }
            }
            #endregion




        }

        private static bool IsGridClosed(TreeViewClass gridTree)
        {
            foreach(var order in gridTree.Orders)
                if(order.CloseDate == DateTime.MinValue) return false;
            return true;
        }

        private static int GetGridLenght(NewReport report, TreeViewClass tree)
        {
            var price = new List<float>();

            if(tree.CountOrders < 2)
                return 0;
            
            
            foreach(var order in tree.Orders)
            {
                price.Add(order.OpenPrice);
            }

            short digits = 10000;
            if(report.Digits == 3)
                digits = 1000;

            return Convert.ToInt32((price.Max() - price.Min())*digits);
        }
    }
}
