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
        ObservableCollection<Report_BL.ReportModel.TreeViewClass> grid =
            new ObservableCollection<Report_BL.ReportModel.TreeViewClass>();

        Report_BL.ReportModel.TreeViewClass ddddf = new Report_BL.ReportModel.TreeViewClass();
        
        
        public UC_TreeTab()
        {
            InitializeComponent();
            ddddf.Sell_Buy = "buy";
            grid.Add(ddddf);

            TreeViewer.ItemsSource = grid;
        }
    }
}
