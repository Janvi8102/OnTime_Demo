using Npgsql;
using OnTime_Demo.IServices;
using OnTime_Demo.Services;
using System.Data;

namespace OnTime_Demo
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        );
            });
            string connectionString = configRoot.GetSection("ConnectionStrings:ConnectionString").Value;
            services.AddTransient<IDbConnection>(s => new NpgsqlConnection(connectionString));
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddHttpClient();
            services.AddHttpClient<IJiraHttpClient, JiraHttpClient>();
            services.AddScoped<IProject, Project>();
            
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (Environments.Development.Equals(env.EnvironmentName))
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
