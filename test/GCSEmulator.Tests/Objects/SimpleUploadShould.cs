using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GCSEmulator.Data.Models.Buckets;
using GCSEmulator.Dtos.Objects;
using Google.Cloud.Storage.V1;
using Raven.Embedded;
using Xunit;

namespace GCSEmulator.Tests.Objects
{
    public class SimpleUploadShould : IClassFixture<ServerClientFixtures>
    {
        private readonly ServerClientFixtures _fixtures;

        public SimpleUploadShould(ServerClientFixtures fixtures)
        {
            _fixtures = fixtures;
        }

        [Fact]
        public async Task UploadValidObject()
        {
            var bucket = new Bucket("my-google-project", "my-cloud-bucket");
            using var session = _fixtures.OpenAsyncSession();
            await session.StoreAsync(bucket);
            await session.SaveChangesAsync();

            var client = new HttpClient(_fixtures.Server.CreateHandler()) { BaseAddress = new Uri("http://localhost/storage/v1/") };
            var response = await client.PostAsync("b/my-cloud-bucket/o?name=some-object", new ByteArrayContent(Encoding.UTF8.GetBytes("upload contents")));
            // var retrievedBucket = await _fixtures.StorageClient.UploadObjectAsync(bucket.Name, "some-object",
            //     "text/plain", new MemoryStream(Encoding.UTF8.GetBytes("upload contents")), new UploadObjectOptions { });
            EmbeddedServer.Instance.OpenStudioInBrowser();
            await response.Content.ReadFromJsonAsync<StorageObjectDto>();

        }
    }
}