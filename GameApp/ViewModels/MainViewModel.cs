using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameApp.Models;
using GameApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace GameApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly CharacterService _characterService;
        public ObservableCollection<Character> Characters { get; set; }

        private Character _selectedCharacter;
        public Character SelectedCharacter
        {
            get => _selectedCharacter;
            set { _selectedCharacter = value; OnPropertyChanged(); }
        }

        public ICommand LoadCharactersCommand { get; }

        public MainViewModel()
        {
            LoadCharactersCommand = new RelayCommand(async () => await LoadCharacters());
        }

        private async Task LoadCharacters()
        {
            Characters.Clear();
            var characters = await _characterService.GetCharactersAsync();
            foreach (var c in characters)
                Characters.Add(c);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
