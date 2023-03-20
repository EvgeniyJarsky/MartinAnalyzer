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
                                Report_BL.DataCollection.TreeCollection.grid.Add(buyGrid);
                                buyGrid = new Report_BL.ReportModel.TreeViewClass();
                                break;
                            }
                        }
                    }
                }
            }
        }

        private static bool IsGridClosed(TreeViewClass gridTree)
        {
            foreach(var order in gridTree.Orders)
                if(order.CloseDate == DateTime.MinValue) return false;
            return true;
        }
    }
}
