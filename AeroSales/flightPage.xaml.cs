using Microsoft.Win32;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Логика взаимодействия для flightPage.xaml
    /// </summary>
    public partial class flightPage : Page
    {
        string constr = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;";
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        List<string> id = new List<string>();
        List<string> names = new List<string>();
        List<string> id1 = new List<string>();
        List<string> names1 = new List<string>();
        DataTable datatbl = new DataTable();
        int Role = 0;
        public flightPage(MainWindow MW, int role)
        {
            InitializeComponent();
            Mv = MW;
            Role = role;
            load();
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            connection.Open();
            NpgsqlDataReader dataReader = null;
            string sql = $@"SELECT * FROM Flight_List";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                cmbFliList.Items.Add(dataReader[$@"ID_Flight_List"].ToString());
                id.Add(dataReader[$@"ID_Flight_List"].ToString());
                names.Add(dataReader[$@"ID_Flight_List"].ToString());
            }
            connection.Close();

            connection = new NpgsqlConnection(constr);
            connection.Open();
            dataReader = null;
            sql = $@"SELECT * FROM Contract";
            command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                cmbNameCo.Items.Add(dataReader[$@"carrier_company_name"].ToString());
                id1.Add(dataReader[$@"ID_Contract"].ToString());
                names1.Add(dataReader[$@"carrier_company_name"].ToString());
            }
            connection.Close();
        }
        public void load()
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            connection.Open();
            string com = "select * from Flight_View;";
            NpgsqlCommand command = new NpgsqlCommand(com, connection);
            datatbl.Load(command.ExecuteReader());
            dg1.ItemsSource = datatbl.DefaultView;
            connection.Close();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            connection.Open();
            string com = $"select * from Flight_View where \"Пункт отправления\" like '%{txtSearch.Text}%' or \"Пункт прибытия\" like '%{txtSearch.Text}%'";
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
                DateofGo.Text = row["Дата рейса"].ToString();
                txtTimeofDeparture.Text = row["Время отправления"].ToString();
                txtArrivalTime.Text = row["Время прибытия"].ToString();
                txtBookingTime.Text = row["Время бронирования"].ToString();
                txtPointofDeparture.Text = row["Пункт отправления"].ToString();
                txtPointofArrival.Text = row["Пункт прибытия"].ToString();
                txtAircraftType.Text = row["Тип воздушного судна"].ToString();
                txtCost.Text = row["Стоимость"].ToString();
                cmbFliList.Text = row["Номер списка рейсов"].ToString();
                cmbNameCo.Text = row["Наименование компании перевозчик"].ToString();
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            try
            {
                int a = names.IndexOf(cmbFliList.Text);
                string index = id[a];
                int b = names1.IndexOf(cmbNameCo.Text);
                string index1 = id1[b];
                if (DateofGo.Text != "" && txtPointofArrival.Text != "" && txtPointofDeparture.Text != "" && cmbNameCo.Text != "" && cmbFliList.Text != "" && txtBookingTime.Text != "" && txtArrivalTime.Text != "" && txtTimeofDeparture.Text != "" && txtAircraftType.Text != "" && txtCost.Text != "")
                {
                    connection.Open();
                    string com = $@"call flight_insert ('{DateofGo.Text}','{txtTimeofDeparture.Text}','{txtArrivalTime.Text}','{txtPointofDeparture.Text}','{txtPointofArrival.Text}','{txtBookingTime.Text}','{txtAircraftType.Text}','{txtCost.Text}','{index1}','{index}')";
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
                int a = names.IndexOf(cmbFliList.Text);
                string index = id[a];
                int b = names1.IndexOf(cmbNameCo.Text);
                string index1 = id1[b];
                if (row != null)
                {
                    if (DateofGo.Text != "" && txtPointofArrival.Text != "" && txtPointofDeparture.Text != "" && cmbNameCo.Text != "" && cmbFliList.Text != "" && txtBookingTime.Text != "" && txtArrivalTime.Text != "" && txtTimeofDeparture.Text != "" && txtAircraftType.Text != "" && txtCost.Text != "")
                    {
                        connection.Open();
                        string com = $@"call flight_update ({(int)row["Код рейса"]},'{DateofGo.Text}','{txtTimeofDeparture.Text}','{txtArrivalTime.Text}','{txtPointofDeparture.Text}','{txtPointofArrival.Text}','{txtBookingTime.Text}','{txtAircraftType.Text}','{txtCost.Text}','{index1}','{index}')";
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
                    string com = $"call flight_delete ({(int)row["Код рейса"]})";
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
            Mv.MainFrame.NavigationService.Navigate(new adminPage(Mv, Role));
        }

        private void btncsv_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files (.csv)|*.csv";
            saveFileDialog.InitialDirectory = @"C:\Users\Дмитрий\Desktop";

            if (saveFileDialog.ShowDialog() == true)
            {
                ToCSV(datatbl, saveFileDialog.FileName);
            }

            MessageBox.Show("Выгрузка в CSV прошла успешно");
        }
        /// <summary>
        /// Выгрузка в CSV
        /// </summary>
        /// <param name="dtDataTable">Данные из Datagrid</param>
        /// <param name="strFilePath">Путь до файла</param>
        public static void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}
