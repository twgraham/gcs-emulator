using System.Collections.Generic;

namespace GCSEmulator.Dtos.Buckets
{
    public class ListBucketsResponseDto : IResourceResponse
    {
        public string Kind => "storage#buckets";

        /// <summary>
        /// The continuation token. Provide this value as the pageToken of a subsequent request in order to return the next page of results.
        /// Note that the next page may be empty. If this is the last page of results, then no continuation token will be returned.
        /// </summary>
        /// <remarks>
        /// The nextPageToken is the name of the last bucket in the returned list.
        /// In a subsequent list request using the pageToken, items that come after the token are shown (up to maxResults).
        /// </remarks>
        public string? NextPageToken { get; set; }

        /// <summary>
        /// The list of buckets.
        /// </summary>
        public List<BucketResourceDto> Items { get; set; }
    }
}