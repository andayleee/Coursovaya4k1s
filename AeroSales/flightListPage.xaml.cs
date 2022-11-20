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
    /// Логика взаимодействия для flightListPage.xaml
    /// </summary>
    public partial class flightListPage : Page
    {
        string constr = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;";
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public flightListPage(MainWindow MW)
        {
            InitializeComponent();
            Mv = MW;
            load();
        }
        public void load()
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            connection.Open();
            string com = "select * from Flight_List_View;";
            NpgsqlCommand command = new NpgsqlCommand(com, connection);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            dg1.ItemsSource = datatbl.DefaultView;
            connection.Close();
        }
        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dg1.SelectedItem == null) return;
                DataRowView row = (DataRowView)dg1.SelectedItem;
                DatePicker.Text = row["Дата составления"].ToString();
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txtSearch.Text.Contains('_')) {
                NpgsqlConnection connection = new NpgsqlConnection(constr);
                connection.Open();
                string com = $"select * from Flight_List_View where \"Дата составления\" >= '%{txtSearch.Text}%'";
                NpgsqlCommand command = new NpgsqlCommand(com, connection);
                DataTable datatbl = new DataTable();
                datatbl.Load(command.ExecuteReader());
                dg1.ItemsSource = datatbl.DefaultView;
                connection.Close();
            }
            else {
                load();
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            try
            {
                if (DatePicker.Text != "")
                {
                    connection.Open();
                    string com = $@"call Flight_List_insert ('{DatePicker.Text}')";
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
                        string com = $@"call Flight_List_update ({(int)row["Код списка рейсов"]},'{DatePicker.Text}')";
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            DataRowView row = (DataRowView)dg1.SelectedItem;
            try
            {
                if (row != null)
                {
                    connection.Open();
                    string com = $"call Flight_List_delete ({(int)row["Код списка рейсов"]})";
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new adminPage(Mv));
        }
    }
}
