using Report_BL.ReportModel;
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
    /// Логика взаимодействия для SelectSymbolMagic.xaml
    /// </summary>
    public partial class SelectSymbolMagic : Window
    {
        Report_BL.ReportModel.FirstInfo firstInfo1 = null;

        /// <summary>
        /// Всплывающее окно выбора символа межджика и периода
        /// </summary>
        /// <param name="firstInfo"> Объект с первичной информацией </param>
        public SelectSymbolMagic(Report_BL.ReportModel.FirstInfo firstInfo)
        {
            InitializeComponent();

            this.firstInfo1 = firstInfo;

            #region Добовляем все символы
            string sym = String.Empty;
            foreach (var item in firstInfo.DicSymbolMagic)
            {
                sym = item.Key;
                symbol.Items.Add(sym);
            }
            // фокус на первом символе из списка
            symbol.SelectedIndex = 0;
            #endregion
            
            // #region Добавляем все меджики для символа sym
                // foreach(var item in firstInfo.DicSymbolMagic[symbol.SelectedValue.ToString()])
                // {
                //     magic.Items.Add(item);
                // }
                // Фокус на первом меджике
                magic.SelectedIndex = 0;
            // #endregion
        }

        // Выбрали новый символ
        private void SymbolChanged(object sender, SelectionChangedEventArgs e)
        {
            // Очищаем список меджиков
            magic.Items.Clear();

            // Добавляем меджики для этого символа
            foreach (var item in firstInfo1.DicSymbolMagic[symbol.SelectedItem.ToString()])
                magic.Items.Add(item);
            
            // Делаем фокус на первом меджике
            magic.SelectedIndex = 0;

        }

        private void ClickButton(object sender, RoutedEventArgs e)
        {
            string? sym = symbol.SelectedItem.ToString();
            string? mag_str = magic.SelectedItem.ToString();

            if(mag_str != null && sym != null)
            {
                int mag = int.Parse(mag_str);

                NewReport rep = new NewReport
                (
                    firstInfo1.FilePath,
                    firstInfo1.ReportType,
                    sym,
                    mag,
                    firstInfo1.StartDate,
                    firstInfo1.EndDate,
                    firstInfo1.StartDeposit
                );
                Report_BL.DataCollection.ReportCollection.newReport.Add(rep);
                // Закрываем окно
                this.Close();
            }
            else MessageBox.Show("Значение депозита должен быть целым числом!");



                

                
            
        }
    }
}
