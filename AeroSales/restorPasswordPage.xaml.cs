using Npgsql;
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

namespace AeroSales
{
    /// <summary>
    /// Логика взаимодействия для restorPasswordPage.xaml
    /// </summary>
    public partial class restorPasswordPage : Page
    {
        string conn_param = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a";//"Server=192.168.132.128;Port=5432;User Id=postgres;Password=P50-2-19;Database=testdb;"; //Например: "Server=127.0.0.1;Port=5432;User Id=postgres;Password=mypass;Database=mybase;"
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public restorPasswordPage(MainWindow MW)
        {
            InitializeComponent();
            Mv = MW;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new authorization(Mv));
        }

        private void btnPass_Click(object sender, RoutedEventArgs e)
        {
            string sql = $@"select count(*) from Client where client_phone_number = '{txtNumber.Text}' and email = '{txtEmail.Text}' and codeword = '{txtCodeWord.Text}';";
            NpgsqlConnection conn = new NpgsqlConnection(conn_param);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
            conn.Open();
            if (comm.ExecuteScalar().ToString() == "1")
            {
                sql = $@"select * from Client where client_phone_number = '{txtNumber.Text}' and email = '{txtEmail.Text}' and codeword = '{txtCodeWord.Text}';";
                conn = new NpgsqlConnection(conn_param);
                comm = new NpgsqlCommand(sql, conn);
                conn.Open();
                NpgsqlDataReader dataReader = null;
                dataReader = comm.ExecuteReader();
                dataReader.Read();
                Mv.MainFrame.NavigationService.Navigate(new newPasswordPage(Mv, dataReader[0].ToString()));
                conn.Close();
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует. Проверьте введенные данные.");
            }

            conn.Close();
        }
    }
}
