using optimal_backup.Models;
using optimal_backup.Parsers;
using optimal_backup.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace optimal_backup
{
    public class App
    {
        private readonly Config _config;
        private readonly ILogger _logger;

        public App(IJSONParser jsonParser, ILogger logger)
        {
            _config = jsonParser.ParseFile(Path.Combine(Directory.GetCurrentDirectory(), "config.json"));
            _logger = logger;
        }
        public void Run()
        {
            try
            {
                performBackup();
            } catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }

        private void performBackup()
        {
            List<List<string>> filesToBackup = getAllFilesToBackup();
            List<FilePair> filteredFilePairs = filterOutExistingFiles(filesToBackup);
            backupFiles(filteredFilePairs);
        }

        private void backupFiles(List<FilePair> filesToBackup)
        {
            foreach (FilePair filePair in filesToBackup)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePair.BackupPath));
                File.Copy(filePair.OriginalPath, filePair.BackupPath);
                _logger.Log($"Copied {filePair.OriginalPath} to {filePair.BackupPath}");
            }
        }

        private List<FilePair> filterOutExistingFiles(List<List<string>> filesToBackup)
        {
            List<FilePair> filtered = new List<FilePair>();
            for (int i = 0; i < filesToBackup.Count; ++i)
            {
                string pathToReplace = _config.OriginalDirs[i];

                foreach (string filePath in filesToBackup[i])
                {
                    string backupPath = filePath.Replace(pathToReplace, _config.BackupDirs[i]);
                    if (!File.Exists(backupPath))
                    {
                        filtered.Add(new FilePair(filePath, backupPath));
                        _logger.Log($"Added {filePath} to backup at {backupPath}");
                    }
                }
            }
            return filtered;
        }

        private List<List<string>> getAllFilesToBackup()
        {
            List<List<string>> filesToBackup = new List<List<string>>();
            for (int i = 0; i < _config.OriginalDirs.Count; ++i)
            {
                filesToBackup.Add(new List<string>());
                string configDir = _config.OriginalDirs[i];

                List<string> originalDirs = Directory.GetDirectories(configDir).ToList();
                filesToBackup[i].AddRange(getFiles(configDir));

                foreach (string dir in originalDirs)
                {
                    filesToBackup[i].AddRange(getFiles(dir));
                }
            }
            return filesToBackup;
        }

        private List<string> getFiles(string path)
        {
            return Directory.GetFiles(path).ToList();
        }
    }
}
