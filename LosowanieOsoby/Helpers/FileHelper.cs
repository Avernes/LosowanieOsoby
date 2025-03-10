using System.Collections.Generic;
using System.IO;

namespace LosowanieOsoby
{
    public static class FileHelper
    {
        public static string GetFilePath(string className)
        {
            return Path.Combine(FileSystem.AppDataDirectory, $"{className}.txt");
        }

        public static void SaveListToFile(string className, List<string> list)
        {
            string filePath = GetFilePath(className);
            File.WriteAllLines(filePath, list);
        }

        public static List<string> LoadListFromFile(string className)
        {
            string filePath = GetFilePath(className);
            return File.Exists(filePath) ? new List<string>(File.ReadAllLines(filePath)) : new List<string>();
        }
    }
}
