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
    /// Логика взаимодействия для newPasswordPage.xaml
    /// </summary>
    public partial class newPasswordPage : Page
    {
        string constr = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;";
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        List<string> names = new List<string>();
        string idCl = "";

        public newPasswordPage(MainWindow MW, string idClient)
        {
            InitializeComponent();
            Mv = MW;
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            connection.Open();
            NpgsqlDataReader dataReader = null;
            string sql = $@"select * from Client where id_client = '{idClient}';";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            txtLogin.Content = dataReader[$@"Login"].ToString();
            idCl = idClient;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new authorization(Mv));
        }

        private void txtPassword2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password.ToString() == txtPassword2.Password.ToString())
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
                if (txtPassword.Password.ToString() != "" && txtPassword2.Password.ToString() != "")
                {
                    connection.Open();
                    string com = $@"call client_password_update ('{idCl}','{txtPassword.Password.ToString()}');";
                    NpgsqlCommand command = new NpgsqlCommand(com, connection);
                    command.ExecuteNonQuery();
                    Mv.MainFrame.NavigationService.Navigate(new mainWindowPage(Mv, idCl, "", "", ""));
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
