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

namespace IZ_DB_app
{
    /// <summary>
    /// Логика взаимодействия для Employeer.xaml
    /// </summary>
    public partial class Employeer : Window
    {
        public static string connection = @"Data Source=DESKTOP-UI0U6SI\SQLEXPRESS;Initial Catalog=izdat;Integrated Security=True";
        private DataGrid roottab = new DataGrid() { IsReadOnly = true };
        private Insert insert = new();

        public Employeer()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
          /*  EmployeeAdd eadd = new EmployeeAdd();
            eadd.Content = new Page1();
            eadd.Show();*/
        }
    }
}
