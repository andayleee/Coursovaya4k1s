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
            idCl = "5";
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
            //int a = 0;
            //for (int i = 1; i <= 118; i++)
            //{
            //    for (int j = 1; j <= 16; j++)
            //    {
            //        a++;
            //        connect.Open();
            //        string com = $"INSERT INTO flight_seat VALUES('{a}','{j}','{i}','false',null);";
            //        command = new NpgsqlCommand(com, connect);
            //        command.ExecuteNonQuery();
            //        connect.Close();
            //    }
            //}
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new authorization(Mv));
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new registrationPage(Mv));
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            idCl = "";
            btnLogIn.Visibility = Visibility.Visible;
            btnReg.Visibility = Visibility.Visible;
            btnExit.Visibility = Visibility.Hidden;
            btnProfile.Visibility = Visibility.Hidden;
            btnTravels.Visibility = Visibility.Hidden;
        }

        private void btnCity1_Click(object sender, RoutedEventArgs e)
        {
            txtTo.Text = sender.ToString().Remove(0, 32);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

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

        private void btnDG_Click(object sender, RoutedEventArgs e)
        {
            from = txtFrom.Text;
            to = txtTo.Text;
            date = dpTo.Text;
            Mv.MainFrame.NavigationService.Navigate(new clientOrderPage(Mv, idCl, id[dg2.SelectedIndex].ToString(), from, to, date));
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new clientProfilePage(Mv, idCl));
        }

        private void btnTravels_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new clientTripPage(Mv, idCl));
        }
    }
}
