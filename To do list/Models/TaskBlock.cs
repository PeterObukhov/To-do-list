using System.Collections.ObjectModel;

namespace To_do_list.Models
{
    public class TaskBlock
    {
        public string Title { get; set; }
        public ObservableCollection<Task> Children { get; set;}

        public TaskBlock() 
        { 
            Children = new ObservableCollection<Task>();
        }
    }
}
