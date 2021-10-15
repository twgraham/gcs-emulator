using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GCSEmulator.Data.Models.Buckets;
using GCSEmulator.Data.Models.Buckets.Indexes;
using GCSEmulator.Dtos;
using GCSEmulator.Dtos.Buckets;
using GCSEmulator.Filters.Attributes;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace GCSEmulator.Controllers
{
    [Route("/b")]
    [ValidModelStateFilter]
    public class BucketController : ControllerBase
    {
        private readonly IAsyncDocumentSession _session;

        public BucketController(IAsyncDocumentSession session)
        {
            _session = session;
        }

        /// <summary>
        /// Get a bucket
        /// </summary>
        /// <remarks>Returns metadata for the specified bucket.</remarks>
        /// <param name="bucketName">Name of the bucket</param>
        /// <param name="ifMetagenerationMatch">
        /// Makes the return of the bucket metadata conditional on whether the bucket's current metageneration matches the given value.
        /// </param>
        /// <param name="ifMetagenerationNotMatch">
        /// Makes the return of the bucket metadata conditional on whether the bucket's current metageneration does not match the given value.
        /// </param>
        /// <param name="projection"><inheritdoc cref="Projection"/></param>
        /// <returns>Bucket resource</returns>
        [HttpGet("{bucketName}")]
        [ProducesResponseType(typeof(BucketResourceDto), 200)]
        public async Task<ActionResult<BucketResourceDto>> GetBucketByName(
            [FromRoute] string bucketName,
            [FromQuery] long ifMetagenerationMatch,
            [FromQuery] long ifMetagenerationNotMatch,
            [FromQuery] Projection projection)
        {
            var storageBucket = await _session.Query<Bucket, Buckets_ByName>()
                .Where(x => x.Name == bucketName, true)
                .FirstOrDefaultAsync();

            if (storageBucket == null)
                return NotFound();

            return BucketResourceDto.Create(storageBucket, projection);
        }

        /// <summary>
        /// List buckets
        /// </summary>
        /// <remarks>
        /// Retrieves a list of buckets for a given project, ordered in the list lexicographically by name.
        /// </remarks>
        /// <see href="https://cloud.google.com/storage/docs/json_api/v1/buckets/list"/>
        /// <param name="project">A valid API project identifier.</param>
        /// <param name="maxResults">
        /// Maximum number of buckets to return in a single response. The service will use the smaller of this parameter or:
        /// <list type="bullet">
        ///     <item>200 items if projection=full.</item>
        ///     <item>1,000 items if projection=noAcl.</item>
        /// </list>
        /// </param>
        /// <param name="pageToken">
        /// A previously-returned page token representing part of the larger set of results to view.
        /// The pageToken is an encoded field containing the name of the last item (bucket) in the returned list.
        /// In a subsequent request using the pageToken, items that come after the pageToken are shown (up to maxResults).
        /// </param>
        /// <param name="prefix">Filter results to buckets whose names begin with this prefix.</param>
        /// <param name="projection"><inheritdoc cref="Projection"/></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ListBucketsResponseDto), 200)]
        public async Task<ActionResult<ListBucketsResponseDto>> ListBuckets(
            [FromQuery][Required] string project,
            [FromQuery] string? pageToken,
            [FromQuery] string? prefix,
            [FromQuery][Range(0, int.MaxValue)] int? maxResults,
            [FromQuery] Projection projection = Projection.NoAcl)
        {
            var bucketQuery = _session.Query<Bucket, Buckets_ByProjectAndName>()
                .Where(x => x.ProjectId == project, true);

            if (prefix != null)
                bucketQuery = bucketQuery.Where(x => x.Name.StartsWith(prefix), false);

            var buckets = await bucketQuery.ToListAsync();

            return new ListBucketsResponseDto
            {
                Items = buckets.Select(x => BucketResourceDto.Create(x, projection)).ToList(),
                NextPageToken = buckets.Count > 0 ? buckets[^1]?.Name : null
            };
        }

        /// <summary>
        /// Google Cloud Storage uses a flat namespace, so you can't create a bucket with a name that is already in use.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="newBucket"></param>
        /// <param name="predefinedAcl"></param>
        /// <param name="predefinedObjectAcl"></param>
        /// <param name="projection"><inheritdoc cref="Projection"/></param>
        /// <returns>Bucket resource</returns>
        [HttpPost]
        public async Task<ActionResult<BucketResourceDto>> CreateBucket(
            [FromQuery][Required] string project,
            [FromBody] NewBucketResourceDto newBucket,
            [FromQuery] PredefinedAcl predefinedAcl,
            [FromQuery] PredefinedObjectAcl predefinedObjectAcl,
            [FromQuery] Projection projection)
        {
            var storageBucket = newBucket.ToBucket(project);

            await _session.StoreAsync(storageBucket);
            await _session.SaveChangesAsync();

            return CreatedAtAction("GetBucketByName", new { bucketName = storageBucket.Name },
                BucketResourceDto.Create(storageBucket, projection));
        }

        [HttpDelete("{bucketName}")]
        public async Task<IActionResult> DeleteBucket([FromRoute] string bucketName)
        {
            var storageBucket = await _session.Query<Bucket, Buckets_ByName>()
                .Where(x => x.Name == bucketName, true)
                .FirstOrDefaultAsync();

            _session.Delete(storageBucket);
            await _session.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{bucketName}")]
        public async Task<IActionResult> UpdateBucket([FromRoute] string bucketName,
            [FromBody] UpdateBucketResourceDto bucketUpdateDto,
            [FromQuery] long ifMetagenerationMatch,
            [FromQuery] long ifMetagenerationNotMatch,
            [FromQuery] PredefinedAcl predefinedAcl,
            [FromQuery] PredefinedObjectAcl predefinedObjectAcl,
            [FromQuery] Projection projection)
        {
            var storageBucket = await _session.Query<Bucket, Buckets_ByName>()
                .Where(x => x.Name == bucketName)
                .FirstOrDefaultAsync();

            if (storageBucket == null)
                return NotFound();

            bucketUpdateDto.ApplyTo(storageBucket);

            await _session.SaveChangesAsync();

            return Ok(BucketResourceDto.Create(storageBucket, projection));
        }
    }
}