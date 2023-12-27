using System;
using System.Windows;
using System.Windows.Controls;

namespace To_do_list
{
    /// <summary>
    /// Логика взаимодействия для NewTaskDialog.xaml
    /// </summary>
    public partial class NewTaskDialog : Window
    {
        public NewTaskDialog()
        {
            InitializeComponent();
            descTextBox.GotFocus += RemoveText;
            descTextBox.LostFocus += AddText;
        }

        public string Description 
        {
            get { return descTextBox.Text; }
        }

        public DateTime? Date
        {
            get { return datePicker.SelectedDate; }
        }

        public void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public void AddText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Введите текст";
            }
        }

        public void RemoveText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Введите текст")
            {
                textBox.Text = string.Empty;
            }
        }
    }
}
