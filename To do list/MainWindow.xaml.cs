using System.Windows;
using To_do_list.Services;
using To_do_list.ViewModels;

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
