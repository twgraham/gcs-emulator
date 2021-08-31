using System.Collections.Generic;
using GCSEmulator.Data.Models.Buckets;

namespace GCSEmulator.Dtos.Buckets
{
    public class CorsEntryDto
    {
        public List<string> Origin { get; set; }
        public List<string> Method { get; set; }
        public List<string> ResponseHeader { get; set; }
        public int MaxAgeSeconds { get; set; }

        public static CorsEntryDto Create(CorsPolicy corsPolicy)
        {
            return new CorsEntryDto
            {
                Origin = new List<string>(corsPolicy.Origin),
                Method = new List<string>(corsPolicy.Method),
                ResponseHeader = new List<string>(corsPolicy.ResponseHeader),
                MaxAgeSeconds = corsPolicy.MaxAgeSeconds
            };
        }
    }
}