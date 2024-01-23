using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using To_do_list.Interfaces;
using To_do_list.Models;

namespace To_do_list.Services
{
    internal class DefaultDialogService : IDialogService
    {
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public string FilePath { get; set; }
        public TaskBlock SelectedTaskBlock { get; set; }

        public bool NewTaskDialog(List<TaskBlock> taskBlocks, TaskBlock selectedTB = null)
        {
            NewTaskDialog newTaskDialog = new NewTaskDialog(taskBlocks, selectedTB);
            if (newTaskDialog.ShowDialog() == true)
            {
                Description = newTaskDialog.Description;
                Deadline = newTaskDialog.Date;
                SelectedTaskBlock = newTaskDialog.SelectedTaskBlock;
                return true;
            }
            return false;
        }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                SaveLastFilePath(FilePath);
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                SaveLastFilePath(FilePath);
                return true;
            }
            return false;
        }

        private void SaveLastFilePath(string filePath)
        {
            string path = String.Format("{0}\\lastFile.txt", Directory.GetCurrentDirectory());
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(filePath);
            }
        }
    }
}
