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
    /// Логика взаимодействия для TopButtons.xaml
    /// </summary>
    public partial class TopButtons : UserControl
    {
        public TopButtons()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler LoadFileEvent;
        public event RoutedEventHandler DeleteItemEvent;
        public event RoutedEventHandler DeleteAllEvent;
        public event RoutedEventHandler FilterEvent;


        private void Btn_LoadFile(object sender, RoutedEventArgs e)
        {
            if (LoadFileEvent != null) LoadFileEvent.Invoke(sender, e);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            DeleteItemEvent?.Invoke(sender, e);
        }

        private void DeleteAll(object sender, RoutedEventArgs e)
        {
            DeleteAllEvent?.Invoke(sender, e);
        }
        
        private void Filter(object sender, RoutedEventArgs e)
        {
            FilterEvent?.Invoke(sender, e);
            WPF_NET6.Filter filter = new();
            filter.Show();
        }
    }
}
