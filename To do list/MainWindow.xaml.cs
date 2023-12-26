using System.Windows;

namespace To_do_list
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new TaskList(new DefaultDialogService());
        }
    }
}
