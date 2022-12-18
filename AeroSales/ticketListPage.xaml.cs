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
    /// Логика взаимодействия для ticketListPage.xaml
    /// </summary>
    public partial class ticketListPage : Page
    {
        string constr = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;";
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        int Role = 0;
        /// <summary>
        /// Инициализация окна
        /// </summary>
        /// <param name="MW">Экземпляр класса MainWindow</param>
        /// /// <param name="role">Переменная, содержащая обозначение роли пользователя</param>
        public ticketListPage(MainWindow MW, int role)
        {
            InitializeComponent();
            Mv = MW;
            Role = role;
            load();
        }
        /// <summary>
        /// Загрузка данных
        /// </summary>
        public void load()
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            connection.Open();
            string com = "select * from Ticket_List_View;";
            NpgsqlCommand command = new NpgsqlCommand(com, connection);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            dg1.ItemsSource = datatbl.DefaultView;
            connection.Close();
        }
        /// <summary>
        /// Выбор в датагриде
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dg1.SelectedItem == null) return;
                DataRowView row = (DataRowView)dg1.SelectedItem;
                DatePicker.Text = row["Дата формирования"].ToString();
            }
            catch { MessageBox.Show("Ошибка"); }
        }
        /// <summary>
        /// Посиск в базе данных
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txtSearch.Text.Contains('_'))
            {
                NpgsqlConnection connection = new NpgsqlConnection(constr);
                connection.Open();
                string com = $"select * from Ticket_List_View where \"Дата формирования\" >= '%{txtSearch.Text}%'";
                NpgsqlCommand command = new NpgsqlCommand(com, connection);
                DataTable datatbl = new DataTable();
                datatbl.Load(command.ExecuteReader());
                dg1.ItemsSource = datatbl.DefaultView;
                connection.Close();
            }
            else
            {
                load();
            }
        }
        /// <summary>
        /// Добавление данных
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            try
            {
                if (DatePicker.Text != "")
                {
                    connection.Open();
                    string com = $@"call Ticket_List_insert ('{DatePicker.Text}')";
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
        /// Изменение данных
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            DataRowView row = (DataRowView)dg1.SelectedItem;
            try
            {
                if (row != null)
                {
                    if (DatePicker.Text != "")
                    {
                        connection.Open();
                        string com = $@"call Ticket_List_update ({(int)row["Номер списка билетов"]},'{DatePicker.Text}')";
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
        /// Удаление данных
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            DataRowView row = (DataRowView)dg1.SelectedItem;
            try
            {
                if (row != null)
                {
                    connection.Open();
                    string com = $"call Ticket_List_delete ({(int)row["Номер списка билетов"]})";
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
        /// <summary>
        /// Переход на страницу назад
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new adminPage(Mv, Role));
        }
    }
}
