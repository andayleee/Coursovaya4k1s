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
    /// Логика взаимодействия для registrationPage.xaml
    /// </summary>
    public partial class registrationPage : Page
    {
        string constr = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;";
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        string idClient = "";
        public registrationPage(MainWindow MW)
        {
            InitializeComponent();
            Mv = MW;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new mainWindowPage(Mv, idClient, "", "", ""));
        }

        private void txtPassword2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password.ToString()==txtPassword2.Password.ToString())
            {
                lbSimilarity.Content = "Пароли совпадают.";
            }
            else
            {
                lbSimilarity.Content = "Пароли не совпадают.";
            }
        }

        private void btnPass_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            try
            {
                if (txtLogin.Text != "" && txtPassword.Password.ToString() != "" && txtPassword2.Password.ToString() != "" && !txtNumber.Text.Contains("_") && txtCodeWord.Text != "" && txtEmail.Text != "" && txtEmail.Text.Contains("@") && txtEmail.Text.Contains("."))
                {
                    var md5 = MD5.Create();
                    var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(txtPassword.Password.ToString()));
                    connection.Open();
                    string com = $@"call client_insert ('{txtNumber.Text}','','','','{txtLogin.Text}','{Convert.ToBase64String(hash)}','2000-01-01','','','{txtEmail.Text}','{txtCodeWord.Text}');";
                    NpgsqlCommand command = new NpgsqlCommand(com, connection);
                    command.ExecuteNonQuery();
                    string sql = $@"select * from Client where login = '{txtLogin.Text}' and password = '{Convert.ToBase64String(hash)}';";
                    NpgsqlConnection conn = new NpgsqlConnection(constr);
                    NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
                    conn.Open();
                    NpgsqlDataReader dataReader = null;
                    dataReader = comm.ExecuteReader();
                    dataReader.Read();
                    idClient = dataReader[0].ToString();
                    Mv.MainFrame.NavigationService.Navigate(new mainWindowPage(Mv, idClient, "", "", ""));
                }
                else { MessageBox.Show("Заполните данные!"); }
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
    }
}
