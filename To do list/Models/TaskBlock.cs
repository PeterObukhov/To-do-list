using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
