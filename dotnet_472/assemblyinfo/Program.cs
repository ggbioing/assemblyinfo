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
            foreach (string file_or_dir in args)
            {
                FileAttributes fattr = File.GetAttributes(file_or_dir);

                if (fattr.HasFlag(FileAttributes.Directory))  // if file_or_dir is a Directory, iterate over all *exe and *dll
                {
                    Matcher matcher = new Matcher();
                    // matcher.AddIncludePatterns(new[] { "**/*.exe", "**/*.dll" });  // recursive on subfolder
                    matcher.AddIncludePatterns(new[] { "*.exe", "*.dll" });

                    foreach (string file in matcher.GetResultsInFullPath(file_or_dir))
                    {
                        PrintAssemblyInfo(file, all: false);
                        Console.WriteLine();
                    }
                }
                else  // if arg[0] is a File
                {
                    PrintAssemblyInfo(file_or_dir, all: false);
                    Console.WriteLine();
                }
            }
        }
        /// <summary>
        /// Print to stdout informations (name, version, etc.) about *.exe or *.dll assemblies
        /// </summary>
        /// <param name="assemblyFile">*.exe or *.dll file</param>
        /// <param name="pad">alines information lines to have the same 'pad' width</param>
        /// <param name="sep">separator between information name and information</param>
        /// <param name="sep2">separator between informations</param>
        /// <param name="all">print all informations of just a part of it</param>
        static void PrintAssemblyInfo(string assemblyFile, int pad = 18, string sep = "", string sep2 = "\n", bool all = true)
        {
            if (File.Exists(assemblyFile))
            {
                List<string> msg = new List<string>();
                FileVersionInfo fv = FileVersionInfo.GetVersionInfo(assemblyFile);
                if (all)
                {
                    msg.Add(fv.ToString());  // complete infos
                }
                else
                {
                    msg.Add("File:".PadRight(pad, ' ') + sep + fv.FileName);
                    msg.Add("InternalName:".PadRight(pad, ' ') + sep + fv.InternalName);
                    msg.Add("OriginalFileName:".PadRight(pad, ' ') + sep + fv.OriginalFilename);
                    msg.Add("FileVersion:".PadRight(pad, ' ') + sep + fv.FileVersion);
                    msg.Add("FileDescription:".PadRight(pad, ' ') + sep + fv.FileDescription);
                    msg.Add("ProductName".PadRight(pad, ' ') + sep + fv.ProductName);
                    msg.Add("ProductVersion".PadRight(pad, ' ') + sep + fv.ProductVersion);
                }
                string s = String.Join(sep2, msg.ToArray());
                Console.WriteLine(s.Trim());
            }
        }
    }
}
