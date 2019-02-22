/* OneZip
 * Create zip file for modified folder with one click
 * Author: Haiyang Li
 * Date: 2019.02.22
 * Email: hyan23lee@hotmail.com
*/

using System;
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
                        DateTime lastModifiedTime = Directory.GetLastWriteTime(task.SrcFolder);
                        bool createZipFile = !File.Exists(task.ZipFilePath);

                        if (dict.ContainsKey(task.Identity))
                        {
                            if (dict[task.Identity] < lastModifiedTime)
                            {
                                createZipFile = true;
                            }
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

        private static void TestMain()
        {
            OneZipTask.TestMain();
        }
    }
}
