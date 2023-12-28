﻿using System;

namespace To_do_list
{
    internal interface IDialogService
    {
        string Description { get; set; }
        string FilePath { get; set; }
        DateTime? Deadline { get; set; }
        bool SaveFileDialog();
        bool OpenFileDialog();
        bool NewTaskDialog();
    }
}