namespace Report_BL.ReportModel
{
    public class MainTable
    {
        public int countOrders {get; set;}

        #region  Столбец "Кол-во сеток"
        public int countGridSell {get; set;}
        public int countGridBuy {get; set;}
        public int countGridAll
        {
            get
            {
                return this.countGridSell + this.countGridBuy;
            }
            private set{}
        }
        #endregion

        #region Столбец "Суммарная прибыль"
        private double totalProfitSell = 0;
        public double TotalProfitSell
        {
            get{return Math.Round(totalProfitSell,2,MidpointRounding.AwayFromZero);}
            set {totalProfitSell = value;}
        }
        private double totalProfitBuy = 0;
        public double TotalProfitBuy
        {
            get{return Math.Round(totalProfitBuy, 2, MidpointRounding.AwayFromZero);}
            set {totalProfitBuy = value;}
        }
        public double TotalProfitAll
        {
            get {return Math.Round((this.totalProfitSell+totalProfitBuy), 2, MidpointRounding.AwayFromZero);}
        }
        #endregion

        #region Столбец "Сумма лотов"
        private double sumLotSell = 0;
        public double SumLotSell
        {
            get{return Math.Round(sumLotSell,2,MidpointRounding.AwayFromZero);}
            set {sumLotSell = value;}
        }
        private double sumLotBuy = 0;
        public double SumLotBuy
        {
            get{return Math.Round(sumLotBuy, 2, MidpointRounding.AwayFromZero);}
            set {sumLotBuy = value;}
        }
        public double SumLotAll
        {
            get {return Math.Round((this.sumLotBuy+sumLotSell), 2, MidpointRounding.AwayFromZero);}
        }
        #endregion

        #region Столбец "Средняя прибыль сетки"
        private double _averageProfitSell = 0;
        public double AverageProfitSell
        {
            get 
            {
                if(this.countGridSell != 0)
                    return Math.Round((this.totalProfitSell/this.countGridSell), 2, MidpointRounding.AwayFromZero);
                else 
                    return _averageProfitSell;
            }
        }
        private double _averageProfitBuy = 0;
        public double AverageProfitBuy
        {
            get
            {
                if(countGridBuy != 0)
                    return Math.Round((this.totalProfitBuy/this.countGridBuy), 2, MidpointRounding.AwayFromZero);
                else
                    return _averageProfitBuy;
            }
        }
        private double _averageProfitAll = 0;
        //! TODO Использую свойства хотя надо использовать поля в Расчете AverageProfit -
        //! надо сделать правильно
        public double AverageProfitAll
        {
            get {return Math.Round(((this.AverageProfitSell+this.AverageProfitBuy)/2), 2, MidpointRounding.AwayFromZero);}
        }
        #endregion
        
        #region Столбец "Процент от прибыли"
        public double PersentOfTotalProfitSell {get; set;} = 0;
        public double PersentOfTotalProfitBuy {get; set;} = 0;
        public double PersentOfTotalProfitAll {get; set;} = 0;

        
        #endregion

        #region Столбец "Максимальный    размер сетки в пунктах
        private int _maxGridSizeSell = 0;
        public int MaxGridSizeSell
        {
            get {return _maxGridSizeSell;}
            set {_maxGridSizeSell = value;}
        }
        private int _maxGridSizeBuy = 0;
        public int MaxGridSizeBuy
        {
            get {return _maxGridSizeBuy;}
            set {_maxGridSizeBuy = value;}
        }
        #endregion

        #region Столбец "Средний размер сетки в пунктах"
        public int AverageGridSizeSell {get; set;}
        public int AverageGridSizeBuy {get; set;}
        public double AverageGridSizeAll
        {
            get {return Math.Round( (((double)this.AverageGridSizeSell+(double)this.AverageGridSizeBuy)/2), 2, MidpointRounding.AwayFromZero);}
        }

        #endregion
        public MaxPointsToTP maxPointsToTP;
        public AveragePointsToTP averagePointsToTP;
        public MaxTimeLifeGrid maxTimeLifeGrid;
        public AverageTimeLifeGrid averageTimeLifeGrid;
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