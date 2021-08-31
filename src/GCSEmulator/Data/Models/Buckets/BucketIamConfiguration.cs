using System;

namespace GCSEmulator.Data.Models.Buckets
{
    public enum PublicAccessPrevention
    {
        Unspecified,
        Default
    }

    public class BucketIamConfiguration
    {
        public PublicAccessPrevention PublicAccessPrevention { get; set; } = PublicAccessPrevention.Unspecified;

        public UniformBucketLevelAccess UniformBucketAccess { get; set; } = new();

        public class UniformBucketLevelAccess
        {
            public bool Enabled { get; set; } = false;
            public DateTime? LockedTime { get; set; } = null;
        }
    }
}