using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для employeePage.xaml
    /// </summary>
    public partial class employeePage : Page
    {
        string constr = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;";
        List<string> id = new List<string>();
        List<string> names = new List<string>();
        int Role = 0;
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        /// <summary>
        /// Отвечает за инициализацию окна, вызов метода load и загрузка данных в ComboBox
        /// </summary>
        /// <param name="MW">Ссылка на главное окно</param>
        public employeePage(MainWindow MW, int role)
        {
            InitializeComponent();
            Mv = MW;
            Role = role;
            load();
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            connection.Open();
            NpgsqlDataReader dataReader = null;
            string sql = $@"SELECT * FROM post";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                cmdPost.Items.Add(dataReader[$@"Name_of_Post"].ToString());
                id.Add(dataReader[$@"ID_post"].ToString());
                names.Add(dataReader[$@"Name_of_Post"].ToString());
            }
            connection.Close();
        }
        /// <summary>
        /// Отвечает за загрузку данных из базы данных в datagrid
        /// </summary>
        public void load()
        {

            NpgsqlConnection connection = new NpgsqlConnection(constr);
            connection.Open();
            string com = "select * from Employee_View;";
            NpgsqlCommand command = new NpgsqlCommand(com, connection);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            dg1.ItemsSource = datatbl.DefaultView;
            connection.Close();
        }
        /// <summary>
        /// Отвечает за переход на предыдущее окно
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных</param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new adminPage(Mv, Role));
        }
        /// <summary>
        /// Отвечает за добавление данных из базы данных в элементы окна при нажатии на элемент в dategrid 
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных</param>
        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dg1.SelectedItem == null) return;
                DataRowView row = (DataRowView)dg1.SelectedItem;
                txtSurname.Text = row["Фамилия"].ToString();
                txtName.Text = row["Имя"].ToString();
                txtMiddleName.Text = row["Отчество"].ToString();
                txtLogin.Text = row["Логин"].ToString();
                txtSnils.Text = row["СНИЛС"].ToString();
                txtINN.Text = row["ИНН"].ToString();
                txtDateofBirth.Text = row["Дата роджения"].ToString();
                txtPassNum.Text = row["Номер паспорта"].ToString();
                txtPassSer.Text = row["Серия паспорта"].ToString();
                txtPhoneNum.Text = row["Номер телефона"].ToString();
                cmdPost.Text = row["Должность"].ToString();
                txtPolis.Text = row["Полис ОМС"].ToString();
            }
            catch { MessageBox.Show("Ошибка"); }
        }
        /// <summary>
        /// Отвечает за поиск данных в базе данных
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных</param>
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            connection.Open();
            string com = $"select * from Employee_View where \"Фамилия\" like '%{txtSearch.Text}%' or \"Имя\" like '%{txtSearch.Text}%' or \"Отчество\" like '%{txtSearch.Text}%' or \"Логин\" like '%{txtSearch.Text}%' or \"Пароль\" like '%{txtSearch.Text}%' or \"СНИЛС\" like '%{txtSearch.Text}%' or \"ИНН\" like '%{txtSearch.Text}%' or \"Номер паспорта\" like '%{txtSearch.Text}%' or \"Серия паспорта\" like '%{txtSearch.Text}%' or \"Номер телефона\" like '%{txtSearch.Text}%' or \"Должность\" like '%{txtSearch.Text}%' or \"Полис ОМС\" like '%{txtSearch.Text}%'";
            NpgsqlCommand command = new NpgsqlCommand(com, connection);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            dg1.ItemsSource = datatbl.DefaultView;
            connection.Close();
        }
        /// <summary>
        /// Отвечает за добавление данных в базу данных
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных</param>
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(txtPassword.Password.ToString()));
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            try
            {
                int a = names.IndexOf(cmdPost.Text);
                string index = id[a];
                if (txtSurname.Text != "" && txtName.Text != "" && txtLogin.Text != "" && txtPassword.Password.ToString() != "" && txtDateofBirth.Text != "" && !txtPassSer.Text.Contains("_") && !txtPassNum.Text.Contains("_") && cmdPost.Text != "" && !txtPhoneNum.Text.Contains("_") && !txtPolis.Text.Contains("_") && !txtSnils.Text.Contains("_") && !txtINN.Text.Contains("_"))
                {
                    connection.Open();
                    string com = $@"call employee_insert ('{txtPhoneNum.Text}','{txtSurname.Text}','{txtName.Text}','{txtMiddleName.Text}','{txtLogin.Text}','{Convert.ToBase64String(hash)}','{txtDateofBirth.SelectedDate.Value.Date.ToString("yyyy.MM.dd")}','{txtPassSer.Text}','{txtPassNum.Text}','{txtSnils.Text}','{txtPolis.Text}','{txtINN.Text}','{index}')";
                    NpgsqlCommand command = new NpgsqlCommand(com, connection);
                    command.ExecuteNonQuery();
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
                load();
            }
        }
        /// <summary>
        /// Отвечает за изменение данных в базе данных
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных</param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(txtPassword.Password.ToString()));
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            DataRowView row = (DataRowView)dg1.SelectedItem;
            try
            {
                int a = names.IndexOf(cmdPost.Text);
                string index = id[a];
                if (row != null)
                {
                    if (txtSurname.Text != "" && txtName.Text != "" && txtLogin.Text != "" && txtPassword.Password.ToString() != "" && txtDateofBirth.Text != "" && !txtPassSer.Text.Contains("_") && !txtPassNum.Text.Contains("_") && cmdPost.Text != "" && !txtPhoneNum.Text.Contains("_") && !txtPolis.Text.Contains("_") && !txtSnils.Text.Contains("_") && !txtINN.Text.Contains("_"))
                    {
                        connection.Open();
                        string com = $@"call Employee_update ({(int)row["Код сотрудника"]},'{txtPhoneNum.Text}','{txtSurname.Text}','{txtName.Text}','{txtMiddleName.Text}','{txtLogin.Text}','{Convert.ToBase64String(hash)}','{txtDateofBirth.SelectedDate.Value.Date.ToString("yyyy.MM.dd")}','{txtPassSer.Text}','{txtPassNum.Text}','{txtSnils.Text}','{txtPolis.Text}','{txtINN.Text}','{index}')";
                        NpgsqlCommand command = new NpgsqlCommand(com, connection);
                        command.ExecuteNonQuery();
                    }
                    else { MessageBox.Show("Заполните данные!"); }
                }
                else { MessageBox.Show("Элемент не выбран"); }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                load();
            }
        }
        /// <summary>
        /// Отвечает за удаление данных в базе данных
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных</param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            DataRowView row = (DataRowView)dg1.SelectedItem;
            try
            {
                if (row != null)
                {
                    connection.Open();
                    string com = $@"call Employee_delete ({(int)row["Код сотрудника"]})";
                    NpgsqlCommand command = new NpgsqlCommand(com, connection);
                    command.ExecuteNonQuery();
                }
                else { MessageBox.Show("Элемент не выбран"); }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                load();
            }
        }
    }
}
