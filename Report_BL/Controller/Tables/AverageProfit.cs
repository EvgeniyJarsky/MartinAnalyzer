using Report_BL.DataCollection;

namespace Report_BL.Controller.Tables
{
    public static class AverageMonthProfit
    {
        public static double[] Get()
        {
            // Массив средних прибылей в каждом месяце за все года
            double[] profit = new double[12];

            double[] monthProfit = new double[12];
            int[] monthCount = new int[12];

            foreach(var row in ProfitTableCollection.profitTable)
            {
                if(row.JanuaryProfit != 0)
                {
                    monthProfit[0] += row.JanuaryProfit;
                    monthCount[0]++;
                }
                if(row.FebruaryProfit != 0)
                {
                    monthProfit[1] += row.FebruaryProfit;
                    monthCount[1]++;
                }
                if(row.MarchProfit != 0)
                {
                    monthProfit[2] += row.MarchProfit;
                    monthCount[2]++;
                }
                if(row.AprilProfit != 0)
                {
                    monthProfit[3] += row.AprilProfit;
                    monthCount[3]++;
                }
                if(row.MayProfit != 0)
                {
                    monthProfit[4] += row.MayProfit;
                    monthCount[4]++;
                }
                if(row.JuneProfit != 0)
                {
                    monthProfit[5] += row.JuneProfit;
                    monthCount[5]++;
                }
                if(row.JulyProfit != 0)
                {
                    monthProfit[6] += row.JulyProfit;
                    monthCount[6]++;
                }
                if(row.AugustProfit != 0)
                {
                    monthProfit[7] += row.AugustProfit;
                    monthCount[7]++;
                }
                if(row.SeptemberProfit != 0)
                {
                    monthProfit[8] += row.SeptemberProfit;
                    monthCount[8]++;
                }
                if(row.OctoberProfit != 0)
                {
                    monthProfit[9] += row.OctoberProfit;
                    monthCount[9]++;
                }
                if(row.NovemberProfit != 0)
                {
                    monthProfit[10] += row.NovemberProfit;
                    monthCount[10]++;
                }
                if(row.DecemberProfit != 0)
                {
                    monthProfit[11] += row.DecemberProfit;
                    monthCount[11]++;
                }
            }
            // Пройдемся по колличетву одинаковый месяцев в отчете что бы исключить деление на ноль
            //! TODO надо ли это делать?
            for(int i=0; i<=11; i++)
            {
                if(monthCount[i] == 0)
                {
                    monthProfit[i] = 0;
                    monthCount[i] = 1;
                }
            }
            // Посчитаем среднюю прибыль для каждого месяца
            for(int i=0; i<=11; i++)
            {
                profit[i] = Math.Round(monthProfit[i]/monthCount[i], 2, MidpointRounding.AwayFromZero);
            }
            return profit;
        }

    }
}