using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace AeroSales
{
    /// <summary>
    /// Логика взаимодействия для mainWindowPage.xaml
    /// </summary>
    public partial class mainWindowPage : Page
    {
        string idCl = "";
        string from = "";
        string to = "";
        string date = "";
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static string constr = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;";
        NpgsqlConnection connect = new NpgsqlConnection(constr);
        List<string> id = new List<string>();
        /// <summary>
        /// Инициализация окна
        /// </summary>
        /// <param name="MW">Экземпляр класса MainWindow</param>
        /// <param name="idClient">Переменная, содержащая обозначение клиента</param>
        /// /// <param name="From">Переменная, содержащая обозначение заказа</param>
        /// /// <param name="To">Переменная, содержащая обозначение билета</param>
        /// /// <param name="Date">Переменная, содержащая обозначение билета</param>
        public mainWindowPage(MainWindow MW, string idClient, string From, string To, string Date)
        {
            InitializeComponent();
            from = From;
            to = To;
            date = Date;
            dg2.Visibility = Visibility.Hidden;
            lbEx.Visibility = Visibility.Hidden;
            btnProfile.Visibility = Visibility.Hidden;
            btnTravels.Visibility = Visibility.Hidden;
            idCl = idClient;
            Mv = MW;
            if (idCl!="")
            {
                btnLogIn.Visibility = Visibility.Hidden;
                btnReg.Visibility = Visibility.Hidden;
                btnExit.Visibility = Visibility.Visible;
                btnProfile.Visibility = Visibility.Visible;
                btnTravels.Visibility = Visibility.Visible;
            }
            else
            {
                btnLogIn.Visibility = Visibility.Visible;
                btnReg.Visibility = Visibility.Visible;
                btnExit.Visibility = Visibility.Hidden;
                btnProfile.Visibility = Visibility.Hidden;
                btnTravels.Visibility = Visibility.Hidden;
            }
            NpgsqlCommand command = new NpgsqlCommand($@"select concat(point_of_departure,' - ', point_of_arrival,'   ',Cost_Ticket,' руб.') from flight  where point_of_arrival = 'Стамбул'", connect);
            connect.Open();
            lbDB.Items.Clear();
            NpgsqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                lbDB.Items.Add(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            lbDB2.Items.Clear();
            command = new NpgsqlCommand($@"select concat(point_of_departure,' - ', point_of_arrival,'   ',Cost_Ticket,' руб.') from flight  where point_of_arrival = 'Тель-Авив'", connect);
            dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                lbDB2.Items.Add(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            lbDB3.Items.Clear();
            command = new NpgsqlCommand($@"select concat(point_of_departure,' - ', point_of_arrival,'   ',Cost_Ticket,' руб.') from flight  where point_of_arrival = 'Минск'", connect);
            dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                lbDB3.Items.Add(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            lbDB4.Items.Clear();
            command = new NpgsqlCommand($@"select concat(point_of_departure,' - ', point_of_arrival,'   ',Cost_Ticket,' руб.') from flight  where point_of_arrival = 'Ереван'", connect);
            dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                lbDB4.Items.Add(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();
            connect.Close();
            if(from !=""&&to!=""&&date!="")
            {
                txtFrom.Text = from;
                txtTo.Text = to;
                dpTo.Text = date;
                Search();
            }
        }
        /// <summary>
        /// Переход на страницу авторизации
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new authorization(Mv));
        }
        /// <summary>
        /// Переход на страницу регистрации
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new registrationPage(Mv));
        }
        /// <summary>
        /// Переход на страницу назад
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            idCl = "";
            btnLogIn.Visibility = Visibility.Visible;
            btnReg.Visibility = Visibility.Visible;
            btnExit.Visibility = Visibility.Hidden;
            btnProfile.Visibility = Visibility.Hidden;
            btnTravels.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Выбор города
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnCity1_Click(object sender, RoutedEventArgs e)
        {
            txtTo.Text = sender.ToString().Remove(0, 32);
        }
        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
        /// <summary>
        /// Поиск
        /// </summary>
        private void Search()
        {
            try
            {
                if (txtFrom.Text != "" && txtTo.Text != "" && dpTo.SelectedDate != null)
                {
                    NpgsqlConnection connect = new NpgsqlConnection(constr);
                    connect.Open();
                    DataTable datatbl = new DataTable();
                    NpgsqlDataAdapter mySqlDataAdapter = new NpgsqlDataAdapter($"select point_of_departure as \"point_of_departure\", point_of_arrival as \"point_of_arrival\", to_char(flight_date, 'dd.mm.yyyy') as \"flight_date\", time_of_departure as \"time_of_departure\", arrival_time as \"arrival_time\" from flight where \"point_of_departure\" like '%{txtFrom.Text}%' and \"point_of_arrival\" like '%{txtTo.Text}%' and \"flight_date\" >= '{dpTo.Text}'", connect);
                    // mySqlDataAdapter1.Fill(datatbl1);
                    mySqlDataAdapter.Fill(datatbl);
                    dg2.Visibility = Visibility.Visible;
                    dg2.ItemsSource = datatbl.DefaultView;
                    NpgsqlDataReader dataReader = null;
                    string sql = $"SELECT * FROM flight  where \"point_of_departure\" like '%{txtFrom.Text}%' and \"point_of_arrival\" like '%{txtTo.Text}%' and \"flight_date\" >= '{dpTo.Text}'";
                    NpgsqlCommand command = new NpgsqlCommand(sql, connect);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        id.Add(dataReader[$@"ID_flight"].ToString());
                    }
                    if (dg2.Items.Count == 0)
                    {
                        dg2.Visibility = Visibility.Hidden;
                        lbEx.Content = "РЕЙСОВ НЕ НАЙДЕНО!";
                        lbEx.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        dg2.Visibility = Visibility.Visible;
                        lbEx.Visibility = Visibility.Hidden;

                    }
                    // gridMyResult.ItemsSource = datatbl1.DefaultView;
                    connect.Close();
                }
                else
                {
                    lbEx.Visibility = Visibility.Visible;
                    lbEx.Content = "ВВЕДИТЕ ВСЕ ДАННЫЕ";
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Переход на страницу заказа клиента
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnDG_Click(object sender, RoutedEventArgs e)
        {
            if (idCl!="") {
                from = txtFrom.Text;
                to = txtTo.Text;
                date = dpTo.Text;
                Mv.MainFrame.NavigationService.Navigate(new clientOrderPage(Mv, idCl, id[dg2.SelectedIndex].ToString(), from, to, date));
            }
            else
            {
                MessageBox.Show("Авторизуйтесь");
            }
        }
        /// <summary>
        /// Переход на страницу профия
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new clientProfilePage(Mv, idCl));
        }
        /// <summary>
        /// Переход на страницу путешествий
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnTravels_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new clientTripPage(Mv, idCl));
        }
    }
}
