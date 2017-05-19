using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            _thread = new Thread(() =>
            {
                while (true)
                {
                    lbl_hour.Dispatcher.Invoke(new Action(() => lbl_hour.Content =
                        DateTime.Now.ToString("HH:mm")));
                    lbl_date.Dispatcher.Invoke(new Action(() => lbl_date.Content =
                        DateTime.Now.ToString("dddd, dd - MM - yyyy")));
                    Thread.Sleep(2000);
                }
            });
            _thread.Start();

            this.WindowStartupLocation = WindowStartupLocation.Manual;
            try
            {
                this.Top = int.Parse(File.ReadAllLines("config.ini")[0]);
                this.Left = int.Parse(File.ReadAllLines("config.ini")[1]);
            } 
            catch
            {
                this.Top = 0;
                this.Left = 0;
            }
        }

        Thread _thread;

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            File.WriteAllLines("config.ini", new string[] { this.Top.ToString(),
            this.Left.ToString() });
            _thread.Abort();
        }
    }
}
