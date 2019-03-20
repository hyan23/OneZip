using System;
using System.IO.Compression;
using System.Text;

namespace OneZip
{
    class OneZipTask
    {
        public static void TestMain()
        {
            OneZipTask task = new OneZipTask()
            {
                Name = "任务"
            };
            System.Console.WriteLine(task);
        }

        public OneZipTask()
        {
            Name = "Task name";
            SrcFolder = "The path to the source folder";
            ZipFilePath = "The path to the output file";
            Confirmation = false;
            Level = CompressionLevel.NoCompression;
        }

        public override string ToString()
        {
            return string.Format("Name: {0}\nSrcFolder: {1}\nZipFilePath: {2}", Name, SrcFolder, ZipFilePath);
        }

        /// <summary>
        /// Task name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The path to the source folder
        /// </summary>
        public string SrcFolder { get; set; }

        /// <summary>
        /// The path to the output file
        /// </summary>
        public string ZipFilePath { get; set; }

        /// <summary>
        /// Ask the user for confirmation before overwrite an existing file
        /// </summary>
        public bool Confirmation { get; set; }

        /// <summary>
        /// Compression level, possible values are NoCompression, Fastest, Optimal
        /// </summary>
        public CompressionLevel Level { get; set; }
    }
}
