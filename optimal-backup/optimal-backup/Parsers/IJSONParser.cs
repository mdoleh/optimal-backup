using optimal_backup.Models;

namespace optimal_backup.Parsers
{
    public interface IJSONParser
    {
        Config ParseFile(string path);
    }
}
