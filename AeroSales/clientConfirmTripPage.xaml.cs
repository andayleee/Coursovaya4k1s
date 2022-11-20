using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Логика взаимодействия для clientConfirmTripPage.xaml
    /// </summary>
    public partial class clientConfirmTripPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static string constr = "Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;";
        string idCl = "";
        string idOr = "";
        NpgsqlConnection connect = new NpgsqlConnection(constr);
        public clientConfirmTripPage(MainWindow MW, string idClient, string idOrder)
        {
            InitializeComponent();
            Mv = MW;
            idCl = idClient;
            idOr = idOrder;
            spCode.Visibility = Visibility.Hidden;
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($@"select point_of_departure,point_of_arrival,to_char(flight_date, 'dd.mm.yyyy'),aircraft_type,time_of_departure, arrival_time,cost_ticket, id_flight from orderr join ticket on orderr_id=id_orderr join flight on flight_id=id_flight where id_orderr='{idOr}'", connect);
            NpgsqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            dataReader.Read();
            lbHeader.Content = dataReader[0].ToString() + " - " + dataReader[1].ToString() + "  " + dataReader[2].ToString();
            lbPoints.Content = dataReader[0].ToString() + " - " + dataReader[1].ToString();
            lbPointFrom.Content = dataReader[0].ToString();
            lbPointTo.Content = dataReader[1].ToString();
            lbAirType.Content = dataReader[3].ToString();
            lbTimeFrom.Content = dataReader[4].ToString();
            lbTimeTo.Content = dataReader[5].ToString();
            int count = (int)dataReader[6];
            string idFl = dataReader[7].ToString();
            TimeSpan timeFrom = (TimeSpan)dataReader[4];
            TimeSpan timeTo = (TimeSpan)dataReader[5];
            if (timeFrom > timeTo)
            {
                TimeSpan ts1 = new TimeSpan(24, 0, 0);
                TimeSpan ts2 = ts1 - timeFrom;
                lbTripTime.Content = (ts2 + timeTo).ToString();
            }
            else
            {
                lbTripTime.Content = (timeTo - timeFrom).ToString();
            }
            connect.Close();
            connect.Open();
            DataTable datatbl = new DataTable();
            NpgsqlDataAdapter mySqlDataAdapter = new NpgsqlDataAdapter($"select place as \"place\" from flight_seat join seat on seat_id=id_seat where client_id='{idCl}' and flight_id='{idFl}'", connect);
            // mySqlDataAdapter1.Fill(datatbl1);
            mySqlDataAdapter.Fill(datatbl);
            dg1.ItemsSource = datatbl.DefaultView;
            connect.Close();

            connect.Open();
            command = new NpgsqlCommand($"SELECT count(*) as \"Количество\" FROM flight_seat join seat on seat_id=id_seat where client_id='{idCl}' and flight_id='{idFl}'", connect);
            string countOfTickets = command.ExecuteScalar().ToString();
            connect.Close();
            lbCount.Content = count * Convert.ToInt32(countOfTickets);

            connect.Open();
            command = new NpgsqlCommand($@"select* from client where id_client='{idCl}'", connect);
            dataReader = null;
            dataReader = command.ExecuteReader();
            dataReader.Read();
            txtSurname.Content = dataReader[2].ToString();
            txtName.Content = dataReader[3].ToString();
            txtMiddleName.Content = dataReader[4].ToString();
            txtPhoneNum.Text = dataReader[1].ToString();
            dpDateBirth.Text = dataReader[7].ToString();
            txtEmail.Text = dataReader[11].ToString();
            txtPassNum.Text = dataReader[9].ToString();
            txtPassSer.Text = dataReader[8].ToString();
            connect.Close();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Mv.MainFrame.NavigationService.Navigate(new clientTripPage(Mv, idCl));
        }

        private async void btnEmailSend_Click(object sender, RoutedEventArgs e)
        {
            spCode.Visibility = Visibility.Visible;
            btnEmailSend.IsEnabled = false;
            await SendEmailAsync("вфмм", "Подтверждение оплаты", txtEmail.Text);
            //// отправитель - устанавливаем адрес и отображаемое в письме имя
            //MailAddress from = new MailAddress("isip_a.a.aksenov@mpt.ru", "AeroSales");
            //// кому отправляем
            //MailAddress to = new MailAddress(txtEmail.Text);
            //// создаем объект сообщения
            //MailMessage m = new MailMessage(from, to);
            //// тема письма
            //m.Subject = "Подтверждение оплаты";
            //// текст письма
            //m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
            //// письмо представляет код html
            //m.IsBodyHtml = true;
            //// адрес smtp-сервера и порт, с которого будем отправлять письмо
            //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            //// логин и пароль
            //smtp.Credentials = new NetworkCredential("isip_a.a.aksenov@mpt.ru", "lh.gf2003");
            //smtp.EnableSsl = true;
            //smtp.Send(m);



        }
        private async Task SendEmailAsync(string bodyMail, string subject, string to_address)
        {
            string mailsender = "isip_a.a.aksenov@mpt.ru";
            string pass = "lh.gf2003";
            string smtpserver = "smtp.gmail.com";
            int port = 587;

            MailAddress fromAddress = new MailAddress(mailsender, "AeroSales");
            MailAddress toAddress = new MailAddress(to_address);

            var smtp = new SmtpClient
            {
                Host = smtpserver,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, pass),
                Timeout = 20000
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = bodyMail,
                IsBodyHtml = bodyMail.ToLower().Contains("html") ? true : false // если в тексте есть "html" отправляем как HTML, в противном случае как текст
            })
            {
                try
                {
                    await smtp.SendMailAsync(message);
                }
                catch (Exception ex)
                {
                    smtp.Dispose();

                    MessageBox.Show(ex.Message, "Ошибка отправки");
                }
            }
        }
    }
}
