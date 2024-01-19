using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace To_do_list.Views
{
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : UserControl
    {

        public static readonly DependencyProperty ToggleCompletionCommandProperty =
            DependencyProperty.Register("ToggleCompletionCommand", typeof(ICommand), typeof(TaskView), new FrameworkPropertyMetadata(null));

        public ICommand ToggleCompletionCommand
        {
            get
            {
                return (ICommand)GetValue(ToggleCompletionCommandProperty);
            }
            set
            {
                SetValue(ToggleCompletionCommandProperty, value);
            }
        }


        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(TaskView), new FrameworkPropertyMetadata(null));
        public string Description
        {
            get
            {
                return (string)GetValue(DescriptionProperty);
            }
            set
            {
                SetValue(DescriptionProperty, value);
            }
        }

        public static readonly DependencyProperty DeadlineProperty =
            DependencyProperty.Register("Deadline", typeof(DateTime), typeof(TaskView), new FrameworkPropertyMetadata(null));
        public DateTime Deadline
        {
            get
            {
                return (DateTime)GetValue(DeadlineProperty);
            }
            set
            {
                SetValue(DeadlineProperty, value);
            }
        }

        public static readonly DependencyProperty IsCompletedProperty =
            DependencyProperty.Register("IsCompleted", typeof(bool), typeof(TaskView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnCompletionChanged)));
        public bool IsCompleted
        {
            get
            {
                return (bool)GetValue(IsCompletedProperty);
            }
            set
            {
                SetValue(IsCompletedProperty, value);
            }
        }

        private static void OnCompletionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TaskView taskView = d as TaskView;
            taskView.checkBox.IsChecked = (bool)e.NewValue;

            if ((bool)e.NewValue)
            {
                taskView.DescriptionBlock.TextDecorations = TextDecorations.Strikethrough;
            }
            else
            {
                taskView.DescriptionBlock.TextDecorations = null;
            }
        }

        public TaskView()
        {
            InitializeComponent();
        }

        private void ToggleStrikethrough(object sender, RoutedEventArgs e)
        {
            if (IsCompleted)
            {
                DescriptionBlock.TextDecorations = TextDecorations.Strikethrough;
                checkBox.IsChecked = true;
            }
            else
            {
                DescriptionBlock.TextDecorations = null;
                checkBox.IsChecked = false;
            }
        }
    }
}
