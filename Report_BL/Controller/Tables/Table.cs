using Report_BL.DataCollection;
using Report_BL.ReportModel;
using Report_BL.SQL_Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.Tables
{
    public static class Table
    {
        public static void CreateProfiTable()
        {
            // Перебираем всю коллекцию сеток
            foreach(var tree in TreeCollection.grid)
            {
                int year = tree.EndDate.Year;
                int month = tree.EndDate.Month;

                //Проверим если уже этот год в коллекции
                bool isYearCreated = IsYearCreated.CheckProfitTable(year);

                if(isYearCreated) // Если год уже есть в коллекции
                {
                    foreach (var item in ProfitTableCollection.profitTable)
                    {
                        if (Int32.Parse(item.YearVal) == year)
                            AddProfit(month, item, tree);
                    }
                }
                else // Если года нет в коллекции
                {
                    var newYear = new ProfitTable { YearVal = year.ToString() };
                    
                    AddProfit(month, newYear, tree);

                    ProfitTableCollection.profitTable.Add(newYear);
                }
            }

            #region  После формирования таблицы Прибыли, нужно добавить последнюю строку со средними значениями
            double[] averageMonthProfit = new double[12];
            averageMonthProfit = AverageMonthProfit.Get();

            var endRowProfit = new ProfitTable
            {
                YearVal = "Среднее",
                JanuaryProfit = averageMonthProfit[0],
                FebruaryProfit = averageMonthProfit[1],
                MarchProfit = averageMonthProfit[2],
                AprilProfit = averageMonthProfit[3],
                MayProfit = averageMonthProfit[4],
                JuneProfit = averageMonthProfit[5],
                JulyProfit = averageMonthProfit[6],
                AugustProfit = averageMonthProfit[7],
                SeptemberProfit = averageMonthProfit[8],
                OctoberProfit = averageMonthProfit[9],
                NovemberProfit = averageMonthProfit[10],
                DecemberProfit = averageMonthProfit[11]
            };
            endRowProfit.AverageProfit = endRowProfit.GetAverageValue();
            endRowProfit.SumProfit = GetSumProfit();
            ProfitTableCollection.profitTable.Add(endRowProfit);
            #endregion
        }

        public static void CreateMaxOrdersGridTable()
        {
            // Перебираем всю коллекцию сеток
            foreach(var tree in TreeCollection.grid)
            {
                int year = tree.EndDate.Year;
                int month = tree.EndDate.Month;

                //Проверим если уже этот год в коллекции
                bool isYearCreated = IsYearCreated.CheckMaxOrdersTable(year);

                if(isYearCreated) // Если год уже есть в коллекции
                {
                    // Ищем строку с нужным годом
                    foreach (var item in GridOrdersCountTableCollection.MaxOrdersTable)
                    {
                        if (Int32.Parse(item.YearVal) == year)
                        {
                            switch (month)
                            {
                                case 1:
                                    if(tree.Orders.Count() > item.JanuaryMaxGridOrdersCount)
                                        item.JanuaryMaxGridOrdersCount = tree.CountOrders;
                                    break;
                                case 2:
                                    if(tree.Orders.Count() > item.FebruaryMaxGridOrdersCount)
                                        item.FebruaryMaxGridOrdersCount = tree.CountOrders;
                                    break;
                                case 3:
                                    if(tree.Orders.Count() > item.MarchMaxGridOrdersCount)
                                        item.MarchMaxGridOrdersCount = tree.CountOrders;
                                    break;
                                case 4:
                                    if(tree.Orders.Count() > item.AprilMaxGridOrdersCount)
                                        item.AprilMaxGridOrdersCount = tree.CountOrders;
                                    break;
                                case 5:
                                    if(tree.Orders.Count() > item.MayMaxGridOrdersCount)
                                        item.MayMaxGridOrdersCount = tree.CountOrders;
                                    break;
                                case 6:
                                    if(tree.Orders.Count() > item.JuneMaxGridOrdersCount)
                                        item.JuneMaxGridOrdersCount = tree.CountOrders;
                                    break;
                                case 7:
                                    if(tree.Orders.Count() > item.JulyMaxGridOrdersCount)
                                        item.JulyMaxGridOrdersCount = tree.CountOrders;
                                    break;
                                case 8:
                                    if(tree.Orders.Count() > item.AugustMaxGridOrdersCount)
                                        item.AugustMaxGridOrdersCount = tree.CountOrders;
                                    break;
                                case 9:
                                    if(tree.Orders.Count() > item.SeptemberMaxGridOrdersCount)
                                        item.SeptemberMaxGridOrdersCount = tree.CountOrders;
                                    break;
                                case 10:
                                    if(tree.Orders.Count() > item.OctoberMaxGridOrdersCount)
                                        item.OctoberMaxGridOrdersCount = tree.CountOrders;
                                    break;
                                case 11:
                                    if(tree.Orders.Count() > item.NovemberMaxGridOrdersCount)
                                        item.NovemberMaxGridOrdersCount = tree.CountOrders;
                                    break;
                                case 12:
                                    if(tree.Orders.Count() > item.DecemberMaxGridOrdersCount)
                                        item.DecemberMaxGridOrdersCount = tree.CountOrders;
                                    break;
                            }
                        }
                        
                    }
                }
                else // Если года нет в коллекции
                {
                    var newRow = new GridOrdersCountTable{};
                    newRow.YearVal = year.ToString();
                    switch (month)
                    {
                        case 1:
                            newRow.JanuaryMaxGridOrdersCount = tree.CountOrders;
                            break;
                        case 2:
                            newRow.FebruaryMaxGridOrdersCount = tree.CountOrders;
                            break;
                        case 3:
                            newRow.MarchMaxGridOrdersCount = tree.CountOrders;
                            break;
                        case 4:
                            newRow.AprilMaxGridOrdersCount = tree.CountOrders;
                            break;
                        case 5:
                            newRow.MayMaxGridOrdersCount = tree.CountOrders;
                            break;
                        case 6:
                            newRow.JuneMaxGridOrdersCount = tree.CountOrders;
                            break;
                        case 7:
                            newRow.JulyMaxGridOrdersCount = tree.CountOrders;
                            break;
                        case 8:
                            newRow.AugustMaxGridOrdersCount = tree.CountOrders;
                            break;
                        case 9:
                            newRow.SeptemberMaxGridOrdersCount = tree.CountOrders;
                            break;
                        case 10:
                            newRow.OctoberMaxGridOrdersCount = tree.CountOrders;
                            break;
                        case 11:
                            newRow.NovemberMaxGridOrdersCount = tree.CountOrders;
                            break;
                        case 12:
                            newRow.DecemberMaxGridOrdersCount = tree.CountOrders;
                            break;
                    }

                    GridOrdersCountTableCollection.MaxOrdersTable.Add(newRow);
                }
            }

            // После как сформированли таблицу надо заполнить нижнюю строку средних значени 
            // и средних значений за год
            var LastRow = new GridOrdersCountTable{YearVal = "Среднее"};

            GridOrdersCountTableCollection.MaxOrdersTable.Add(LastRow);
        }

        // Создаем новую строку прибылей по месяцам
        private static void AddProfit(int month, ProfitTable currenTable, TreeViewClass tree)
        {
            if (month == 1)
                currenTable.JanuaryProfit += tree.Profit;
            if (month == 2)
                currenTable.FebruaryProfit += tree.Profit;
            if (month == 3)
                currenTable.MarchProfit += tree.Profit;
            if (month == 4)
                currenTable.AprilProfit += tree.Profit;
            if (month == 5)
                currenTable.MayProfit += tree.Profit;
            if (month == 6)
                currenTable.JuneProfit += tree.Profit;
            if (month == 7)
                currenTable.JulyProfit += tree.Profit;
            if (month == 8)
                currenTable.AugustProfit += tree.Profit;
            if (month == 9)
                currenTable.SeptemberProfit += tree.Profit;
            if (month == 10)
                currenTable.OctoberProfit += tree.Profit;
            if (month == 11)
                currenTable.NovemberProfit += tree.Profit;
            if (month == 12)
                currenTable.DecemberProfit += tree.Profit;
            currenTable.SumProfit = currenTable.GetSum();
            currenTable.AverageProfit = currenTable.GetAverageValue();
        }

        // Суммарная прибыль за все время
        private static double GetSumProfit()
        {
            double totalProfit = 0;
            foreach(var grid in TreeCollection.grid)
                totalProfit += grid.Profit;
            return Math.Round(totalProfit, 2, MidpointRounding.AwayFromZero);

        }
    }
}
