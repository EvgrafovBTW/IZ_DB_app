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
using System.Data.SqlClient;
using System.Data;

namespace IZ_DB_app
{
    /// <summary>
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        public static string connection = @"Data Source=DESKTOP-UI0U6SI\SQLEXPRESS;Initial Catalog=izdat;Integrated Security=True";
        private DataGrid roottab = new DataGrid() { IsReadOnly = true };
        private Insert insert = new();
        private DataSet workDS = new DataSet();
        /// <summary>
        /// prid это выбранный айди поставщика из списка
        /// </summary>
        private int prid;
        /// <summary>
        /// переменная для простомтра айди в меседж боксе
        /// </summary>
        private string temp;

        /// <summary>
        /// актуальный в момент работы айди шляпы контракта
        /// </summary>
        private int cnid;
        public Manager()
        {
            InitializeComponent();
        }

        private void SeeProviders()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                //Заполнение поставщиками
                SqlDataAdapter sda = new SqlDataAdapter("select [provider_name], [provider_id] from [dbo].[Providers]", conn);
                sda.Fill(workDS, "Providers");
                for (int i = 0; i < workDS.Tables["Providers"].Rows.Count; i++)
                {
                    prList.Items.Add(workDS.Tables["Providers"].Rows[i].ItemArray[0]);
                }

            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SeeProviders();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void TableOutput(string sql, DataGrid dataGrid)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGrid.ItemsSource = ds.Tables[0].DefaultView;
                conn.Close();
            }
        }
        private void TableOutput(string sql, DataGrid dataGrid, string tableName)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                workDS.Tables[tableName]?.Clear();
                sda.Fill(workDS,tableName);
                dataGrid.ItemsSource = workDS.Tables[tableName].DefaultView;
                conn.Close();
            }
        }

        private void Refresh()
        {
            vivod.Items.Clear();
        }

        private void MenuItem_change_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow parent = (LoginWindow)this.Owner;
            parent.Show();
            Hide();
        }
        private void Button_addProvider_Click(object sender, RoutedEventArgs e)
        {
            addProvider.IsOpen = true;
            vivod.Items.Add(new TabItem { Header = "Поставщики", Content = roottab });
            vivod.SelectedIndex = 0;
            TableOutput("exec loadProviders", roottab);
            insert.insertCommand = @"insert into [dbo].[Providers] values('?', '?', '?')";
        }

        private void accept_Click(object sender, RoutedEventArgs e)
        {
            if (Padd1.Text != ""
            && (Padd2.Text != "")
            && (Padd3.Text != ""))
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    List<string> vars = new();

                    vars.Add(Padd1.Text);
                    vars.Add(Padd2.Text);
                    vars.Add(Padd3.Text);

                    insert.SetVars(vars);

                    string command = insert.GetTableCommand();
                    SqlCommand com = new SqlCommand(command, conn);
                    com.ExecuteNonQuery();
                }

                Padd1.Clear();
                Padd2.Clear();
                Padd3.Clear();

                TableOutput("exec loadProviders", roottab);
            }
            else
            {
                MessageBox.Show("Остались пустые поля", "Беда!", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                //Заполнение поставщиками
                SqlDataAdapter sda = new SqlDataAdapter("select [provider_name], [provider_id] from [dbo].[Providers]", conn);
                sda.Fill(workDS, "Providers");
                for (int i = (workDS.Tables["Providers"].Rows.Count)-1; i < workDS.Tables["Providers"].Rows.Count; i++)
                {
                    prList.Items.Add(workDS.Tables["Providers"].Rows[i].ItemArray[0]);
                }

            }
        }

        private void decline_Click(object sender, RoutedEventArgs e)
        {
            Padd1.Clear();
            Padd2.Clear();
            Padd3.Clear();

            addProvider.IsOpen = false;
        }

        private void PrList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            prid = int.Parse(workDS.Tables["Providers"].Rows[prList.SelectedIndex].ItemArray[1].ToString());
            temp = Convert.ToString(prid);
            //MessageBox.Show(temp, "sada", MessageBoxButton.OK);
            ContractSpan.Visibility = Visibility.Visible;
            ScStep.Visibility = Visibility.Visible;
        }

        private void ScStep_Click(object sender, RoutedEventArgs e)
        {
            if (ContractSpan.Text != "" && prid >= 1)
            {

                ContractSpan.IsEnabled = false;
                ScStep.IsEnabled = false;

                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    List<string> vars = new();

                    vars.Add(ContractSpan.Text);
                    vars.Add(prid.ToString());

                    insert.SetVars(vars);

                    string command = insert.GetTableCommand();
                    SqlCommand com = new SqlCommand(command, conn);
                    com.ExecuteNonQuery();

                }

                TableOutput("exec loadContract", roottab, "Contract");
               
                cnid = int.Parse(workDS.Tables["Contract"].Rows[prList.SelectedIndex].ItemArray[1].ToString());
                
                

            }
            else
            {
                MessageBox.Show("Заполните поле описания!", "Беда!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ContractSpan_TextChanged(object sender, TextChangedEventArgs e)
        {
            insert.insertCommand = @"insert into [dbo].[Contract] values('?', ?)";
        }
    }

}
