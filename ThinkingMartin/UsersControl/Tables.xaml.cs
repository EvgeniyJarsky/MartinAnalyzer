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
using static System.Net.Mime.MediaTypeNames;

namespace WPF_NET6.UsersControl
{
    /// <summary>
    /// Логика взаимодействия для Tables.xaml
    /// </summary>
    public partial class Tables : UserControl
    {
        public Tables()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // Масштаб шрифта таблиц
        private void SliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(prO != null)
            {
                prO.profitTable_Profit.FontSize = e.NewValue;
                prO.profitTable_Years.FontSize = e.NewValue;
                prO.profitTable_January.FontSize = e.NewValue;
                prO.profitTable_February.FontSize = e.NewValue;
                prO.profitTable_March.FontSize = e.NewValue;
                prO.profitTable_April.FontSize = e.NewValue;
                prO.profitTable_May.FontSize = e.NewValue;
                prO.profitTable_June.FontSize = e.NewValue;
                prO.profitTable_July.FontSize = e.NewValue;
                prO.profitTable_August.FontSize = e.NewValue;
                prO.profitTable_September.FontSize = e.NewValue;
                prO.profitTable_October.FontSize = e.NewValue;
                prO.profitTable_November.FontSize = e.NewValue;
                prO.profitTable_Desember.FontSize = e.NewValue;
                prO.profitTable_Average.FontSize = e.NewValue;
                prO.profitTable_Total.FontSize = e.NewValue;


                if (prO.year_ != null) prO.year_.FontSize = e.NewValue;
                if (prO.january != null) prO.january.FontSize = e.NewValue;
                if (prO.february != null) prO.february.FontSize = e.NewValue;
                if (prO.march != null) prO.march.FontSize = e.NewValue;
                if (prO.april != null) prO.april.FontSize = e.NewValue;
                if (prO.may != null) prO.may.FontSize = e.NewValue;
                if (prO.june != null) prO.june.FontSize = e.NewValue;
                if (prO.july != null) prO.july.FontSize = e.NewValue;
                if (prO.august != null) prO.august.FontSize = e.NewValue;
                if (prO.september != null) prO.september.FontSize = e.NewValue;
                if (prO.october != null) prO.october.FontSize = e.NewValue;
                if (prO.november != null) prO.november.FontSize = e.NewValue;
                if (prO.december != null) prO.december.FontSize = e.NewValue;
                
                if (prO.averageProfit != null) prO.averageProfit.FontSize = e.NewValue;
                if (prO.sumProfit != null) prO.sumProfit.FontSize = e.NewValue;
            }
        
            if(orderCount != null)
            {
                orderCount.orderCountTable_Year.FontSize = e.NewValue;
                orderCount.orderCountTable_Average.FontSize = e.NewValue;
                orderCount.orderCountTable_LongHead.FontSize = e.NewValue;
                orderCount.orderCountTable_January.FontSize = e.NewValue;
                orderCount.orderCountTable_February.FontSize = e.NewValue;
                orderCount.orderCountTable_March.FontSize = e.NewValue;
                orderCount.orderCountTable_April.FontSize = e.NewValue;
                orderCount.orderCountTable_May.FontSize = e.NewValue;
                orderCount.orderCountTable_June.FontSize = e.NewValue;
                orderCount.orderCountTable_July.FontSize = e.NewValue;
                orderCount.orderCountTable_August.FontSize = e.NewValue;
                orderCount.orderCountTable_September.FontSize = e.NewValue;
                orderCount.orderCountTable_October.FontSize = e.NewValue;
                orderCount.orderCountTable_November.FontSize = e.NewValue;
                orderCount.orderCountTable_December.FontSize = e.NewValue;

                if (orderCount.year_ != null) orderCount.year_.FontSize = e.NewValue;
                if (orderCount.orderCountTable_LongHead != null) orderCount.orderCountTable_LongHead.FontSize = e.NewValue;
                if (orderCount.january != null) orderCount.january.FontSize = e.NewValue;
                if (orderCount.february != null) orderCount.february.FontSize = e.NewValue;
                if (orderCount.march != null) orderCount.march.FontSize = e.NewValue;
                if (orderCount.april != null) orderCount.april.FontSize = e.NewValue;
                if (orderCount.may != null) orderCount.may.FontSize = e.NewValue;
                if (orderCount.june != null) orderCount.june.FontSize = e.NewValue;
                if (orderCount.july != null) orderCount.july.FontSize = e.NewValue;
                if (orderCount.august != null) orderCount.august.FontSize = e.NewValue;
                if (orderCount.september != null) orderCount.september.FontSize = e.NewValue;
                if (orderCount.october != null) orderCount.october.FontSize = e.NewValue;
                if (orderCount.november != null) orderCount.november.FontSize = e.NewValue;
                if (orderCount.december != null) orderCount.december.FontSize = e.NewValue;
                if (orderCount.averageCountOrders != null) orderCount.averageCountOrders.FontSize = e.NewValue;
            }

