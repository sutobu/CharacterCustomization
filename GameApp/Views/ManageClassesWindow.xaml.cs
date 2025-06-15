using GameApp.Models;
using GameApp.Services;
using GameApp.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace GameApp.Views
{
    public partial class ManageClassesWindow : Window
    {
        private readonly CharacterService _service;
        private List<Class> _classes;

        public ManageClassesWindow()
        {
            InitializeComponent();
            _service = new CharacterService(new AppDbContext());
            Loaded += async (sender, e) => await ManageClassesWindow_Loaded();
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
                await Refresh();
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
                    await Refresh();
                }
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClassListBox.SelectedItem is Class selected)
            {
                if (MessageBox.Show("Удалить этот класс?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await _service.DeleteClassAsync(selected.Id);
                    await Refresh();
                }
            }
        }

        private async Task Refresh()
        {
            _classes = await _service.GetClassesAsync();
            ClassListBox.ItemsSource = null;
            ClassListBox.ItemsSource = _classes;
        }

        private string Prompt(string message, string defaultValue = "")
        {
            return Microsoft.VisualBasic.Interaction.InputBox(message, "Класс", defaultValue);
        }
    }
}
