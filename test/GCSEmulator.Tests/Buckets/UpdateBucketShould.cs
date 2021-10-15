using System.Threading.Tasks;
using FluentAssertions;
using GCSEmulator.Data.Models.Buckets;
using Xunit;

namespace GCSEmulator.Tests.Buckets
{
    public class UpdateBucketShould : IClassFixture<ServerClientFixtures>
    {
        private readonly ServerClientFixtures _fixtures;

        public UpdateBucketShould(ServerClientFixtures fixtures)
        {
            _fixtures = fixtures;
        }

        [Fact]
        public async Task UpdateBucketWithValidRequest()
        {
            // GIVEN an existing bucket
            var bucket = new Bucket("my-google-project", "my-cloud-bucket") { DefaultEventBasedHold = false };
            using var session = _fixtures.OpenAsyncSession();
            await session.StoreAsync(bucket);
            await session.SaveChangesAsync();

            // WHEN we try to update the bucket
            var retrievedBucket = await _fixtures.StorageClient.UpdateBucketAsync(new Google.Apis.Storage.v1.Data.Bucket
            {
                Name = "my-cloud-bucket",
                DefaultEventBasedHold = true
            });

            // THEN the bucket updates
            retrievedBucket.DefaultEventBasedHold.Should().Be(true);
        }
    }
}