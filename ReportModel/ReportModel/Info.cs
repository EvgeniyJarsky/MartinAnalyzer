using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.ReportModel
{
    /// <summary>
    /// Класс для совокупности 2-х значения параметр и значение
    /// </summary>
    public class Info : INotifyPropertyChanged
    {
        /// <summary>
        /// Параметр
        /// </summary>
        string parametr;
        public string Parametr
        {
            get { return this.parametr; }
            set
            {
                if (this.parametr != value)
                {
                    this.parametr = value;
                    this.NotifyPropertyChanged("Parametr");
                }
            }
        }

        /// <summary>
        /// Значение
        /// </summary>
        string value;
        public string Value
        {
            get { return this.value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.NotifyPropertyChanged("Value");
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
