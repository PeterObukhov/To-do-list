using System.Collections.Generic;
using To_do_list.Models;

namespace To_do_list.Interfaces
{
    internal interface IFileService
    {
        List<Task> Open(string path);
        void Save(string path, List<Task> tasks);
    }
}
