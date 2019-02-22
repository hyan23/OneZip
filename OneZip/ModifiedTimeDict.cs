using System;
using System.Collections.Generic;

namespace OneZip
{
    class ModifiedTimeDict : Dictionary<string, DateTime>
    {
        /// <summary>
        /// Get the dict from file, if the file isn't existing, returns null
        /// </summary>
        /// <returns></returns>
        public static ModifiedTimeDict Get()
        {
            try
            {
                return Serialization.YamlDeserialize<ModifiedTimeDict>(Path);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Serialize the dict
        /// </summary>
        /// <param name="dict"></param>
        public static void Serialize(ModifiedTimeDict dict)
        {
            Serialization.YamlSerialize(Path, dict);
        }

        /// <summary>
        /// Serialization file path
        /// </summary>
        public static readonly string Path = "ModifiedTimeDict.yml";
    }
}
