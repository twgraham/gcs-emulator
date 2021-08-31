using GCSEmulator.Data.Models.Buckets;

namespace GCSEmulator.Dtos.Buckets
{
    /// <summary>
    /// Encryption configuration for a bucket.
    /// </summary>
    public class EncryptionDto
    {
        /// <summary>
        /// A Cloud KMS key that will be used to encrypt objects inserted into this bucket, if no encryption method is specified.
        /// </summary>
        public string? DefaultKmsKeyName { get; set; }

        public static EncryptionDto Create(Encryption encryption)
        {
            return new EncryptionDto
            {
                DefaultKmsKeyName = encryption.DefaultKmsKeyName
            };
        }
    }
}