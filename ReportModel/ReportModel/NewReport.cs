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
        // TODO Здесь надо получить имя файла из всего пути
        private string fileName;
        public string FileName
        {
            get { return this.fileName;} 
            set { this.fileName = Path.GetFileName(filePath); }
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
        private DateTime startDate = DateTime.MinValue;
        public DateTime StartDate
        {
            get { return this.startDate; }
            set
            {
                if(this.startDate != value)
                {
                    this.startDate = value;
                    this.NotifyPropertyChanged("StartDate");
                }
            }
        }

        /// <summary>
        /// Дата конца раппорта
        /// </summary>
        private DateTime endDate;
        public DateTime EndDate
        {
            get { return this.endDate; }
            //set { this.endDate = value; }
            set
            {
                if (this.endDate != value)
                {
                    this.endDate = value;
                    this.NotifyPropertyChanged("EndDate");
                }
            }
        }

        /// <summary>
        /// Период тестирования
        /// </summary>
        private string tradePeriod = String.Empty;
        public string TradePeriod
        {
            get { return $"{this.StartDate.ToString()}  -  {this.EndDate.ToString()}"; }
            set
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

        /// <summary>
        /// Статус объекта:
        /// false - заполнена первичная информация
        /// true - заполнен полностью
        /// </summary>
        private bool reportStatus = false;
        public bool ReportStatus
        {
            get { return this.reportStatus; }
            set { this.reportStatus = value; }
        }
        #endregion

        /// <summary>
        /// Конструктор класса Report
        /// </summary>
        /// <param name="reportType">Тип рапорта</param>
        /// <param name="filePath">Путь к файлу отчета</param>
        /// <param name="expertName">Название советника</param>
        /// <param name="symbol">Валюта(Символ)</param>
        /// <param name="startDate">Дата начала теста</param>
        /// <param name="endDate">Дата конца теста</param>
        /// <param name="timeFrame">Период тестирования</param>
        /// <param name="deposit">Начальный депозит</param>
        /// <param name="profit">Суммарная прибыль</param>
        /// <param name="drawDown">Максимальная просадка</param>
        /// <param name="magic">Меджик номер ордеров</param>
        /// <param name="digits">Кол-во цифр после запятой в котировках</param>
        /// <param name="reportStatus">Статус заполнености объекта</param>
        public NewReport(string reportType = "",
                      string filePath = "",
                      string expertName = "",
                      string symbol = "",
                      DateTime startDate = new DateTime(),
                      DateTime endDate = new DateTime(),
                      string timeFrame = "",
                      string testPeriod = "",
                      int deposit = 0,
                      double profit = 0,
                      double drawDown = 0,
                      int magic = 0,
                      int digits = 0,
                      bool reportStatus = true)
        {
            //if (digits != 3 || digits != 4 || digits != 5)
            //    throw new ArgumentException("Digits must be equal 3 or 4 or 5");
        }

        public NewReport(string filePath,
            string reportType,
            string symbol,
            int magic,
            DateTime startDate,
            DateTime endDate,
            int deposit)
        {
            this.filePath = filePath;
            this.reportType = reportType;
            this.symbol = symbol;
            this.magic = magic;
            this.startDate = startDate;
            this.endDate = endDate;
            this.deposit = deposit;

            this.fileName = Path.GetFileName(filePath);
            this.tradePeriod = $"{this.StartDate.ToString()}  -  {this.EndDate.ToString()}";
        }

        









        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }


    }
}
