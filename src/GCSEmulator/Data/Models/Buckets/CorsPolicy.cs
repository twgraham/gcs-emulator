using System.Collections.Generic;

namespace GCSEmulator.Data.Models.Buckets
{
    public class CorsPolicy
    {
        public List<string> Origin { get; set; }
        public List<string> Method { get; set; }
        public List<string> ResponseHeader { get; set; }
        public int MaxAgeSeconds { get; set; }
    }
}