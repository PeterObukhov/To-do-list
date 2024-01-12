using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using To_do_list.Models;
using To_do_list.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using To_do_list.Interfaces;

namespace To_do_list.ViewModels
{
    class TreeViewViewModel
    {
        private IDialogService dialogService;
        public ObservableCollection<TaskBlock> TreeModel { get; set; }


        private TreeViewItem selectedTask;
        public TreeViewItem SelectedTask
        {
            get
            {
                return selectedTask;
            }
            set
            {
                selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        Task task = obj as Task;
                        //Tasks.Insert(Tasks.Count, task);
                    }));
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand(obj =>
                    {
                        if (obj is Task)
                        {
                            Task task = obj as Task;
                            foreach (TaskBlock tb in TreeModel)
                            {
                                if(tb.Children.Contains(task)) 
                                    tb.Children.Remove(task);
                            }
                        }
                        else if (obj is TaskBlock)
                        {
                            TaskBlock taskBlock = obj as TaskBlock;
                            TreeModel.Remove(taskBlock);
                        }
                    },
                    (obj) => TreeModel.Count > 0));
            }
        }

        private RelayCommand createNewTask;
        public RelayCommand CreateNewTask
        {
            get
            {
                return createNewTask ??
                    (createNewTask = new RelayCommand(obj =>
                    {
                        if (dialogService.NewTaskDialog(TreeModel.ToList()) == true)
                        {
                            AddCommand.Execute(
                                (
                                dialogService.SelectedTaskBlock, 
                                new Task()
                                {
                                    Description = dialogService.Description,
                                    Deadline = dialogService.Deadline
                                }));
                        }
                    }));
            }
        }

        private RelayCommand toggleCompletionCommand;
        public RelayCommand ToggleCompletionCommand
        {
            get
            {
                return toggleCompletionCommand ??
                    (toggleCompletionCommand = new RelayCommand(obj =>
                    {
                        int id = (int)obj;
                        Task selectedTask = TreeModel.SelectMany(tb => tb.Children).FirstOrDefault(t => t.Id == id);
                        selectedTask.IsCompleted = !selectedTask.IsCompleted;
                        MessageBox.Show(selectedTask.IsCompleted.ToString());
                    }));
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        if (dialogService.SaveFileDialog() == true)
                        {
                            JSONFileService jSONFileService = new JSONFileService();
                            jSONFileService.Save(dialogService.FilePath, TreeModel.ToList());
                        }
                    }));
            }
        }

        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                    (openCommand = new RelayCommand(obj =>
                    {
                        if (dialogService.OpenFileDialog() == true)
                        {
                            TreeModel.Clear();
                            JSONFileService jSONFileService = new JSONFileService();
                            List<TaskBlock> tasks = jSONFileService.Open(dialogService.FilePath);
                            foreach (TaskBlock taskBlock in tasks)
                            {
                                TreeModel.Add(taskBlock);
                            }
                        }
                    }));
            }
        }

        public TreeViewViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            TreeModel = new ObservableCollection<TaskBlock>()
            {
                new TaskBlock()
                {
                    Title="Список 1",
                    Children = new ObservableCollection<Models.Task>()
                    {
                        new Models.Task(){Id = 0, Description = "Задача 1"},
                        new Models.Task(){Id = 1, Description = "Задача 2"},
                    }
                },
                new TaskBlock()
                {
                    Title="Список 2",
                    Children = new ObservableCollection<Models.Task>()
                    {
                        new Models.Task(){Id = 2, Description = "Задача 3"},
                        new Models.Task(){Id = 3, Description = "Задача 4"},
                    }
                }
            };
        }
    }
}
