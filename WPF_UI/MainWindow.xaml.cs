using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkWithFiles.LoadFile;

using Report_BL.ReportModel;

namespace WPF_UI
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

            //listBox_.ItemsSource = report;
            info.ItemsSource = param;
            deals.ItemsSource = dealsCollection;
        }

        /// <summary>
        /// Кнопка открыть файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void DeleteItem(object sender, EventArgs e)
        {
            Report_BL.DataCollection.ClearAllData.ClearParamAndDeals();
            // получим список выбранных отчетов
            var selectedList = listBox_.SelectedItems;
            
            // если отчетов выбрано несколько то удалим первый
            //! todo надо убрать ввозможность выбирать несколько отчетов
            if (selectedList.Count != 0)
            {
                var firstSelected = ((Report)selectedList[0]);
                report.Remove(firstSelected);
            }
            
        }

        private void DeleteAll(object sender, EventArgs e)
        {
            Report_BL.DataCollection.ClearAllData.ClearAll();
        }

        private void ChangeSelectedListBox(object sender, SelectionChangedEventArgs e)
        {
            Report_BL.DataCollection.ClearAllData.ClearParamAndDeals();

            var selectedList = listBox_.SelectedItems;// список выбранных отчетов
            if (selectedList.Count != 0) // todo излишняя проверка!!! наверное
            {
                NewReport firstSelected = (NewReport)selectedList[0];
                Report_BL.DataCollection.ParamentrsCollection.AddNewItem(firstSelected);

                //******************************************************
                /*
                Report firstSelected = ((Report)selectedList[0]);

                Report_BL.DataCollection.ParamentrsCollection.AddNewItem(firstSelected);

                // todo заполняем таблицу сделок
                Report_BL.Controller.GetDeals.GetDeals.Get(firstSelected);
                */
                //*********************************************************
            }
        }
    }
}
