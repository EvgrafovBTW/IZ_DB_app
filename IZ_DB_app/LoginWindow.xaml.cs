using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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

namespace IZ_DB_app
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>

    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_admin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (MainWindow)this.Owner;
            parent.Show();
            Hide();

        }


        private void Button_manager_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = new();
            manager.Owner = this;
            manager.Show();
            Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_employeer_Click(object sender, RoutedEventArgs e)
        {
            Employeer employeer = new();
            employeer.Owner = this;
            employeer.Show();
            Hide();
        }
    }
}