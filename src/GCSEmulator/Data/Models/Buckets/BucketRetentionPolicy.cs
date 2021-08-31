using System;

namespace GCSEmulator.Data.Models.Buckets
{
    public class BucketRetentionPolicy
    {
        public DateTime EffectiveTime { get; set; }
        public bool IsLocked { get; set; }
        public ulong RetentionPeriod { get; set; }
    }
}