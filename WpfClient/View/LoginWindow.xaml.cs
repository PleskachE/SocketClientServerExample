using System.Windows;
using System.Windows.Controls;
using WpfTcpClient.Infrastructure;

namespace WpfTcpClient.View
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : ContentControl
    {
        public RelayCommand DialogCommand { get; set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogCommand?.Execute(true);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogCommand?.Execute(false);
        }
    }
}
