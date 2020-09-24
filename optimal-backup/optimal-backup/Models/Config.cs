using System;
using System.Collections.Generic;
using System.Text;

namespace optimal_backup.Models
{
    public class Config
    {
        public List<string> BackupDirs { get; set; }
        public List<string> OriginalDirs { get; set; }
    }
}
