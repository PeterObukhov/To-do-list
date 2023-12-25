using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
