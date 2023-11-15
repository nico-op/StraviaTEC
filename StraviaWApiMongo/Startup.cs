using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;  // Add this line
using MongoDB.Driver;
using Microsoft.OpenApi.Models;

using StraviaWApiMongo.Services;


namespace StraviaWApiMongo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configurar la conexión a la base de datos MongoDBSettings
            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDBSettings"));
            services.AddSingleton<IMongoDBSettings>(d => d.GetRequiredService<IOptions<MongoDBSettings>>().Value);
            services.AddSingleton<MongoDBService>();

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
