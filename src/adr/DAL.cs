using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using adr.Models;

namespace adr
{
    public static class DAL
    {
        private const string ADR_FolderName = "ADR";
        private const string ADR_File_Prefix = "adr";
        private const string ADR_Extension = "md";
        private const string ADR_Template_File = "adr-template.md";

        private const string ADR_File_Seperator = "-";

        public static string InitRepository(string projectPath)
        {
            var repo = FindADRRepository(projectPath);
            if (!string.IsNullOrEmpty(repo)) throw new ApplicationException("ADR repository already exists at " + repo);

            repo = Path.Join(projectPath, ADR_FolderName);
            Directory.CreateDirectory(repo);

            return repo;
        }

        public static string SaveADR(Models.ADR adr)
        {
            string adrPath = FindADRRepository(Environment.CurrentDirectory);
            if (string.IsNullOrEmpty(adrPath)) throw new ApplicationException("Unable to find ADR directory, are you in the right location?");

            int adrNumber = GetNextADRNumber(adrPath);
            // set a number if new
            if (!adr.Number.HasValue)
                adr.Number = adrNumber;

            Console.WriteLine("ADR Number=" + adrNumber);

            string fileName = GetADRFileName(adr);
            var filePath = Path.Join(adrPath, fileName);

            var fileContent = adr.GetContent();

            System.IO.File.WriteAllText(filePath, fileContent);
            return filePath;
        }

        private static string GetADRFileName(ADR adr)
        {
            return ADR_File_Prefix
            + ADR_File_Seperator
            + adr.Number.Value.ToString("D4")
            + ADR_File_Seperator
            + adr.Title.Trim().Replace(" ", "-")
            + "." + ADR_Extension;
        }

        private static string FindADRRepository(string projectPath)
        {
            var projectDirectory = new DirectoryInfo(projectPath);
            //Console.WriteLine("Looking in " + projectDirectory.FullName);
            //Console.WriteLine("DirName " + Path.GetFileName(projectPath));
            if (projectDirectory.Name == ADR_FolderName)
                return projectDirectory.FullName;

            return Directory.GetDirectories(projectPath).ToList().FirstOrDefault(x => Path.GetFileName(x) == ADR_FolderName);
        }

        private static int GetNextADRNumber(string adrRepositoryPath)
        {
            int highest = 0;
            foreach (var file in Directory.GetFiles(adrRepositoryPath))
            {
                Console.WriteLine("Checking file:" + file);
                if (Path.GetFileName(file).StartsWith(ADR_File_Prefix))
                {
                    Console.WriteLine("Matched file:" + file);
                    try
                    {
                        var bits = file.Split(ADR_File_Seperator);
                        int tmp = int.Parse(bits[1] ?? "0");
                        Console.WriteLine("Found record:" + tmp);
                        highest = tmp > highest ? tmp : highest;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                }
            }
            return ++highest;
        }

        public static Models.ADR LoadADR(int number)
        {
            throw new NotImplementedException();
        }
        public static Models.ADR LoadADR(string path)
        {
            throw new NotImplementedException();
        }
    }
}