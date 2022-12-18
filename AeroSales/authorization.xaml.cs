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
        /// <summary>
        /// Инициализация окна
        /// </summary>
        /// <param name="MW">Экземпляр класса MainWindow</param>
        public authorization(MainWindow MW)
        {
            InitializeComponent();
            Mv = MW;
        }
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(txtPassword.Password.ToString()));
            string sql = $@"select count(*) from Employee where Login = '{txtLogin.Text}' and Password = '{Convert.ToBase64String(hash)}';";
            NpgsqlConnection conn = new NpgsqlConnection(conn_param);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
            conn.Open();
            if (comm.ExecuteScalar().ToString() == "1")
            {
                sql = $@"select Name_of_Post from Post join Employee on Post_ID = ID_Post where Login = '{txtLogin.Text}'";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                if (command.ExecuteScalar().ToString().Contains("Администратор"))
                {
                    Mv.MainFrame.NavigationService.Navigate(new adminPage(Mv, 1));
                }
                else if (command.ExecuteScalar().ToString().Contains("Бухгалтер"))
                {
                    Mv.MainFrame.NavigationService.Navigate(new adminPage(Mv, 2));
                }
                else if (command.ExecuteScalar().ToString().Contains("Закупщик"))
                {
                    Mv.MainFrame.NavigationService.Navigate(new adminPage(Mv, 3));
                }
                else if (command.ExecuteScalar().ToString().Contains("Кадровик"))
                {
                    Mv.MainFrame.NavigationService.Navigate(new adminPage(Mv, 4));
                }

            }
            else
            {
                sql = $@"select count(*) from client where Login = '{txtLogin.Text}' and Password = '{Convert.ToBase64String(hash)}'";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                if (command.ExecuteScalar().ToString() == "1")
                {
                    sql = $@"select * from Client where login = '{txtLogin.Text}' and password = '{Convert.ToBase64String(hash)}';";
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
        /// <summary>
        /// Переход на страницу регистрации
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new registrationPage(Mv));
        }
        /// <summary>
        /// Переход на страницу восстановления пароля
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnPass_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new restorPasswordPage(Mv));
        }
        /// <summary>
        /// Переход на главную страницу
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new mainWindowPage(Mv, idClient, "", "", ""));
        }
    }
}
