using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GCSEmulator.Data.Models.Buckets;
using GCSEmulator.Dtos.Shared;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GCSEmulator.Dtos.Buckets
{
    [PublicAPI]
    public sealed class BucketResourceDto : IResourceResponse
    {
        public string Kind => "storage#bucket";

        public string Id { get; set; }
        public string SelfLink { get; set; }
        public string ProjectNumber { get; set; }
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime Updated { get; set; }
        public bool DefaultEventBasedHold { get; set; }
        public RetentionPolicyDto RetentionPolicy { get; set; }
        public string Metageneration { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<BucketAccessControlDto>? Acl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<DefaultObjectAccessControlDto>? DefaultObjectAcl { get; set; }
        public BucketIamConfigurationDto IamConfiguration { get; set; }
        public EncryptionDto Encryption { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public OwnerDto Owner { get; set; }
        public string Location { get; set; }
        public string LocationType { get; set; }
        public WebsiteDto Website { get; set; }
        public LoggingDto Logging { get; set; }
        public VersioningDto Versioning { get; set; }
        public List<CorsEntryDto> Cors { get; set; }
        public LifecycleDto Lifecycle { get; set; }
        public Dictionary<string, string> Labels { get; set; }
        public string StorageClass { get; set; }

        /// <summary>
        /// The bucket's billing configuration.
        /// </summary>
        public BillingDto Billing { get; set; }

        /// <summary>
        ///  HTTP 1.1 Entity tag for the bucket.
        /// </summary>
        public string Etag { get; set; }

        public static BucketResourceDto Create(Bucket bucket, Projection projection)
        {
            return new BucketResourceDto
            {
                Acl = projection == Projection.NoAcl
                    ? null
                    : bucket.Acl.Select(x => BucketAccessControlDto.Create(bucket, x)).ToList(),
                Billing = BillingDto.Create(bucket.Billing),
                Cors = bucket.Cors.Select(CorsEntryDto.Create).ToList(),
                Encryption = EncryptionDto.Create(bucket.Encryption),
                Etag = Convert.ToBase64String(Encoding.UTF8.GetBytes(bucket.Updated.ToString("u"))), // bucket.GetHashCode().ToString(),
                Id = bucket.Name,
                Name = bucket.Name,
                Labels = bucket.Labels,
                Lifecycle = LifecycleDto.Create(bucket.Lifecycle),
                Location = bucket.Location,
                LocationType = bucket.LocationType,
                Logging = LoggingDto.Create(bucket.Logging),
                Metageneration = bucket.Metageneration.ToString(),
                Owner = new OwnerDto { },
                Updated = bucket.Updated,
                Versioning = VersioningDto.Create(bucket.Versioning),
                Website = WebsiteDto.Create(bucket.Website),
                IamConfiguration = BucketIamConfigurationDto.Create(bucket.IamConfiguration),
                ProjectNumber = bucket.ProjectNumber.ToString(),
                RetentionPolicy = RetentionPolicyDto.Create(bucket.RetentionPolicy),
                // TODO: SelfLink =,
                StorageClass = bucket.StorageClass,
                TimeCreated = bucket.TimeCreated,
                DefaultObjectAcl = bucket.DefaultObjectAcl.Select(x => DefaultObjectAccessControlDto.Create(bucket, x)).ToList(),
                DefaultEventBasedHold = bucket.DefaultEventBasedHold
            };
        }
    }
}