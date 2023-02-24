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
        //! TODO CreateMainTable переделать - есть повторяющиеся циклы
        public static void CreateMainTable(Report_BL.ReportModel.NewReport report)
        {
            /*
                Проходим по всему списку сеток и начинаем их заполнять
                При этом если сеток с таким количеством колен не было то
                такая строка не создастся - это исправим ниже
            */
            foreach(var tree in TreeCollection.grid)
            {
                // Находим строку в главной таблице с tree.CountOrders кол-вом колен
                // если такой строки не то она создается пустой и добавляется в коллекцию
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
                var f = Report_BL.DataCollection.MainTable.mainTable[i].countOrders-1;
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
            
            // Размер таблицы
            int tableSize_ =  Report_BL.DataCollection.MainTable.mainTable.Count();

            #region Создаем колонки максимальное и среднее кол-во пунктов до ТП + последние колонки где время жизни
            //! TODO Все это необходимо разбить на отдельные функции

            TimeSpan[] maxLiveTime_Sell = new TimeSpan[tableSize_+1];
            TimeSpan[] maxLiveTime_Buy  = new TimeSpan[tableSize_+1];
            TimeSpan[] averageLiveTime_Sell = new TimeSpan[tableSize_+1];
            TimeSpan[] averageLiveTime_Buy  = new TimeSpan[tableSize_+1];

            List <DateTime> openDate  = new List<DateTime>();
            List <DateTime> closeDate = new List<DateTime>();

            List<TimeSpan>[] averageGridLiveTime_Sell_list = new List<TimeSpan>[tableSize_+1];
            List<TimeSpan>[] averageGridLiveTime_Buy_list  = new List<TimeSpan>[tableSize_+1];


            int[] maxPointsToTP_Sell     = new int[tableSize_+1];
            int[] maxPointsToTP_Buy      = new int[tableSize_+1];
            int[] averagePointsToTP_Sell = new int[tableSize_+1];
            int[] averagePointsToTP_Buy  = new int[tableSize_+1];

            List<double> openPrice  = new List<double>();
            List<double> closePrice = new List<double>();

            List<int>[] averagePointsToTP_Sell_list = new List<int>[tableSize_+1];
            List<int>[] averagePointsToTP_Buy_list  = new List<int>[tableSize_+1];

            int digits = 1000;
            if(report.Digits != 3)
                digits = 10000;

            foreach(var tree in TreeCollection.grid)
            {
                // Собираем все не нулевые цены открытия и закрытия всех ордеров и даты открытия и закрытия ордеров
                foreach(var order in tree.Orders)
                {
                    openDate.Add(order.OpenDate);
                    closeDate.Add(order.CloseDate);
                    
                    if(order.OpenPrice != 0)
                        openPrice.Add(order.OpenPrice);
                    if(order.ClosePrice != 0)
                        closePrice.Add(order.ClosePrice);
                }
                
                TimeSpan gridLife = TimeSpanExeptWeekDays(openDate.Min(), closeDate.Max());
                

                openDate.Clear();
                closeDate.Clear();

                double averageClosePrice = closePrice.Average(); // Средняя цена закрытия

                if(tree.Sell_Buy == "sell")
                {
                    if(averageGridLiveTime_Sell_list[tree.CountOrders-1] == null)
                        averageGridLiveTime_Sell_list[tree.CountOrders-1] = new List <TimeSpan>();
                    averageGridLiveTime_Sell_list[tree.CountOrders-1].Add(gridLife);
                    
                    if(maxLiveTime_Sell[tree.CountOrders-1] < gridLife)
                        maxLiveTime_Sell[tree.CountOrders-1] = gridLife;

                    int maxPoints = Convert.ToInt32((openPrice.Max() - averageClosePrice)*digits); 
                    if(maxPoints > maxPointsToTP_Sell[tree.CountOrders-1])
                        maxPointsToTP_Sell[tree.CountOrders-1] = maxPoints;
                    
                    if(averagePointsToTP_Sell_list[tree.CountOrders-1] == null)
                        averagePointsToTP_Sell_list[tree.CountOrders-1] = new List<int>();
                    averagePointsToTP_Sell_list[tree.CountOrders-1].Add(maxPoints);
                }
                else
                {
                    if(averageGridLiveTime_Buy_list[tree.CountOrders-1] == null)
                        averageGridLiveTime_Buy_list[tree.CountOrders-1] = new List <TimeSpan>();
                    averageGridLiveTime_Buy_list[tree.CountOrders-1].Add(gridLife);
                    
                    if(maxLiveTime_Buy[tree.CountOrders-1] < gridLife)
                        maxLiveTime_Buy[tree.CountOrders-1] = gridLife;
                    
                    int maxPoints = Convert.ToInt32((averageClosePrice - openPrice.Min())*digits);
                    if(maxPoints > maxPointsToTP_Buy[tree.CountOrders-1])
                        maxPointsToTP_Buy[tree.CountOrders-1] = maxPoints;

                    if(averagePointsToTP_Buy_list[tree.CountOrders-1] == null)
                        averagePointsToTP_Buy_list[tree.CountOrders-1] = new List<int>();
                    averagePointsToTP_Buy_list[tree.CountOrders-1].Add(maxPoints);
                }

                openPrice.Clear();
                closePrice.Clear();

            }
            double doubleAverageTicks = 0;
            long longAverageTicks = 0;
            for(int i =0; i < tableSize_+1; i++)
                {
                    doubleAverageTicks = averageGridLiveTime_Sell_list[i]?.Average(TimeSpan => TimeSpan.Ticks) ?? 0;
                    longAverageTicks = Convert.ToInt64(doubleAverageTicks);
                    averageLiveTime_Sell[i] = new TimeSpan(longAverageTicks);

                    doubleAverageTicks = averageGridLiveTime_Buy_list[i]?.Average(TimeSpan => TimeSpan.Ticks) ?? 0;
                    longAverageTicks = Convert.ToInt64(doubleAverageTicks);
                    averageLiveTime_Buy[i] = new TimeSpan(longAverageTicks);

                    averagePointsToTP_Sell[i] = Convert.ToInt32(averagePointsToTP_Sell_list[i]?.Average() ?? 0);
                    averagePointsToTP_Buy[i]  = Convert.ToInt32(averagePointsToTP_Buy_list[i]?.Average() ?? 0);
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

            //! TODO Вынести в отдельную функцию и изменить метод сортировки, пока пузырьковая
            #region  Сортировка
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
            int[] maxSizeGridSell     = new int[tableSize_+1];
            int[] maxSizeGridBuy      = new int[tableSize_+1];
            int[] averageSizeGridSell = new int[tableSize_+1];
            int[] averageSizeGridBuy  = new int[tableSize_+1];

            GridSize(out maxSizeGridSell, out maxSizeGridBuy, out averageSizeGridSell, out averageSizeGridBuy);
            #endregion
            
            // Заполняем коллекцию главной таблицы, которая биндится
            int count = 0;
            foreach(var row in Report_BL.DataCollection.MainTable.mainTable)
            {
                row.MaxGridSizeSell = maxSizeGridSell[count];
                row.MaxGridSizeBuy  = maxSizeGridBuy[count];

                row.AverageGridSizeSell = averageSizeGridSell[count];
                row.AverageGridSizeBuy  = averageSizeGridBuy[count];


                row.MaxPointsToTP_Sell = maxPointsToTP_Sell[count];
                row.MaxPointsToTP_Buy  = maxPointsToTP_Buy[count];

                row.AveragePointsToTP_Sell = averagePointsToTP_Sell[count];
                row.AveragePointsToTP_Buy  = averagePointsToTP_Buy[count];

                row.MaxTimeLifeGrid_Sell = maxLiveTime_Sell[count];
                row.MaxTimeLifeGrid_Buy  = maxLiveTime_Buy[count];

                row.AverageLifeGrid_Sell = averageLiveTime_Sell[count];
                row.AverageLifeGrid_Buy  = averageLiveTime_Buy[count];


                

                count++;
            }
            
        }

        // Возвращает TimeSpan с учетом выходных(не считает их)
        private static TimeSpan TimeSpanExeptWeekDays(DateTime start, DateTime end)
            {
                int count = 0;

                if(end.Subtract(start).Days > 1)
                    {
                        for(int i = 1; i <= end.Subtract(start).Days; i++)
                        {
                            if((int)start.AddDays(i).DayOfWeek == 6)
                                count += 2;
                        }
                    }
                DateTime newEnd = end.AddDays(-count);

                TimeSpan betWeen_ = newEnd.Subtract(start);
                return betWeen_;
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

        // Создаем столбцы максимальное и среднее кол-во пунктов до Тр
        public static void PiontsToTP()
        {
            foreach(var tree in Report_BL.DataCollection.TreeCollection.grid)
            {
                
            }
        }
    }

}
