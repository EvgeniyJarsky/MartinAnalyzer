﻿using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.IO;
using System.Linq;


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
            // listBox_.ItemsSource = newReport;
            listBox_.ItemsSource = Report_BL.DataCollection.ReportCollection.newReport;
            // info.ItemsSource = param;
            info.ItemsSource = Report_BL.DataCollection.ParamentrsCollection.param;
            //deals.ItemsSource = dealsCollection;
        }
        
        /// <summary>
        /// Кнопка открыть файл
        /// </summary>
        private void Btn_LoadFile(object sender, RoutedEventArgs e)
        {
            // Очишаем данные
            Report_BL.DataCollection.ClearAllData.ClearParamAndDeals();
            Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable.Clear();
            Report_BL.DataCollection.ProfitTableCollection.profitTable.Clear();
            Report_BL.DataCollection.MainTable.mainTable.Clear();


            // открываем диалог и выбираем файл
            string filePath = LoadFiles.LoadFile();

            // Определяем тип файла
            string reportType = Report_BL.Controller.MainInfo.DetectReportType.GetReportType(filePath);
            // Если неизвестый файл - выводим сообщение и завершаем добавление файла
            if(reportType == "UnKnownFile")
            {
                MessageBox.Show("Неверный файл!");
                return;
            }

            //Report_BL.ReportModel.FirstInfo firstInfo = dicFirstInfo[reportType](filePath);
            Report_BL.ReportModel.FirstInfo? firstInfo = Report_BL.Controller.MainInfo.FirstInfo.Get(reportType, filePath);
            if(firstInfo == null)
                {
                    MessageBox.Show("Ошибка чтения первичной информации!!!");
                    return;
                }

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
            var selectedReports = listBox_.SelectedItems;

            // если отчетов выбрано несколько то удалим первый
            //! todo надо убрать ввозможность выбирать несколько отчетов
            if (selectedReports.Count != 0 && selectedReports != null)
            {
                var selectedReport = ((NewReport)selectedReports[0]);
                Report_BL.DataCollection.ReportCollection.newReport.Remove(selectedReport);
            }
            Report_BL.DataCollection.TreeCollection.grid.Clear();
            Report_BL.DataCollection.ClearAllData.ClearParamAndDeals();

            Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable.Clear();
            Report_BL.DataCollection.ProfitTableCollection.profitTable.Clear();
            Report_BL.DataCollection.MainTable.mainTable.Clear();

        }

        private void DeleteAll(object sender, RoutedEventArgs e)
        {
            Report_BL.DataCollection.TreeCollection.grid.Clear();
            Report_BL.DataCollection.ClearAllData.ClearAll();

            Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable.Clear();
            Report_BL.DataCollection.ProfitTableCollection.profitTable.Clear();
            Report_BL.DataCollection.MainTable.mainTable.Clear();
            
            Report_BL.DataCollection.ReportCollection.newReport.Clear();
        }

        // При изменении выбранного отчета
        private void ChangeSelectedListBox(object sender, SelectionChangedEventArgs e)
        {
            Report_BL.DataCollection.ClearAllData.ClearParamAndDeals();
            Report_BL.DataCollection.TreeCollection.grid.Clear();

            Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable.Clear();
            Report_BL.DataCollection.ProfitTableCollection.profitTable.Clear();
            Report_BL.DataCollection.MainTable.mainTable.Clear();

            var selectedList = listBox_.SelectedItems;// список выбранных отчетов

            if (selectedList.Count != 0) // проверка если удалили последний объект
            {
                NewReport firstSelected = (NewReport)selectedList[0];
                Report_BL.DataCollection.ParamentrsCollection.AddNewItem(firstSelected);

                Report_BL.Controller.GetDeals.GetDeals.Get(firstSelected);

                //Формируем таблицу прибыли по месяцам
                Report_BL.Controller.Tables.Table.CreateProfiTable();

                // Формируем таблицу максимального кол-ва колен за месяц
                Report_BL.Controller.Tables.Table.CreateMaxOrdersGridTable();

                // Формируем главную таблицу
                Report_BL.Controller.Tables.Table.CreateMainTable(firstSelected);
            }
        }
        
        // При нажатии кнопки Фильтр - идем сюда и очищаем коллекции
        private void Filter(object sender, RoutedEventArgs e)
        {
            // Очищаем таблицы
            Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable.Clear();
            Report_BL.DataCollection.ProfitTableCollection.profitTable.Clear();
            Report_BL.DataCollection.MainTable.mainTable.Clear();
            WPF_NET6.Filter filter = new();
            filter.Show();
            filter.Owner = this;
            // Это событие будет выполнятся при нажатии кнопки в окне фильтра
            filter.CountFilterName.Click += CountFilterName_Click;
            
        }

        // При нажатии кнопки в окне фильтра будет выполняться этот код
        private void CountFilterName_Click(object sender, RoutedEventArgs e)
        {
            var filter = this.OwnedWindows[0];


            // Коллекция куда добавляем элементы не подходящие под условия фильтра
            var tempGridColliction = new List<Report_BL.ReportModel.TreeViewClass>();

            foreach(var grid in Report_BL.DataCollection.TreeCollection.grid)
            {
                
                var hourOpen = grid.Orders[0].OpenDate.Hour;
                var mass = Report_BL.DataCollection.HourFilter.hourFilter.hourFilterMassive();
                if(mass[hourOpen] == false)
                {
                        tempGridColliction.Add(grid);
                }
            }
            
            foreach(var grid in tempGridColliction)
                Report_BL.DataCollection.TreeCollection.grid.Remove(grid);

            //Формируем таблицу прибыли по месяцам
            Report_BL.Controller.Tables.Table.CreateProfiTable();

            // Формируем таблицу максимального кол-ва колен за месяц
            Report_BL.Controller.Tables.Table.CreateMaxOrdersGridTable();

            var selectedList = listBox_.SelectedItems;// список выбранных отчетов
            if (selectedList.Count != 0) // проверка если удалили последний объект
            {
                NewReport firstSelected = (NewReport)selectedList[0];
                Report_BL.Controller.Tables.Table.CreateMainTable(firstSelected);
            }

            filter.Close();
            
            //throw new NotImplementedException();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public event EventHandler getSelectedReport;

        private NewReport Button_Click()
        {
            if (getSelectedReport != null)
            {
                var selectedList = listBox_.SelectedItems;// список выбранных отчетов
                if (selectedList.Count != 0) // проверка если удалили последний объект
                {
                    var f = selectedList[0];
                    return (NewReport)f;
                }
                NewReport firstSelected = (NewReport)selectedList[0];
            }
            return new NewReport();
        }

        

    }
}
