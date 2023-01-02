using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.IO;

using WorkWithFiles.LoadFile;

using Report_BL.ReportModel;

using System.Data.SQLite;


namespace WPF_NET6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<NewReport> newReport =
            Report_BL.DataCollection.ReportCollection.newReport;

        /// <summary>
        /// Коллекция объектов Info(состоит из 2-х значений - парамет и значение)
        /// </summary>
        public static ObservableCollection<Info> param = Report_BL.DataCollection.ParamentrsCollection.param;

        /// <summary>
        /// Коллекция объектов Report(отчетов)
        /// </summary>
        public static ObservableCollection<Report> report = Report_BL.DataCollection.ReportCollection.report;
        
        /// <summary>
        /// Коллекция объектов "Сделка" с основными переметрами - время открытия/закрытия, тип ордера, лот и т.д.
        /// </summary>
        public static ObservableCollection<Deal> dealsCollection = Report_BL.DataCollection.DealsCollection.dealsCollection;

        public MainWindow()
        {
            InitializeComponent();
            listBox_.ItemsSource = newReport;
            info.ItemsSource = param;
            //deals.ItemsSource = dealsCollection;
        }
        
        /// <summary>
        /// Кнопка открыть файл
        /// </summary>
        private void Btn_LoadFile(object sender, RoutedEventArgs e)
        {
            // Очишаем данные
            Report_BL.DataCollection.ClearAllData.ClearParamAndDeals();

            #region Словарь для первичной информации
            var dicFirstInfo = new Dictionary<string, Func<string, Report_BL.ReportModel.FirstInfo>>();

            // MT4Tester
            dicFirstInfo.Add(
                    "MT4Tester",
                    new Func<string, Report_BL.ReportModel.FirstInfo>
                    (Report_BL.Controller.MainInfo.MT4Tester.GetFirstInfoMT4.GetSymbolDateMagic));
            // MT4History
            dicFirstInfo.Add(
                "MT4History",
                new Func<string, Report_BL.ReportModel.FirstInfo>
                (Report_BL.Controller.MainInfo.MT4History.GetFirstInfoMT4History.GetSymbolDateMagic));
            #endregion

            // открываем диалог и выбираем файл
            string filePath = LoadFiles.LoadFile();

            // TODO проверить что reportType != "UnKnownFile"
            string reportType = Report_BL.Controller.MainInfo.DetectReportType.GetReportType(filePath);

            //Report_BL.ReportModel.FirstInfo firstInfo = dicFirstInfo[reportType](filePath);
            Report_BL.ReportModel.FirstInfo firstInfo = Report_BL.Controller.MainInfo.FirstInfo.Get(reportType, filePath);

            if (firstInfo == null)
                MessageBox.Show("Ошибка чтения файла!!!");
            else
            {
                // Окно выбора символа и меджика и даты и депозита
                SelectSymbolMagic selectMagSym = new SelectSymbolMagic(firstInfo);
                selectMagSym.ShowDialog();
            }



            // Создаем новый объект класса Report и вытягиваем
            // основные параметры
            Report_BL.Controller.MainInfo.MainInfo.GetMainInfo(filePath);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            
            // получим список выбранных отчетов
            var selectedList = listBox_.SelectedItems;

            // если отчетов выбрано несколько то удалим первый
            //! todo надо убрать ввозможность выбирать несколько отчетов
            if (selectedList.Count != 0)
            {
                var firstSelected = ((NewReport)selectedList[0]);
                newReport.Remove(firstSelected);
            }
            TreeGrid.Items.Clear();
            Report_BL.DataCollection.ClearAllData.ClearParamAndDeals();

        }

        private void DeleteAll(object sender, RoutedEventArgs e)
        {
            TreeGrid.Items.Clear();
            Report_BL.DataCollection.ClearAllData.ClearAll();
            listBox_.Items.Clear();
        }

        // При изменении выбранного отчета
        private void ChangeSelectedListBox(object sender, SelectionChangedEventArgs e)
        {
            Report_BL.DataCollection.ClearAllData.ClearParamAndDeals();

            var selectedList = listBox_.SelectedItems;// список выбранных отчетов

            if (selectedList.Count != 0) // проверка если удалили последний объект
            {
                NewReport firstSelected = (NewReport)selectedList[0];
                Report_BL.DataCollection.ParamentrsCollection.AddNewItem(firstSelected);

                Report_BL.Controller.GetDeals.GetDeals.Get(firstSelected);

                //******************************************************
                // Читаем БД и формируем дерево во второй вкладке
                string pathToBD = $"database/{Path.GetFileNameWithoutExtension(firstSelected.FileName) + ".db"}";
                using (var connection = new SQLiteConnection(string.Format("Data Source={0};", pathToBD)))
                {
                    connection.Open();

                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = connection;

                    // Определим сколько всего сеток было построено
                    command.CommandText = "SELECT COUNT(id) FROM grid";
                    var countGrid = command.ExecuteScalar();
                    // переведем значение в int32
                    int count = Convert.ToInt32(countGrid);

                    // перебираем все сетки
                    for(int i =1; i <= count; i++ )
                    {
                        // Создаем новый элемент
                        var newGrid = new TreeViewItem();

                        command.Parameters.AddWithValue("$item", i);
                        command.Parameters.AddWithValue("$reportSymbol", firstSelected.Symbol);

                        #region  Получим информацию о сетке
                        command.CommandText = @$"SELECT
                                                grid_number,
                                                symbol_name,
                                                type
                                            FROM grid
                                            JOIN  symbol ON symbol.id  = symbol_id
                                            JOIN buy_sell ON buy_sell.id = grid_type_id
                                            WHERE grid_number = $item;";
                        string symbol = "";
                        string gridType = "";
                        using(SQLiteDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            symbol = reader["symbol_name"].ToString() ?? "";
                            gridType = reader["type"].ToString() ?? "";
                        }
                        #endregion

                        #region  Получим все сделки входящие в эту сетку
                        command.CommandText = @$"SELECT open_date,
                                                        close_date,
                                                        open_price,
                                                        close_price,
                                                        lot,
                                                        profit
                                                FROM deal
                                                JOIN grid ON grid.id = grid_id
                                                JOIN symbol ON symbol.id = deal.symbol_id
                                                WHERE grid_number = $item
                                                AND symbol.symbol_name = $reportSymbol;";
                        string openDate = "";
                        string closeDate = "";
                        double openPrice = 0;
                        double closePrice = 0;
                        double lot = 0;
                        double profit = 0;

                        DateTime startDate = DateTime.MinValue;
                        DateTime endDate = DateTime.MaxValue;

                        // подключаемся к БД
                        using(SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // перебираем БД пока есть строки в БД 
                            if(reader.HasRows)
                            {
                                while(reader.Read())
                                {
                                    
                                    
                                    openDate = Convert.ToString(reader["open_date"]) ?? "";
                                    closeDate = Convert.ToString(reader["close_date"]) ?? "";
                                    openPrice = Convert.ToDouble(reader["open_price"]);
                                    closePrice = Convert.ToDouble(reader["close_price"]);
                                    lot = Convert.ToDouble(reader["lot"]);
                                    profit = Convert.ToDouble(reader["profit"]);

                                    if(startDate == DateTime.MinValue)
                                        startDate = DateTime.Parse(openDate);
                                    endDate = DateTime.Parse(closeDate);

                                    newGrid.Items.Add(@$"Дата открытия = {openDate} | Дата закрытия = {closeDate} | Цена открытия = {openPrice} | Цена закрытия = {closePrice} | Лот = {lot} | Прибыль = {profit}");
                                }
                            }
                        }
                        #endregion

                        #region  Получим колличество колен в сетке
                        command.CommandText = @$"SELECT   COUNT(lot)
                                                FROM deal
                                                JOIN grid ON grid.id = grid_id
                                                JOIN symbol ON symbol.id = deal.symbol_id
                                                WHERE grid_number = $item
                                                AND symbol.symbol_name = $reportSymbol;"; 
                        int countColen = Convert.ToInt32(command.ExecuteScalar());
                        #endregion
                        
                        #region  Получим сумарную лотность
                        command.CommandText = @$"SELECT   SUM(lot)
                                                FROM deal
                                                JOIN grid ON grid.id = grid_id
                                                JOIN symbol ON symbol.id = deal.symbol_id
                                                WHERE grid_number = $item
                                                AND symbol.symbol_name = $reportSymbol;"; 
                        double sumLot = Math.Round(Convert.ToDouble(command.ExecuteScalar()), 2);
                        #endregion

                        #region  Получим сумарную прибыль
                        
                        command.CommandText = @$"SELECT   SUM(profit)
                                                FROM deal
                                                JOIN grid ON grid.id = grid_id
                                                JOIN symbol ON symbol.id = deal.symbol_id
                                                WHERE grid_number = $item
                                                AND symbol.symbol_name = $reportSymbol;"; 
                        double sumProfit = Math.Round(Convert.ToDouble(command.ExecuteScalar()), 2);
                        #endregion

                        #region определим размер сетки
                        command.CommandText = @$"SELECT 
                                            MAX(deal.open_price)
                                        FROM deal
                                        JOIN grid ON grid.id = grid_id
                                        JOIN symbol ON symbol.id = deal.symbol_id
                                        JOIN buy_sell ON buy_sell.id = buy_sell_id
                                        WHERE grid_number = $item;";
                        double maxPrice = Convert.ToDouble(command.ExecuteScalar());
                        
                        command.CommandText = @$"SELECT 
                                            MIN(deal.open_price)
                                        FROM deal
                                        JOIN grid ON grid.id = grid_id
                                        JOIN symbol ON symbol.id = deal.symbol_id
                                        JOIN buy_sell ON buy_sell.id = buy_sell_id
                                        WHERE grid_number = $item;";
                        double mixPrice = Convert.ToDouble(command.ExecuteScalar());

                        int digit = 100000;
                        if(firstSelected.Digits == 3)
                            digit = 1000;

                        int gridLenght = Convert.ToInt32((maxPrice - mixPrice)*digit);
                        #endregion

                        // Добавим основную информацию о сетке - голова дерева
                        string f = Report_BL.Controller.Func.Dates.HowManyTimesBetween(startDate.ToString(), endDate.ToString());

                        newGrid.Header = $"Сетка {i} | Колен = {countColen} | Символ = {symbol} | Тип = {gridType} | Суммарный лот = {sumLot} | Прибыль = {sumProfit} | Дина сетки = {gridLenght} | Время = {f}";
                        TreeGrid.Items.Add(newGrid);
                        
                    }
                    connection.Close();
                }

                //*********************************************************
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
