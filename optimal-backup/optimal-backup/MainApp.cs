using optimal_backup.Parsers;
using optimal_backup.Utils;

namespace optimal_backup
{
    class MainApp
    {
        static void Main(string[] args)
        {
            new App(new JSONParser(), new Logger()).Run();
        }
    }
}
