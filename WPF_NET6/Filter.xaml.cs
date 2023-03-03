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
        }

        private void SelectAllHours(object sender, RoutedEventArgs e)
        {
            if (hour_1 != null) hour_1.IsChecked = true;
            if (hour_2 != null) hour_2.IsChecked = true;
            if (hour_3 != null) hour_3.IsChecked = true;
            if (hour_4 != null) hour_4.IsChecked = true;
            if (hour_5 != null) hour_5.IsChecked = true;
            if (hour_6 != null) hour_6.IsChecked = true;
            if (hour_7 != null) hour_7.IsChecked = true;
            if (hour_8 != null) hour_8.IsChecked = true;
            if (hour_9 != null) hour_9.IsChecked = true;
            if (hour_10 != null) hour_10.IsChecked = true;
            if (hour_11 != null) hour_11.IsChecked = true;
            if (hour_12 != null) hour_12.IsChecked = true;
            if (hour_13 != null) hour_13.IsChecked = true;
            if (hour_14 != null) hour_14.IsChecked = true;
            if (hour_15 != null) hour_15.IsChecked = true;
            if (hour_16 != null) hour_16.IsChecked = true;
            if (hour_17 != null) hour_17.IsChecked = true;
            if (hour_18 != null) hour_18.IsChecked = true;
            if (hour_19 != null) hour_19.IsChecked = true;
            if (hour_20 != null) hour_20.IsChecked = true;
            if (hour_21 != null) hour_21.IsChecked = true;
            if (hour_22 != null) hour_22.IsChecked = true;
            if (hour_23 != null) hour_23.IsChecked = true;
            if (hour_24 != null) hour_24.IsChecked = true;
        }

        private void UnSelectAllHours(object sender, RoutedEventArgs e)
        {
            if (hour_1 != null) hour_1.IsChecked = false;
            if (hour_2 != null) hour_2.IsChecked = false;
            if (hour_3 != null) hour_3.IsChecked = false;
            if (hour_4 != null) hour_4.IsChecked = false;
            if (hour_5 != null) hour_5.IsChecked = false;
            if (hour_6 != null) hour_6.IsChecked = false;
            if (hour_7 != null) hour_7.IsChecked = false;
            if (hour_8 != null) hour_8.IsChecked = false;
            if (hour_9 != null) hour_9.IsChecked = false;
            if (hour_10 != null) hour_10.IsChecked = false;
            if (hour_11 != null) hour_11.IsChecked = false;
            if (hour_12 != null) hour_12.IsChecked = false;
            if (hour_13 != null) hour_13.IsChecked = false;
            if (hour_14 != null) hour_14.IsChecked = false;
            if (hour_15 != null) hour_15.IsChecked = false;
            if (hour_16 != null) hour_16.IsChecked = false;
            if (hour_17 != null) hour_17.IsChecked = false;
            if (hour_18 != null) hour_18.IsChecked = false;
            if (hour_19 != null) hour_19.IsChecked = false;
            if (hour_20 != null) hour_20.IsChecked = false;
            if (hour_21 != null) hour_21.IsChecked = false;
            if (hour_22 != null) hour_22.IsChecked = false;
            if (hour_23 != null) hour_23.IsChecked = false;
            if (hour_24 != null) hour_24.IsChecked = false;
        }

        private void SelectAllDays(object sender, RoutedEventArgs e)
        {
            if (mon != null) mon.IsChecked = true;
            if (tue != null) tue.IsChecked = true;
            if (wed != null) wed.IsChecked = true;
            if (thu != null) thu.IsChecked = true;
            if (fri != null) fri.IsChecked = true;
        }

        private void UnSelectAllDays(object sender, RoutedEventArgs e)
        {
            if (mon != null) mon.IsChecked = false;
            if (tue != null) tue.IsChecked = false;
            if (wed != null) wed.IsChecked = false;
            if (thu != null) thu.IsChecked = false;
            if (fri != null) fri.IsChecked = false;
        }
    }
}
