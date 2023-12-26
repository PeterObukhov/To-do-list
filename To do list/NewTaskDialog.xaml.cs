using System;
using System.Windows;

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
    }
}
