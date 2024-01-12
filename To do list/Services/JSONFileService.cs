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

        public List<TaskBlock> Open(string path)
        {
            List<TaskBlock> tasks = new List<TaskBlock>();
            using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.OpenOrCreate)))
            {
                tasks = jsonSerializer.Deserialize(sr, typeof(List<TaskBlock>)) as List<TaskBlock>;
            }
            return tasks;
        }

        public void Save(string path, List<TaskBlock> tasks)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate)))
            {
                jsonSerializer.Serialize(sw, tasks);
            }
        }
    }
}
