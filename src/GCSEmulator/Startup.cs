using Anemonis.AspNetCore.RequestDecompression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Embedded;

namespace GCSEmulator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GCSEmulator", Version = "v1" });
            });

            services.AddResponseCompression();
            services.AddRequestDecompression(o => o.Providers.Add<GzipDecompressionProvider>());

            InitializeAndConfigureRavenDb(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase("/storage/v1");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GCSEmulator v1"));
            }

            // app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseRequestDecompression();
            app.UseResponseCompression();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private static void InitializeAndConfigureRavenDb(IServiceCollection services, IConfiguration configuration)
        {
            var ravenDbConfig = configuration.GetSection("RavenDB");
            var serverOptions = new ServerOptions();
            var overrideFrameworkVersion = ravenDbConfig.GetValue<string?>("FrameworkVersion");

            if (overrideFrameworkVersion != null)
                serverOptions.FrameworkVersion = overrideFrameworkVersion;

            EmbeddedServer.Instance.StartServer(serverOptions);

            var store = EmbeddedServer.Instance.GetDocumentStore(
                ravenDbConfig.GetValue("Database", "gcs-database"));

            IndexCreation.CreateIndexes(typeof(Program).Assembly, store);

            services.AddSingleton(store);
            services.AddTransient(p => p.GetRequiredService<IDocumentStore>().OpenAsyncSession());
        }
    }
}