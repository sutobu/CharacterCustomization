using GameApp.Models;
using GameApp.Services;
using System.Collections.Generic;
using System.Windows;

namespace GameApp.Views
{
    public partial class ManageClassesWindow : Window
    {
        private readonly CharacterService _service = new();
        private List<Class> _classes;

        public ManageClassesWindow()
        {
            InitializeComponent();
            Loaded += async (sender, e) => await ManageClassesWindow_Loaded(); // Use an async lambda for the Loaded event
        }

        private async Task ManageClassesWindow_Loaded()
        {
            _classes = await _service.GetClassesAsync();
            ClassListBox.ItemsSource = _classes;
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = Prompt("Введите имя класса:");
            if (!string.IsNullOrWhiteSpace(name))
            {
                await _service.AddClassAsync(new Class { Name = name });
                await Refresh(); // Await the Refresh method
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClassListBox.SelectedItem is Class selected)
            {
                string name = Prompt("Новое имя класса:", selected.Name);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    selected.Name = name;
                    await _service.UpdateClassAsync(selected);
                    await Refresh(); // Await the Refresh method
                }
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClassListBox.SelectedItem is Class selected)
            {
                if (MessageBox.Show("Удалить этот класс?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await _service.DeleteClassAsync(selected.Id); // Pass the Id of the class
                    await Refresh(); // Await the Refresh method
                }
            }
        }

        private async Task Refresh()
        {
            _classes = await _service.GetClassesAsync();
            ClassListBox.ItemsSource = null; // Clear the ItemsSource
            ClassListBox.ItemsSource = _classes; // Reassign the updated list
        }

        private string Prompt(string message, string defaultValue = "")
        {
            return Microsoft.VisualBasic.Interaction.InputBox(message, "Класс", defaultValue);
        }
    }
}