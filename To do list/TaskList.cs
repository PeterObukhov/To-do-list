using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace To_do_list
{
    internal class TaskList : INotifyPropertyChanged
    {
        public ObservableCollection<Task> Tasks { get; set; }

        IDialogService dialogService;
        public int Count
        {
            get { return Tasks.Count; }
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
                        if(task != null)
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
                        if (dialogService.OpenDialog() == true)
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
