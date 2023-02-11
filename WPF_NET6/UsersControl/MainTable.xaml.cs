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
    /// Логика взаимодействия для MainTable.xaml
    /// </summary>
    public partial class MainTable : UserControl
    {
        public MainTable()
        {
            InitializeComponent();

            number_.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            GridCountSell.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            GridCountBuy.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            GridCountAll.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            SumLotSell.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            SumLotBuy.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            SumLotAll.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            TotalProfitSell.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            TotalProfitBuy.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            TotalProfitAll.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            AverageProfitSell.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            AverageProfitBuy.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            AverageProfitAll.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            PersentOfTotalProfitSell.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            PersentOfTotalProfitBuy.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            PersentOfTotalProfitAll.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            MaxGridSizeSell.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            MaxGridSizeBuy.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            AverageGridSizeSell.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            AverageGridSizeBuy.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            AverageGridSizeAll.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            MaxPointsToTP_Sell.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            MaxPointsToTP_Buy.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            AveragePointsToTP_Sell.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            AveragePointsToTP_Buy.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            AveragePointsToTP_All.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            MaxTimeLifeGrid_Sell_str.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            MaxTimeLifeGrid_Buy_str.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            AverageLifeGrid_Sell_str.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            AverageLifeGrid_Buy_str.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
            AverageLifeGrid_All_str.ItemsSource = Report_BL.DataCollection.MainTable.mainTable;
        }
    }
}
