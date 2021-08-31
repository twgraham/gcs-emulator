using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GCSEmulator.Data.Models.Buckets;

namespace GCSEmulator.Dtos.Buckets
{
    public class NewBucketResourceDto
    {
        [Required]
        public string? Name { get; set; }
        // public bool? DefaultEventBasedHold { get; set; }
        // public RetentionPolicyDto? RetentionPolicy { get; set; }
        // public List<BucketAccessControlDto>? Acl { get; set; }
        // public List<DefaultObjectAccessControlDto>? DefaultObjectAcl { get; set; }
        // public BucketIamConfigurationDto? IamConfiguration { get; set; }
        // public EncryptionDto? Encryption { get; set; }
        // public string? Location { get; set; }
        // public WebsiteDto? Website { get; set; }
        // public LoggingDto? Logging { get; set; }
        // public VersioningDto? Versioning { get; set; }
        // public List<CorsEntryDto>? Cors { get; set; }
        // public LifecycleDto? Lifecycle { get; set; }
        // public Dictionary<string, string>? Labels { get; set; }
        // public string? StorageClass { get; set; }
        //
        // /// <summary>
        // /// The bucket's billing configuration.
        // /// </summary>
        // public BillingDto? Billing { get; set; }

        public Bucket ToBucket(string projectId)
        {
            return new Bucket(projectId, Name)
            {
                Acl = { },
                Billing = { }
            };
        }
    }
}