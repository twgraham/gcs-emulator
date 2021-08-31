using System;
using System.Collections.Generic;

namespace GCSEmulator.Data.Models.Buckets
{
    public class Bucket : IEntity
    {
        public string Id { get; private set; }
        public string ProjectId { get; init; }
        public ulong ProjectNumber { get; private set; }
        public string Name { get; init; }
        public DateTime TimeCreated { get; private set; }
        public DateTime Updated { get; private set; }
        public bool DefaultEventBasedHold { get; set; }
        public BucketRetentionPolicy RetentionPolicy { get; set; } = new();
        public long Metageneration { get; private set; }
        public List<BucketAccessControl> Acl { get; set; } = new();
        public List<DefaultObjectAccessControl> DefaultObjectAcl { get; set; } = new();
        public BucketIamConfiguration IamConfiguration { get; set; } = new();
        public Encryption Encryption { get; set; } = new();
        public string Location { get; set; }
        public string LocationType { get; set; }
        public WebsiteSettings Website { get; set; } = new();
        public LoggingSettings Logging { get; set; } = new();
        public VersioningSettings Versioning { get; set; } = new();
        public List<CorsPolicy> Cors { get; set; } = new();
        public LifecycleSettings Lifecycle { get; set; } = new();
        public Dictionary<string, string> Labels { get; set; } = new();
        public string StorageClass { get; set; }

        /// <summary>
        /// The bucket's billing configuration.
        /// </summary>
        public BillingSettings Billing { get; set; } = new();

        public Bucket(string projectId, string name)
        {
            ProjectId = projectId;
            Name = name;
        }
    }
}