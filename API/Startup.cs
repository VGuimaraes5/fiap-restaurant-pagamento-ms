using Microsoft.AspNetCore.Mvc.Authorization;
using Infrastructure.Configurations;
using Infrastructure.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Application.Consumers;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public AppConfigurations AppConfigurations { get; set; }

        private readonly IWebHostEnvironment Environment;

        public void ConfigureServices(IServiceCollection services)
        {
            this.AppConfigurations = services.RegisterConfigurations(Configuration);

            RegisterServiceExtensions.RegisterService(services);

            services.AddRazorPages();
            services.AddSwaggerGen();
            services.AddCognitoIdentity();
            services.AuthenticationConfigurations(Configuration);
            services.AddHttpContextAccessor();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AllowAnonymousFilter());
            }).AddNewtonsoftJson(options => ConfigureJsonOptionsSerializer(options.SerializerSettings));

            services.RegisterDatabases(Configuration, Environment);

            services.AddMemoryCache();
            services.AddControllers();

            services.AddCors();

            services.AddHostedService<PedidoCreateConsumer>();
        }

        private void ConfigureJsonOptionsSerializer(JsonSerializerSettings serializerSettings)
        {
            serializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            serializerSettings.Converters.Add(new StringEnumConverter());
            serializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("x-frame-options", "DENY");

                await next();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
            });
            app.UseReDoc(c =>
            {
                c.DocumentTitle = "Documento REDOC API";
                c.SpecUrl = "/swagger/v1/swagger.json";
                c.RoutePrefix = "";
            });

            app.ExecuteMigration();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
