using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace IZ_DB_app
{
    /// <summary>
    /// Логика взаимодействия для EmployeeAdd.xaml
    /// </summary>
    public partial class EmployeeAdd : UserControl
    {
        private static string connection = Connection.connection;

        public EmployeeAdd()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (add1.Text != ""
            && (add2.Text != "")
            && (add3.Text != "")
            && (add4.Text != "")
            && (add5.Text != ""))
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    List<string> vars = new();
                    SqlCommand com = new ($"insert into [dbo].[Employees] values(\'{add1.Text}\',\'{add2.Text}\',\'{add3.Text}\',{add4.Text}, \'{add5.Text}\')", conn);
                    com.ExecuteNonQuery();
                }

                add1.Clear();
                add2.Clear();
                add3.Clear();
                add4.Clear();
                add5.Clear();

            }
            else
            {
                MessageBox.Show("Остались пустые поля", "Беда!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Content = connection;
        }
    }
}
