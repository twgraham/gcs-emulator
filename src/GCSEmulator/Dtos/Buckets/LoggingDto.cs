using GCSEmulator.Data.Models.Buckets;

namespace GCSEmulator.Dtos.Buckets
{
    /// <summary>
    /// The bucket's logging configuration, which defines the destination bucket and optional name prefix for the current bucket's logs.
    /// </summary>
    public sealed class LoggingDto
    {
        /// <summary>
        /// The destination bucket where the current bucket's logs should be placed.
        /// </summary>
        public string LogBucket { get; set; }

        /// <summary>
        /// A prefix for log object names. The default prefix is the bucket name.
        /// </summary>
        public string LogObjectPrefix { get; set; }

        public static LoggingDto Create(LoggingSettings loggingSettings)
        {
            return new LoggingDto
            {
                LogBucket = loggingSettings.LogBucket,
                LogObjectPrefix = loggingSettings.LogObjectPrefix
            };
        }
    }
}