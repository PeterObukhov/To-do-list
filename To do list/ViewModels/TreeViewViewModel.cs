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
using System.IO;

namespace To_do_list.ViewModels
{
    class TreeViewViewModel
    {
        private IDialogService dialogService;
        private IFileService fileService;
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
                        ValueTuple<TaskBlock, Task> newTask = (ValueTuple<TaskBlock, Task>)obj;
                        if (!TreeModel.Contains(newTask.Item1)) 
                        {
                            TreeModel.Add(newTask.Item1);
                        }
                        TreeModel.Single(tb => tb == newTask.Item1).Children.Add(newTask.Item2);
                    }));
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
                        if(dialogService.FilePath != null)
                        {
                            fileService.Save(dialogService.FilePath, TreeModel.ToList());
                        }
                    }));
            }
        }

        private RelayCommand saveAsCommand;
        public RelayCommand SaveAsCommand
        {
            get
            {
                return saveAsCommand ??
                    (saveAsCommand = new RelayCommand(obj =>
                    {
                        if (dialogService.SaveFileDialog() == true)
                        {
                            fileService.Save(dialogService.FilePath, TreeModel.ToList());
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
                            List<TaskBlock> taskBlocks = fileService.Open(dialogService.FilePath);
                            foreach (TaskBlock taskBlock in taskBlocks)
                            {
                                TreeModel.Add(taskBlock);
                            }
                        }
                    }));
            }
        }

        public TreeViewViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
            TreeModel = new ObservableCollection<TaskBlock>();
            Load();
        }

        private void Load()
        {
            string path = String.Format("{0}\\lastFile.txt", Directory.GetCurrentDirectory());
            if(File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string filePath = sr.ReadLine();
                    if (!String.IsNullOrEmpty(filePath))
                    {
                        dialogService.FilePath = filePath;
                        TreeModel.Clear();
                        List<TaskBlock> taskBlocks = fileService.Open(filePath);
                        foreach (TaskBlock taskBlock in taskBlocks)
                        {
                            TreeModel.Add(taskBlock);
                        }
                    }
                }
            }
        }
    }
}
