using DBAccess.Repositories;

namespace coursework_Main
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
            services.AddMvc();
            services.AddControllersWithViews();
            services.AddTransient<IRepositories<RepositoryPerson>>();
            services.AddTransient<IRepositories<RepositoryUser>>();
            services.AddTransient<IRepositories<RepositoryImage>>();
        }
        private void Configure(IApplicationBuilder app, IRepositories<RepositoryPerson> repositories)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("hui");
            });
        }

    }
}
