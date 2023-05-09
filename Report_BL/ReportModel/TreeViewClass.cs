using System.Collections.ObjectModel;


namespace Report_BL.ReportModel
{
    //<summary>
    // Класс для биндинга девера сеток
    //</summary>
    public class TreeViewClass
    {
        #region Свойства
        // Номер сетки
        public int NumberGrid {get; set;}
        // Кол-во колен
        public int CountOrders {get; set;}
        // Имя символа
        public string Symbol {get; set;}
        // Тип сетки Sell/Buy
        public string Sell_Buy {get; set;}
        //Дата начала сетки
        public DateTime StartDate = DateTime.MinValue;
        // Дата конца сетки
        public DateTime EndDate = DateTime.MaxValue;
        // Суммарный лот
        private double lot = 0;
        public double Lot
        {
            get {return Math.Round(lot, 2, MidpointRounding.AwayFromZero);}
            set {lot = value;}
        }
        
        // Profit
        private double profit = 0;
        public double Profit
        {
            // get {return Math.Round(profit, 2, MidpointRounding.AwayFromZero);}
            // set {profit = value;}
            get { return Math.Round(GetGridProfit(), 2);}
        }
        // Длина сетки
        public int GridLenght {set; get;}
        // Продолжительность сетки
        public string GridPeriod
        {
            private set{}
            get
            {
                return Report_BL.Controller.Func.Dates.HowManyTimesBetween(this.StartDate.ToString(), this.EndDate.ToString());
            }
        }
        
        // кол-во пунктрв до ТР
        public int PointsToTP {get; set;} = 0;
        #endregion

        public ObservableCollection<Order> Orders {set; get;}

        public TreeViewClass()
        {
            Orders = new ObservableCollection<Order>();
        }
        // Суммарная информация о сетке
        public string GridInfo
        {
            private set{}
            get
            {
                return @$"Сетка {NumberGrid} | Колен {CountOrders} | Символ {Symbol} | Тип {Sell_Buy} | Суммарный лот {Lot} | Прибыль {Profit} | Длина сетки {GridLenght} | Время жизни {GridPeriod} | Пунктов до ТП {PointsToTP}({PercentToTP(this.GridLenght,this.PointsToTP)})";
            }
        }
    
        private float GetGridProfit()
        {
            float profit = 0;
            foreach(var order in this.Orders)
                profit += (float)order.Profit;

            return (float)Math.Round(profit,2, MidpointRounding.AwayFromZero);
        }

        private string PercentToTP(int gridLenght, int pointToTP)
        {
            if(gridLenght == 0) return "0%";
            double gridLenghtD = gridLenght;
            double pointToTPD = pointToTP;
            var rezInt = Convert.ToInt32((pointToTPD/gridLenghtD)*100); 
            return rezInt.ToString() + '%';
        }
    }

    public class Order
    {
        #region Свойства
        // Номер ордера
        public int orderNumber = 0;
        // Время открытия ордера
        public DateTime OpenDate {set; get;}
        // Время закрытия ордера
        public DateTime CloseDate {set; get;}
        // Цена открытия
        public float OpenPrice {set; get;}
        // Цена закрытия
        public float ClosePrice {set; get;}
        // Лот
        private float lot = 0;
        public float Lot
        {
            get {return (float)Math.Round(lot, 2, MidpointRounding.AwayFromZero);}
            set {lot = value;}
        }
        // Прибыль
        private double profit = 0;
        public double Profit
        {
            get {return Math.Round(profit, 2, MidpointRounding.AwayFromZero);}
            set {profit = value;}
        }
        #endregion

        public string OrderInfo
        {
            private set {}
            get
            {
                return @$"Дата открытия {OpenDate} | Дата закрытия {CloseDate} | Цена открытия {OpenPrice} | Цена закрытия {ClosePrice} | Лот {Lot} | Прибыль {Profit}";
            }
        }
    }
}
