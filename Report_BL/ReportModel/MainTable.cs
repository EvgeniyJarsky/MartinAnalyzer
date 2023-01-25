namespace Report_BL.ReportModel
{
    public class MainTable
    {
        public int countOrders {get; set;}

        // Столбец "Кол-во сеток"
        public CountGrid countGrid {get; set;}
        public SumLot sumLot {get; set;}
        public TotalProfit totalProfit {get; set;}
        public AverageProfit averageProfit;
        public PersentOfTotalProfit persentOfTotalProfit;
        public MaxGridSize maxGridSize;
        public AverageGridSize averageGridSize;
        public MaxPointsToTP maxPointsToTP;
        public AveragePointsToTP averagePointsToTP;
        public MaxTimeLifeGrid maxTimeLifeGrid;
        public AverageTimeLifeGrid averageTimeLifeGrid;
    }

    
    public struct CountGrid
    {
        public int sell;
        public int buy;
        public int all;

        public void countAll()
        {
            this.all = sell + buy;
        }
    }

    // Столбец "Сумма лотов"
    public struct SumLot
    {
        public double sell;
        public double buy;
        public double all;

        public void countAll()
        {
            this.all = sell + buy;
        }
    }

    // Столбец "Суммарная прибыль"
    public struct TotalProfit
    {
        public double sell;
        public double buy;
        public double all;

        public void countAll()
        {
            this.all = sell + buy;
        }
    }

    // Столбец "Средняя прибыль"
    public struct AverageProfit
    {
        public double sell;
        public double buy;
        public double all;

        public void countAll()
        {
            this.all = sell + buy;
        }
    }

    // Столбец "% от общей прибыли"
    public struct PersentOfTotalProfit
    {
        public double sell;
        public double buy;
        public double all;

        public void countAll()
        {
            this.all = sell + buy;
        }
    }

    // Столбец "Максимальное размер сетки в пунктах"
    public struct MaxGridSize
    {
        public int sell;
        public int buy;
    }

    // Столбец "Средний размер сетки в пунктах"
    public struct AverageGridSize
    {
        public int sell;
        public int buy;
        public int all;

        public void countAll()
        {
            this.all = sell + buy;
        }
    }  

    // Столбец "Макс. кол-во пунктов до ТР"
    public struct MaxPointsToTP
    {
        public int sell;
        public int buy;
        public int all;

        public void countAll()
        {
            this.all = sell + buy;
        }
    }

    // Столбец "Среднее кол-во пунктов до ТР
    public struct AveragePointsToTP
    {
        public int sell;
        public int buy;
        public int all;

        public void countAll()
        {
            this.all = sell + buy;
        }
    }

    // Столбец "максимальное время жизни сетки"
    public struct MaxTimeLifeGrid
    {
        public TimeSpan sell;
        public TimeSpan buy;
    }

    // Столбец "Среднее время жизни сетки"
    public struct AverageTimeLifeGrid
    {
        public TimeSpan sell;
        public TimeSpan buy;
        public TimeSpan all;
    }
}