using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;


using WorkWithFiles.LoadFile;

using Report_BL.ReportModel;

using System.Data.SQLite;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading;

namespace WPF_NET6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static object _lock = new Object(); // для асинхронного изменения коллекций

        public MainWindow()
        {
            InitializeComponent();

            listBox_.ItemsSource = Report_BL.DataCollection.ReportCollection.newReport;
            info.ItemsSource = Report_BL.DataCollection.ParamentrsCollection.param;
        }


        private void ShowWindow2()
        {
            ThinkingMartin.Progress sd = new();
            sd.ShowDialog();
            System.Windows.Threading.Dispatcher.Run();
        }

        async private Task TaskAsyncFunc(NewReport report)
        {
            // progressWindow.ShowDialog();
            await Task.Run(()=> ClearFunc());
                
            await Task.Run(()=>Report_BL.DataCollection.ParamentrsCollection.AddNewItem(report));

            await Task.Run(()=>Report_BL.Controller.GetDeals.GetDeals.Get(report));

            await Task.Run(()=>Report_BL.Controller.Tables.Table.CreateProfiTable());

            await Task.Run(()=>Report_BL.Controller.Tables.Table.CreateMaxOrdersGridTable());

            await Task.Run(()=>Report_BL.Controller.Tables.Table.CreateMainTable(report));

        }

        private void ClearFunc()
        {
                // Report_BL.DataCollection.TreeCollection.grid.Clear();
                
                // Report_BL.DataCollection.ClearAllData.ClearParamAndDeals();
    
                Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable.Clear();
                Report_BL.DataCollection.ProfitTableCollection.profitTable.Clear();
                Report_BL.DataCollection.MainTable.mainTable.Clear();

                // graphImage.Source = null;
                Report_BL.DataCollection.ParamentrsCollection.param.Clear();
                Report_BL.DataCollection.TreeCollection.grid.Clear();
                Report_BL.DataCollection.DealsCollection.dealsCollection.Clear();
        }

        /// <summary>
        /// Кнопка открыть файл
        /// </summary>
        private void Btn_LoadFile(object sender, RoutedEventArgs e)
        {
            
            
            #region Очишаем данные
            Report_BL.DataCollection.ClearAllData.ClearParamAndDeals();
            Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable.Clear();
            Report_BL.DataCollection.ProfitTableCollection.profitTable.Clear();
            Report_BL.DataCollection.MainTable.mainTable.Clear();

            graphImage.Source = null;
            #endregion
            


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
            graphImage.Source = null;

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

            graphImage.Source = null;
        }

        // При изменении выбранного отчета
        private async void ChangeSelectedListBox(object sender, SelectionChangedEventArgs e)
        {
            #region Сделаем коллекции работающими с разными потоками
                System.Windows.Data.BindingOperations.EnableCollectionSynchronization
                (
                    Report_BL.DataCollection.ParamentrsCollection.param, _lock
                );
                System.Windows.Data.BindingOperations.EnableCollectionSynchronization
                (
                    Report_BL.DataCollection.TreeCollection.grid, _lock
                );
                System.Windows.Data.BindingOperations.EnableCollectionSynchronization
                (
                    Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable, _lock
                );
                System.Windows.Data.BindingOperations.EnableCollectionSynchronization
                (
                    Report_BL.DataCollection.ProfitTableCollection.profitTable, _lock
                );
                System.Windows.Data.BindingOperations.EnableCollectionSynchronization
                (
                    Report_BL.DataCollection.MainTable.mainTable, _lock
                );
                System.Windows.Data.BindingOperations.EnableCollectionSynchronization
                (
                    Report_BL.DataCollection.MainTable.mainTable, _lock
                );
                System.Windows.Data.BindingOperations.EnableCollectionSynchronization
                (
                    Report_BL.DataCollection.DealsCollection.dealsCollection, _lock
                );
            #endregion
            
            var selectedReport = listBox_.SelectedItems;// список выбранных отчетов

            NewReport report = null;

            if (selectedReport.Count != 0) // проверка если удалили последний объект
            {
                if (selectedReport[0] is NewReport && selectedReport[0] !=null)
                {
                    report = (NewReport)selectedReport[0];
                }
            }

            // Путь к графику
            if(report != null)
            {
                string fileName_ = Path.GetFileNameWithoutExtension(report.FilePath) + ".gif";
                string pathToImage_ = Path.GetDirectoryName(report.FilePath)+ "\\" + fileName_;
                
                if(File.Exists(pathToImage_))
                {
                    graphImage.Source = new BitmapImage(new Uri(pathToImage_, UriKind.Absolute));
                }

                ThinkingMartin.Progress progressWindow = new();
                progressWindow.Show();
                progressWindow.Owner = this;

                // await TaskAsyncFunc(report);
                var waitW = TaskAsyncFunc(report);
                // waitW.Wait();
                var f = waitW.AsyncState;
                await waitW;
                if(waitW.IsCompleted)
                    progressWindow.Close();
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
            WPF_NET6.Filter filter = (Filter)this.OwnedWindows[0];

            // Равно/больше/меньше/не важно - берется из окна фильтра для длины сетки
            string? ComboGridLenght = filter.ComboGridLenght.SelectionBoxItem.ToString();
            // Длина сетки - берется из окна фильтра
            string GridLenghtText = filter.GridLenghtText.Text;
            // Равно/больше/меньше/не важно - берется из окна фильтра для кол-ва колен сетки
            string? ComboOrdersInGrid = filter.ComboOrdersInGrid.SelectionBoxItem.ToString();
            // Кол-во колен сетки - берется из окна фильтра
            string OrdersInGridText = filter.OrdersInGridText.Text;

            var mass = Report_BL.DataCollection.HourFilter.hourFilter.hourFilterMassive();
            var dayMass = Report_BL.DataCollection.DaysFilter.dayFilter.daysFilterMassive();

            // Коллекция куда добавляем элементы не подходящие под условия фильтра
            var tempGridColliction = new List<Report_BL.ReportModel.TreeViewClass>();

            foreach(var grid in Report_BL.DataCollection.TreeCollection.grid)
            {
                if(GridFilterLogic(ComboGridLenght,GridLenghtText, grid.GridLenght) == false)
                {
                    tempGridColliction.Add(grid);
                    continue;
                }

                if(GridFilterLogic(ComboOrdersInGrid,OrdersInGridText, grid.CountOrders) == false)
                {
                    tempGridColliction.Add(grid);
                    continue;
                }

                #region Если час начала сетки hourOpen равен FALSE => отмечаем эту сетку на удаление
                    var hourOpen = grid.Orders[0].OpenDate.Hour;
                    if(mass[hourOpen] == false)
                    {
                            tempGridColliction.Add(grid);
                            continue;
                    }
                #endregion

                #region Если день недели начала сетки hourOpen равен FALSE => отмечаем эту сетку на удаление
                    var startGridDay = (int)grid.Orders[0].OpenDate.DayOfWeek;
                    if(dayMass[startGridDay] == false)
                        tempGridColliction.Add(grid);
                #endregion
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
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ContextMenuAnaliseGrid(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("sdfsd");
            GridAnalise gridAnalise = new GridAnalise();
            gridAnalise.Owner = this;
            gridAnalise.ShowDialog();
            // gridAnalise.Show();

            

        }

        private bool GridFilterLogic(string mathValue, string textValue, int valueToCompare)
        {
            if(mathValue == null) return false;
            // Если выбрано не важно  то сразу TRUE
            if(mathValue == "не важно") return true;

            //? Если неудачно парсим введенное число вовращаем всегда FALSE => пустые данные
            if(!int.TryParse(textValue, out int intValue))
            {
                MessageBox.Show("Введено не верное значение. Должно быть целое число.");
                return false;
                throw new Exception();
            }   
            if(mathValue == "больше")
            {
                // bool compareDigits = valueToCompare > intValue;
                switch (valueToCompare > intValue)
                {
                    case true:
                        return true;
                    case false:
                        return false;
                } 
            } 
            else if(mathValue == "меньше")
            {
                switch (valueToCompare < intValue)
                {
                    case true:
                        return true;
                    case false:
                        return false;
                } 
            }
            else if(mathValue == "равно")
            {
                switch (valueToCompare == intValue)
                {
                    case true:
                        return true;
                    case false:
                        return false;
                } 
            }
            return false; // Прочие случаи возвращаем FALSE => пустые таблицы 
            
        }

        public int Digits()
        {
            var selectedList = listBox_.SelectedItems;// список выбранных отчетов

            if (selectedList.Count != 0) // проверка если удалили последний объект
            {
                NewReport firstSelected = (NewReport)selectedList[0];
                return firstSelected.Digits;
            }
            else throw new Exception("Похоже не выбран отчет...");
        }
    }
}
