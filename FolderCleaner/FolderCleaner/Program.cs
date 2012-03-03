using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace BenProjects
{
    class FolderCleaner
    {
        private ArgumentsParser arguments;
        private List<string> filePatterns = new List<string>();
        private List<string> folderPatterns = new List<string>();

        private const string DEFAULT_TARGET_File = "targets.txt";
        private const string PARAMETER_TARGET_FILE = "tf";
        private const string PARAMETER_TARGET_FOLDER = "d";
        private const string PARAMETER_TEST_MODE = "tm";
        private const string PARAMETER_HELP = "?";

        static void Main(string[] args)
        {
            FolderCleaner cleaner = new FolderCleaner();

            cleaner.arguments = new ArgumentsParser(args);

            if (cleaner.arguments[PARAMETER_HELP] == "true")
            {
                cleaner.DisplayHelp();
                return;
            }

            if (string.IsNullOrEmpty(cleaner.arguments[PARAMETER_TARGET_FILE]))
            {
                cleaner.ParseTargetsFile(DEFAULT_TARGET_File);
            }
            else
            {
                cleaner.ParseTargetsFile(cleaner.arguments[PARAMETER_TARGET_FILE]);
            }


            DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.CurrentDirectory);

            if (!string.IsNullOrEmpty(cleaner.arguments[PARAMETER_TARGET_FOLDER]))
            {
                dirInfo = new DirectoryInfo(cleaner.arguments[PARAMETER_TARGET_FOLDER]);
            }
            cleaner.CleanFolder(dirInfo);

            cleaner.DisplayImformation("Cleaning is finished.");
        }
        private void DisplayHelp()
        {
            Console.WriteLine("Folder Cleaner 1.0");
            Console.WriteLine("Usage: FolderCleaner [-options]");
            Console.WriteLine(" -?                 print the help message.");
            Console.WriteLine(" -tf=<target file>  the file specifys target files should be deleted");
            Console.WriteLine(" -tm                test mode");
            Console.WriteLine(" -d=<working dir>  ");
        }

        private void CleanFolder(DirectoryInfo folder)
        {
            foreach (FileInfo file in folder.GetFiles())
            {
                if (file.FullName != Assembly.GetExecutingAssembly().Location 
                    && IsFileMatch(file))
                {
                    this.DeleteTarget(file);
                }
            }

            foreach (DirectoryInfo subFolder in folder.GetDirectories())
            {
                if (IsFolderMatch(subFolder))
                {
                    this.DeleteTarget(subFolder);
                }
                else
                {
                    CleanFolder(subFolder);
                }
            }
        }

        /// <summary>
        /// Support following patterns
        /// *.exe
        /// dir1\*
        /// dir1\*.*
        /// dir\*\*.dll
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool IsFileMatch(FileInfo file)
        {
            foreach (string pattern in this.filePatterns)
            {
                Regex spliter = new Regex(@"\\|\/", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                string[] patternParts = spliter.Split(pattern);
                string[] filePathParts = spliter.Split(file.FullName);

                int patternLen = patternParts.Length;
                int pathLen = filePathParts.Length;
                int times = Math.Min(patternLen, pathLen);

                int i = 0;
                for (i = 1; i <= times; i++ )
                {
                    if (patternParts[patternLen - i] == "*")
                        continue;

                    if (i == 1)
                    {                     
                        if (patternParts[patternLen -i].StartsWith("*.") 
                            && patternParts[patternLen -i].EndsWith(file.Extension)
                            && !string.IsNullOrEmpty(file.Extension))
                        {
                            continue;
                        }
                    }
                    if (string.Compare(patternParts[patternLen - i], filePathParts[pathLen - i], StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        continue;
                    }
                    else
                    {
                        break;   //Dose not match current parrent part
                    }
                }
                if (i > times)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Support following patterns
        /// bin\
        /// dir1/*/
        /// 
        /// </summary>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        private bool IsFolderMatch(DirectoryInfo subFolder)
        {
            foreach (string pattern in this.folderPatterns)
            {
                Regex spliter = new Regex(@"\\|\/", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                //remove the / or \ at the end of folder pattern
                string[] patternParts = spliter.Split( pattern.Remove(pattern.Length - 1, 1));
                string[] filePathParts = spliter.Split(subFolder.FullName);

                int patternLen = patternParts.Length;
                int pathLen = filePathParts.Length;
                int times = Math.Min(patternLen, pathLen);

                int i = 0;
                for (i = 1; i <= times; i++)
                {
                    if (patternParts[patternLen - i] == "*")
                        continue;

                    if (string.Compare(patternParts[patternLen - i], filePathParts[pathLen - i], StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        continue;
                    }
                    else
                    {
                        break;   //Dose not match current parrent part
                    }
                }
                if(i > times)
                {
                    return true;
                }
            }
            return false;
        }

        private void DeleteTarget(FileSystemInfo fi)
        {
            string type = "file";
            if (fi is FileInfo)
            {
                type = "foler";
            }


            if (this.arguments[PARAMETER_TEST_MODE] == "true")
            {
                this.DisplayImformation(string.Format("{0} {1} should be deleted ", type, fi.FullName));
            }
            else
            {
                try
                {
                    if (fi is FieldInfo)
                    {
                        File.Delete(fi.FullName);
                    }
                    else if (fi is DirectoryInfo)
                    {
                        Directory.Delete(fi.FullName, true);
                    }
                    this.DisplayImformation(string.Format("Delete {0} successful ", fi.FullName));
                }
                catch (Exception exp)
                {
                    this.DisplayImformation(string.Format("Delete {0} failed : {1}", fi.FullName, exp.Message));
                }
            }
        }

        private void DisplayImformation(string msg)
        {
            Console.WriteLine(msg);
        }

        private void ParseTargetsFile(string fileName)
        {
            if (File.Exists(fileName))
            {
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

                        //if (sline.EndsWith(Path.DirectorySeparatorChar.ToString()))
                        if (sline.EndsWith("/") || sline.EndsWith(@"\"))
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
            else
            {
                this.folderPatterns.Add(@"bin\");
                this.folderPatterns.Add(@"obj\");

                this.filePatterns.Add("*.bin");
                this.filePatterns.Add("*.exe");
                this.filePatterns.Add("*.dll");
                this.filePatterns.Add("*.pdb");
                this.filePatterns.Add("*.manifest");
            }
        }
    }
}
