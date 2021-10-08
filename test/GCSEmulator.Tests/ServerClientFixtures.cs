using System.Threading.Tasks;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Session;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using Xunit;

namespace GCSEmulator.Tests
{
    public class ServerClientFixtures : IAsyncLifetime
    {
        public TestServer Server { get; private set; }
        public StorageClient StorageClient { get; private set; }

        public ServerClientFixtures()
        {
            Server = new TestServer(WebHost.CreateDefaultBuilder<Startup>(new string[] {}));

            StorageClient = new StorageClientBuilder
            {
                UnauthenticatedAccess = true,
                BaseUri = "http://localhost/storage/v1/"
            }.Build();
            StorageClient.Service.HttpClient.MessageHandler.InnerHandler = Server.CreateHandler();
        }

        public IAsyncDocumentSession OpenAsyncSession()
        {
            return Server.Services.CreateScope().ServiceProvider.GetRequiredService<IAsyncDocumentSession>();
        }

        public async Task InitializeAsync()
        {
            var store = Server.Services.GetRequiredService<IDocumentStore>();
            var db = store.Database;
            await store.Maintenance.Server.SendAsync(new DeleteDatabasesOperation(db, true));
            await store.Maintenance.Server.SendAsync(new CreateDatabaseOperation(new DatabaseRecord(db)));
            await IndexCreation.CreateIndexesAsync(typeof(Startup).Assembly, store);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}