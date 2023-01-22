﻿using Report_BL.DataCollection;
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
        public static void CreateMainTables()
        {
            // Перебираем всю коллекцию сеток
            foreach(var tree in TreeCollection.grid)
            {
                int year = tree.EndDate.Year;
                int month = tree.EndDate.Month;

                //Проверим если уже этот год в коллекции
                bool isYearCreated = IsYearCreated.Get(year);

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
