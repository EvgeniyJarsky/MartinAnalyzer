using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Report_BL.ReportModel
{
    /// <summary>
    /// Модель таблицы анализа сетки
    /// </summary>
    public class AnaliseGrid : INotifyPropertyChanged
    {
        public int OrderNumber { get; set; }
        public float Lot { get; set; }
        public int Step { get; set; }
        public int GridLenght { get; set; }
        public float SumLot { get; set; }
        float margin = 0;
        public float Margin
        {
            get {return this.margin;}
            set
            {
                if (this.margin != value)
                {
                    this.margin = value;
                    this.NotifyPropertyChanged("Magic");
                }
            }
        }
        public float PointPrice { get; set; }
        public float DrawDownMoney { get; set; }
        public float DrawDownProcent { get; set; }
        public float DrawDownMoneyAndMargin { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
