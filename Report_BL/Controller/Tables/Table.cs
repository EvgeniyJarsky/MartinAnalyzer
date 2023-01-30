using Report_BL.DataCollection;
using Report_BL.ReportModel;
using System.Linq;
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
        // Создаем таблицу прибылей по месяцам
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

        
        // Создаем таблицу максимальное количество колен сетки по месяцам
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
            
            // Получим средние значения ордеров в сетке за месяц всех годов
            int[] averageOrders = AverageOrders.Get();
            
            LastRow.JanuaryMaxGridOrdersCount = averageOrders[0];
            LastRow.FebruaryMaxGridOrdersCount = averageOrders[1];
            LastRow.MarchMaxGridOrdersCount = averageOrders[2];
            LastRow.AprilMaxGridOrdersCount = averageOrders[3];
            LastRow.MayMaxGridOrdersCount = averageOrders[4];
            LastRow.JuneMaxGridOrdersCount = averageOrders[5];
            LastRow.JulyMaxGridOrdersCount = averageOrders[6];
            LastRow.AugustMaxGridOrdersCount = averageOrders[7];
            LastRow.SeptemberMaxGridOrdersCount = averageOrders[8];
            LastRow.OctoberMaxGridOrdersCount = averageOrders[9];
            LastRow.NovemberMaxGridOrdersCount = averageOrders[10];
            LastRow.DecemberMaxGridOrdersCount = averageOrders[11];

            GridOrdersCountTableCollection.MaxOrdersTable.Add(LastRow);

            // Заполняем последнюю колонку таблицы ордеров
            foreach(var row in GridOrdersCountTableCollection.MaxOrdersTable)
            {
                row.AverageMaxGridOrdersCount = row.GetAverageValue();
            }
        }

        // Создаем главную таблицу
        public static void CreateMainTable(Report_BL.ReportModel.NewReport report)
        {
            /*
                Проходим по всему списку сеток и начинаем их заполнять
                При этом если сеток с таким количеством колен не было то
                такая строка не создастся
            */
            foreach(var tree in TreeCollection.grid)
            {
                var newRow = Report_BL.DataCollection.MainTable.GetRow(tree.CountOrders);

                switch (tree.Sell_Buy)
                {
                    case "sell":
                        newRow.countGridSell++;
                        newRow.SumLotSell += tree.Lot;
                        newRow.TotalProfitSell += tree.Profit;
                        break;
                    case "buy":
                        newRow.countGridBuy++;
                        newRow.SumLotBuy += tree.Lot;
                        newRow.TotalProfitBuy += tree.Profit;
                        break;
                }
            }

            #region  Создадим строки с сетками с количеством колен которые в данном отчете отсутствуют
            // что бы не было пропусков номеров в таблице
            int countRows = Report_BL.DataCollection.MainTable.mainTable.Count();
            bool[] countmass = new bool[countRows]; 
            Array.Fill(countmass, false);

            // Заполняем массив значениями true там где есть строки сеток
            for(int i = countRows-1; i < countRows; i++)
            {
                countmass[Report_BL.DataCollection.MainTable.mainTable[i].countOrders-1] = true;
            }
            // Создадим пустые пропущенные строки в главной таблице
            for(int i = 0; i < countmass.Length; i++)
            {
                if(countmass[i] == false)
                {
                    Report_BL.DataCollection.MainTable.GetRow(i+1);
                }
            }
            #endregion

            #region  Считаем столбец % от общей прибыли для сеток

            // Найдем суммарную прибыль за все время из таблицы прибыли по месяцам - 
            // это самый правый столбец и самое нижнее значение
            int tableSize =  Report_BL.DataCollection.ProfitTableCollection.profitTable.Count();
            double totalProfit =  Report_BL.DataCollection.ProfitTableCollection.profitTable[tableSize-1].SumProfit;

            foreach(var item in Report_BL.DataCollection.MainTable.mainTable)
            {
                item.PersentOfTotalProfitSell = Math.Round(((item.TotalProfitSell/totalProfit)*100),2, MidpointRounding.AwayFromZero); 
                item.PersentOfTotalProfitBuy = Math.Round(((item.TotalProfitBuy/totalProfit)*100),2, MidpointRounding.AwayFromZero); 
                item.PersentOfTotalProfitAll = Math.Round(((item.TotalProfitAll/totalProfit)*100),2, MidpointRounding.AwayFromZero); 
            }
            #endregion

            //! Вынести в отдельную функцию
            #region  Сортировка
            
            // var temp_ = Report_BL.DataCollection.MainTable.mainTable.OrderBy(s => s.countOrders);

            // Report_BL.DataCollection.MainTable.mainTable = new
            // System.Collections.ObjectModel.ObservableCollection<ReportModel.MainTable>(Report_BL.DataCollection.MainTable.mainTable.OrderBy(s => s.countOrders));
            
            // Report_BL.DataCollection.MainTable.mainTable.Add(new ReportModel.MainTable{countOrders=100});
            
            // var newRow1 = Report_BL.DataCollection.MainTable.GetRow(100);

            

            for(int i=0; i<Report_BL.DataCollection.MainTable.mainTable.Count(); i++)
            {
                for(int j = i+1; j<Report_BL.DataCollection.MainTable.mainTable.Count(); j++)
                {
                if(Report_BL.DataCollection.MainTable.mainTable[i].countOrders > Report_BL.DataCollection.MainTable.mainTable[j].countOrders)
                {
                    var temp = Report_BL.DataCollection.MainTable.mainTable[i];
                    Report_BL.DataCollection.MainTable.mainTable[i] = Report_BL.DataCollection.MainTable.mainTable[j];
                    Report_BL.DataCollection.MainTable.mainTable[j] = temp;
                }
                }
            }
            #endregion

            #region Считаем максимальный и средний размеры сетки
            int tableSize_ =  Report_BL.DataCollection.MainTable.mainTable.Count();
            int[] maxSizeGridSell     = new int[tableSize_+1];
            int[] maxSizeGridBuy      = new int[tableSize_+1];
            int[] averageSizeGridSell = new int[tableSize_+1];
            int[] averageSizeGridBuy  = new int[tableSize_+1];
            GridSize(out maxSizeGridSell, out maxSizeGridBuy, out averageSizeGridSell, out averageSizeGridBuy);

            // Заполняем коллекцию главной таблицы, которая биндится
            int count = 0;
            foreach(var row in Report_BL.DataCollection.MainTable.mainTable)
            {
                row.MaxGridSizeSell = maxSizeGridSell[count];
                row.MaxGridSizeBuy = maxSizeGridBuy[count];

                row.AverageGridSizeSell = averageSizeGridSell[count];
                row.AverageGridSizeBuy = averageSizeGridBuy[count];


                count++;
            }

            #endregion
        }


        // Максимальный размер сетки в пункта
        // Средний размер сетки в пункта
        private static void GridSize(out int[] maxSizeGridSell, out int[] maxSizeGridBuy, out int[] avegageSizeGridSell, out int[] averageSizeGridBuy)
        {
            // Определим кол-во строк в главной таблице
            int tableSize =  Report_BL.DataCollection.MainTable.mainTable.Count();

            List<int>[] gridSizeSell = new List<int>[tableSize+1];
            List<int>[] gridSizeBuy  = new List<int>[tableSize+1];


            int[] maxSizeGridSell_     = new int[tableSize+1];
            int[] maxSizeGridBuy_      = new int[tableSize+1];
            int[] avegageSizeGridSell_ = new int[tableSize+1];
            int[] averageSizeGridBuy_  = new int[tableSize+1];

            
            

            //  Перебираем дерево сеток
            foreach(var item in Report_BL.DataCollection.TreeCollection.grid)
            {
                // // Размер сетки с одним ордером пропускаем - потому что равен 0 
                // if(item.CountOrders ==1)
                //     continue;
                
                // // Перебираем ордера сетки и находим цену первого ордера и цену самого дальнего ордера
                // double startPrice = 0;
                // double endPrice = 0;

                // foreach(var order in item.Orders)
                // {
                //     switch (item.Sell_Buy)
                //     {
                //         case "sell":
                //             if(startPrice == 0) 
                //             {
                //                 startPrice = order.OpenPrice;
                //                 break;
                //             }
                //             if(endPrice < order.OpenPrice)
                //             {
                //                 endPrice = order.OpenPrice;
                //             }
                //             break;

                //         case "buy":
                //             endPrice = int.MaxValue; // Заведомо высокая несуществующая цена
                //             if(startPrice == 0)
                //             {
                //                 startPrice = order.OpenPrice;
                //                 break;
                //             }
                //             if(endPrice > order.OpenPrice)
                //             {
                //                 endPrice = order.OpenPrice;
                //                 break;
                //             }
                //             break;
                //     }
                // }
                // // Получим размер сетки
                // int gridSize = Convert.ToInt32(Math.Abs(startPrice-endPrice))*report.Digits;

                
                // Заполняем массив
                switch (item.Sell_Buy)
                {
                    case "sell":
                        if(gridSizeSell[item.CountOrders-1] == null)
                            gridSizeSell[item.CountOrders-1] = new List<int>(){item.GridLenght};
                        else
                            gridSizeSell[item.CountOrders-1].Add(item.GridLenght);
                        break;
                    case "buy":
                        if(gridSizeBuy[item.CountOrders-1] == null)
                            gridSizeBuy[item.CountOrders-1] = new List<int>(){item.GridLenght};
                        else
                            gridSizeBuy[item.CountOrders-1].Add(item.GridLenght);
                        break;
                }
            }
            // Ищем максимальный и средние размеры сеток для сеток с заданным кол-вом колен

            // для SELL
            for(int i = 1; i <= tableSize; i++)
            {
                maxSizeGridSell_[i] = gridSizeSell[i]?.Max() ?? 0;
                maxSizeGridBuy_[i]  = gridSizeBuy[i]?.Max() ?? 0;

                avegageSizeGridSell_[i] = Convert.ToInt32(gridSizeSell[i]?.Average() ?? 0);
                averageSizeGridBuy_[i]  = Convert.ToInt32(gridSizeBuy[i]?.Average());
            }
            maxSizeGridSell = maxSizeGridSell_;
            maxSizeGridBuy  = maxSizeGridBuy_;

            avegageSizeGridSell = avegageSizeGridSell_;
            averageSizeGridBuy  = averageSizeGridBuy_;
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
