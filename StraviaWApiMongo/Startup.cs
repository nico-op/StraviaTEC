
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace StraviaWApiMongo
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Get the MongoDB connection string from the appsettings.json file
            string connectionString = _configuration.GetConnectionString("MongoDBConnection");

            // Create a new MongoClient object
            var mongoClient = new MongoClient(connectionString);

            // Get the database name from the appsettings.json file
            string databaseName = _configuration.GetValue<string>("MongoDBSettings:DatabaseName");

            // Get the database object
            var database = mongoClient.GetDatabase(databaseName);

            // Register the database object with the DI container
            services.AddSingleton(database);

            // Add other services to the DI container
            services.AddControllers();


                        services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mongo Stravia API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            // Configurar otros servicios necesarios para tu aplicación
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors("AllowAllOrigins");

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
