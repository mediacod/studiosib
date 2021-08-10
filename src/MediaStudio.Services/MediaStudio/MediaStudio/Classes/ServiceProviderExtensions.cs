namespace MediaStudio.Core
{
    using DBContext.Connect;
    using MediaStudio.Service.Services.Audit;
    using MediaStudio.Service.Services.UserHistory;
    using MediaStudioService;
    using MediaStudioService.AccountServic;
    using MediaStudioService.Builder.PageModelBuilder;
    using MediaStudioService.Core;
    using MediaStudioService.Core.Interfaces;
    using MediaStudioService.ModelBulder;
    using MediaStudioService.Models.Input;
    using MediaStudioService.Service.ResourceService;
    using MediaStudioService.Services;
    using MediaStudioService.Services.AudioService;
    using MediaStudioService.Services.audit;
    using MediaStudioService.Services.UI;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceProviderExtensions
    {
        public static void AddContextDb(this IServiceCollection services, IConfiguration Configuration)
        {
            string connectionString = Configuration.GetConnectionString("PostgreSQL");
            services.AddDbContext<MediaStudioContext>(options => options.UseNpgsql(connectionString));
        }

        public static void AddCustomAuthorization(this IServiceCollection services)
        {
            // добавляем политики
            services.AddAuthorization(options => PolicyManager.BuldAuthOption(Policy.SignUpWithRole, options));
            services.AddAuthorization(options => PolicyManager.BuldAuthOption(Policy.FullAlbumControl, options));
            services.AddAuthorization(options => PolicyManager.BuldAuthOption(Policy.CreateTrack, options));
            services.AddAuthorization(options => PolicyManager.BuldAuthOption(Policy.AuditViewing, options));
            services.AddAuthorization(options => PolicyManager.BuldAuthOption(Policy.AdminApplication, options));
            services.AddAuthorization(options => PolicyManager.BuldAuthOption(Policy.FullProperties, options));
            services.AddAuthorization(options => PolicyManager.BuldAuthOption(Policy.FullPerformer, options));
            services.AddAuthorization(options => PolicyManager.BuldAuthOption(Policy.FullPlaylist, options));
            services.AddAuthorization(options => PolicyManager.BuldAuthOption(Policy.FullPage, options));
        }

        public static void AddUserHistory(this IServiceCollection services)
        {
            services.AddTransient<UserHistoryAlbumService>();
        }

        public static void AddAudit(this IServiceCollection services)
        {
            services.AddTransient<AuditAccountService>();
            services.AddTransient<AuditAlbumService>();
            services.AddTransient<AuditTrackService>();
            services.AddTransient<AuthHistoryService>();
            services.AddTransient<AuditPropertiesService>();
            services.AddTransient<AuditPerformerService>();
        }

        public static void AddAuthenticationUser(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddTransient<AuthService>();
            services.AddTransient<AccountService>();
            services.AddTransient<UserService>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => JWTManager.BuldJWTOption(Configuration, x));
        }

        public static void AddAudioControllerService(this IServiceCollection services)
        {
            services.AddTransient<TrackPropertiesService>();
            services.AddTransient<AlbumService>();
            services.AddTransient<TrackService>();
            services.AddTransient<TrackAlbumService>();
            services.AddTransient<PropertiesServices>();
            services.AddTransient<PerformerService>();
            services.AddTransient<PlaylistService>();
            services.AddTransient<ITrackSearching<SearchTrackRequest>, TrackSearcService>();
            services.AddTransient<ClientSearchService>();
        }

        public static void AddBuilderService(this IServiceCollection services)
        {
            services.AddTransient<AccountBuilderSerivice>();
            services.AddTransient<AlbumBuilderService>();
            services.AddTransient<PlaylistBuilderService>();
            services.AddTransient<TrackBuilderService>();
        }

        public static void AddInterfaceService(this IServiceCollection services)
        {
            services.AddTransient<SectionService>();
            services.AddTransient<PageService>();
        }

        public static void AddCloudService(this IServiceCollection services)
        {
            services.AddSingleton<ICloudStorageService<string>, ParentMinioService>();
            services.AddTransient<TrackMinioService>();
            services.AddTransient<PlaylistMinioService>();
            services.AddTransient<AlbumMinioService>();
        }

        public static void AddPathCloudService(this IServiceCollection services)
        {
            services.AddTransient<TrackPathService>();
            services.AddTransient<AlbumPathService>();
            services.AddTransient<PlaylistPathService>();
        }
    }
}
