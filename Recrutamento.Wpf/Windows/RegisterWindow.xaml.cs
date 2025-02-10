using Recrutamento.Wpf.Services;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Recrutamento.Wpf.Windows
{
    /// <summary>
    /// Lógica interna para RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password.Trim();
            string confirmPassword = txtConfirmPassword.Password.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                lblMessage.Text = "Preencha todos os campos!";
                lblMessage.Visibility = Visibility.Visible;
                return;
            }

            if (password != confirmPassword)
            {
                lblMessage.Text = "As senhas não coincidem!";
                lblMessage.Visibility = Visibility.Visible;
                return;
            }

            bool isRegistered = await AuthService.RegisterAsync(email, password);

            if (isRegistered)
            {
                MessageBox.Show("Cadastro realizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                lblMessage.Visibility = Visibility.Collapsed;
                Close();
            }
            else
            {
                lblMessage.Text = "Falha ao cadastrar. Verifique suas informações.";
                lblMessage.Visibility = Visibility.Visible;
            }
        }
    }
}
