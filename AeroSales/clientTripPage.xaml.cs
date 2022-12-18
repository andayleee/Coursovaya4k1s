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
    /// Логика взаимодействия для clientTripPage.xaml
    /// </summary>
    public partial class clientTripPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static string constr = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;";
        string idCl = "";
        string idTicket = "";
        NpgsqlConnection connect = new NpgsqlConnection(constr);
        List<string> id = new List<string>();
        /// <summary>
        /// Инициализация окна
        /// </summary>
        /// <param name="MW">Экземпляр класса MainWindow</param>
        /// /// <param name="idClient">Переменная, содержащая обозначение роли пользователя</param>
        public clientTripPage(MainWindow MW, string idClient)
        {
            InitializeComponent();
            Mv = MW;
            idCl = idClient;
            gbTicketInfo.Visibility = Visibility.Hidden;
            dg1.Visibility = Visibility.Hidden;
            lbSeats.Visibility = Visibility.Hidden;
            load();
        }
        /// <summary>
        /// Загрузка данных из базы данных
        /// </summary>
        private void load()
        {
            NpgsqlConnection connect = new NpgsqlConnection(constr);
            connect.Open();
            DataTable datatbl = new DataTable();
            NpgsqlDataAdapter mySqlDataAdapter = new NpgsqlDataAdapter($"select isvisible as \"isvisible\", client_id as \"client_id\", point_of_departure as \"point_of_departure\", point_of_arrival as \"point_of_arrival\", to_char(flight_date, 'dd.mm.yyyy') as \"flight_date\", time_of_departure as \"time_of_departure\", arrival_time as \"arrival_time\", Replace_TF(booking_status) as \"booking_status\", to_char(booking_end_date, 'dd.mm.yyyy') as \"booking_end_date\" from orderr join ticket on orderr_id=id_orderr join flight on flight_id=id_flight where \"client_id\" = '{idCl}' and \"isvisible\" = 'true'", connect);
            // mySqlDataAdapter1.Fill(datatbl1);
            mySqlDataAdapter.Fill(datatbl);
            dg2.Visibility = Visibility.Visible;
            dg2.ItemsSource = datatbl.DefaultView;
            NpgsqlDataReader dataReader = null;
            id.Clear();
            string sql = $"SELECT * FROM orderr  where \"client_id\" = '{idCl}' and \"isvisible\" = 'true'";
            NpgsqlCommand command = new NpgsqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                id.Add(dataReader[$@"ID_orderr"].ToString());
            }
            connect.Close();
        }
        /// <summary>
        /// Переход на страницу назад
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new mainWindowPage(Mv, idCl, "", "", ""));
        }
        /// <summary>
        /// Выбор и вывод данных из базы данных
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void dg2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg2.SelectedIndex >= 0)
            {

                gbTicketInfo.Visibility = Visibility.Visible;
                dg1.Visibility = Visibility.Visible;
                lbSeats.Visibility = Visibility.Visible;
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand($@"select point_of_departure,point_of_arrival,to_char(flight_date, 'dd.mm.yyyy'),aircraft_type,time_of_departure, arrival_time,cost_ticket, id_flight, id_ticket from orderr join ticket on orderr_id=id_orderr join flight on flight_id=id_flight where id_orderr='{id[dg2.SelectedIndex].ToString()}'", connect);
                NpgsqlDataReader dataReader = null;
                dataReader = command.ExecuteReader();
                dataReader.Read();
                lbHeader.Content = dataReader[0].ToString() + " - " + dataReader[1].ToString() + "  " + dataReader[2].ToString();
                lbPoints.Content = dataReader[0].ToString() + " - " + dataReader[1].ToString();
                lbPointFrom.Content = dataReader[0].ToString();
                lbPointTo.Content = dataReader[1].ToString();
                lbAirType.Content = dataReader[3].ToString();
                lbTimeFrom.Content = dataReader[4].ToString();
                lbTimeTo.Content = dataReader[5].ToString();
                int count = (int)dataReader[6];
                string idFl = dataReader[7].ToString();
                idTicket = dataReader[8].ToString();
                TimeSpan timeFrom = (TimeSpan)dataReader[4];
                TimeSpan timeTo = (TimeSpan)dataReader[5];
                if (timeFrom > timeTo)
                {
                    TimeSpan ts1 = new TimeSpan(24, 0, 0);
                    TimeSpan ts2 = ts1 - timeFrom;
                    lbTripTime.Content = (ts2 + timeTo).ToString();
                }
                else
                {
                    lbTripTime.Content = (timeTo - timeFrom).ToString();
                }
                connect.Close();
                connect.Open();
                DataTable datatbl = new DataTable();
                NpgsqlDataAdapter mySqlDataAdapter = new NpgsqlDataAdapter($"select place as \"place\" from flight_seat join seat on seat_id=id_seat where client_id='{idCl}' and flight_id='{idFl}'", connect);
                // mySqlDataAdapter1.Fill(datatbl1);
                mySqlDataAdapter.Fill(datatbl);
                dg1.ItemsSource = datatbl.DefaultView;
                connect.Close();

                connect.Open();
                command = new NpgsqlCommand($"SELECT count(*) as \"Количество\" FROM flight_seat join seat on seat_id=id_seat where client_id='{idCl}' and flight_id='{idFl}'", connect);
                string countOfTickets = command.ExecuteScalar().ToString();
                connect.Close();
                lbCount.Content = count * Convert.ToInt32(countOfTickets);
            }
        }
        /// <summary>
        /// Удаление данных
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnDGDelete_Click(object sender, RoutedEventArgs e)
        {

            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($@"UPDATE orderr SET isvisible = 'false' WHERE id_orderr = '{id[dg2.SelectedIndex].ToString()}';", connect);
            command.ExecuteNonQuery();
            connect.Close();
            id.RemoveAt(dg2.SelectedIndex);
            gbTicketInfo.Visibility = Visibility.Hidden;
            load();
        }
        /// <summary>
        /// Подробная информация
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnDG_Click(object sender, RoutedEventArgs e)
        {
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($@"select booking_status from ticket where id_ticket='{idTicket}'", connect);
            NpgsqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            dataReader.Read();
            string book = dataReader[0].ToString();
            connect.Close();
            if (book == "False")
            {
                Mv.MainFrame.NavigationService.Navigate(new clientConfirmTripPage(Mv, idCl, id[dg2.SelectedIndex].ToString(), idTicket));
            }
            else
            {
                MessageBox.Show("Заказ уже подтвержден");
            }
        }
    }
}
