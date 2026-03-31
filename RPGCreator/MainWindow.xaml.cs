using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RPGCreator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем имя
            string name = NameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Введите имя персонажа!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Определяем класс
            string characterClass = GetSelectedClass();

            // Получаем выбранные навыки
            List<string> skills = GetSelectedSkills();

            // Формируем результат
            string result = $"=== Персонаж создан ===\n\n";
            result += $"Имя: {name}\n";
            result += $"Класс: {characterClass}\n\n";

            if (skills.Count > 0)
            {
                result += "Навыки:\n";
                foreach (string skill in skills)
                {
                    result += $"  • {skill}\n";
                }
            }
            else
            {
                result += "Навыки: не выбраны\n";
            }

            ResultTextBox.Text = result;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Очищаем имя
            NameTextBox.Text = string.Empty;

            // Сбрасываем класс на первый
            WarriorRadio.IsChecked = true;

            // Снимаем все галочки с навыков
            SmithingCheck.IsChecked = false;
            AlchemyCheck.IsChecked = false;
            PotionCheck.IsChecked = false;
            LockpickCheck.IsChecked = false;
            StealthCheck.IsChecked = false;

            // Очищаем результат
            ResultTextBox.Text = string.Empty;
        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();

            // Случайное имя
            string[] names = { "Артур", "Мерлин", "Леголас", "Арагорн", "Гэндальф", "Торин", "Боромир", "Фарамир" };
            NameTextBox.Text = names[random.Next(names.Length)];

            // Случайный класс
            RadioButton[] classRadios = { WarriorRadio, MageRadio, ArcherRadio, RogueRadio };
            classRadios[random.Next(classRadios.Length)].IsChecked = true;

            // Случайные навыки (от 1 до 3)
            CheckBox[] skillChecks = { SmithingCheck, AlchemyCheck, PotionCheck, LockpickCheck, StealthCheck };

            // Сначала сбрасываем все
            foreach (var check in skillChecks)
                check.IsChecked = false;

            // Выбираем случайное количество навыков
            int skillCount = random.Next(1, 4);
            var shuffled = skillChecks.OrderBy(x => random.Next()).Take(skillCount);
            foreach (var check in shuffled)
                check.IsChecked = true;
        }

        private string GetSelectedClass()
        {
            if (WarriorRadio.IsChecked == true) return "Воин";
            if (MageRadio.IsChecked == true) return "Маг";
            if (ArcherRadio.IsChecked == true) return "Лучник";
            if (RogueRadio.IsChecked == true) return "Вор";
            return "Не выбран";
        }

        private List<string> GetSelectedSkills()
        {
            List<string> skills = new List<string>();
            if (SmithingCheck.IsChecked == true) skills.Add("Кузнечное дело");
            if (AlchemyCheck.IsChecked == true) skills.Add("Алхимия");
            if (PotionCheck.IsChecked == true) skills.Add("Зельеварение");
            if (LockpickCheck.IsChecked == true) skills.Add("Взлом замков");
            if (StealthCheck.IsChecked == true) skills.Add("Скрытность");
            return skills;
        }
    }
}
