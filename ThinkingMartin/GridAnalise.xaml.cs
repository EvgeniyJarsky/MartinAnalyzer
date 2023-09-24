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
    /// Окно анализа сетки
    /// Логика взаимодействия для GridAnalise.xaml
    /// </summary>
    public partial class GridAnalise : Window
    {
        public GridAnalise()
        {
            InitializeComponent();

            this.Closed += GridAnalise_Closed;

            this.Activated += ActivatedWindow;
           
        }

        /// <summary>
        /// При активации окна определить валютную пару и привязать ее к SymbolName
        /// </summary>
        private void ActivatedWindow(object? sender, EventArgs e)
        {
            // Тут надо получить значение валютной пары и отбразить 
            SymbolName.Content = Report_BL.DataCollection.DealsCollection.dealsCollection[0].Symbol;

            //throw new NotImplementedException();
        }

        private void AnaliseGridButton(object sender, RoutedEventArgs e)
        {
            #region Data validation
                if(!float.TryParse(MoneyFor1Lot.Text.Replace('.',','), out float moneyFor1Lot))
                {
                    MessageBox.Show("Не верное значение залога.");
                    return;
                }
                if(!float.TryParse(PointPrice.Text.Replace('.',','), out float pointPrice))
                {
                    MessageBox.Show("Не верное значение цены пункта.");
                    return;
                }
                if(!float.TryParse(Commission.Text.Replace('.',','), out float commission))
                {
                    MessageBox.Show("Не верное значение комиссии.");
                    return;
                }
                if(!float.TryParse(Deposit.Text.Replace('.',','), out float deposit))
                {
                    MessageBox.Show("Не верное значение комиссии.");
                    return;
                }
            #endregion

            int countOrder = 0;
            float previousDrawDownMoney = 0;
            float previousSumLot = 0;

            foreach(var order in Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection)
            {
                order.Margin     = (float)Math.Round(order.SumLot*moneyFor1Lot,2);
                order.PointPrice = (float)Math.Round(order.SumLot*pointPrice,2);

                #region Считаем просадку
                    if(countOrder == 0) // если это первый ордер
                    {
                        if(float.TryParse(Commission.Text.Replace('.',','), out float comission))
                        {
                            order.DrawDownMoney = order.Lot * comission;

                            order.DrawDownProcent = (float)Math.Round(order.DrawDownMoney/float.Parse(Deposit.Text.Replace('.',',')));

                            order.DrawDownMoneyAndMargin = (float)Math.Round(order.DrawDownMoney + order.Margin, 2);
                            
                            previousDrawDownMoney = order.DrawDownMoney;
                            previousSumLot = order.SumLot;
                            countOrder++;
                        }
                        else
                        {
                            MessageBox.Show("Комиссия должна быть целым числом.");
                            return;
                        }
                    }
                    else
                    {
                        order.DrawDownMoney = (float)Math.Round(previousSumLot*(order.Step)*pointPrice + previousDrawDownMoney + order.Lot*commission, 2);
                        order.DrawDownProcent = (float)Math.Round((order.DrawDownMoney/float.Parse(Deposit.Text.Replace('.',',')))*100,2);
                        order.DrawDownMoneyAndMargin = (float)Math.Round(order.DrawDownMoney + order.Margin, 2);
                        previousDrawDownMoney = order.DrawDownMoney;
                        previousSumLot = order.SumLot;
                    }
                #endregion
                
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
