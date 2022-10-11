using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.ReportModel
{
    /// <summary>
    /// Класс для запоминания основных параметров:
    /// Имя символа, Мэджик номер, Дата начала торгов, Дата конца торгов
    /// Используется для вывода информации в окне при выборе отчета
    /// Что бы была возможность какой символ, меджик и период теста
    /// </summary>
    public class FirstInfo
    {
        #region Поля и свойства
        /// <summary>
        /// Путь к файлу
        /// </summary>
        private string filePath = String.Empty;
        public string FilePath { get { return filePath; } }
        /// <summary>
        /// Тип отчета
        /// </summary>
        private string reportType = String.Empty;
        public string ReportType { get { return reportType; } }
        /// <summary>
        /// Словарь <symbol, magic>
        /// </summary>
        private Dictionary<string, List<int>> _dicSymbolMagic;
        public Dictionary<string, List<int>> DicSymbolMagic { get; set; }
        /// <summary>
        /// Дата начала торгов
        /// </summary>
        private readonly DateTime startDate = DateTime.MinValue;
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Дата конца торгов
        /// </summary>
        private readonly DateTime endDate = new DateTime(80, 01, 17);
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Начальный депозит
        /// </summary>
        private int startDeposit = 0;
        public int StartDeposit
        {
            get { return startDeposit; }
            set
            {
                // Депозит должен быть числом int и быть >=0
                if (value.GetType() == typeof(int) && value >= 0)
                    startDeposit = value;
                else
                    startDeposit = 0;
            }
        }
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="sym">Имя символа</param>
        /// <param name="mag">Мэджик номер</param>
        /// <param name="startD">Дата начала торгов</param>
        /// <param name="endD">Дата конца торгов</param>
        public FirstInfo(string filePath, string reportType, Dictionary<string, List<int>> dic, DateTime startD, DateTime endD, int startDeposit)
        {
            this.filePath       = filePath;
            this.reportType = reportType;
            this.DicSymbolMagic = dic;
            this.StartDate      = startD;
            this.EndDate        = endD;
            this.StartDeposit   = startDeposit;
        }
    }
}
