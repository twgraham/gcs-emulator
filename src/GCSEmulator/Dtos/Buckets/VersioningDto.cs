using GCSEmulator.Data.Models.Buckets;

namespace GCSEmulator.Dtos.Buckets
{
    /// <summary>
    /// The bucket's versioning configuration.
    /// </summary>
    public sealed class VersioningDto
    {
        /// <summary>
        /// While set to true, versioning is fully enabled for this bucket.
        /// </summary>
        public bool Enabled { get; set; }

        public static VersioningDto Create(VersioningSettings versioningSettings)
        {
            return new VersioningDto
            {
                Enabled = versioningSettings.Enabled
            };
        }
    }
}