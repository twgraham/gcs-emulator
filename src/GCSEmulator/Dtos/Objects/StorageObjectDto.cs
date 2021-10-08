using System;
using System.Collections.Generic;
using GCSEmulator.Dtos.Shared;

namespace GCSEmulator.Dtos.Objects
{
    public class StorageObjectDto : IResourceResponse
    {
        public string Kind => "storage#object";
        public string Id { get; set; }
        public string SelfLink { get; set; }
        public string Name { get; set; }
        public string Bucket { get; set; }
        public string Generation { get; set; }
        public string Metageneration { get; set; }
        public string ContentType { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime Updated { get; set; }
        public DateTime CustomTime { get; set; }
        public DateTime? TimeDeleted { get; set; }
        public bool TemporaryHold { get; set; }
        public bool EventBasedHold { get; set; }
        public DateTime RetentionExpirationTime { get; set; }
        public string StorageClass { get; set; }
        public DateTime TimeStorageClassUpdated { get; set; }
        public string Size { get; set; }
        public string Md5Hash { get; set; }
        public string MediaLink { get; set; }
        public string ContentEncoding { get; set; }
        public string ContentDisposition { get; set; }
        public string ContentLanguage { get; set; }
        public string CacheControl { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public List<ObjectAccessControlDto> Acl { get; set; }
        public OwnerDto Owner { get; set; }
        public string Crc32c { get; set; }
        public int ComponentCount { get; set; }
        public string Etag { get; set; }
        public EncryptionDto CustomerEncryption { get; set; }
        public string KmsKeyName { get; set; }
    }
}