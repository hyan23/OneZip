using System;
using System.Collections.Generic;

namespace OneZip
{
    class TaskList : List<OneZipTask>
    {
        /// <summary>
        /// Get the list from file
        /// </summary>
        /// <returns></returns>
        public static TaskList Get()
        {
            return Serialization.YamlDeserialize<TaskList>(Path);
        }

        /// <summary>
        /// Serialized file path
        /// </summary>
        public static readonly string Path = "TaskList.yml";
    }
}
