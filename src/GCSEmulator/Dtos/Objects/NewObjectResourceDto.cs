using System;
using System.Collections.Generic;

namespace GCSEmulator.Dtos.Objects
{
    public class NewObjectResourceDto
    {
        public List<ObjectAccessControlDto> Acl { get; set; }
        public string CacheControl { get; set; }
        public string ContentDisposition { get; set; }
        public string ContentEncoding { get; set; }
        public string ContentLanguage { get; set; }
        public string ContentType { get; set; }
        public string Crc32c { get; set; }
        public DateTime CustomTime { get; set; }
        public bool EventBasedHold { get; set; }
        public string Md5Hash { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Name { get; set; }
        public string StorageClass { get; set; }
        public bool TemporaryHold { get; set; }
    }
}