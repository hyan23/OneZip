/* OneZip
 * Create zip file for modified folder with one click
 * Author: Haiyang Li
 * Date: 2019.02.22
 * Email: hyan23lee@hotmail.com
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace OneZip
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                foreach (var task in TaskList.Get())
                {
                    System.Console.WriteLine("Start task " + task.Name);

                    var dict = ModifiedTimeDict.Get() ?? new ModifiedTimeDict();
                    if (Directory.Exists(task.SrcFolder))
                    {
                        DateTime lastModifiedTime = GetLastModifiedTime(task.SrcFolder);
                        // System.Console.WriteLine(lastModifiedTime);
                        bool createZipFile = !File.Exists(task.ZipFilePath);

                        if (dict.ContainsKey(task.Identity))
                        {
                            if (dict[task.Identity] < lastModifiedTime)
                            {
                                createZipFile = true;
                            }
                            // System.Console.WriteLine(dict[task.Identity]);
                        }
                        else
                        {
                            createZipFile = true;
                        }

                        if (createZipFile)
                        {
                            System.Console.WriteLine("Compressing {0}", task.SrcFolder);
                            bool confirmation = true;

                            if (File.Exists(task.ZipFilePath))
                            {
                                if (task.Confirmation)
                                {
                                    System.Console.Write(string.Format("File {0} is existing, overwrite?(Y/n)", task.ZipFilePath));
                                L0:
                                    string answer = System.Console.ReadLine();
                                    if (answer == "Y")
                                    {
                                        System.Console.WriteLine("Overwriting {0}", task.ZipFilePath);
                                    }
                                    else if (answer == "n")
                                    {
                                        confirmation = false;
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("Please enter 'Y' or 'n'");
                                        goto L0;
                                    }
                                }
                            }

                            if (confirmation)
                            {
                                if (File.Exists(task.ZipFilePath))
                                {
                                    File.Delete(task.ZipFilePath);
                                }
                                ZipFile.CreateFromDirectory(task.SrcFolder, task.ZipFilePath, task.Level, false);
                                dict[task.Identity] = lastModifiedTime;
                                ModifiedTimeDict.Serialize(dict);
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("Ignore {0}", task.SrcFolder);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Error: folder {0} does not exist", task.SrcFolder);
                    }
                    System.Console.WriteLine("Finished task " + task.Name);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }

            System.Console.WriteLine("All done");
            System.Console.ReadLine();
        }

        /// <summary>
        /// Get the last modified time for file or folder
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Exception"></exception>
        private static DateTime GetLastModifiedTime(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            if (!attr.HasFlag(FileAttributes.Directory))
            {
                return File.GetLastWriteTime(path);
            }

            DirectoryInfo root = new DirectoryInfo(path);
            DateTime lastModifiedTime = root.LastWriteTime;
            Stack<DirectoryInfo> st = new Stack<DirectoryInfo>();
            st.Push(new DirectoryInfo(path));

            while (st.Count > 0)
            {
                DirectoryInfo subDir = st.Pop();
                if (subDir.LastWriteTime > lastModifiedTime)
                {
                    lastModifiedTime = subDir.LastWriteTime;
                }
                foreach (var file in subDir.GetFiles())
                {
                    if (file.LastWriteTime > lastModifiedTime)
                    {
                        lastModifiedTime = file.LastWriteTime;
                    }
                }
                foreach (var dir in subDir.GetDirectories())
                {
                    st.Push(dir);
                }
            }

            return lastModifiedTime;
        }

        private static void TestMain()
        {
            OneZipTask.TestMain();
        }
    }
}
