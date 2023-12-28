using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace To_do_list
{
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : UserControl
    {
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(TaskView), new FrameworkPropertyMetadata(null));

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

        public TaskView()
        {
            InitializeComponent();
        }

        private void ToggleStrikethrough(object sender, RoutedEventArgs e)
        {
            DescriptionBlock.TextDecorations = DescriptionBlock.TextDecorations == TextDecorations.Strikethrough ? null : TextDecorations.Strikethrough;
        }
    }
}
