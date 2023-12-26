﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace To_do_list
{
    internal class Task : INotifyPropertyChanged
    {
        private string description;
        private bool isCompleted;

        public string Description 
        {
            get
            {
                return description;
            }
            set 
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public bool IsCompleted
        {
            get
            {
                return isCompleted;
            }
            set
            {
                isCompleted = value;
                OnPropertyChanged("IsCompleted");
            }
        }
        public List<Task> Subtasks { get; set; }
        public DateTime? Deadline { get; set; }
        public bool HasSubtasks
        {
            get { return Subtasks != null && Subtasks.Count > 0; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void ChangeState()
        {
            IsCompleted = !IsCompleted;
        }
    }
}
