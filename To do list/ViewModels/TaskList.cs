using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using To_do_list.Interfaces;
using To_do_list.Models;
using To_do_list.Services;

namespace To_do_list.ViewModels
{
    internal class TaskList : INotifyPropertyChanged
    {
        public ObservableCollection<Task> Tasks { get; set; }
        private IDialogService dialogService;
        private Task selectedTask;
        public Task SelectedTask
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

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        Task task = obj as Task;
                        Tasks.Insert(Tasks.Count, task);
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
                        Task task = obj as Task;
                        if (task != null)
                        {
                            Tasks.Remove(task);
                        }

                    },
                    (obj) => Tasks.Count > 0));
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
                        if (dialogService.NewTaskDialog() == true)
                        {
                            AddCommand.Execute(new Task()
                            {
                                Description = dialogService.Description,
                                Deadline = dialogService.Deadline
                            });
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
                        string description = obj as string;
                        Task selectedTask = Tasks.First(x => x.Description == description);
                        selectedTask.IsCompleted = !selectedTask.IsCompleted;
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
                            jSONFileService.Save(dialogService.FilePath, Tasks.ToList());
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
                            Tasks.Clear();
                            JSONFileService jSONFileService = new JSONFileService();
                            List<Task> tasks = jSONFileService.Open(dialogService.FilePath);
                            foreach (Task task in tasks)
                            {
                                Tasks.Add(task);
                            }
                        }
                    }));
            }
        }

        public TaskList(IDialogService dialogService)
        {
            Tasks = new ObservableCollection<Task>()
            {
                new Task() { Description = "Задача 1" },
                new Task() { Description = "Задача 2" },
            };
            this.dialogService = dialogService;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
