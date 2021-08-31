using System;
using GCSEmulator.Data.Models.Buckets;
using JetBrains.Annotations;

namespace GCSEmulator.Dtos.Buckets
{
    public enum PublicAccessPrevention
    {
        Unspecified,
        Default
    }

    [PublicAPI]
    public sealed class UniformBucketLevelAccessDto
    {
        /// <summary>
        /// Whether or not the bucket uses uniform bucket-level access. If set, access checks only use bucket-level IAM policies or above.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// The deadline time for changing iamConfiguration.uniformBucketLevelAccess.enabled from true to false, in RFC 3339 format.
        /// </summary>
        /// <remarks><see cref="Enabled">Enabled</see> may be changed from true to false until the locked time, after which the field is immutable.</remarks>
        public DateTime? LockedTime { get; set; }

        public static UniformBucketLevelAccessDto Create(BucketIamConfiguration.UniformBucketLevelAccess uniformAccess)
        {
            return new UniformBucketLevelAccessDto
            {
                Enabled = uniformAccess.Enabled,
                LockedTime = uniformAccess.LockedTime
            };
        }
    }

    /// <summary>
    /// The bucket's IAM configuration.
    /// </summary>
    [PublicAPI]
    public sealed class BucketIamConfigurationDto
    {
        public PublicAccessPrevention PublicAccessPrevention { get; set; } = PublicAccessPrevention.Unspecified;

        /// <summary>
        /// The bucket's uniform bucket-level access configuration.
        /// </summary>
        public UniformBucketLevelAccessDto UniformBucketLevelAccess { get; set; } = new();

        public static BucketIamConfigurationDto Create(BucketIamConfiguration configuration)
        {
            return new BucketIamConfigurationDto
            {
                PublicAccessPrevention = configuration.PublicAccessPrevention switch
                {
                    Data.Models.Buckets.PublicAccessPrevention.Unspecified => PublicAccessPrevention.Unspecified,
                    Data.Models.Buckets.PublicAccessPrevention.Default => PublicAccessPrevention.Default,
                    _ => throw new ArgumentOutOfRangeException()
                },
                UniformBucketLevelAccess = UniformBucketLevelAccessDto.Create(configuration.UniformBucketAccess)
            };
        }
    }
}