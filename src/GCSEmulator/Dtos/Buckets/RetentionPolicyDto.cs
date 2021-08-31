using System;
using System.ComponentModel.DataAnnotations;
using GCSEmulator.Data.Models.Buckets;

namespace GCSEmulator.Dtos.Buckets
{
    /// <summary>
    /// The bucket's retention policy, which defines the minimum age an object in the bucket must reach before it can be deleted or replaced.
    /// </summary>
    public class RetentionPolicyDto
    {
        /// <summary>
        /// The time from which the retentionPolicy was effective, in RFC 3339 format
        /// </summary>
        public DateTime EffectiveTime { get; set; }

        public bool IsLocked { get; set; }

        /// <summary>
        /// The period of time, in seconds, that objects in the bucket must be retained and cannot be deleted, replaced, or made noncurrent.
        /// </summary>
        [Range(1, 3_155_760_000)]
        public string RetentionPeriod { get; set; }

        public static RetentionPolicyDto Create(BucketRetentionPolicy retentionPolicy)
        {
            return new RetentionPolicyDto
            {
                EffectiveTime = retentionPolicy.EffectiveTime,
                IsLocked = retentionPolicy.IsLocked,
                RetentionPeriod = retentionPolicy.RetentionPeriod.ToString()
            };
        }
    }
}