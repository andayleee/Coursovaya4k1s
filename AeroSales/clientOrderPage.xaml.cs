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
    /// Логика взаимодействия для clientOrderPage.xaml
    /// </summary>
    public partial class clientOrderPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        string idCl = "";
        string idFl = "";
        string from = "";
        string to = "";
        string date = "";
        int cost = 0;
        static string constr = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;";
        NpgsqlConnection connect = new NpgsqlConnection(constr);
        List<string> id = new List<string>();
        List<string> choosedid = new List<string>();
        List<string> idTList = new List<string>();

        /// <summary>
        /// Инициализация окна
        /// </summary>
        /// <param name="MW">Экземпляр класса MainWindow</param>
        /// <param name="Date">Переменная, содержащая обозначение даты рейса</param>
        /// /// <param name="idClient">Переменная, содержащая обозначение роли пользователя</param>
        /// /// <param name="idFlight">Переменная, содержащая обозначение рейса</param>
        /// /// <param name="From">Переменная, содержащая обозначение отправления</param>
        /// /// <param name="To">Переменная, содержащая обозначение прибытия</param>
        public clientOrderPage(MainWindow MW, string idClient, string idFlight, string From, string To, string Date)
        {
            InitializeComponent();
            from = From;
            to = To;
            date = Date;
            Mv = MW;
            idCl = idClient;
            idFl = idFlight;
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($@"select point_of_departure,point_of_arrival,to_char(flight_date, 'dd.mm.yyyy'),aircraft_type,time_of_departure, arrival_time,cost_ticket from flight where id_flight='{idFl}'", connect);
            NpgsqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            dataReader.Read();
            lbHeader.Content = dataReader[0].ToString() + " - " + dataReader[1].ToString() + "  " + dataReader[2].ToString();
            lbPoints.Content = dataReader[0].ToString()+ " - " + dataReader[1].ToString();
            lbPointFrom.Content = dataReader[0].ToString();
            lbPointTo.Content = dataReader[1].ToString();
            lbAirType.Content = dataReader[3].ToString();
            lbTimeFrom.Content = dataReader[4].ToString();
            lbTimeTo.Content = dataReader[5].ToString();
            lbCost.Content = dataReader[6].ToString();
            TimeSpan timeFrom = (TimeSpan)dataReader[4];
            TimeSpan timeTo = (TimeSpan)dataReader[5];
            if (timeFrom>timeTo)
            {
                TimeSpan ts1 = new TimeSpan(24, 0, 0);
                TimeSpan ts2 = ts1 - timeFrom;
                lbTripTime.Content = (ts2 + timeTo).ToString();
            }
            else
            {
                lbTripTime.Content = (timeTo - timeFrom).ToString();
            }
            cost = (int)dataReader[6];
            connect.Close();

            connect.Open();
            command = new NpgsqlCommand($@"select carrier_company_name from contract join flight on contract_id=id_contract where id_flight = '{idFl}'", connect);
            dataReader = null;
            dataReader = command.ExecuteReader();
            dataReader.Read();
            lbCarrierCompanyName.Content = dataReader[0].ToString();
            connect.Close();

            connect = new NpgsqlConnection(constr);
            connect.Open();
            DataTable datatbl = new DataTable();
            NpgsqlDataAdapter mySqlDataAdapter = new NpgsqlDataAdapter($"select place as \"place\", status from seat join flight_seat on seat_id=id_seat where status='false' and flight_id='{idFl}' ", connect);
            // mySqlDataAdapter1.Fill(datatbl1);
            mySqlDataAdapter.Fill(datatbl);
            dg2.ItemsSource = datatbl.DefaultView;
            connect.Close();
            gbSeat.Visibility = Visibility.Hidden;
            wrpTotal.Visibility = Visibility.Hidden;
            btnApplyOrder.Visibility = Visibility.Hidden;
            connect.Open();
            dataReader = null;
            string sql = $"select * from seat";
            command = new NpgsqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                id.Add(dataReader[$@"ID_seat"].ToString());
            }
            connect.Close();

            connect.Open();
            dataReader = null;
            sql = $"select * from ticket_list";
            command = new NpgsqlCommand(sql, connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                idTList.Add(dataReader[$@"ID_ticket_list"].ToString());
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
            Mv.MainFrame.NavigationService.Navigate(new mainWindowPage(Mv, idCl, from, to, date));
        }
        /// <summary>
        /// Подтверждение кол-ва
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnApplyCount_Click(object sender, RoutedEventArgs e)
        {
            if (txtCount.Value > 0)
            {
                gbSeat.Visibility = Visibility.Visible;
                txtCount.IsReadOnly = true;
            }
        }
        /// <summary>
        /// Запись мета в базу данных
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void chDG_Checked(object sender, RoutedEventArgs e)
        {
            choosedid.Add(id[dg2.SelectedIndex]);
            connect.Open();
            string com = $"UPDATE flight_seat SET client_id = '{idCl}', status = 'true' WHERE flight_id = '{idFl}' and seat_id = '{id[dg2.SelectedIndex]}';";
            NpgsqlCommand command = new NpgsqlCommand(com, connect);
            command.ExecuteNonQuery();
            connect.Close();
            Order();
        }
        /// <summary>
        /// Оформление заказа
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void Order()
        {
            if (choosedid.Count == txtCount.Value)
            {
                wrpTotal.Visibility = Visibility.Visible;
                btnApplyOrder.Visibility = Visibility.Visible;
                lbTotal.Content = (cost * choosedid.Count).ToString();
            }
            else
            {
                wrpTotal.Visibility = Visibility.Hidden;
                btnApplyOrder.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// Удаление записи места
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void chDG_Unchecked(object sender, RoutedEventArgs e)
        {
            choosedid.Remove(id[dg2.SelectedIndex]);
            connect.Open();
            string com = $"UPDATE flight_seat SET client_id = null, status = 'false'  WHERE flight_id = '{idFl}' and seat_id = '{id[dg2.SelectedIndex]}';";
            NpgsqlCommand command = new NpgsqlCommand(com, connect);
            command.ExecuteNonQuery();
            connect.Close();
            Order();
        }
        /// <summary>
        /// Оформление заказа
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnApplyOrder_Click(object sender, RoutedEventArgs e)
        {
            if (idCl != "")
            {
                NpgsqlConnection connection = new NpgsqlConnection(constr);
                try
                {
                    connect.Open();
                    NpgsqlCommand command = new NpgsqlCommand($@"select* from client where id_client='{idCl}'", connect);
                    NpgsqlDataReader dataReader = null;
                    dataReader = command.ExecuteReader();
                    dataReader.Read();
                    if (dataReader[1].ToString() != "" && dataReader[2].ToString() != "" && dataReader[3].ToString() != "" && dataReader[5].ToString() != "" && dataReader[6].ToString() != "" && dataReader[7].ToString() != "" && dataReader[8].ToString() != "" && dataReader[9].ToString() != "" && dataReader[10].ToString() != "" && dataReader[11].ToString() != "")
                    {
                        connect.Close();
                        connection.Open();
                        string com = $"call orderr_insert ('{DateTime.Now.ToString("yyyy'-'MM'-'dd")}','{lbTotal.Content.ToString()}','{idCl}','true')";
                        command = new NpgsqlCommand(com, connection);
                        command.ExecuteNonQuery();
                        connection.Close();

                        connection.Open();
                        command = new NpgsqlCommand($@"select * from orderr where date_of_order='{DateTime.Now.ToString("yyyy'-'MM'-'dd")}' and total_price = '{lbTotal.Content.ToString()}' and client_id = '{idCl}'", connection);
                        dataReader = null;
                        dataReader = command.ExecuteReader();
                        dataReader.Read();
                        string idOrder = dataReader[0].ToString();
                        connection.Close();

                        connection.Open();
                        Random rnd = new Random();
                        com = $"call ticket_insert ('false','{DateTime.Now.ToString("yyyy'-'MM'-'dd")}','{DateTime.Now.AddDays(5).ToString("yyyy'-'MM'-'dd")}','{idFl}','{idTList[rnd.Next(0, idTList.Count - 1)]}','1','{idOrder}')";
                        command = new NpgsqlCommand(com, connection);
                        command.ExecuteNonQuery();
                        connection.Close();

                       

                        Mv.MainFrame.NavigationService.Navigate(new mainWindowPage(Mv, idCl,"","",""));
                    }
                    else
                    {
                        MessageBox.Show("Заполните личные данные в профиле.");
                    }

                }
                catch (NpgsqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Войдите в аккаунт и повторите снова.");
            }
        }
    }
}
