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
        public UC_TreeTab()
        {
            InitializeComponent();
            TreeViewer.ItemsSource = Report_BL.DataCollection.TreeCollection.grid;
        }

        /// <summary>
        /// Кнопка нажатия контекстного меню в дереве сеток
        /// </summary>
        private void AnalisGrid(object sender, RoutedEventArgs e)
        {
            #region Определим кол-во знаков после запятой
                // Так как не знаю как получить значение кол-во цифр после запятой
                // из выбранного отчета => пройдемся по дереву сеток и найдем 
                // максимально кол-во цифр после запятой за небольшое кол-во итераций
                // всю сетку проходить нет смысла
                int count = 0;
                int maxIter = 10;
                int digits = 0;
                foreach(var grid_ in Report_BL.DataCollection.TreeCollection.grid)
                {
                    foreach(var order in grid_.Orders)
                    {
                        int dig = Report_BL.Controller.GetDeals.CountDigits.Count((float)order.OpenPrice);
                        digits = Math.Max(dig, digits);
                        dig = Report_BL.Controller.GetDeals.CountDigits.Count((float)order.ClosePrice);
                        digits = Math.Max(dig, digits);
                        count++;
                    }
                    if (count > maxIter) break;
                }
                int mult;
                switch(digits)
                {
                    case 3:
                        mult = 1000;
                        break;
                    case 4:
                        mult = 10000;
                        break;
                    default:
                        mult = 100000;
                        break;
                }
            #endregion

            #region Проверка если выбрали не сетку а ордер
                var selectedItem = TreeViewer.SelectedItem;
                Report_BL.ReportModel.TreeViewClass grid;
                if(selectedItem is Report_BL.ReportModel.TreeViewClass)
                {
                    grid =  (Report_BL.ReportModel.TreeViewClass)TreeViewer.SelectedItem;
                }
                else 
                {
                    MessageBox.Show("Нужно выбрать сетку");
                    return;
                }
            #endregion

            #region Если не выбрали сетку для анализа
                if (grid == null)
                {
                    MessageBox.Show("Выберите сетку для анализа");
                    return;
                }
            #endregion
            
            count = 0;
            double lastPrice = 0;
            double firstPrice = 0;
            double sumLot = 0;
            double deposit = 0;
            foreach(var order in grid.Orders)
            {
                count++;
                var newLine = new Report_BL.ReportModel.AnaliseGrid();
                newLine.OrderNumber = count;
                newLine.Lot = (float)order.Lot;
                sumLot += order.Lot;
                newLine.SumLot = (float)Math.Round(sumLot,2);

                

                #region Текщая длина сетки
                    if(firstPrice == 0)
                    {
                        newLine.GridLenght = 0;
                        firstPrice = order.OpenPrice;
                    }
                    else
                    {
                        newLine.GridLenght = Convert.ToInt32(Math.Abs((order.OpenPrice-firstPrice)*mult));
                    }
                #endregion

                #region Определим шаг текушего ордера
                    if(lastPrice == 0) 
                    {
                        newLine.Step = 0;
                        lastPrice = order.OpenPrice;
                    }
                    else
                    {
                        newLine.Step = Convert.ToInt32(Math.Abs((order.OpenPrice-lastPrice)*mult));
                        lastPrice = order.OpenPrice;
                    }
                #endregion


                Report_BL.DataCollection.AnaliseGridCollection.analiseDealsCollection.Add(newLine);
                
            }
            
            GridAnalise gridAnalise = new GridAnalise();
            gridAnalise.Show();

            gridAnalise.SymbolName.Content = grid.Symbol;

        }
    }
}
