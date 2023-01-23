namespace Report_BL.ReportModel
{
    /// <summary>
    /// Класс таблицы максимальное кол-во колен сетки за месяц
    /// </summary>
    public class GridOrdersCountTable
    {
        /// <summary>
        /// Номер года
        /// </summary>
        public string YearVal { get; set; } = "0";
        /// <summary>
        /// Максимальное кол-во колен в сетке за январь
        /// </summary>
        int januaryMaxGridOrdersCount = 0;
        public int JanuaryMaxGridOrdersCount
        {
            get {return januaryMaxGridOrdersCount;}
            set {januaryMaxGridOrdersCount = value;}
        }
        /// <summary>
        /// Максимальное кол-во колен в сетке за Февраль
        /// </summary>
        int februaryMaxGridOrdersCount = 0;
        public int FebruaryMaxGridOrdersCount
        {
            get {return februaryMaxGridOrdersCount;}
            set {februaryMaxGridOrdersCount = value;}
        }
        /// <summary>
        /// Максимальное кол-во колен в сетке за Март
        /// </summary>
        int marchMaxGridOrdersCount = 0;
        public int MarchMaxGridOrdersCount
        {
            get {return marchMaxGridOrdersCount;}
            set {marchMaxGridOrdersCount = value;}
        }
        /// <summary>
        /// Максимальное кол-во колен в сетке за Апрель
        /// </summary>
        int aprilMaxGridOrdersCount = 0;
        public int AprilMaxGridOrdersCount
        {
            get {return aprilMaxGridOrdersCount;}
            set {aprilMaxGridOrdersCount = value;}
        }
        /// <summary>
        /// Максимальное кол-во колен в сетке за Май
        /// </summary>
        int mayMaxGridOrdersCount = 0;
        public int MayMaxGridOrdersCount
        {
            get {return mayMaxGridOrdersCount;}
            set {mayMaxGridOrdersCount = value;}
        }
        /// <summary>
        /// Максимальное кол-во колен в сетке за Июнь
        /// </summary>
        int juneMaxGridOrdersCount = 0;
        public int JuneMaxGridOrdersCount
        {
            get {return juneMaxGridOrdersCount;}
            set {juneMaxGridOrdersCount = value;}
        }
        /// <summary>
        /// Максимальное кол-во колен в сетке за Июль
        /// </summary>
        int julyMaxGridOrdersCount = 0;
        public int JulyMaxGridOrdersCount
        {
            get {return julyMaxGridOrdersCount;}
            set {julyMaxGridOrdersCount = value;}
        }
        /// <summary>
        /// Максимальное кол-во колен в сетке за Август
        /// </summary>
        int augustMaxGridOrdersCount = 0;
        public int AugustMaxGridOrdersCount
        {
            get {return augustMaxGridOrdersCount;}
            set {augustMaxGridOrdersCount = value;}
        }
        /// <summary>
        /// Максимальное кол-во колен в сетке за Сентябрь
        /// </summary>
        int septemberMaxGridOrdersCount = 0;
        public int SeptemberMaxGridOrdersCount
        {
            get {return septemberMaxGridOrdersCount;}
            set {septemberMaxGridOrdersCount = value;}
        }
        /// <summary>
        /// Максимальное кол-во колен в сетке за Октябрь
        /// </summary>
        int octoberMaxGridOrdersCount = 0;
        public int OctoberMaxGridOrdersCount
        {
            get {return octoberMaxGridOrdersCount;}
            set {octoberMaxGridOrdersCount = value;}
        }
        /// <summary>
        /// Максимальное кол-во колен в сетке за Ноябрь
        /// </summary>
        int novemberMaxGridOrdersCount = 0;
        public int NovemberMaxGridOrdersCount
        {
            get {return novemberMaxGridOrdersCount;}
            set {novemberMaxGridOrdersCount = value;}
        }
        /// <summary>
        /// Максимальное кол-во колен в сетке за Декабрь
        /// </summary>
        int decemberMaxGridOrdersCount = 0;
        public int DecemberMaxGridOrdersCount
        {
            get {return decemberMaxGridOrdersCount;}
            set {decemberMaxGridOrdersCount = value;}
        }
        
        /// <summary>
        /// Сумарная всех значений максимальных колен в месяце за весь год
        /// </summary>
        int sumMaxGridOrdersCount = 0;
        public int SumMaxGridOrdersCount
        {
            get {return sumMaxGridOrdersCount;}
            set {sumMaxGridOrdersCount = value;}
        }

        /// <summary>
        /// Среднее кол-во колен в сетке за год
        /// </summary>
        double averageMaxGridOrdersCount = 0;
        public double AverageMaxGridOrdersCount
        {
            get {return averageMaxGridOrdersCount;}
            set {averageMaxGridOrdersCount = value;}
        }

        public double GetSum()
        {
            double rez = 
                    januaryMaxGridOrdersCount+
                    februaryMaxGridOrdersCount+
                    marchMaxGridOrdersCount+
                    aprilMaxGridOrdersCount+
                    mayMaxGridOrdersCount+
                    juneMaxGridOrdersCount+
                    julyMaxGridOrdersCount+
                    augustMaxGridOrdersCount+
                    septemberMaxGridOrdersCount+
                    octoberMaxGridOrdersCount+
                    novemberMaxGridOrdersCount+
                    decemberMaxGridOrdersCount;

                return rez;
        }

        public double GetAverageValue()
        {
            int countMonths = 0;
                double rez = 0;
                #region Подсчет месяцев в которых были торги - не нулевая прибыль
                if(this.januaryMaxGridOrdersCount != 0)
                    countMonths++;
                if(this.februaryMaxGridOrdersCount != 0)
                    countMonths++;
                if(this.marchMaxGridOrdersCount != 0)
                    countMonths++;
                if(this.aprilMaxGridOrdersCount != 0)
                    countMonths++;
                if(this.mayMaxGridOrdersCount != 0)
                    countMonths++;
                if(this.juneMaxGridOrdersCount != 0)
                    countMonths++;
                if(this.julyMaxGridOrdersCount != 0)
                    countMonths++;
                if(this.augustMaxGridOrdersCount != 0)
                    countMonths++;
                if(this.septemberMaxGridOrdersCount != 0)
                    countMonths++;
                if(this.octoberMaxGridOrdersCount != 0)
                    countMonths++;
                if(this.novemberMaxGridOrdersCount != 0)
                    countMonths++;
                if(this.decemberMaxGridOrdersCount != 0)
                    countMonths++;
                #endregion
                if(countMonths != 0)
                    rez = Math.Round(this.GetSum()/countMonths, 2, MidpointRounding.AwayFromZero);
                return rez;
        }
    }
}
