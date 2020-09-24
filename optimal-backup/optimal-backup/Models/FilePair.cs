namespace optimal_backup.Models
{
    public class FilePair
    {
        public string OriginalPath { get; set; }
        public string BackupPath { get; set; }

        public FilePair(string original, string backup)
        {
            OriginalPath = original;
            BackupPath = backup;
        }
    }
}
