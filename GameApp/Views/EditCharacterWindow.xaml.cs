using GameApp.Models;
using GameApp.Services;
using System.Collections.Generic;
using System.Windows;

namespace GameApp.Views
{
    public partial class EditCharacterWindow : Window
    {
        public Character Character { get; private set; }
        private readonly CharacterService _service;
        private List<Class> _classes;

        public EditCharacterWindow(CharacterService service, Character character = null)
        {
            InitializeComponent();
            _service = service;
            Character = character;

            Loaded += EditCharacterWindow_Loaded; 
        }

        private async void EditCharacterWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _classes = await _service.GetClassesAsync();
            ClassComboBox.ItemsSource = _classes;
            ClassComboBox.DisplayMemberPath = "Name";
            ClassComboBox.SelectedValuePath = "Id";

            if (Character != null)
            {
                NameBox.Text = Character.Name;
                LevelBox.Text = Character.Level.ToString();
                ClassComboBox.SelectedValue = Character.ClassId;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(LevelBox.Text, out int level))
            {
                MessageBox.Show("Введите корректный уровень.");
                return;
            }

            if (ClassComboBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите класс.");
                return;
            }

            Character ??= new Character();
            Character.Name = NameBox.Text;
            Character.Level = level;
            Character.ClassId = (int)ClassComboBox.SelectedValue;

            DialogResult = true;
            Close();
        }
    }
}
