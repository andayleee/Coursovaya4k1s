using Microsoft.Win32;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для adminPage.xaml
    /// </summary>
    public partial class adminPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static string constr = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;";
        NpgsqlConnection connection = new NpgsqlConnection(constr);
        int Role = 0;
        /// <summary>
        /// Инициализация окна
        /// </summary>
        /// <param name="MW">Экземпляр класса MainWindow</param>
        /// <param name="role">Переменная, содержащая обозначение роли пользователя</param>
        public adminPage(MainWindow MW, int role)
        {
            InitializeComponent();
            Mv = MW;
            Role = role;
            btnFinanciatReport.Visibility = Visibility.Hidden;
            btnTaxReturn.Visibility = Visibility.Hidden;
            btnCompanyAccount.Visibility = Visibility.Hidden;
            btnFlight.Visibility = Visibility.Hidden;
            btnFlightList.Visibility = Visibility.Hidden;
            btnContract.Visibility = Visibility.Hidden;
            btnPost.Visibility = Visibility.Hidden;
            btnEmployee.Visibility = Visibility.Hidden;
            btnTicketList.Visibility = Visibility.Hidden;
            btnGraf.Visibility = Visibility.Hidden;
            btnRezervnoye.Visibility = Visibility.Hidden;
            btnBackUp.Visibility = Visibility.Hidden;
            if (role == 2)
            {
                btnFinanciatReport.Visibility = Visibility.Visible;
                btnTaxReturn.Visibility = Visibility.Visible;
                btnCompanyAccount.Visibility = Visibility.Visible;
            } 
            else if (role == 3)
            {
                btnFlight.Visibility = Visibility.Visible;
                btnFlightList.Visibility = Visibility.Visible;
                btnContract.Visibility = Visibility.Visible;
            }
            else if (role == 4)
            {
                btnPost.Visibility = Visibility.Visible;
                btnEmployee.Visibility = Visibility.Visible;
                btnTicketList.Visibility = Visibility.Visible;
            }
            else
            {
                btnFinanciatReport.Visibility = Visibility.Visible;
                btnTaxReturn.Visibility = Visibility.Visible;
                btnCompanyAccount.Visibility = Visibility.Visible;
                btnFlight.Visibility = Visibility.Visible;
                btnFlightList.Visibility = Visibility.Visible;
                btnContract.Visibility = Visibility.Visible;
                btnPost.Visibility = Visibility.Visible;
                btnEmployee.Visibility = Visibility.Visible;
                btnTicketList.Visibility = Visibility.Visible;
                btnGraf.Visibility = Visibility.Visible;
                btnRezervnoye.Visibility = Visibility.Visible;
                btnBackUp.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// Переход на страницу авторизации
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new authorization(Mv));
        }
        /// <summary>
        /// Переход на страницу должностей
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new postPage(Mv, Role));
        }
        /// <summary>
        /// Переход на страницу сотрудников
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new employeePage(Mv, Role));
        }
        /// <summary>
        /// Переход на страницу списков рейса
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnFlightList_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new flightListPage(Mv, Role));
        }
        /// <summary>
        /// Переход на страницу контрактов
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnContract_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new contractPage(Mv, Role));
        }
        /// <summary>
        /// Переход на страницу рейсов
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnFlight_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new flightPage(Mv, Role));
        }
        /// <summary>
        /// Переход на страницу списков билетов
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnTicketList_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new ticketListPage(Mv, Role));
        }
        /// <summary>
        /// Переход на страницу счетов компании
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnCompanyAccount_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new companyAccountPage(Mv, Role));
        }
        /// <summary>
        /// Переход на страницу финансовой отчетности
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnFinanciatReport_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new financialReportPage(Mv, Role));
        }
        /// <summary>
        /// Переход на страницу налоговой отчетности
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnTaxReturn_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new taxReturnPage(Mv, Role));
        }
        /// <summary>
        /// Переход на страницу отображения столбчатой диаграммы
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnGraf_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new graphPage(Mv, Role));
        }
        /// <summary>
        /// Резервное копирование
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnRezervnoye_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "BACKUP Files|*.backup*"
            };
            dialog.Title = "Save as backup file";
            if (dialog.ShowDialog() == true)
            {
                Backup(dialog.FileName + ".backup");
            }
        }
        string strPG_dumpPath = "SET PGPASSWORD=a\r\n\r\ncd /D C:\\Program Files\r\n\r\ncd PostgreSQL\r\n\r\ncd 13\r\n\r\ncd bin\r\n\r\n";
        string strServer = "localhost";
        string strPort = "5432";
        string strDatabaseName = "AeroSales";
        /// <summary>
        /// Резервное копирование
        /// </summary>
        /// <param name="pathSave">Путь сохраненной базы данных</param>
        private void Backup(string pathSave)
        {
            try
            {
                StreamWriter sw = new StreamWriter("DBBackup.bat");
                StringBuilder strSB = new StringBuilder(strPG_dumpPath);
                if (strSB.Length != 0)
                {
                    strSB.Append("pg_dump.exe --host " + strServer + " --port " + strPort + " --username postgres --format custom --blobs --verbose --file ");
                    strSB.Append("\"" + pathSave + "\"");
                    strSB.Append(" \"" + strDatabaseName + "\"" + "\r\n\r\n");
                    sw.WriteLine(strSB);
                    sw.Dispose();
                    sw.Close();
                    Process processDB = Process.Start("DBBackup.bat");
                    MessageBox.Show("Резервная копия успешно сохранена");
                }
                else
                {
                    MessageBox.Show("Пожалуйста, укажите место для создания резервной копии");
                }
            }
            catch
            { }
        }
        /// <summary>
        /// Бэкап базы данных
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">Экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnBackUp_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "BACKUP Files|*.backup*"
            };
            dialog.Title = "Choose backup file";
            if (dialog.ShowDialog() == true)
            {
                Restore(dialog.FileName);
            }
        }
        /// <summary>
        /// Бэкап базы данных
        /// </summary>
        /// <param name="pathFile">Путь выбранного файла</param>
        public void Restore(string pathFile)
        {
            strDatabaseName = "AeroSalesRestore";
            connection.Open();
            try
            {
                if (new NpgsqlCommand("SELECT 1 as count FROM pg_database WHERE datname='AeroSalesRestore'", connection).ExecuteScalar().ToString() == "1")
                {
                    new NpgsqlCommand("DROP DATABASE \"AeroSalesRestore\";", connection).ExecuteNonQuery();
                }
            }
            catch { }
            NpgsqlCommand command = new NpgsqlCommand("Create DATABASE \"AeroSalesRestore\";", connection);
            command.ExecuteNonQuery();
            connection.Close();
            try
            {
                if (strDatabaseName != "")
                {
                    if (pathFile != "")
                    {
                        StreamWriter sw = new StreamWriter("DBRestore.bat");
                        StringBuilder strSB = new StringBuilder(strPG_dumpPath);
                        if (strSB.Length != 0)
                        {
                            strSB.Append("pg_restore.exe --host " + strServer +
                               " --port " + strPort + " --username postgres --dbname");
                            strSB.Append(" \"" + strDatabaseName + "\"");
                            strSB.Append(" --verbose ");
                            strSB.Append("\"" + pathFile + "\"");
                            sw.WriteLine(strSB);
                            sw.Dispose();
                            sw.Close();
                            Process processDB = Process.Start("DBRestore.bat");
                            MessageBox.Show("Успешный BackUp данных");
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста, введите путь сохранения, чтобы получить резервную копию");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите имя базы данных!");
                }
            }
            catch
            {
                MessageBox.Show("База данных занята другой программой"); 
                connection.Close();
            }
        }
    }
}
