using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace Report_BL.ReportModel
{
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
        // Суммарный лот
        public double Lot {get; set;}
        // Profit
        public double Profit {get; set;}
        // Длина сетки
        public int GridLenght {set; get;}
        // Продолжительность сетки
        public string GridPeriod {set; get;}
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
                return @$"Сетка {NumberGrid} | Колен {CountOrders} | Символ {Symbol} | Тип {Sell_Buy} | Суммарный лот {Lot} | Прибыль {Profit} | Длина сетки {GridLenght} | Время жизни {GridPeriod}";
            }
        }
    }

    public class Order
    {
        #region Свойства
        // Время открытия ордера
        public DateTime OpenDate {set; get;}
        // Время закрытия ордера
        public DateTime CloseDate {set; get;}
        // Цена открытия
        public double OpenPrice {set; get;}
        // Цена закрытия
        public double ClosePrice {set; get;}
        // Лот
        public double Lot {set; get;}
        // Прибыль
        public double Profit {set; get;}
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
