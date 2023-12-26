using System;

namespace To_do_list
{
    internal class DefaultDialogService : IDialogService
    {
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }

        public bool OpenDialog()
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
    }
}
