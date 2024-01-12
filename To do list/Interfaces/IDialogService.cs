using System;
using System.Collections.Generic;
using To_do_list.Models;

namespace To_do_list.Interfaces
{
    public interface IDialogService
    {
        string Description { get; set; }
        string FilePath { get; set; }
        DateTime? Deadline { get; set; }
        TaskBlock SelectedTaskBlock { get; set; }
        bool SaveFileDialog();
        bool OpenFileDialog();
        bool NewTaskDialog(List<TaskBlock> taskBlocks);
    }
}
