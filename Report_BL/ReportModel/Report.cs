using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.ReportModel
{
    /// <summary>
    /// Рапорт
    /// </summary>
    public class Report : INotifyPropertyChanged
    {
        #region Свойства
        /// <summary>
        /// Тип рапорта
        /// </summary>
        string reportType = String.Empty;
        public string ReportType
        {
            get { return this.reportType; }
            set
            {
                if (this.reportType != value)
                {
                    this.reportType = value;
                    this.NotifyPropertyChanged("ReportType");
                }
            }
        }
        
        /// <summary>
        /// Путь к файлу отчета
        /// </summary>
        string filePath = String.Empty;
        public string FilePath
        {
            get { return this.filePath; }
            set
            {
                if (this.filePath != value)
                {
                    this.filePath = value;
                    this.NotifyPropertyChanged("FilePath");
                }
            }
        }

        /// <summary>
        /// Название советника
        /// </summary>
        string expertName = String.Empty;
        public string ExpertName
        {
            get { return this.expertName; }
            set
            {
                if (this.expertName != value)
                {
                    this.expertName = value;
                    this.NotifyPropertyChanged("ExpertName");
                }
            }
        }

        /// <summary>
        /// Валюта(Символ)
        /// </summary>
        string curency = String.Empty;
        public string Curency
        {
            get { return this.curency; }
            set
            {
                if (this.curency != value)
                {
                    this.curency = value;
                    this.NotifyPropertyChanged("Curency");
                }
            }
        }

        /// <summary>
        /// Период тестирования
        /// </summary>
        string timeFrame = String.Empty;
        public string TimeFrame
        {
            get { return this.timeFrame; }
            set
            {
                if (this.timeFrame != value)
                {
                    this.timeFrame = value;
                    this.NotifyPropertyChanged("TimeFrame");
                }
            }
        }

        /// <summary>
        /// Таймфрейм отчета тестироани
        /// </summary>
        string testPeriod = String.Empty;
        public string TestPeriod
        {
            get { return this.testPeriod; }
            set
            {
                if (this.testPeriod != value)
                {
                    this.testPeriod = value;
                    this.NotifyPropertyChanged("TestPeriod");
                }
            }
        }

        /// <summary>
        /// Начальный депозит
        /// </summary>
        string deposit = String.Empty;
        public string Deposit
        {
            get { return this.deposit; }
            set
            {
                if (this.deposit != value)
                {
                    this.deposit = value;
                    this.NotifyPropertyChanged("Deposit");
                }
            }
        }

        /// <summary>
        /// Суммарная прибыль
        /// </summary>
        string profit = String.Empty;
        public string Profit
        {
            get { return this.profit; }
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
        /// Максимальная просадка
        /// </summary>
        string drawDown = String.Empty;
        public string DrawDown
        {
            get { return this.drawDown; }
            set
            {
                if (this.drawDown != value)
                {
                    this.drawDown = value;
                    this.NotifyPropertyChanged("DrawDown");
                }
            }
        }

        /// <summary>
        /// Меджик номер ордеров
        /// </summary>
        string magic = String.Empty;
        public string Magic
        {
            get { return this.magic; }
            set
            {
                if (this.magic != value)
                {
                    this.magic = value;
                    this.NotifyPropertyChanged("Magic");
                }
            }
        }

        /// <summary>
        /// Рентабельность
        /// </summary>
        string profitability = String.Empty; // Рентабельность
        public string Profitability
        {
            get { return this.profitability; }
            set
            {
                if (this.profitability != value)
                {
                    this.profitability = value;
                    this.NotifyPropertyChanged("Profitability");
                }
            }
        }

        /// <summary>
        /// Кол-во цифр после запятой в котировках
        /// </summary>
        string digits = String.Empty;
        public string Digits
        {
            get { return this.digits; }
            set
            {
                if (this.digits != value)
                {
                    this.digits = value;
                    this.NotifyPropertyChanged("Digits");
                }
            }
        }
        #endregion

        /// <summary>
        /// Конструктор класса Report
        /// </summary>
        /// <param name="reportType">Тип рапорта</param>
        /// <param name="filePath">Путь к файлу отчета</param>
        /// <param name="expertName">Название советника</param>
        /// <param name="curency">Валюта(Символ)</param>
        /// <param name="timeFrame">Период тестирования</param>
        /// <param name="testPeriod">Таймфрейм отчета тестироани</param>
        /// <param name="deposit">Начальный депозит</param>
        /// <param name="profit">Суммарная прибыль</param>
        /// <param name="drawDown">Максимальная просадка</param>
        /// <param name="magic">Меджик номер ордеров</param>
        /// <param name="profitability">Рентабельность</param>
        /// <param name="digits">Кол-во цифр после запятой в котировках</param>
        public Report(string reportType    = "",
                      string filePath      = "",
                      string expertName    = "",
                      string curency       = "",
                      string timeFrame     = "",
                      string testPeriod    = "",
                      string deposit       = "",
                      string profit        = "",
                      string drawDown      = "",
                      string magic         = "",
                      string profitability = "",
                      string digits        = "")
        {

        }











        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }


    }
}
