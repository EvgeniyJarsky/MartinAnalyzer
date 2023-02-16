using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WPF_NET6
{
    /// <summary>
    /// Логика взаимодействия для Filter.xaml
    /// </summary>
    public partial class Filter : Window
    {
        public Filter()
        {
            InitializeComponent();
        }

        private void CountFilter(object sender, RoutedEventArgs e)
        {
            #region Записываем значения checkBox для каждого часа
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_1 = hour_1?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_2 = hour_2?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_3 = hour_3?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_4 = hour_4?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_5 = hour_5?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_6 = hour_6?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_7 = hour_7?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_8 = hour_8?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_9 = hour_9?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_10 = hour_10?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_11 = hour_11?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_12 = hour_12?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_13 = hour_13?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_14 = hour_14?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_15 = hour_15?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_16 = hour_16?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_17 = hour_17?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_18 = hour_18?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_19 = hour_19?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_20 = hour_20?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_21 = hour_21?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_22 = hour_22?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_23 = hour_23?.IsChecked ?? true;
            Report_BL.DataCollection.HourFilter.hourFilter.Hour_24 = hour_24?.IsChecked ?? true;
            #endregion
            
            var tempGridColliction = new List<Report_BL.ReportModel.TreeViewClass>();


            foreach(var grid in Report_BL.DataCollection.TreeCollection.grid)
            {
                foreach(var order in grid.Orders)
                {
                    var mass = Report_BL.DataCollection.HourFilter.hourFilter.hourFilterMassive();
                    if(Report_BL.DataCollection.HourFilter.hourFilter.hourFilterMassive().Any(fig => fig == (sbyte)order.OpenDate.Hour))
                    {
                        break;
                    } 
                    else
                    {
                        // Report_BL.DataCollection.TreeCollection.grid.Remove(grid);
                        tempGridColliction.Add(grid);
                        break;
                    }
                }
            }
            
            foreach(var grid in tempGridColliction)
                Report_BL.DataCollection.TreeCollection.grid.Remove(grid);

            //Формируем таблицу прибыли по месяцам
            Report_BL.Controller.Tables.Table.CreateProfiTable();

            // Формируем таблицу максимального кол-ва колен за месяц
            Report_BL.Controller.Tables.Table.CreateMaxOrdersGridTable();

            // var selectedList = WPF_NET6.MainWindow.listBox_.SelectedItems;

            this.Close();
        }
    }
}
