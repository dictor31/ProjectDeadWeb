using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebDead.Model;

namespace WpfDead
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client;

        public User User { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("https://localhost:7012/api/");
            DataContext = this;
        }

        private async void Enter(object sender, RoutedEventArgs e)
        {
            var responce = await client.GetAsync("DB/GetUsers");
            var responceBody = await responce.Content.ReadAsStringAsync();
            List<User> users = JsonConvert.DeserializeObject
        }
    }
}