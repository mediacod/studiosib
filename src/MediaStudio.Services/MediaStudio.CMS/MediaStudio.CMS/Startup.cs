namespace MediaStudio
{
    using System;
    using System.IO;
    using System.Reflection;
    using MediaStudio.Classes;
    using MediaStudio.Core;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        private static readonly int LimitUploadFile = 209715200;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Media Studio",
                    Version = "v1",
                    Description = "The MediaStudio HTTP API",
                });

                var xmlMStudio = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlMStudioService = "MediaStudioService.xml";
                var xmlDbContext = "DBContext.xml";

                var xmlPathMStudio = Path.Combine(AppContext.BaseDirectory, xmlMStudio);
                var xmlPathMStudioService = Path.Combine(AppContext.BaseDirectory, xmlMStudioService);
                var xmlPathDbContext = Path.Combine(AppContext.BaseDirectory, xmlDbContext);

                option.IncludeXmlComments(xmlPathMStudio);
                option.IncludeXmlComments(xmlPathMStudioService);
                option.IncludeXmlComments(xmlPathDbContext);
            });

            services.Configure<FormOptions>(formOption =>
            {
                formOption.ValueLengthLimit = LimitUploadFile;
                formOption.MultipartBodyLengthLimit = LimitUploadFile; // In case of multipart
            });

            services.AddContextDb(Configuration);
            services.AddAuthenticationUser(Configuration);
            services.AddCustomAuthorization();
            services.AddAudit();
            services.AddBuilderService();
            services.AddCloudService();
            services.AddPathCloudService();
            services.AddInterfaceService();
            services.AddAudioControllerService();
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseCors(
                builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            // .AllowCredentials() //чтобы получить запрос с XMLHttpRequest.withCredentials и отправить cookies обратно клиенту

            // app.UseHsts();
            // app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Hello, see you soon!!!"));
            });

            app.UseSwagger().UseSwaggerUI(sw =>
                {
                    sw.SwaggerEndpoint("/swagger/v1/swagger.json", "The MediaStudio HTTP API");
                });
        }
    }
}
