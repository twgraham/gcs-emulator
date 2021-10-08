using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GCSEmulator.Data.Models.Buckets;
using Google;
using Xunit;

namespace GCSEmulator.Tests.Buckets
{
    public class GetBucketShould : IClassFixture<ServerClientFixtures>
    {
        private readonly ServerClientFixtures _fixtures;

        public GetBucketShould(ServerClientFixtures fixtures)
        {
            _fixtures = fixtures;
        }

        [Fact]
        public async Task GetExistingBucket()
        {
            var bucket = new Bucket("my-google-project", "my-cloud-bucket");
            using var session = _fixtures.OpenAsyncSession();
            await session.StoreAsync(bucket);
            await session.SaveChangesAsync();

            var retrievedBucket = await _fixtures.StorageClient.GetBucketAsync(bucket.Name);
            retrievedBucket.Name.Should().Be(bucket.Name);
        }

        [Fact]
        public async Task MatchBucketCreatedByApi()
        {
            var createdBucket = await _fixtures.StorageClient.CreateBucketAsync("google-cloud-project",
                "mystoragebucket");

            var retrievedBucket = await _fixtures.StorageClient.GetBucketAsync("mystoragebucket");
            retrievedBucket.Should().BeEquivalentTo(createdBucket);
        }

        [Fact]
        public async Task DeletingABucketShould_NotShowItInTheAPI()
        {
            var createdBucket = await _fixtures.StorageClient.CreateBucketAsync("google-cloud-project",
                "mybucketcool");

            await _fixtures.StorageClient.DeleteBucketAsync(createdBucket.Name);

            var exception = await _fixtures.StorageClient.Awaiting(x => x.GetBucketAsync(createdBucket.Name))
                .Should().ThrowAsync<GoogleApiException>();

            exception.And.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}