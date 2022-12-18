using LiveCharts;
using LiveCharts.Wpf;
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
    /// Логика взаимодействия для graphPage.xaml
    /// </summary>
    public partial class graphPage : Page
    {
        MainWindow Mv = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        NpgsqlCommand command = new NpgsqlCommand();
        DataTable dataTable = new DataTable();
        List<string> list = new List<string>();
        List<double> list1 = new List<double>();
        List<double> list2 = new List<double>();
        List<double> list3 = new List<double>();
        int Role = 0;
        /// <summary>
        /// Инициализация окна
        /// </summary>
        /// <param name="MW">Экземпляр класса MainWindow</param>
        /// /// <param name="role">Переменная, содержащая обозначение роли пользователя</param>
        public graphPage(MainWindow MW, int role)
        {
            InitializeComponent();
            Mv = MW;
            Role = role;
            connectionString = new NpgsqlConnection("Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;");
            connectionString.Open();
            command = new NpgsqlCommand($"select * from Financial_report_View;", connectionString);
            dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            NpgsqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                list.Add(dataReader[$@"Номер финансового отчета"].ToString());
                list1.Add(Convert.ToDouble(dataReader[$@"Доходов с учетом расходов"]));
                list2.Add(Convert.ToDouble(dataReader[$@"Поступлений всего"]));
                list3.Add(Convert.ToDouble(dataReader[$@"Платежей всего"]));
            }
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Доходов с учетом расходов",
                    Values = new ChartValues<double>(list1),
                    Fill = Brushes.Orange
                }
            };
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Доходов всего",
                Values = new ChartValues<double>(list2),
                Fill = Brushes.Green
            });
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Расходов всего",
                Values = new ChartValues<double>(list3),
                Fill = Brushes.Red
            });


            BarLabels = new string[list.Count];
            for (int i = 0; i < BarLabels.Length; i++)
                BarLabels[i] = list[i];
            Formatter = values => values.ToString("N");
            DataContext = this;
        
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] BarLabels { get; set; }

        public Func<double, string> Formatter { get; set; }

        public NpgsqlConnection connectionString { get; }
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
