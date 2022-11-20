using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для authorization.xaml
    /// </summary>
    public partial class authorization : Page
    {
        string conn_param = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a";//"Server=192.168.132.128;Port=5432;User Id=postgres;Password=P50-2-19;Database=testdb;"; //Например: "Server=127.0.0.1;Port=5432;User Id=postgres;Password=mypass;Database=mybase;"
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        string idClient = "";
        public authorization(MainWindow MW)
        {
            InitializeComponent();
            Mv = MW;
        }
        
        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            string sql = $@"select count(*) from Employee where Login = '{txtLogin.Text}' and Password = '{txtPassword.Password.ToString()}';";
            NpgsqlConnection conn = new NpgsqlConnection(conn_param);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
            conn.Open();
            //NpgsqlDataReader rdr = comm.ExecuteReader();
           
            if (comm.ExecuteScalar().ToString() == "1")
            {
                sql = $@"select Name_of_Post from Post join Employee on Post_ID = ID_Post where Login = '{txtLogin.Text}'";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                if (command.ExecuteScalar().ToString().Contains("Администратор"))
                {
                    Mv.MainFrame.NavigationService.Navigate(new adminPage(Mv));
                }
                else if (command.ExecuteScalar().ToString().Contains("Сотрудник"))
                {

                }
                else
                {

                }
            }
            else
            {
                sql = $@"select count(*) from client where Login = '{txtLogin.Text}' and Password = '{txtPassword.Password.ToString()}'";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                if (command.ExecuteScalar().ToString() == "1")
                {
                    sql = $@"select * from Client where login = '{txtLogin.Text}' and password = '{txtPassword.Password.ToString()}';";
                    conn = new NpgsqlConnection(conn_param);
                    comm = new NpgsqlCommand(sql, conn);
                    conn.Open();
                    NpgsqlDataReader dataReader = null;
                    dataReader = comm.ExecuteReader();
                    dataReader.Read();
                    idClient = dataReader[0].ToString();
                    Mv.MainFrame.NavigationService.Navigate(new mainWindowPage(Mv, dataReader[0].ToString(),"","",""));
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
            }

            conn.Close();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new registrationPage(Mv));
        }

        private void btnPass_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new restorPasswordPage(Mv));
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new mainWindowPage(Mv, idClient, "", "", ""));
        }
    }
}
