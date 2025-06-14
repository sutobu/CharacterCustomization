using GameApp.Data;
using GameApp.Models;
using GameApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;


namespace GameApp.Views
{
    public partial class EditCharacterWindow : Window
    {
        public Character Character { get; private set; }
        private readonly CharacterService _service = new();
        private List<Class> _classes;

        public EditCharacterWindow(Character character = null)
        {
            InitializeComponent();
            LoadClasses();

            if (character != null)
            {
                Character = character;
                NameBox.Text = character.Name;
                LevelBox.Text = character.Level.ToString();
                ClassComboBox.SelectedValue = character.ClassId;
            }
        }

        private async void LoadClasses()
        {
           
            ClassComboBox.ItemsSource = _classes;
            ClassComboBox.DisplayMemberPath = "Name";
            ClassComboBox.SelectedValuePath = "Id";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Character ??= new Character();
            Character.Name = NameBox.Text;
            Character.Level = int.Parse(LevelBox.Text);
            Character.ClassId = (int)ClassComboBox.SelectedValue;

            DialogResult = true;
            Close();
        }

        private void NameBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
        private void AddCharacter_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditCharacterWindow();
            window.ShowDialog(); // показываем окно
        }
    }
}

