using Report_BL.DataCollection;

namespace Report_BL.Controller.Tables
{
    public static class AverageOrders
    {
        public static int[] Get()
        {
            // Массив средних прибылей в каждом месяце за все года
            int[] orders = new int[12];

            double[] ordersInMonth = new double[12];
            int[] monthCount = new int[12];

            foreach(var row in GridOrdersCountTableCollection.MaxOrdersTable)
            {
                if(row.JanuaryMaxGridOrdersCount != 0)
                {
                    ordersInMonth[0] += row.JanuaryMaxGridOrdersCount;
                    monthCount[0]++;
                }
                if(row.FebruaryMaxGridOrdersCount != 0)
                {
                    ordersInMonth[1] += row.FebruaryMaxGridOrdersCount;
                    monthCount[1]++;
                }
                if(row.MarchMaxGridOrdersCount != 0)
                {
                    ordersInMonth[2] += row.MarchMaxGridOrdersCount;
                    monthCount[2]++;
                }
                if(row.AprilMaxGridOrdersCount != 0)
                {
                    ordersInMonth[3] += row.AprilMaxGridOrdersCount;
                    monthCount[3]++;
                }
                if(row.MayMaxGridOrdersCount != 0)
                {
                    ordersInMonth[4] += row.MayMaxGridOrdersCount;
                    monthCount[4]++;
                }
                if(row.JuneMaxGridOrdersCount != 0)
                {
                    ordersInMonth[5] += row.JuneMaxGridOrdersCount;
                    monthCount[5]++;
                }
                if(row.JulyMaxGridOrdersCount != 0)
                {
                    ordersInMonth[6] += row.JulyMaxGridOrdersCount;
                    monthCount[6]++;
                }
                if(row.AugustMaxGridOrdersCount != 0)
                {
                    ordersInMonth[7] += row.AugustMaxGridOrdersCount;
                    monthCount[7]++;
                }
                if(row.SeptemberMaxGridOrdersCount != 0)
                {
                    ordersInMonth[8] += row.SeptemberMaxGridOrdersCount;
                    monthCount[8]++;
                }
                if(row.OctoberMaxGridOrdersCount != 0)
                {
                    ordersInMonth[9] += row.OctoberMaxGridOrdersCount;
                    monthCount[9]++;
                }
                if(row.NovemberMaxGridOrdersCount != 0)
                {
                    ordersInMonth[10] += row.NovemberMaxGridOrdersCount;
                    monthCount[10]++;
                }
                if(row.DecemberMaxGridOrdersCount != 0)
                {
                    ordersInMonth[11] += row.DecemberMaxGridOrdersCount;
                    monthCount[11]++;
                }
            }
            // Пройдемся по колличетву одинаковый месяцев в отчете что бы исключить деление на ноль
            //! TODO надо ли это делать?
            for(int i=0; i<=11; i++)
            {
                if(monthCount[i] == 0)
                {
                    ordersInMonth[i] = 0;
                    monthCount[i] = 1;
                }
            }
            // Посчитаем среднюю прибыль для каждого месяца
            for(int i=0; i<=11; i++)
            {
                orders[i] = Convert.ToInt32(Math.Round(ordersInMonth[i]/monthCount[i], 2, MidpointRounding.AwayFromZero));
            }
            return orders;
        }

    }
}