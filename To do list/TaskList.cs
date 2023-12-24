using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace To_do_list
{
    internal class TaskList : INotifyPropertyChanged
    {
        //Task selectedTask
        //{
        //    get { return selectedTask; }
        //    set 
        //    { 
        //        selectedTask = value;
        //        OnPropertyChanged("SelectedTask");
        //    }
        //}
        public ObservableCollection<Task> Tasks { get; set; }
        public int Count
        {
            get { return Tasks.Count; }
        }

        public void Delete(int id)
        {
            Tasks.RemoveAt(id);
        }

        public TaskList()
        {
            Tasks = new ObservableCollection<Task>() 
            { 
                new Task() 
                { 
                    Description = "Задача 1", 
                    Subtasks = new List<Task>()
                    { 
                        new Task() { Description = "Подзадача 1" },
                        new Task() { Description = "Подзадача 2" }
                    } 
                },
                new Task() { Description = "Задача 2" },
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
