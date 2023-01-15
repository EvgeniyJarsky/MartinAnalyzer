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
        public static void CreateMainTables()
        {
            // Перебираем всю коллекцию сеток
            foreach(var tree in TreeCollection.grid)
            {
                int year = tree.EndDate.Year;
                int month = tree.EndDate.Month;

                #region Проверим если уже этот год в коллекции
                bool isYearCreated = false;
                foreach(var item in ProfitTableCollection.profitTable)
                {
                    if(item.YearVal == year)
                    {
                        isYearCreated = true;
                        break;
                    }
                }
                #endregion

                if(isYearCreated) // Если год уже есть в коллекции
                {
                    foreach (var item in ProfitTableCollection.profitTable)
                    {
                        if (item.YearVal == year)
                        {
                            AddProfit(month, item, tree);
                        }
                    }
                }
                else // Если года нет в коллекции
                {
                    // TODO может надо обнулить все переметры?
                    var newYear = new ProfitTable { YearVal = year };
                    
                    AddProfit(month, newYear, tree);

                    ProfitTableCollection.profitTable.Add(newYear);
                }
            }
        }

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
        }
    }
}
