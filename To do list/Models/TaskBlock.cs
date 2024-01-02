using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using To_do_list.Services;

namespace To_do_list.Models
{
    class TaskBlock
    {
        public string Title { get; set; }
        public ObservableCollection<Task>? TaskBlockList { get; set;}
    }
}
