using System.Windows;
using System.Windows.Controls;

namespace To_do_list
{
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : UserControl
    {
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(int), typeof(UserControl), new FrameworkPropertyMetadata(null));

        public TaskView()
        {
            InitializeComponent();
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

        private void ToggleStrikethrough(object sender, RoutedEventArgs e)
        {
            // Set the underline decoration directly to the text block.
            DescriptionBlock.TextDecorations = DescriptionBlock.TextDecorations == TextDecorations.Strikethrough ? null : TextDecorations.Strikethrough;
        }
    }
}
