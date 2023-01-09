using Report_BL.ReportModel;
using System;
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

namespace WPF_NET6.UsersControl
{
    /// <summary>
    /// Логика взаимодействия для UC_TreeTab.xaml
    /// </summary>
    public partial class UC_TreeTab : UserControl
    {
        ObservableCollection<Report_BL.ReportModel.TreeViewClass> grid1 =
            new ObservableCollection<Report_BL.ReportModel.TreeViewClass>();

        Report_BL.ReportModel.TreeViewClass ddddf = new Report_BL.ReportModel.TreeViewClass();
        
        
        public UC_TreeTab()
        {
            InitializeComponent();

            ddddf.Sell_Buy = "buy";
            ddddf.Profit = 10000;
            ddddf.Symbol = "EURUSD";
            ddddf.Orders.Add(new Order { Lot = 0.01, Profit = 456 });
            ddddf.Orders.Add(new Order { Lot = 0.02, Profit = 1100 });
            ddddf.Orders.Add(new Order { Lot = 0.03, Profit = 200 });

            grid1.Add(ddddf);

            TreeViewer.ItemsSource = Report_BL.DataCollection.TreeCollection.grid;
        }
    }
}