            if(mainTable != null)
            {
                mainTable.mainTable_NN.FontSize = e.NewValue;

                mainTable.mainTable_CountGrid.FontSize = e.NewValue;
                mainTable.mainTable_CountGridSell.FontSize = e.NewValue;
                mainTable.mainTable_CountGridBuy.FontSize = e.NewValue;
                mainTable.mainTable_CountGridAll.FontSize = e.NewValue;

                mainTable.mainTable_SumLot1.FontSize = e.NewValue;
                mainTable.mainTable_SumLot2.FontSize = e.NewValue;
                mainTable.mainTable_SumLotSell.FontSize = e.NewValue;
                mainTable.mainTable_SumLotBuy.FontSize = e.NewValue;
                mainTable.mainTable_SumLotAll.FontSize = e.NewValue;
                
                mainTable.mainTable_TotalProfit1.FontSize = e.NewValue;
                mainTable.mainTable_TotalProfit2.FontSize = e.NewValue;
                mainTable.mainTable_TotalProfitSell.FontSize = e.NewValue;
                mainTable.mainTable_TotalProfitBuy.FontSize = e.NewValue;
                mainTable.mainTable_TotalProfitAll.FontSize = e.NewValue;

                mainTable.mainTable_AverageProfit1.FontSize = e.NewValue;
                mainTable.mainTable_AverageProfit2.FontSize = e.NewValue;
                mainTable.mainTable_AverageProfitSell.FontSize = e.NewValue;
                mainTable.mainTable_AverageProfitBuy.FontSize = e.NewValue;
                mainTable.mainTable_AverageProfitAll.FontSize = e.NewValue;

                mainTable.mainTable_PercentProfit1.FontSize = e.NewValue;
                mainTable.mainTable_PercentProfit2.FontSize = e.NewValue;
                mainTable.mainTable_PercentProfitSell.FontSize = e.NewValue;
                mainTable.mainTable_PercentProfitBuy.FontSize = e.NewValue;
                mainTable.mainTable_PercentProfitAll.FontSize = e.NewValue;

                mainTable.mainTable_MaxGridSize1.FontSize = e.NewValue;
                mainTable.mainTable_MaxGridSize2.FontSize = e.NewValue;
                mainTable.mainTable_MaxGridSizeSell.FontSize = e.NewValue;
                mainTable.mainTable_MaxGridSizeBuy.FontSize = e.NewValue;

                mainTable.mainTable_AverageGridSize1.FontSize = e.NewValue;
                mainTable.mainTable_AverageGridSize2.FontSize = e.NewValue;
                mainTable.mainTable_AverageGridSizeSell.FontSize = e.NewValue;
                mainTable.mainTable_AverageGridSizeBuy.FontSize = e.NewValue;
                mainTable.mainTable_AverageGridSizeAll.FontSize = e.NewValue;

                mainTable.mainTable_MaxPointsToTP1.FontSize = e.NewValue;
                mainTable.mainTable_MaxPointsToTP2.FontSize = e.NewValue;
                mainTable.mainTable_MaxPointsToTPSell.FontSize = e.NewValue;
                mainTable.mainTable_MaxPointsToTPBuy.FontSize = e.NewValue;

                mainTable.mainTable_AveragePointsToTP1.FontSize = e.NewValue;
                mainTable.mainTable_AveragePointsToTP2.FontSize = e.NewValue;
                mainTable.mainTable_AveragePointsToTPSell.FontSize = e.NewValue;
                mainTable.mainTable_AveragePointsToTPBuy.FontSize = e.NewValue;
                mainTable.mainTable_AveragePointsToTPAll.FontSize = e.NewValue;

                mainTable.mainTable_MaxLifeGrid1.FontSize = e.NewValue;
                mainTable.mainTable_MaxLifeGrid2.FontSize = e.NewValue;
                mainTable.mainTable_MaxLifeGridSell.FontSize = e.NewValue;
                mainTable.mainTable_MaxLifeGridBuy.FontSize = e.NewValue;

                mainTable.mainTable_AverageLifeGrid1.FontSize = e.NewValue;
                mainTable.mainTable_AverageLifeGrid2.FontSize = e.NewValue;
                mainTable.mainTable_AverageLifeGridSell.FontSize = e.NewValue;
                mainTable.mainTable_AverageLifeGridBuy.FontSize = e.NewValue;
                mainTable.mainTable_AverageLifeGridAll.FontSize = e.NewValue;

                if (mainTable.number_ != null) mainTable.number_.FontSize = e.NewValue;

                if (mainTable.GridCountSell != null) mainTable.GridCountSell.FontSize = e.NewValue;
                if (mainTable.GridCountBuy != null) mainTable.GridCountBuy.FontSize = e.NewValue;
                if (mainTable.GridCountAll != null) mainTable.GridCountAll.FontSize = e.NewValue;

                if (mainTable.SumLotSell != null) mainTable.SumLotSell.FontSize = e.NewValue;
                if (mainTable.SumLotBuy != null) mainTable.SumLotBuy.FontSize = e.NewValue;
                if (mainTable.SumLotAll != null) mainTable.SumLotAll.FontSize = e.NewValue;

                if (mainTable.TotalProfitSell != null) mainTable.TotalProfitSell.FontSize = e.NewValue;
                if (mainTable.TotalProfitBuy != null) mainTable.TotalProfitBuy.FontSize = e.NewValue;
                if (mainTable.TotalProfitAll != null) mainTable.TotalProfitAll.FontSize = e.NewValue;

                if (mainTable.AverageProfitSell != null) mainTable.AverageProfitSell.FontSize = e.NewValue;
                if (mainTable.AverageProfitBuy != null) mainTable.AverageProfitBuy.FontSize = e.NewValue;
                if (mainTable.AverageProfitAll != null) mainTable.AverageProfitAll.FontSize = e.NewValue;

                if (mainTable.PersentOfTotalProfitSell != null) mainTable.PersentOfTotalProfitSell.FontSize = e.NewValue;
                if (mainTable.PersentOfTotalProfitBuy != null) mainTable.PersentOfTotalProfitBuy.FontSize = e.NewValue;
                if (mainTable.PersentOfTotalProfitAll != null) mainTable.PersentOfTotalProfitAll.FontSize = e.NewValue;

                if (mainTable.MaxGridSizeSell != null) mainTable.MaxGridSizeSell.FontSize = e.NewValue;
                if (mainTable.MaxGridSizeBuy != null) mainTable.MaxGridSizeBuy.FontSize = e.NewValue;
                
                if (mainTable.AverageGridSizeSell != null) mainTable.AverageGridSizeSell.FontSize = e.NewValue;
                if (mainTable.AverageGridSizeBuy != null) mainTable.AverageGridSizeBuy.FontSize = e.NewValue;
                if (mainTable.AverageGridSizeAll != null) mainTable.AverageGridSizeAll.FontSize = e.NewValue;
                
                if (mainTable.MaxPointsToTP_Sell != null) mainTable.MaxPointsToTP_Sell.FontSize = e.NewValue;
                if (mainTable.MaxPointsToTP_Buy != null) mainTable.MaxPointsToTP_Buy.FontSize = e.NewValue;

                if (mainTable.AveragePointsToTP_Sell != null) mainTable.AveragePointsToTP_Sell.FontSize = e.NewValue;
                if (mainTable.AveragePointsToTP_Buy != null) mainTable.AveragePointsToTP_Buy.FontSize = e.NewValue;
                if (mainTable.AveragePointsToTP_All != null) mainTable.AveragePointsToTP_All.FontSize = e.NewValue;

                if (mainTable.MaxTimeLifeGrid_Sell_str != null) mainTable.MaxTimeLifeGrid_Sell_str.FontSize = e.NewValue;
                if (mainTable.MaxTimeLifeGrid_Buy_str != null) mainTable.MaxTimeLifeGrid_Buy_str.FontSize = e.NewValue;

                if (mainTable.AverageLifeGrid_Sell_str != null) mainTable.AverageLifeGrid_Sell_str.FontSize = e.NewValue;
                if (mainTable.AverageLifeGrid_Buy_str != null) mainTable.AverageLifeGrid_Buy_str.FontSize = e.NewValue;
                if (mainTable.AverageLifeGrid_All_str != null) mainTable.AverageLifeGrid_All_str.FontSize = e.NewValue;


            }
        }
    }
}
