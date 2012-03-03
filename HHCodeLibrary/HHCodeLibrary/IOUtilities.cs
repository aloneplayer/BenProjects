using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace dotNetLibrary
{
    class IOUtilities
    {
        /// <summary>
        /// Demo to DirectoryInfo.EnumerateDirectories  
        /// Refer to "7 New methods to Enumerate Directory and Files in .NET 4.0"
        /// http://www.devcurry.com/2010/06/7-new-methods-to-enumerate-directory.html
        /// </summary>
        /// <param name="rootPath"></param>
        public void EnumHiddenDirectories(string rootPath)
        {

            var dir = new DirectoryInfo(rootPath)
                    .EnumerateDirectories()
                    .Where(x => x.Attributes.HasFlag(FileAttributes.Hidden))
                    .Select(x => x.Name);

            foreach (var file in dir)
            {
                Console.WriteLine(file);
            }

            Console.ReadLine();
        }

        public string GetExecutePath() 
        {
            string currentApplicationPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string currentApplicationPath2 = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            return currentApplicationPath;
        }
    }
}
