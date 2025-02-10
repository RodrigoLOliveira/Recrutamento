using System.Windows;
using Recrutamento.Wpf.Services;
using Recrutamento.Wpf.Windows;

namespace Recrutamento.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtEmail.Text = "rodrigo16rj@gmail.com";
            txtPassword.Password = "1996@Rodrigo";
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Preencha todos os campos!";
                lblMessage.Visibility = Visibility.Visible;
                return;
            }

            bool isAuthenticated = await AuthService.LoginAsync(email, password);

            if (isAuthenticated)
            {
                MessageBox.Show("Login realizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                lblMessage.Visibility = Visibility.Collapsed;
                Visibility = Visibility.Collapsed;

                TasksWindow taskWindow = new TasksWindow();
                taskWindow.Show();
            }
            else
            {
                lblMessage.Text = "Falha ao autenticar. Verifique suas credenciais.";
                lblMessage.Visibility = Visibility.Visible;
            }
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }
    }
}