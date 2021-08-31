using System;
using GCSEmulator.Data.Models.Buckets;
using GCSEmulator.Dtos.Shared;
using Newtonsoft.Json;

namespace GCSEmulator.Dtos.Buckets
{
    public class BucketAccessControlDto : IResourceResponse
    {
        public string Kind => "storage#bucketAccessControl";

        public string Bucket { get; set; }
        public string Entity { get; set; }
        public string Role { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Email { get; set; }

        public string EntityId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Domain { get; set; }
        public ProjectTeamDto ProjectTeam { get; set; }
        public string Etag { get; set; }

        public static BucketAccessControlDto Create(Bucket bucket, BucketAccessControl accessControl)
        {
            return new BucketAccessControlDto
            {
                Bucket = bucket.Name,
                Entity = accessControl.Entity,
                EntityId = accessControl.EntityId,
                Domain = accessControl.Domain,
                Email = accessControl.Domain,
                Etag = accessControl.GetHashCode().ToString(),
                Role = accessControl.Role,
                ProjectTeam = ProjectTeamDto.Create(accessControl.ProjectTeam, bucket.ProjectNumber.ToString())
            };
        }
    }
}