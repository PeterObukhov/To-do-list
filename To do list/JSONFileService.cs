using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace To_do_list
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
