using Recrutamento.Domain.Enums;
using Recrutamento.Wpf.DTOs;
using Recrutamento.Wpf.Services;
using System.Windows;
using System.Windows.Controls;

namespace Recrutamento.Wpf.Windows
{
    public partial class EditTaskWindow : Window
    {
        private readonly TaskService _taskService = new();
        private readonly TaskItemDto _task;

        public EditTaskWindow(TaskItemDto task)
        {
            InitializeComponent();
            _task = task;
            LoadTaskData();
        }

        private void LoadTaskData()
        {
            txtTitulo.Text = _task.Titulo;
            txtDescricao.Text = _task.Descricao;
            dpDataConclusao.SelectedDate = _task.DataConclusao;

            // Define o status inicial no ComboBox
            cbStatus.SelectedIndex = (int)_task.Status;
        }

        private async void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cb = (ComboBoxItem)cbStatus.SelectedItem;

            var updatedTask = new UpdateTaskItemDto
            {
                Titulo = txtTitulo.Text.Trim(),
                Descricao = txtDescricao.Text.Trim(),
                DataConclusao = dpDataConclusao.SelectedDate,
                Status = ConvertStringToEnum((string)cb.Content)
            };

            bool success = await _taskService.UpdateTaskAsync(_task.Id, updatedTask);

            if (success)
            {
                MessageBox.Show("Tarefa atualizada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao atualizar a tarefa.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string ConvertEnumToString(EnumTaskStatus status)
        {
            switch (status)
            {
                case EnumTaskStatus.Pendente:
                    return "Pendente";
                case EnumTaskStatus.EmProgresso:
                    return "Em Progresso";
                case EnumTaskStatus.Concluida:
                    return "Concluída";
                default:
                    return "Invalida";
            }
        }

        private EnumTaskStatus ConvertStringToEnum(string status)
        {
            switch (status)
            {
                case "Pendente":
                    return EnumTaskStatus.Pendente;
                case "Em Progresso":
                    return EnumTaskStatus.EmProgresso;
                case "Concluída":
                    return EnumTaskStatus.Concluida;
                default:
                    return EnumTaskStatus.Invalida;
            }
        }
    }
}
