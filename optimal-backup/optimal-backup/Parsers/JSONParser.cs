using Newtonsoft.Json;
using optimal_backup.Models;
using System.IO;

namespace optimal_backup.Parsers
{
    public class JSONParser: IJSONParser
    {
        public Config ParseFile(string path)
        {
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
        }
    }
}
