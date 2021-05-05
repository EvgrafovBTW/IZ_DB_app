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
    /// Interaction logic for MainWindow.xaml
    /// </summary>




    public partial class MainWindow : Window
    {
        public static string connection = @"Data Source=DESKTOP-UI0U6SI\SQLEXPRESS;Initial Catalog=izdat;Integrated Security=True";
        private Insert insert = new();
        private DataGrid roottab = new DataGrid() { IsReadOnly = true };
        private DataGrid sctab = new() { IsReadOnly = true };
        public MainWindow()
        {
            InitializeComponent();
        }

        LoginWindow login = new LoginWindow();

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

        private void Refresh()
        {
            vivod.Items.Clear();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            login.Owner = this;
            login.Show();
            Hide();
        }

        private void MenuItem_change_Click(object sender, RoutedEventArgs e)
        {
            login.Show();
            Hide();
        }

        private void MenuItem_tirazh_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление тиража";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "Есть ли картинки? (+/-)";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "Количество страниц";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Visible;
            header3.Content = "Тираж";
            add3.Visibility = Visibility.Visible;

            header4.Visibility = Visibility.Visible;
            header4.Content = "Качество бумаги";
            add4.Visibility = Visibility.Visible;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;
            Refresh();

            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Тиражи", Content = roottab });
            TableOutput("exec loadNeeds", roottab);

            insert.insertCommand = @"insert into [dbo].[Needs] values('?',?,?,'?')";

        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (add1.Text != ""
            && (add2.Text != "" || add2.Visibility == Visibility.Hidden)
            && (add3.Text != "" || add3.Visibility == Visibility.Hidden)
            && (add4.Text != "" || add4.Visibility == Visibility.Hidden)
            && (add5.Text != "" || add5.Visibility == Visibility.Hidden))
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    List<string> vars = new();

                    vars.Add(add1.Text);
                    vars.Add(add2.Text);
                    vars.Add(add3.Text);
                    vars.Add(add4.Text);
                    vars.Add(add5.Text);

                    insert.SetVars(vars);

                    string command = insert.GetTableCommand();
                    SqlCommand com = new SqlCommand(command, conn);
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

        private void MenuItem_view_tirazh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.Items.Add(new TabItem { Header = "Тиражи", Content = roottab });
            vivod.SelectedIndex = 0;
            TableOutput("exec loadNeeds", roottab);
        }

        private void MenuItem_view_authors_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.Items.Add(new TabItem
            {
                Header = "Авторы",
                Content = roottab
            });
            vivod.SelectedIndex = 0;
            TableOutput("exec loadAuthors", roottab);
        }

        private void MenuItem_employee_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление сотрудника";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "Имя сотрудника";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "Фамилия Сотрудника";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Visible;
            header3.Content = "Отчество сотрудника";
            add3.Visibility = Visibility.Visible;

            header4.Visibility = Visibility.Visible;
            header4.Content = "Телефон сотрудника";
            add4.Visibility = Visibility.Visible;

            header5.Visibility = Visibility.Visible;
            header5.Content = "День рождения сотрудника ГГГГ-ММ-ДД";
            add5.Visibility = Visibility.Visible;
            Refresh();

            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Сотрудники", Content = roottab });
            TableOutput("exec loadEmployees", roottab);
            insert.insertCommand = @"insert into [dbo].[Employees] values('?','?','?',?, '?')";

        }

        private void MenuItem_view_employee_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.Items.Add(new TabItem {Header = "Сотрудники", Content = roottab});
            vivod.SelectedIndex = 0;
            TableOutput("exec loadEmployees", roottab);
        }

        private void MenuItem_author_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление автора";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "Имя автора";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "Фамилия автора";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Visible;
            header3.Content = "Отчество автора";
            add3.Visibility = Visibility.Visible;

            header4.Visibility = Visibility.Visible;
            header4.Content = "Телефон автора";
            add4.Visibility = Visibility.Visible;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;
            Refresh();

            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Авторы", Content = roottab });
            TableOutput("exec loadAuthors", roottab);
            insert.insertCommand = @"insert into [dbo].[Authors] values('?','?','?','?')";
        }

        private void MenuItem_appointment_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление должности";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "Наименование должности";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Hidden;
            add2.Visibility = Visibility.Hidden;

            header3.Visibility = Visibility.Hidden;
            add3.Visibility = Visibility.Hidden;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;
            Refresh();

            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Должности", Content = roottab });
            TableOutput("exec loadAppointment", roottab);
            insert.insertCommand = @"insert into [dbo].[Appointment] values('?')";

        }

        private void MenuItem_view_appointment_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.Items.Add(new TabItem
            {
                Header = "Должности",
                Content = roottab
            });
            vivod.SelectedIndex = 0;
            TableOutput("exec loadAppointment", roottab);
        }

        private void MenuItem_estate_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление недвижимости";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "Адрес недвижимости";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "Назаначение недвижимости";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Hidden;
            add3.Visibility = Visibility.Hidden;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;
            Refresh();

            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Недвижимость", Content = roottab });
            TableOutput("exec loadIZ_estate", roottab);
            insert.insertCommand = @"insert into [dbo].[IZ_estate] values('?', '?')";
        }

        private void MenuItem_view_estate_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.Items.Add(new TabItem
            {
                Header = "Недвижимость",
                Content = roottab
            });
            vivod.SelectedIndex = 0;
            TableOutput("exec loadIZ_estate", roottab);
        }

        private void MenuItem_maintenance_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление обслуживания";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "Тип обслуживания";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Hidden;
            add2.Visibility = Visibility.Hidden;

            header3.Visibility = Visibility.Hidden;
            add3.Visibility = Visibility.Hidden;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;
            Refresh();

            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Обслуживание", Content = roottab });
            TableOutput("exec loadMaintenance", roottab);
            insert.insertCommand = @"insert into [dbo].[Maintenance] values('?')";
        }

        private void MenuItem_view_maintenance_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.Items.Add(new TabItem
            {
                Header = "Обслуживание",
                Content = roottab
            });
            vivod.SelectedIndex = 0;
            TableOutput("exec loadMaintenance", roottab);
        }

        private void MenuItem_provider_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление поставщика";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "Наименование поставщика";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "Телефон поставщика";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Visible;
            header3.Content = "Адрес поставщика";
            add3.Visibility = Visibility.Visible;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;
            Refresh();

            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Поставщики", Content = roottab });
            TableOutput("exec loadProviders", roottab);
            insert.insertCommand = @"insert into [dbo].[Providers] values('?', '?', '?')";
        }

        private void MenuItem_view_provider_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.Items.Add(new TabItem {Header = "Поставщики", Content = roottab});
            vivod.SelectedIndex = 0;
            TableOutput("exec loadProviders", roottab);
        }

        private void MenuItem_material_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление материала";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "Название материала";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "Количество материала";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Hidden;
            add3.Visibility = Visibility.Hidden;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;
            Refresh();

            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Материалы", Content = roottab });
            TableOutput("exec loadMaterials", roottab);
            insert.insertCommand = @"insert into [dbo].[Materials] values('?', ?)";
        }

        private void MenuItem_view_material_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.Items.Add(new TabItem {Header = "Материалы", Content = roottab });
            vivod.SelectedIndex = 0;
            TableOutput("exec loadMaterials", roottab);
        }

        private void MenuItem_contract_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Новая накладная";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "Описание";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "ID поставщика";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Hidden;
            add3.Visibility = Visibility.Hidden;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;
            Refresh();

            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Поставщики", Content = roottab });
            TableOutput("exec loadProviders", roottab);
            insert.insertCommand = @"insert into [dbo].[Contract] values('?', ?)";
        }

        private void MenuItem_view_contract_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Приходная нкаладная", Content = roottab });
            TableOutput("exec loadContract", roottab);
        }

        private void MenuItem_view_work_materials_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Материалы для работы", Content = roottab });
            TableOutput("exec loadWork_materials", roottab);
        }

        private void MenuItem_work_materials_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Новый материал для работы";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "ID материала";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "Кол-во материала";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Hidden;
            add3.Visibility = Visibility.Hidden;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;
            Refresh();

            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Материалы", Content = roottab });
            TableOutput("exec loadMaterials", roottab);
            insert.insertCommand = @"insert into [dbo].[Work_materials] values(?, ?)";
        }

        private void MenuItem_view_Equipment_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Оборудование", Content = roottab });
            TableOutput("exec loadEquipment", roottab);
        }

        private void MenuItem_equipment_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление оборудования";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "Наименование оборудования";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "ID мат. для работы";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Hidden;
            add3.Visibility = Visibility.Hidden;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;
            Refresh();

            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Материалы для работы", Content = roottab });
            TableOutput("exec loadWork_materials", roottab);
            insert.insertCommand = @"insert into [dbo].[Equipment] values('?', ?)";
        }

        private void MenuItem_view_ordered_books_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Заказы", Content = roottab });
            TableOutput("exec loadOrdered_books", roottab);
        }

        private void MenuItem_ordered_books_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление заказа на книгу";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "Название книги";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "Жанр книги";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Visible;
            header3.Content = "номер isbn";
            add3.Visibility = Visibility.Visible;

            header4.Visibility = Visibility.Visible;
            header4.Content = "ID тиража";
            add4.Visibility = Visibility.Visible;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;
            Refresh();

            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Тиражи", Content = roottab });
            TableOutput("exec loadNeeds", roottab);
            insert.insertCommand = @"insert into [dbo].[Ordered_books] values('?', '?', ?, ?)";
        }

        private void MenuItem_view_administrative_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Помещения", Content = roottab });
            TableOutput("exec loadAdministrative", roottab);
        }

        private void MenuItem_administrative_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление поммещения";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "ID недвижимости";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "Номер помещения";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Visible;
            header3.Content = "Наименование помещения";
            add3.Visibility = Visibility.Visible;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;

            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Недвижимость", Content = roottab });
            TableOutput("exec loadIZ_estate", roottab);
            insert.insertCommand = @"insert into [dbo].[Administrative] values(?, '?', '?')";

        }

        private void MenuItem_view_workplace_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Рабочие места", Content = roottab });
            TableOutput("exec loadWorkplace", roottab);
        }

        private void MenuItem_workplace_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление поммещения";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "Тип рабочего места";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "ID помещения";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Hidden;
            add3.Visibility = Visibility.Hidden;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;

            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Помещения", Content = roottab });
            TableOutput("exec loadAdministrative", roottab);
            insert.insertCommand = @"insert into [dbo].[Workplace] values('?', ?)";
        }

        private void MenuItem_order_check_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление итога заказа";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "ID книги";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "ID материала";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Visible;
            header3.Content = "Затрачено бумаги";
            add3.Visibility = Visibility.Visible;

            header4.Visibility = Visibility.Visible;
            header4.Content = "Затрачено чернил";
            add4.Visibility = Visibility.Visible;

            header5.Visibility = Visibility.Visible;
            header5.Content = "Цена";
            add5.Visibility = Visibility.Visible;

            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Материалы для работы", Content = roottab });
            TableOutput("exec loadWork_materials", roottab);

            TableOutput("exec loadOrdered_books", sctab);
            vivod.Items.Add(new TabItem { Header = "Книги", Content = sctab });
            insert.insertCommand = @"insert into [dbo].[Order_check] values(?, ?, ?, ?, ?)";
        }

        private void MenuItem_view_order_check_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Итог заказа", Content = roottab });
            TableOutput("exec loadOrder_check", roottab);
        }

        private void MenuItem_view_contract_materials_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Приходная накладная (тело)", Content = roottab });
            TableOutput("exec loadContract_materials", roottab);


        }

        private void MenuItem_contract_materials_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление приходной накладной (тело)";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "ID шапки";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "ID материала";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Visible;
            header3.Content = "Цена";
            add3.Visibility = Visibility.Visible;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;

            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Шапка", Content = roottab });
            TableOutput("exec loadContract", roottab);

            TableOutput("exec loadMaterials", sctab);
            vivod.Items.Add(new TabItem { Header = "Материалы", Content = sctab });

            insert.insertCommand = @"insert into [dbo].[Contract_materials] values(?, ?, ?)";
        }

        private void MenuItem_view_books_authors_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Книги по авторам", Content = roottab });
            TableOutput("exec loadBooks_authors", roottab);
        }

        private void MenuItem_books_authors_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавлении книги по автору";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "ID книги";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "ID автора";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Hidden;
            add3.Visibility = Visibility.Hidden;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;

            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Книги", Content = roottab });
            TableOutput("exec loadOrdered_books", roottab);

            vivod.Items.Add(new TabItem { Header = "Авторы", Content = sctab });
            TableOutput("exec loadAuthors", sctab);

            insert.insertCommand = @"insert into [dbo].[Books_authors] values(?, ?)";
        }

        private void MenuItem_view_masters_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Мастера", Content = roottab });
            TableOutput("exec loadMasters", roottab);
        }

        private void MenuItem_masters_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Назаначение мастера";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "ID сотрудника";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "ID обслуживания";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Hidden;
            add3.Visibility = Visibility.Hidden;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;

            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Сотрудники", Content = roottab });
            TableOutput("exec loadEmployees", roottab);

            vivod.Items.Add(new TabItem { Header = "Типы обслуживания", Content = sctab });
            TableOutput("exec loadMaintenance", sctab);

            insert.insertCommand = @"insert into [dbo].[Masters] values(?, ?)";
        }

        private void MenuItem_view_mactivities_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Мероприятия по обслуживанию", Content = roottab });
            TableOutput("exec loadMActivities", roottab);
        }

        private void MenuItem_mactivitiess_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление мероприятия по обслуживанию";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "ID оборудования";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "ID обслуживания";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Hidden;
            add3.Visibility = Visibility.Hidden;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;

            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Оборудование", Content = roottab });
            TableOutput("exec loadEquipment", roottab);

            vivod.Items.Add(new TabItem { Header = "Типа обслуживания", Content = sctab });
            TableOutput("exec loadMaintenance", sctab);

            insert.insertCommand = @"insert into [dbo].[MActivities] values(?, ?)";
        }

        private void MenuItem_view_workplace_employees_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Сотрудниики по рабочим местам", Content = roottab });
            TableOutput("exec loadWorkplace_Employees", roottab);
        }

        private void MenuItem_workplace_employees_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление сотрудника на рабочее место";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "ID рабочего места";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "ID сотрудника";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Hidden;
            add3.Visibility = Visibility.Hidden;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;

            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Рабочие места", Content = roottab });
            TableOutput("exec loadWorkplace", roottab);

            vivod.Items.Add(new TabItem { Header = "Сотрудники", Content = sctab });
            TableOutput("exec loadEmployees", sctab);

            insert.insertCommand = @"insert into [dbo].[Workplace_Employees] values(?, ?)";
        }

        private void MenuItem_view_workplace_equipment_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Оборудование по рабочим местам", Content = roottab });
            TableOutput("exec loadWorkplace_equipment", roottab);
        }

        private void MenuItem_workplace_equipment_Click(object sender, RoutedEventArgs e)
        {
            addTitle.Visibility = Visibility.Visible;
            addTitle.Content = "Добавление оборудовния на рабочее место";

            Submit.Visibility = Visibility.Visible;

            header1.Visibility = Visibility.Visible;
            header1.Content = "ID рабочего места";
            add1.Visibility = Visibility.Visible;

            header2.Visibility = Visibility.Visible;
            header2.Content = "ID оборудования";
            add2.Visibility = Visibility.Visible;

            header3.Visibility = Visibility.Hidden;
            add3.Visibility = Visibility.Hidden;

            header4.Visibility = Visibility.Hidden;
            add4.Visibility = Visibility.Hidden;

            header5.Visibility = Visibility.Hidden;
            add5.Visibility = Visibility.Hidden;

            Refresh();
            vivod.SelectedIndex = 0;
            vivod.Items.Add(new TabItem { Header = "Рабочие места", Content = roottab });
            TableOutput("exec loadWorkplace", roottab);

            vivod.Items.Add(new TabItem { Header = "Оборудование", Content = sctab });
            TableOutput("exec loadEquipment", sctab);

            insert.insertCommand = @"insert into [dbo].[Workplace_Equipment] values(?, ?)";
        }

       
    }

}
