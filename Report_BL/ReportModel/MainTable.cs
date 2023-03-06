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
        private double _averageProfitAll =0;
        
        //! TODO Использую свойства хотя надо использовать поля в Расчете AverageProfit -
        //! надо сделать правильно
        public double AverageProfitAll
        {
            get
            {
                //! TODO Так считать не правильно => надо все сделки и селл и бай складывать и делить на общее кол-во
                // это надо исправлять во всей таблице
                if(this.AverageProfitSell == 0) return (double)this.AverageProfitBuy;
                if(this.AverageProfitBuy == 0) return (double)this.AverageProfitSell;
                return Math.Round( (((double)this.AverageProfitSell+(double)this.AverageProfitBuy)/2), 2, MidpointRounding.AwayFromZero);
            }
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
            get
            {
                if(this.AverageGridSizeSell == 0) return (double)this.AverageGridSizeBuy;
                if(this.AverageGridSizeBuy == 0) return (double)this.AverageGridSizeSell;
                return Math.Round( (((double)this.AverageGridSizeSell+(double)this.AverageGridSizeBuy)/2), 2, MidpointRounding.AwayFromZero);
            }
        }

        #endregion
        #region Столбец "Максимальное кол-во пунктов до ТП"
        private int _maxPointsToTP_Sell = 0;
        public int MaxPointsToTP_Sell
        {
            get {return _maxPointsToTP_Sell;}
            set {_maxPointsToTP_Sell = value;}
        }
        private int _maxPointsToTP_Buy = 0;
        public int MaxPointsToTP_Buy
        {
            get {return _maxPointsToTP_Buy;}
            set {_maxPointsToTP_Buy = value;}
        }
        #endregion
        
        #region Столбец "Среднне кол-во пунктов до ТП"
        private int _averagePointsToTP_Sell = 0;
        public int AveragePointsToTP_Sell
        {
            get {return _averagePointsToTP_Sell;}
            set {_averagePointsToTP_Sell = value;}
        }
        private int _averagePointsToTP_Buy = 0;
        public int AveragePointsToTP_Buy
        {
            get {return _averagePointsToTP_Buy;}
            set {_averagePointsToTP_Buy = value;}
        }
        private int _averagePointsToTP_All = 0;
        public int AveragePointsToTP_All
        {
            get
            {
                if(_averagePointsToTP_Sell == 0)
                    return _averagePointsToTP_Buy;
                if(_averagePointsToTP_Buy == 0)
                    return _averagePointsToTP_Sell;
                return Convert.ToInt32((_averagePointsToTP_Sell + _averagePointsToTP_Buy)/2);
            }
        }
        #endregion
        #region Столбец "Максимальное время жизни сетки"
        private TimeSpan _maxTimeLifeGrid_Sell;
        public TimeSpan MaxTimeLifeGrid_Sell
        {
            get {return _maxTimeLifeGrid_Sell;}
            set {_maxTimeLifeGrid_Sell = value;}
        }
        private TimeSpan _maxTimeLifeGrid_Buy;
        public TimeSpan MaxTimeLifeGrid_Buy
        {
            get {return _maxTimeLifeGrid_Buy;}
            set {_maxTimeLifeGrid_Buy = value;}
        }
        
        private string _maxTimeLifeGrid_Sell_str = "";
        public string MaxTimeLifeGrid_Sell_str
        {
            get
            {
                switch(_maxTimeLifeGrid_Sell.Days)
                {
                    case 0:
                    if(_maxTimeLifeGrid_Sell.Minutes < 10)
                        return $"{_maxTimeLifeGrid_Sell.Hours}:0{_maxTimeLifeGrid_Sell.Minutes}";
                    return $"{_maxTimeLifeGrid_Sell.Hours}:{_maxTimeLifeGrid_Sell.Minutes}";
                    default:
                    if(_maxTimeLifeGrid_Sell.Minutes < 10)
                        return $"{_maxTimeLifeGrid_Sell.Days}d {_maxTimeLifeGrid_Sell.Hours}:0{_maxTimeLifeGrid_Sell.Minutes}";
                    return $"{_maxTimeLifeGrid_Sell.Days}d {_maxTimeLifeGrid_Sell.Hours}:{_maxTimeLifeGrid_Sell.Minutes}";
                }
            }
        }

        private string _maxTimeLifeGrid_Buy_str = "";
        public string MaxTimeLifeGrid_Buy_str
        {
            get
            {
                switch(_maxTimeLifeGrid_Buy.Days)
                {
                    case 0: // Если дней нет - не выводим их
                    if(_maxTimeLifeGrid_Buy.Minutes < 10) // Если минут меньше 10 - добавим 0 спереди
                        return $"{_maxTimeLifeGrid_Buy.Hours}:0{_maxTimeLifeGrid_Buy.Minutes}";
                    return $"{_maxTimeLifeGrid_Buy.Hours}:{_maxTimeLifeGrid_Buy.Minutes}";
                    default:
                    if(_maxTimeLifeGrid_Buy.Minutes < 10)
                        return $"{_maxTimeLifeGrid_Buy.Days}d {_maxTimeLifeGrid_Buy.Hours}:0{_maxTimeLifeGrid_Buy.Minutes}";
                    return $"{_maxTimeLifeGrid_Buy.Days}d {_maxTimeLifeGrid_Buy.Hours}:{_maxTimeLifeGrid_Buy.Minutes}";
                }
            }
        }
        #endregion
        
        #region Столбец "Среднее время жизни сетки"
        private TimeSpan _averageLifeGrid_Sell;
        public TimeSpan AverageLifeGrid_Sell
        {
            get {return _averageLifeGrid_Sell;}
            set {_averageLifeGrid_Sell = value;}
        }
        private TimeSpan _averageLifeGrid_Buy;
        public TimeSpan AverageLifeGrid_Buy
        {
            get {return _averageLifeGrid_Buy;}
            set {_averageLifeGrid_Buy = value;}
        }
        private string _averageLifeGrid_Sell_str = "";
        public string AverageLifeGrid_Sell_str
        {
            get
            {
                switch(_averageLifeGrid_Sell.Days)
                {
                    case 0:
                    if(_averageLifeGrid_Sell.Minutes < 10)
                        return $"{_averageLifeGrid_Sell.Hours}:0{_averageLifeGrid_Sell.Minutes}";
                    return $"{_averageLifeGrid_Sell.Hours}:{_averageLifeGrid_Sell.Minutes}";
                    default:
                    if(_averageLifeGrid_Sell.Minutes < 10)
                        return $"{_averageLifeGrid_Sell.Days}d {_averageLifeGrid_Sell.Hours}:0{_averageLifeGrid_Sell.Minutes}";
                    return $"{_averageLifeGrid_Sell.Days}d {_averageLifeGrid_Sell.Hours}:{_averageLifeGrid_Sell.Minutes}";
                }
            }
        }

        private string _averageLifeGrid_Buy_str = "";
        public string AverageLifeGrid_Buy_str
        {
            get
            {
                switch(_averageLifeGrid_Buy.Days)
                {
                    case 0:
                    if(_averageLifeGrid_Buy.Minutes < 10)
                        return $"{_averageLifeGrid_Buy.Hours}:0{_averageLifeGrid_Buy.Minutes}";
                    return $"{_averageLifeGrid_Buy.Hours}:{_averageLifeGrid_Buy.Minutes}";
                    default:
                    if(_averageLifeGrid_Buy.Minutes < 10)
                        return $"{_averageLifeGrid_Buy.Days}d {_averageLifeGrid_Buy.Hours}:0{_averageLifeGrid_Buy.Minutes}";
                    return $"{_averageLifeGrid_Buy.Days}d {_averageLifeGrid_Buy.Hours}:{_averageLifeGrid_Buy.Minutes}";
                }
            }
        }
        
        private string _averageLifeGrid_All_str = "";
        public string AverageLifeGrid_All_str
        {
            get
            {
                List <TimeSpan> av = new List<TimeSpan>();
                if(_averageLifeGrid_Sell.TotalMinutes != 0)
                    av.Add(_averageLifeGrid_Sell);
                if(_averageLifeGrid_Buy.TotalMinutes != 0)
                    av.Add(_averageLifeGrid_Buy);
                if(av.Count() == 0)
                    return "0";
                double doubleAverageTicks = av?.Average(TimeSpan => TimeSpan.Ticks) ?? 0;
                long longAverageTicks = Convert.ToInt64(doubleAverageTicks);
                TimeSpan rez = new TimeSpan(longAverageTicks);

                switch(rez.Days)
                {
                    case 0:
                    if(rez.Minutes < 10)
                        return $"{rez.Hours}:0{rez.Minutes}";
                    return $"{rez.Hours}:{rez.Minutes}";
                    default:
                    if(rez.Minutes < 10)
                        return $"{rez.Days}d {rez.Hours}:0{rez.Minutes}";
                    return $"{rez.Days}d {rez.Hours}:{rez.Minutes}";
                }
            }
        }
        #endregion
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