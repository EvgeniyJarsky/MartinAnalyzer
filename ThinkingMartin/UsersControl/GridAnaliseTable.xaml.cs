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
    /// Логика взаимодействия для GridAnaliseTable.xaml
    /// </summary>
    public partial class GridAnaliseTable : UserControl
    {
        public GridAnaliseTable()
        {
            InitializeComponent();

            var request = new Report_BL.Controller.MyWebRequest.GetRequest("https://scripts.tlap.com/quotes.php?q=AUDCAD");
            request.Run();
            var rez = request.Response;

            OrderNumber.ItemsSource = Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection;
            Lot.ItemsSource = Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection;
            Step.ItemsSource = Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection;
            GridLenght.ItemsSource = Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection;
            SumLot.ItemsSource = Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection;
            Margin.ItemsSource = Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection;
            PointPrice.ItemsSource = Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection;
            DrawDownMoney.ItemsSource = Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection;
            DrawDownProcent.ItemsSource = Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection;
            DrawDownMoneyAndMargin.ItemsSource = Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection;
        }
    }
}
