using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BenProjects
{
    class FolderCleaner
    {
        private List<string> filePatterns = new List<string>();
        private List<string> folderPatterns = new List<string>();

        private const string DEFAULT_TARGET_File = "targets.txt";
        private const string PARAMETER_TARGET_FILE = "t";
        private const string PARAMETER_TARGET_FOLDER = "f";

        static void Main(string[] args)
        {
            FolderCleaner cleaner = new FolderCleaner();

            ArgumentsParser arguments = new ArgumentsParser(args);

            if (string.IsNullOrEmpty(arguments[PARAMETER_TARGET_FILE]))
            {
                cleaner.ParseTargetsFile(DEFAULT_TARGET_File);
            }
            else
            {
                cleaner.ParseTargetsFile(arguments[PARAMETER_TARGET_FILE]);
            }


            DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.CurrentDirectory);
                
            if (!string.IsNullOrEmpty(arguments[PARAMETER_TARGET_FOLDER]))
            {
                dirInfo = new DirectoryInfo(arguments[PARAMETER_TARGET_FOLDER]);
            }
            cleaner.CleanFolder(dirInfo);
        }


        private void CleanFolder(DirectoryInfo folder)
        {
            foreach (FileInfo file in folder.GetFiles())
            {

            }

            foreach (DirectoryInfo subFolder in folder.GetDirectories())
            {
                CleanFolder(subFolder);
            }
        }

        private void DisplayImformation(string msg)
        {
            Console.WriteLine(msg);
        }

        private void ParseTargetsFile(string fileName)
        {
            List<string> patterns = new List<string>();
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(fileName);

                string sline = string.Empty;

                do
                {
                    sline = sr.ReadLine();
                    if (sline.StartsWith("#"))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(sline))
                    {
                        continue;
                    }

                    if (sline.EndsWith(Path.DirectorySeparatorChar))
                    {
                        this.folderPatterns.Add(sline);
                    }
                    else
                    {
                        this.filePatterns.Add(sline);
                    }
                }
                while (sline != null);
            }
            catch (Exception exp)
            {
                this.DisplayImformation(exp.Message);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }
    }
}
