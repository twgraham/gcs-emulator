using System.Collections.Generic;

namespace GCSEmulator.Data.Models
{
    public class StorageObject : IEntity
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public Dictionary<string, string> Metadata { get; set; }

        public StorageObject(string name)
        {
            Name = name;
        }
    }
}