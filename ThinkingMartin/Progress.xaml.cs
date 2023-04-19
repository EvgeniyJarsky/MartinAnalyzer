using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;
using WPF_NET6;
using System.ComponentModel;

namespace ThinkingMartin
{
    /// <summary>
    /// Логика взаимодействия для Progress.xaml
    /// </summary>
    public partial class Progress : Window
    {
        public static Progress Instance { get; private set; }

        BackgroundWorker worker = new BackgroundWorker();

        

        public Progress()
        {
            InitializeComponent();

            // Topmost = true;

            this.Loaded += WindowLoaded;

            label1.Content = "Выполняется расчет...";

            Instance = this;

            worker.DoWork += worker_DoWork;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            // worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.ProgressChanged += worker_ProgressChanged;
        }

        private void startAsyncButton_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy != true)
            {
                // Start the asynchronous operation.
                worker.RunWorkerAsync();
            }
        }

        private void cancelAsyncButton_Click(object sender, EventArgs e)
        {
            if (worker.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                worker.CancelAsync();
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)  
        {  
            label1.Content = e.ProgressPercentage.ToString(); 
        }  
            
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for(int i =0; i<10000;i++)
            {
                System.Threading.Thread.Sleep(1000);
                worker.ReportProgress(i);
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                label1.Content = "Canceled!";
            }
            else if (e.Error != null)
            {
                label1.Content = "Error: " + e.Error.Message;
            }
            else
            {
                label1.Content = "Done!";
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            
            worker.RunWorkerAsync();

        }

    }
}
