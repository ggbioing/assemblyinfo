using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.FileSystemGlobbing;

namespace assemblyinfo
{
    class Program
    {
        static void Main(string[] args)
        {
            //string file_or_dir = Path.GetFullPath(args[0]);
            string file_or_dir = args[0];

            FileAttributes fattr = File.GetAttributes(file_or_dir);

            if (fattr.HasFlag(FileAttributes.Directory))
            {
                Matcher matcher = new Matcher();
                //matcher.AddIncludePatterns(new[] { "**/*.exe" });
                //matcher.AddIncludePatterns(new[] { "**/*.exe", "**/*.dll" });
                matcher.AddIncludePatterns(new[] { "*.exe", "*.dll" });

                foreach (string file in matcher.GetResultsInFullPath(file_or_dir))
                {
                    PrintAssemblyInfo(file);
                }
            }
            else
            {
                PrintAssemblyInfo(file_or_dir);
            }
        }
        static void PrintAssemblyInfo(string assemblyFile, string sep = "\t")
        {
            if (File.Exists(assemblyFile))
            {
                List<string> msg = new List<string>();
                FileVersionInfo fv = FileVersionInfo.GetVersionInfo(assemblyFile);
                //msg.Add(Path.GetFileName(fv.FileName));
                //// msg.Add("File" + sep + fv.FileName);
                //// msg.Add("InternalName" + sep + fv.InternalName);
                //// msg.Add("OriginalFileName" + sep + fv.OriginalFilename);
                //msg.Add("ProductName" + sep + fv.ProductName);
                //msg.Add("ProductVersion" + sep + fv.ProductVersion);
                //msg.Add("FileVersion" + sep + fv.FileVersion);
                msg.Add(fv.ToString());  // complete infos
                string s = String.Join(sep, msg.ToArray());
                Console.WriteLine(s.Trim());
            }
        }
    }
}
