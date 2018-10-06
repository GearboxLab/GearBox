using Newtonsoft.Json;
using System.IO;

namespace GearBox.Config
{
    public class Config
    {
        public Apache Apache;
        public Memcached Memcached;

        public static Config Load(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return JsonConvert.DeserializeObject<Config>(reader.ReadToEnd());
            }
        }
    }

    public class Apache
    {
        public string FolderName;
    }

    public class Memcached
    {
        public string FolderName;
        public string Ip = "127.0.0.1";
        public int Port = 11211;
        public int Size = 64;
    }
}
