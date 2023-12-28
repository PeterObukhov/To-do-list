using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using To_do_list.Interfaces;
using To_do_list.Models;

namespace To_do_list.Services
{
    internal class JSONFileService : IFileService
    {
        private JsonSerializer jsonSerializer;

        public JSONFileService()
        {
            jsonSerializer = new JsonSerializer();
        }

        public List<Task> Open(string path)
        {
            List<Task> tasks = new List<Task>();
            using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.OpenOrCreate)))
            {
                tasks = jsonSerializer.Deserialize(sr, typeof(List<Task>)) as List<Task>;
            }
            return tasks;
        }

        public void Save(string path, List<Task> tasks)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate)))
            {
                jsonSerializer.Serialize(sw, tasks);
            }
        }
    }
}
