using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SensitiveWords.Services
{
    public class Logger
    {
        private readonly string _logDirectory;
        private readonly string _logFilePrefix;
        private readonly long _maxFileSizeBytes;
        private readonly int _maxFilesToKeep;

        public Logger(IConfiguration configuration)
        {
            _logDirectory = configuration.GetValue<string>("SensitiveWords:LogDirectory");
            _logFilePrefix = configuration.GetValue<string>("SensitiveWords:LogFilePrefix");
            _maxFileSizeBytes = configuration.GetValue<long>("SensitiveWords:MaxFileSizeBytes");
            _maxFilesToKeep = configuration.GetValue<int>("SensitiveWords:MaxFilesToKeep");
        }

        public void Log(string message)
        {
            string fileName = $"{_logFilePrefix}_{DateTime.Now:yyyy-MM-dd}.log";
            string filePath = Path.Combine(_logDirectory, fileName);

            try
            {
                // Check if log directory exists, if not, create it
                if (!Directory.Exists(_logDirectory))
                {
                    Directory.CreateDirectory(_logDirectory);
                }

                // Check log file size, if exceeds, create a new file
                if (File.Exists(filePath) && new FileInfo(filePath).Length >= _maxFileSizeBytes)
                {
                    // Rename existing files with incrementing numbers
                    for (int i = _maxFilesToKeep - 1; i >= 1; i--)
                    {
                        string currentFile = $"{_logFilePrefix}_{DateTime.Now.AddDays(-i):yyyy-MM-dd}.log";
                        string newFile = $"{_logFilePrefix}_{DateTime.Now.AddDays(-i + 1):yyyy-MM-dd}.log";
                        string currentFilePath = Path.Combine(_logDirectory, currentFile);
                        string newFilePath = Path.Combine(_logDirectory, newFile);

                        if (File.Exists(currentFilePath))
                        {
                            File.Move(currentFilePath, newFilePath);
                        }
                    }

                    // Create a new log file
                    fileName = $"{_logFilePrefix}_{DateTime.Now:yyyy-MM-dd}.log";
                    filePath = Path.Combine(_logDirectory, fileName);
                }

                // Write message to log file
                using (StreamWriter streamWriter = new StreamWriter(filePath, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions during logging process
                Console.WriteLine($"Error occurred while logging: {ex.Message}");
            }
        }
    }
}
