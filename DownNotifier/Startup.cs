
using Microsoft.EntityFrameworkCore;
using DownNotifierEntities.Context;
using DownNotifierData.Repositories;
using System.Data.Entity.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using DownNotifierData;

namespace DownNotifier
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
            services.AddDistributedMemoryCache();

            services.AddSession();

            services.AddControllersWithViews();


            Logs.connectionstring = Configuration["ConnectionStrings:DownNotifierDatabase"].ToString();
            services.AddDbContext<DownNotifierContext>(use => use.UseSqlServer(Logs.connectionstring));

            services.AddDbContext<DownNotifierContext>(use => use.UseSqlServer(Configuration["ConnectionStrings:DownNotifierDatabase"].ToString()));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITargetAppRepository, TargetAppRepository>();
            services.AddTransient<IParameterRepository, ParameterRepository>();
            services.AddTransient<ILogRepository, LogRepository>();

            services.AddControllersWithViews();

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Sign}/{action=SignIn}");
            });
        }
    }
}
