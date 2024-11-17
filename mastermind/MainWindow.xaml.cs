using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mastermind
{
    public partial class MainWindow : Window
    {
        private List<string> _colors = new List<string> { "Rood", "Geel", "Oranje", "Wit", "Groen", "Blauw" };
        private List<string> _code = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Vul de ComboBoxen met kleuren
            var comboBoxes = new[] { ComboBox1, ComboBox2, ComboBox3, ComboBox4 };
            foreach (var comboBox in comboBoxes)
            {
                comboBox.ItemsSource = _colors;
            }

            // Genereer een random code
            Random random = new Random();
            _code = Enumerable.Range(0, 4).Select(_ => _colors[random.Next(_colors.Count)]).ToList();

            // Toon de code in de titel (voor debug/doel van testen)
            this.Title = "Mastermind - Code: " + string.Join(", ", _code);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Toon de gekozen kleur in de corresponderende label
            if (sender is ComboBox comboBox)
            {
                int index = Array.IndexOf(new[] { ComboBox1, ComboBox2, ComboBox3, ComboBox4 }, comboBox);
                var labels = new[] { Label1, Label2, Label3, Label4 };
                labels[index].Content = comboBox.SelectedItem?.ToString();
            }
        }

        private void CheckCode_Click(object sender, RoutedEventArgs e)
        {
            // Haal de geselecteerde kleuren op
            var selectedColors = new List<string>
            {
                ComboBox1.SelectedItem?.ToString(),
                ComboBox2.SelectedItem?.ToString(),
                ComboBox3.SelectedItem?.ToString(),
                ComboBox4.SelectedItem?.ToString()
            };

            // Reset de labels' borders
            var labels = new[] { Label1, Label2, Label3, Label4 };
            foreach (var label in labels)
            {
                label.BorderBrush = Brushes.Gray;
            }

            // Controleer de ingevoerde code
            for (int i = 0; i < 4; i++)
            {
                if (selectedColors[i] == null) continue;

                if (selectedColors[i] == _code[i])
                {
                    // Correcte kleur op juiste positie
                    labels[i].BorderBrush = Brushes.DarkRed;
                }
                else if (_code.Contains(selectedColors[i]))
                {
                    // Kleur komt voor, maar niet op juiste positie
                    labels[i].BorderBrush = Brushes.Wheat;
                }
            }
        }
    }
}
