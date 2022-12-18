using Microsoft.Win32;
using Npgsql;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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
    /// Логика взаимодействия для financialReportPage.xaml
    /// </summary>
    public partial class financialReportPage : Page
    {
        string constr = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;";
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        List<string> id = new List<string>();
        List<string> names = new List<string>();
        List<string> id1 = new List<string>();
        List<string> names1 = new List<string>();
        int Role = 0;
        /// <summary>
        /// Инициализация окна
        /// </summary>
        /// <param name="MW">Экземпляр класса MainWindow</param>
        /// /// <param name="role">Переменная, содержащая обозначение роли пользователя</param>
        public financialReportPage(MainWindow MW, int role)
        {
            InitializeComponent();
            Mv = MW;
            load();
            Role = role;
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
        /// <summary>
        /// Загрузка данных
        /// </summary>
        public void load()
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            connection.Open();
            string com = "select * from Financial_report_View;";
            NpgsqlCommand command = new NpgsqlCommand(com, connection);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            dg1.ItemsSource = datatbl.DefaultView;
            connection.Close();
        }
        /// <summary>
        /// Посиск в базе данных
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            connection.Open();
            string com = $"select * from Financial_report_View where  \"ФИО сотрудника\" like '%{txtSearch.Text}%' or \"Банковский счет\" like '%{txtSearch.Text}%'";
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
                dpDateForm.Text = row["Дата формирования"].ToString();
                txtTotalIncome.Text = row["Поступлений всего"].ToString();
                txtTotalPayments.Text = row["Платежей всего"].ToString();
                txtRevenue.Text = row["Выручка"].ToString();
                txtGross_Profit.Text = row["Валовая прибыль"].ToString();
                txtOther.Text = row["Прочие расходы"].ToString();
                cmbFIO.Text = row["ФИО сотрудника"].ToString();
                cmbBankAccount.Text = row["Банковский счет"].ToString();
            }
            catch { MessageBox.Show("Ошибка"); }
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
                int a = names.IndexOf(cmbFIO.Text);
                string index = id[a];
                int b = names1.IndexOf(cmbBankAccount.Text);
                string index1 = id1[b];
                if (dpDateForm.Text != "" && txtTotalPayments.Text != "" && txtTotalIncome.Text != "" && txtRevenue.Text != "" && txtGross_Profit.Text != "" && txtOther.Text != "" && cmbFIO.Text != "" && cmbBankAccount.Text != "")
                {
                    connection.Open();
                    string com = $@"call Financial_report_insert ('{dpDateForm.Text}','{txtTotalIncome.Text}','{txtTotalPayments.Text}','{txtRevenue.Text}','{txtGross_Profit.Text}','{txtOther.Text}','{index}','{index1}')";
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
                int a = names.IndexOf(cmbFIO.Text);
                string index = id[a];
                int b = names1.IndexOf(cmbBankAccount.Text);
                string index1 = id1[b];
                if (row != null)
                {
                    if (dpDateForm.Text != "" && txtTotalIncome.Text != "" && txtTotalPayments.Text != "" && txtRevenue.Text != "" && txtGross_Profit.Text != "" && txtOther.Text != "" && cmbFIO.Text != "" && cmbBankAccount.Text != "")
                    {
                        connection.Open();
                        string com = $@"call Financial_report_update ({(int)row["Номер финансового отчета"]},'{dpDateForm.Text}','{txtTotalIncome.Text}','{txtTotalPayments.Text}','{txtRevenue.Text}','{txtGross_Profit.Text}','{txtOther.Text}','{index}','{index1}')";
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
                    string com = $"call Financial_report_delete ({(int)row["Номер финансового отчета"]})";
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
        /// <summary>
        /// Генерация документа для выгрузки по шаблону
        /// </summary>
        /// <returns>Документ</returns>
        private byte[] Generate()
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            DataRowView row = (DataRowView)dg1.SelectedItem;

            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand($@"select date_of_formation, total_income, total_payments, revenue, gross_profit, other_expenses, concat(surname , ' ' , first_name , ' ' , middle_name), employee_phone_number, name_of_the_bank, bank_account from financial_report join employee on employee_id=id_employee join company_account on company_account_id=id_company_account where id_financial_report = '{(int)row["Номер финансового отчета"]}'", connection);
            NpgsqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            dataReader.Read();
            string date_of_formation = dataReader[0].ToString();
            string total_income = dataReader[1].ToString();
            string total_payments = dataReader[2].ToString();
            string revenue = dataReader[3].ToString();
            string gross_profit = dataReader[4].ToString();
            string other_expenses = dataReader[5].ToString();
            string FIO = dataReader[6].ToString();
            string employee_phone_number = dataReader[7].ToString();
            string name_of_the_bank = dataReader[8].ToString();
            string bank_account = dataReader[9].ToString();
            connection.Close();
            connection.Open();
            command = new NpgsqlCommand($@"select * from income_with_payments('{total_income}', '{total_payments}', '{revenue}', '{gross_profit}', '{other_expenses}')", connection);
            dataReader = null;
            dataReader = command.ExecuteReader();
            dataReader.Read();
            string income_with_payments = dataReader[0].ToString();
            connection.Close();

            var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets
            .Add("Market Report");

            sheet.Column(2).Width = 40;
            sheet.Column(3).Width = 2;
            sheet.Column(4).Width = 40;
            sheet.Cells["C1"].Value = $"Финансовая отчетность oт {date_of_formation}";
            sheet.Cells["C1"].Style.Font.Bold = true;
            sheet.Cells["C1"].Style.Font.Size = 16;
            sheet.Cells["B2"].Value = "Выручка:";
            sheet.Cells["D2"].Value = revenue;
            sheet.Cells["B3"].Value = "Валовая прибыль:";
            sheet.Cells["D3"].Value = gross_profit;
            sheet.Cells["B4"].Value = "Прочие расходы:";
            sheet.Cells["D4"].Value = other_expenses;
            sheet.Cells["B5"].Value = "Поступлений всего:";
            sheet.Cells["D5"].Value = total_income;
            sheet.Cells["B6"].Value = "Платежей всего:";
            sheet.Cells["D6"].Value = total_payments;
            sheet.Cells["B7"].Value = "Доходов с учетом расходов:";
            sheet.Cells["B7"].Style.Font.Bold = true;
            sheet.Cells["D7"].Value = income_with_payments;
            sheet.Cells["D7"].Style.Font.Bold = true;
            sheet.Cells["B8"].Value = "Сотрудник:";
            sheet.Cells["D8"].Value = FIO;
            sheet.Cells["B9"].Value = "Телефонный номер сотрудника:";
            sheet.Cells["D9"].Value = employee_phone_number;
            sheet.Cells["B10"].Value = "Наименование банка:";
            sheet.Cells["D10"].Value = name_of_the_bank;
            sheet.Cells["B11"].Value = "Банковский счет:";
            sheet.Cells["D11"].Value = bank_account;

            sheet.Cells[1, 2, 11, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            sheet.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            sheet.Cells[1, 2, 11, 4].Style.Border.BorderAround(ExcelBorderStyle.Double);
            sheet.Cells[1, 2, 1, 4].Style.Border.BorderAround(ExcelBorderStyle.Double);
            sheet.Cells[2, 2, 6, 4].Style.Border.BorderAround(ExcelBorderStyle.Double);
            sheet.Cells[7, 2, 7, 4].Style.Border.BorderAround(ExcelBorderStyle.Double);
            sheet.Cells[8, 2, 9, 4].Style.Border.BorderAround(ExcelBorderStyle.Double);
            sheet.Cells[10, 2, 11, 4].Style.Border.BorderAround(ExcelBorderStyle.Double);

            sheet.Protection.IsProtected = true;
            return package.GetAsByteArray();
        }
        /// <summary>
        /// Выгрузка
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection(constr);
            DataRowView row = (DataRowView)dg1.SelectedItem;
            if (row != null)
            {
                var reportExcel = Generate();

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel |*.xlsx";
                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        File.WriteAllBytes(saveFileDialog.FileName, reportExcel);
                        MessageBox.Show("Выгрузка прошла успешно!");
                    }
                    catch
                    {
                        MessageBox.Show("Выберите другой путь");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите финансовый отчет в таблице");
            }
        }
    }
}
