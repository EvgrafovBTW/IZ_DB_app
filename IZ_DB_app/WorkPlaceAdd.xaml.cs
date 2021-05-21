using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для WorkPlaceAdd.xaml
    /// </summary>
    public partial class WorkPlaceAdd : UserControl
    {
        private DataSet workDS = new DataSet();
        private static string connection = Connection.connection;
        private int adid;


        public WorkPlaceAdd()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (add1.Text != "" && adid >= 0)
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    SqlCommand com = new($"insert into [dbo].[Workplace] values(\'{add1.Text}\', {adid})", conn);
                    com.ExecuteNonQuery();
                }
                add1.Clear();
            }
            else
            {
                MessageBox.Show("Остались пустые поля", "Беда!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SeeAdministrative()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                //Заполнение
                SqlDataAdapter sda = new SqlDataAdapter("select [cabinet_name], [cabinet_number], [administrative_id] from [dbo].[Administrative]", conn);
                workDS.Tables["Administrative"]?.Clear();
                AdmList.Items?.Clear();
                sda.Fill(workDS, "Administrative");
                for (int i = 0; i < workDS.Tables["Administrative"].Rows.Count; i++)
                {
                    AdmList.Items.Add(workDS.Tables["Administrative"].Rows[i].ItemArray[0]);
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SeeAdministrative();
        }

        private void AdmList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            adid = int.Parse(workDS.Tables["Administrative"].Rows[AdmList.SelectedIndex].ItemArray[2].ToString());
        }
    }
}
