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
                orderCount.may.FontSize = e.NewValue;
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
        }
    }
}
