using Report_BL.DataCollection;
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
    /// Логика взаимодействия для UC_DealsTab.xaml
    /// </summary>
    public partial class UC_DealsTab : UserControl
    {
        /// <summary>
        /// Коллекция объектов "Сделка" с основными переметрами - время открытия/закрытия, тип ордера, лот и т.д.
        /// </summary>
        public static ObservableCollection<Deal> dealsCollection = Report_BL.DataCollection.DealsCollection.dealsCollection;

        public UC_DealsTab()
        {
            InitializeComponent();
            deals.ItemsSource = dealsCollection;
        }

        public event RoutedEventHandler DealsLoaded;
    }
}
