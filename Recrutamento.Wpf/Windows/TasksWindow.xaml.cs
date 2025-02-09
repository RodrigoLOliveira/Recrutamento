﻿using Recrutamento.Wpf.DTOs;
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
    /// Lógica interna para TasksWindow.xaml
    /// </summary>
    public partial class TasksWindow : Window
    {
        private readonly TaskService _taskService = new();

        public TasksWindow()
        {
            InitializeComponent();
            LoadTasksAsync();

            TaskGrid.PreviewKeyDown += TaskGrid_PreviewKeyDown;
        }

        private async void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadTasksAsync();
        }

        private async Task LoadTasksAsync()
        {
            try
            {
                TaskGrid.ItemsSource = await _taskService.GetTasksAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnNovaTarefa_Click(object sender, RoutedEventArgs e)
        {
            CreateTaskWindow createTaskWindow = new();
            createTaskWindow.ShowDialog();
            LoadTasksAsync();
        }

        private async void TaskGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var selectedTask = TaskGrid.SelectedItem as TaskItemDto;

                if (selectedTask == null)
                {
                    MessageBox.Show("Selecione uma tarefa para deletar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MessageBoxResult result = MessageBox.Show(
                    $"Tem certeza que deseja excluir a tarefa \"{selectedTask.Titulo}\"?",
                    "Confirmar Exclusão",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    bool deleted = await _taskService.DeleteTaskAsync(selectedTask.Id);

                    if (deleted)
                    {
                        MessageBox.Show("Tarefa excluída com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        await LoadTasksAsync(); // Atualiza a lista
                    }
                    else
                    {
                        MessageBox.Show("Erro ao excluir a tarefa.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void TaskGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedTask = TaskGrid.SelectedItem as TaskItemDto;

            if (selectedTask == null)
                return;

            EditTaskWindow editTaskWindow = new(selectedTask);
            editTaskWindow.ShowDialog();
            LoadTasksAsync();
        }
    }
}
