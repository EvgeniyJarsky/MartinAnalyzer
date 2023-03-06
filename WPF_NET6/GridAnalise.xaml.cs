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
    /// Логика взаимодействия для GridAnalise.xaml
    /// </summary>
    public partial class GridAnalise : Window
    {
        public GridAnalise()
        {
            InitializeComponent();

            this.Closed += GridAnalise_Closed;
        }

        private void AnaliseGridButton(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(MoneyFor1Lot.Text, out int moneyFor1Lot))
            {
                MessageBox.Show("Не верное значение залога.");
                return;
            }
            if(!int.TryParse(PointPrice.Text, out int pointPrice))
            {
                MessageBox.Show("Не верное значение цены пункта.");
                return;
            }
            
            foreach(var order in Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection)
            {
                
                order.Margin = (float)Math.Round(order.SumLot*moneyFor1Lot,2);
                order.PointPrice = (float)Math.Round(order.SumLot*pointPrice,2);
            }
            //TODO Надо доводить до ума
            #region Не обновляется коллекция в таблице => пришлось делать костыль
                var newCollection = new List<Report_BL.ReportModel.AnaliseGrid>();
                foreach(var line_ in Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection)
                    newCollection.Add(line_);
                Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection.Clear();
                foreach(var line_ in newCollection)
                    Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection.Add(line_);
            #endregion
            
        }
        private void GridAnalise_Closed(object sender, EventArgs e)
        {
            Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection.Clear();
        }
    }
}
