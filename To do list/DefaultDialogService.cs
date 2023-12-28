using Microsoft.Win32;
using System;

namespace To_do_list
{
    internal class DefaultDialogService : IDialogService
    {
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public string FilePath { get; set; }

        public bool NewTaskDialog()
        {
            NewTaskDialog newTaskDialog = new NewTaskDialog();
            if(newTaskDialog.ShowDialog() == true)
            {
                Description = newTaskDialog.Description;
                Deadline = newTaskDialog.Date;
                return true;
            }
            return false;
        }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if(saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }
    }
}
