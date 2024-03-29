﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_NET6.UsersControl
{
    /// <summary>
    /// Логика взаимодействия для ProfitTable.xaml
    /// </summary>
    public partial class ProfitTable : UserControl
    {
        public ProfitTable()
        {
            InitializeComponent();

            year_.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
            january.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
            february.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
            march.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
            april.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
            may.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
            june.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
            july.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
            august.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
            september.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
            october.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
            november.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
            december.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
            averageProfit.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
            sumProfit.ItemsSource = Report_BL.DataCollection.ProfitTableCollection.profitTable;
        }
    }
}
