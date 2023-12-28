using System.Collections.Generic;

namespace To_do_list
{
    internal interface IFileService
    {
        List<Task> Open(string path);
        void Save(string path, List<Task> tasks);
    }
}
