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

namespace WPF_UI
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
            #endregion

            // фокус на первом символе из списка
            symbol.SelectedIndex = 0;


            // Добавляем все меджики для символа sym
            foreach(var item in firstInfo.DicSymbolMagic[sym])
            {
                magic.Items.Add(item);
            }

            // Фокус на первом меджике
            magic.SelectedIndex = 0;

            #region Определяем даты начала и конца теста
            StartDate.DisplayDate = firstInfo.StartDate;
            StartDate.SelectedDate = firstInfo.StartDate;
            EndDate.DisplayDate = firstInfo.EndDate;
            EndDate.SelectedDate = firstInfo.EndDate;
            #endregion

            #region Выводим депозит
            Deposit.Text = firstInfo.StartDeposit.ToString();
            #endregion
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
            string sym = symbol.SelectedItem.ToString();
            string mag = magic.SelectedItem.ToString();
            DateTime start = StartDate.DisplayDate;
            DateTime end = EndDate.DisplayDate;

            // Проверим что в окне Депозит введено целое число
            // TODO Можно вынести в отдельную функцию 
            if (Deposit.Text.All(char.IsDigit))
            {
                if (!int.TryParse(Deposit.Text, out int depo))
                {
                    MessageBox.Show("Депозит должен быть целым числом!");
                }
                else
                {
                    // TODO тут нужно создать или поменять first info и
                    // передать его
                    // Закрываем окно
                    this.Close();
                }
            }
            else MessageBox.Show("Значение депозита должен быть целым числом!");
        }
    }
}
