﻿using Npgsql;
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
    /// Логика взаимодействия для taxReturnPage.xaml
    /// </summary>
    public partial class taxReturnPage : Page
    {
        string constr = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;";
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        List<string> id = new List<string>();
        List<string> names = new List<string>();
        List<string> id1 = new List<string>();
        List<string> names1 = new List<string>();
        public taxReturnPage(MainWindow MW)
        {
            InitializeComponent();
            Mv = MW;
            load();
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            connection.Open();
            NpgsqlDataReader dataReader = null;
            string sql = $@"SELECT * FROM Employee";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                cmbFIO.Items.Add(dataReader[$@"Surname"].ToString() + " " + dataReader[$@"First_Name"].ToString() + " " + dataReader[$@"Middle_name"].ToString());
                id.Add(dataReader[$@"ID_Employee"].ToString());
                names.Add(dataReader[$@"Surname"].ToString() + " " + dataReader[$@"First_Name"].ToString() + " " + dataReader[$@"Middle_name"].ToString());
            }
            connection.Close();

            connection = new NpgsqlConnection(constr);
            connection.Open();
            dataReader = null;
            sql = $@"SELECT * FROM company_account";
            command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                cmbBankAccount.Items.Add(dataReader[$@"bank_account"].ToString());
                id1.Add(dataReader[$@"ID_Company_account"].ToString());
                names1.Add(dataReader[$@"bank_account"].ToString());
            }
            connection.Close();
        }
        public void load()
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            connection.Open();
            string com = "select * from tax_return_View;";
            NpgsqlCommand command = new NpgsqlCommand(com, connection);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            dg1.ItemsSource = datatbl.DefaultView;
            connection.Close();
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            connection.Open();
            string com = $"select * from tax_return_View where \"Наименование организации\" like '%{txtSearch.Text}%' or \"Контактный номер\" like '%{txtSearch.Text}%' or \"ИНН\" like '%{txtSearch.Text}%' or \"КПП\" like '%{txtSearch.Text}%' or \"ФИО сотруника\" like '%{txtSearch.Text}%' or \"Банковский счет\" like '%{txtSearch.Text}%'";
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
                txtNameOfCompany.Text = row["Наименование организации"].ToString();
                txtPhoneNum.Text = row["Контактный номер"].ToString();
                dpDateofForation.Text = row["Дата формирования"].ToString();
                txtINN.Text = row["ИНН"].ToString();
                txtKPP.Text = row["КПП"].ToString();
                txtAll.Text = row["Сумма налога подлежащая к оплате"].ToString();
                cmbFIO.Text = row["ФИО сотруника"].ToString();
                cmbBankAccount.Text = row["Банковский счет"].ToString();
            }
            catch { MessageBox.Show("Ошибка"); }
        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            try
            {
                int a = names.IndexOf(cmbFIO.Text);
                string index = id[a];
                int b = names1.IndexOf(cmbBankAccount.Text);
                string index1 = id1[b];
                if (txtNameOfCompany.Text != "" && !txtPhoneNum.Text.Contains("_") && dpDateofForation.Text != "" && !txtINN.Text.Contains("_") && !txtKPP.Text.Contains("_") && txtAll.Text != "" && cmbFIO.Text != "" && cmbBankAccount.Text != "")
                {
                    connection.Open();
                    string com = $@"call tax_return_insert ('{txtNameOfCompany.Text}','{txtPhoneNum.Text}','{dpDateofForation.Text}','{txtINN.Text}','{txtKPP.Text}','{txtAll.Text}','{index}','{index1}')";
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
                int a = names.IndexOf(cmbFIO.Text);
                string index = id[a];
                int b = names1.IndexOf(cmbBankAccount.Text);
                string index1 = id1[b];
                if (row != null)
                {
                    if (txtNameOfCompany.Text != "" && !txtPhoneNum.Text.Contains("_") && dpDateofForation.Text != "" && !txtINN.Text.Contains("_") && !txtKPP.Text.Contains("_") && txtAll.Text != "" && cmbFIO.Text != "" && cmbBankAccount.Text != "")
                    {
                        connection.Open();
                        string com = $@"call tax_return_update ({(int)row["Номер налоговой декларации"]},'{txtNameOfCompany.Text}','{txtPhoneNum.Text}','{dpDateofForation.Text}','{txtINN.Text}','{txtKPP.Text}','{txtAll.Text}','{index}','{index1}')";
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
                    string com = $"call tax_return_delete ({(int)row["Номер налоговой декларации"]})";
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
