using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Report_BL.ReportModel
{
    /// <summary>
    /// Рапорт
    /// </summary>
    public class NewReport : INotifyPropertyChanged
    {
        #region Свойства
        /// <summary>
        /// Тип рапорта
        /// </summary>
        private string reportType = String.Empty;
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
        private string filePath = String.Empty;
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
        /// Имя файла берется из filePath
        /// </summary>
        private string fileName;
        public string FileName
        {
            get { return this.fileName;} 
            private set { this.fileName = Path.GetFileName(filePath); }
        }

        /// <summary>
        /// Название советника
        /// </summary>
        private string expertName = String.Empty;
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
        private string symbol = String.Empty;
        public string Symbol
        {
            get { return this.symbol; }
            set
            {
                if (this.symbol != value)
                {
                    this.symbol = value;
                    this.NotifyPropertyChanged("Curency");
                }
            }
        }

        /// <summary>
        /// Дата начала рапорта
        /// </summary>
        private DateTime startDate;
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата конца раппорта
        /// </summary>
        private DateTime endDate;
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Период тестирования
        /// </summary>
        private string tradePeriod = String.Empty;
        public string TradePeriod
        {
            get { return $"{this.StartDate.ToString()} + \" - \" {this.EndDate.ToString()}"; }
            private set
            {
                if (this.tradePeriod != value)
                {
                    this.tradePeriod = value;
                    this.NotifyPropertyChanged("TradePeriod");
                }
            }
        }

        /// <summary>
        /// Таймфрейм отчета тестироани
        /// </summary>
        private string timeFrame = String.Empty;
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
        /// Начальный депозит
        /// </summary>
        private int deposit;
        public int Deposit
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
        private double profit;
        public double Profit
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
        private double drawDown;
        public double DrawDown
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
        private int magic;
        public int Magic
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
        private double profitability; // Рентабельность
        public double Profitability
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
        private int digits;
        public int Digits
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
        public NewReport(string reportType = "",
                      string filePath = "",
                      string expertName = "",
                      string curency = "",
                      string timeFrame = "",
                      string testPeriod = "",
                      string deposit = "",
                      string profit = "",
                      string drawDown = "",
                      string magic = "",
                      string profitability = "",
                      string digits = "")
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
