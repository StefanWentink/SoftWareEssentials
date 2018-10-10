namespace SWE.Packer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var folderPath = AppDomain.CurrentDomain.BaseDirectory.Split(@"\SWE.Packer").First();
            var files = GetFiles(folderPath);
            var apiKey = string.Empty;

            if (!files.Any())
            {
                return;
            }

            while (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("Enter nuget ApiKey:");
                string line = Console.ReadLine(); // Get string from user

                if (line.Trim().Length == 46)
                {
                    apiKey = line.Trim();
                }
                else
                {
                    Console.WriteLine("ApiKey invalid:");
                }
            }

            foreach (var file in files)
            {
                Console.WriteLine($"Do you want to publish {file.Key}? (y/n)");
                if (Console.ReadKey().Key.ToString().Equals("y", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine(string.Empty);
                    var command = $"/C nuget push \"{file.Value}\" {apiKey} -Source https://api.nuget.org/v3/index.json";

                    Process.Start("CMD.exe", command);
                }
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static Dictionary<string, string> GetFiles(string folderPath)
        {
            var files = Directory.GetFiles(folderPath, "*.nupkg", SearchOption.AllDirectories);

            return files
                .GroupBy(GetAssemblyName)
                .ToDictionary(x => x.Key, x => x.ToList().OrderByDescending(v => v).First());
        }

        private static string GetAssemblyName(string filePath)
        {
            return filePath.Split(@"\bin").First().Split(@"\").Last();
        }
    }
}