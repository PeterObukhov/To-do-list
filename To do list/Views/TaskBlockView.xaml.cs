using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using To_do_list.ViewModels;

namespace To_do_list.Views
{
    /// <summary>
    /// Interaction logic for TaskBlockView.xaml
    /// </summary>
    public partial class TaskBlockView : UserControl
    {
        public static readonly DependencyProperty TasksProperty =
            DependencyProperty.Register("Tasks", typeof(ObservableCollection<Task>), typeof(TaskBlockView), new FrameworkPropertyMetadata(null));

        public ObservableCollection<Task> Tasks
        {
            get
            {
                return (ObservableCollection<Task>)GetValue(TasksProperty);
            }
            set
            {
                SetValue(TasksProperty, value);
            }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(TaskBlockView), new FrameworkPropertyMetadata(null));

        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public TaskBlockView()
        {
            InitializeComponent();
        }
    }
}
