using CVDTestApp.Data;
using CVDTestApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using CVDTestApp.Data.Repository;

namespace CVDTestApp
{
    public class StartUp
    {
        private IConfigurationRoot _confstring;

        public StartUp(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostEnv)
        {
            _confstring = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContent>(options => options.UseSqlServer(_confstring.GetConnectionString("DefaultConnection")));
            services.AddTransient<IAllEmployees, EmployeeRepository>();
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseRouting();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDbContent content = scope.ServiceProvider.GetRequiredService<AppDbContent>();
                DBObjects.Initial(content);
            }
        }
    }
}
