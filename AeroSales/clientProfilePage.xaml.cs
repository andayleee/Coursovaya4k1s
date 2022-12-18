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
    /// Логика взаимодействия для clientProfilePage.xaml
    /// </summary>
    public partial class clientProfilePage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static string constr = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;";
        string idCl = "";
        NpgsqlConnection connect = new NpgsqlConnection(constr);
        string login = "";
        string password = "";
        string codeword = "";
        /// <summary>
        /// Инициализация окна
        /// </summary>
        /// <param name="MW">Экземпляр класса MainWindow</param>
        /// /// <param name="idClient">Переменная, содержащая обозначение роли пользователя</param>
        public clientProfilePage(MainWindow MW, string idClient)
        {
            InitializeComponent();
            Mv = MW;
            idCl = idClient;
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($@"select* from client where id_client='{idCl}'", connect);
            NpgsqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            dataReader.Read();
            txtSurname.Text = dataReader[2].ToString();
            txtName.Text = dataReader[3].ToString();
            txtMiddleName.Text = dataReader[4].ToString();
            txtPhoneNum.Text = dataReader[1].ToString();
            dpDateBirth.Text = dataReader[7].ToString();
            txtEmail.Text = dataReader[11].ToString();
            txtPassNum.Text = dataReader[9].ToString();
            txtPassSer.Text = dataReader[8].ToString();
            login = dataReader[5].ToString();
            password = dataReader[6].ToString();
            codeword = dataReader[10].ToString();
            connect.Close();
            lbComplete.Visibility = Visibility.Hidden;
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
        /// Сохранения настроек
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(txtSurname.Text!=""&& txtName.Text != "" && txtEmail.Text != "" && !txtPhoneNum.Text.Contains("_") && dpDateBirth.Text != "" && !txtPassNum.Text.Contains("_") && !txtPassSer.Text.Contains("_"))
            {
                if (txtEmail.Text.Contains("@")&& txtEmail.Text.Contains("."))
                {
                    NpgsqlConnection connection = new NpgsqlConnection(constr);
                    try
                    {
                        if (idCl!="")
                        {
                            connection.Open();
                            string com = $@"call client_update ({idCl},'{txtPhoneNum.Text}','{txtSurname.Text}','{txtName.Text}','{txtMiddleName.Text}','{login}','{password}','{dpDateBirth.SelectedDate.Value.Date.ToString("yyyy.MM.dd")}','{txtPassSer.Text}','{txtPassNum.Text}','{txtEmail.Text}','{codeword}')";
                            NpgsqlCommand command = new NpgsqlCommand(com, connection);
                            command.ExecuteNonQuery();
                            lbComplete.Visibility = Visibility.Visible;
                        }
                        else { MessageBox.Show("Проблема с идентификацией пользователя"); }
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
                    MessageBox.Show("Некорректный ввод почты");
                }
            }
            else
            {
                MessageBox.Show("Введите все данные");
            }
        }
    }
}
