using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_do_list.Models;
using To_do_list.Services;
using Task = To_do_list.Models.Task;

namespace To_do_list.ViewModels
{
    class TaskBlockViewModel
    {
        public ObservableCollection<TaskBlock> TaskBlocks { get; set; }

        private RelayCommand addBlockCommand;
        public RelayCommand AddBlockCommand
        {
            get
            {
                return addBlockCommand ?? (addBlockCommand = new RelayCommand(
                    obj =>
                    {
                        TaskBlock taskBlock = obj as TaskBlock;
                        TaskBlocks.Add(taskBlock);
                    }));
            }
        }

        private RelayCommand removeBlockCommand;
        public RelayCommand RemoveBlockCommand
        {
            get
            {
                return removeBlockCommand ?? (removeBlockCommand = new RelayCommand(
                    obj =>
                    {
                        TaskBlock taskBlock = obj as TaskBlock;
                        TaskBlocks.Remove(taskBlock);
                    }, 
                    (obj) => TaskBlocks.Count > 0));
            }
        }

        public TaskBlockViewModel() 
        {
            TaskBlocks = new ObservableCollection<TaskBlock>()
            {
                new TaskBlock()
                {
                    Title = "Список 1",
                    TaskBlockList = new ObservableCollection<Task>()
                    {
                        new Task() { Description = "Задача 1" },
                        new Task() { Description = "Задача 2" },
                    }
                }
            };
        }
    }
}
