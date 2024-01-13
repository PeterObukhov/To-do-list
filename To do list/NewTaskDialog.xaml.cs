using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using To_do_list.Models;

namespace To_do_list
{
    /// <summary>
    /// Логика взаимодействия для NewTaskDialog.xaml
    /// </summary>
    public partial class NewTaskDialog : Window
    {
        public NewTaskDialog(List<TaskBlock> taskBlocks)
        {
            InitializeComponent();
            descTextBox.GotFocus += RemoveText;
            descTextBox.LostFocus += AddText;
            comboBox.ItemsSource = taskBlocks;
            comboBox.DisplayMemberPath = "Title";
        }

        public string Description 
        {
            get { return descTextBox.Text; }
        }

        public DateTime? Date
        {
            get { return datePicker.SelectedDate; }
        }

        public TaskBlock SelectedTaskBlock
        {
            get
            {
                if(comboBox.SelectedItem == null)
                {
                    return new TaskBlock() { Title=comboBox.Text };
                }
                return (TaskBlock)comboBox.SelectedItem;
            }
        }

        public void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public List<TaskBlock> TaskBlocks { get; set; }

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
