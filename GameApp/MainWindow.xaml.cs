using GameApp.Data;
using GameApp.Models;
using GameApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace GameApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly CharacterService _service;
        private List<Character> _characters;

        public MainWindow()
        {
            InitializeComponent();
            _service = new CharacterService(new AppDbContext());
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await SeedTestClassesAsync();
            await LoadCharacters();
        }

        private async Task LoadCharacters()
        {
            _characters = await _service.GetCharactersAsync();
            CharacterGrid.ItemsSource = _characters;
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditCharacterWindow(_service); // ✅ Pass the service here
            if (window.ShowDialog() == true)
            {
                await _service.AddCharacterAsync(window.Character);
                await LoadCharacters();
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (CharacterGrid.SelectedItem is Character selected)
            {
                var window = new EditCharacterWindow(_service, selected); // ✅ Pass the service and character
                if (window.ShowDialog() == true)
                {
                    await _service.UpdateCharacterAsync(window.Character);
                    await LoadCharacters();
                }
            }
            else
            {
                MessageBox.Show("Выберите персонажа для редактирования.");
            }
        }

        private async Task SeedTestClassesAsync()
        {
            var existingClasses = await _service.GetClassesAsync();
            if (existingClasses == null || !existingClasses.Any())
            {
                await _service.AddClassAsync(new Class { Name = "Warrior" });
                await _service.AddClassAsync(new Class { Name = "Mage" });
                await _service.AddClassAsync(new Class { Name = "Archer" });
            }
        }


        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CharacterGrid.SelectedItem is Character selected)
            {
                var result = MessageBox.Show($"Удалить персонажа {selected.Name}?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    await _service.DeleteCharacterAsync(selected.Id);
                    await LoadCharacters();
                }
            }
            else
            {
                MessageBox.Show("Выберите персонажа для удаления.");
            }
        }
    }
}
