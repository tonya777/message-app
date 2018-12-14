using MessageApp.WEB.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageApp.WEB
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
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials());
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            var context = app.ApplicationServices.GetService<ApiContext>();
            DbService.AddTestData(context);

            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
