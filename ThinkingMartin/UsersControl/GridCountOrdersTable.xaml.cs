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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_NET6.UsersControl
{
    /// <summary>
    /// Логика взаимодействия для GridCountOrdersTable.xaml
    /// </summary>
    public partial class GridCountOrdersTable : UserControl
    {
        public GridCountOrdersTable()
        {
            InitializeComponent();

            year_.ItemsSource = Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable;
            january.ItemsSource = Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable;
            february.ItemsSource = Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable;
            march.ItemsSource = Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable;
            april.ItemsSource = Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable;
            may.ItemsSource = Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable;
            june.ItemsSource = Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable;
            july.ItemsSource = Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable;
            august.ItemsSource = Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable;
            september.ItemsSource = Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable;
            october.ItemsSource = Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable;
            november.ItemsSource = Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable;
            december.ItemsSource = Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable;
            averageCountOrders.ItemsSource = Report_BL.DataCollection.GridOrdersCountTableCollection.MaxOrdersTable;
        }
    }
}
