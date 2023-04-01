using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.ReportModel
{
    public class Deal : INotifyPropertyChanged
    /// <summary>
    /// Информация о сделке - время открытия/закрытия, тип ордера, лот и т.д.
    /// </summary>
    {
        /// <summary>
        /// номер сделки
        /// </summary>
        int number;
        public int Number
        {
            get { return this.number; }
            set
            {
                if (this.number != value)
                {
                    this.number = value;
                    this.NotifyPropertyChanged("Number");
                }
            }
        }
        
        /// <summary>
        /// Символ
        /// </summary>
        string symbol;
        public string Symbol
        {
            get { return this.symbol; }
            set
            {
                if (this.symbol != value)
                {
                    this.symbol = value;
                    this.NotifyPropertyChanged("Symbol");
                }
            }
        }

        /// <summary>
        /// Дата
        /// </summary>
        DateTime date;
        public DateTime Date
        {
            get { return this.date; }
            set
            {
                if (this.date != value)
                {
                    this.date = value;
                    this.NotifyPropertyChanged("Date");
                }
            }
        }

        /// <summary>
        /// Тип ордера Buy/Sell
        /// </summary>
        string buy_sell;
        public string Buy_Sell
        {
            get { return this.buy_sell; }
            set
            {
                if (this.buy_sell != value)
                {
                    this.buy_sell = value;
                    this.NotifyPropertyChanged("Buy_Sell");
                }
            }
        }

        /// <summary>
        /// Открытие или закрытия позиции
        /// </summary>
        string direct;
        public string Direct
        {
            get { return this.direct; }
            set
            {
                if (this.direct != value)
                {
                    this.direct = value;
                    this.NotifyPropertyChanged("Direct");
                }
            }
        }

        /// <summary>
        /// Лот
        /// </summary>
        float lot;
        public float Lot
        {
            get { return (float)Math.Round(this.lot, 2) ;}
            set
            {
                if (this.lot != value)
                {
                    this.lot = value;
                    this.NotifyPropertyChanged("Lot");
                }
            }
        }

        /// <summary>
        /// Цена открытия или закрытия позиции
        /// </summary>
        float price;
        public float Price
        {
            get { return (float)Math.Round(this.price, 5);}
            set
            {
                if (this.price != value)
                {
                    this.price = value;
                    this.NotifyPropertyChanged("Price");
                }
            }
        }

        /// <summary>
        /// Прибыль сделки(если она есть)
        /// </summary>
        float profit;
        public float Profit
        {
            get { return (float)Math.Round(this.profit, 2); }
            set
            {
                if (this.profit != value)
                {
                    this.profit = value;
                    this.NotifyPropertyChanged("Profit");
                }
            }
        }

        /// <summary>
        /// Баланс на текущий момент
        /// </summary>
        float balance;
        public float Balance
        {
            get { return (float)Math.Round(this.balance, 2); }
            set
            {
                if (this.balance != value)
                {
                    this.balance = value;
                    this.NotifyPropertyChanged("Balance");
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
