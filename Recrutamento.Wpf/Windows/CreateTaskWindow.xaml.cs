using Recrutamento.Wpf.DTOs;
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
    /// Lógica interna para CreateTaskWindow.xaml
    /// </summary>
    public partial class CreateTaskWindow : Window
    {
        private readonly TaskService _taskService = new();

        public CreateTaskWindow()
        {
            InitializeComponent();
        }

        private async void BtnCriarTarefa_Click(object sender, RoutedEventArgs e)
        {
            string titulo = txtTitulo.Text.Trim();
            string descricao = txtDescricao.Text.Trim();
            DateTime? dataConclusao = dpDataConclusao.SelectedDate;

            if (string.IsNullOrEmpty(titulo))
            {
                MessageBox.Show("O título da tarefa é obrigatório!", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var novaTarefa = new CreateTaskItemDto
            {
                Titulo = titulo,
                Descricao = descricao,
                DataConclusao = dataConclusao
            };

            bool sucesso = await CriarTarefaAsync(novaTarefa);

            if (sucesso)
            {
                MessageBox.Show("Tarefa criada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao criar a tarefa.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> CriarTarefaAsync(CreateTaskItemDto novaTarefa)
        {
            try
            {
                return await _taskService.CreateTaskAsync(novaTarefa);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar tarefa: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
