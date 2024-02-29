using AutoMapper;
using Bono.Employees.Application.AutoMapper;
using Bono.Employees.Data.Context;
using Bono.Employees.Infrastructure.Auth.Services;
using Bono.Employees.Infrastructure.IoC;
using Bono.Employees.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Bono.Employees.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        //TODO: Change this to a specific domain
                        builder.AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader();
                    });
            });
            
            services.AddHealthChecks();
            services.AddControllersWithViews();
            services.AddJwtConfiguration();
            Console.WriteLine("BonoEmployeeConnection: " + Configuration.GetConnectionString("BonoEmployeeDB"));
            services.AddDbContext<BonoEmployeeContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("BonoEmployeeDB")));
            NativeInjector.RegisterServices(services);
            services.AddAutoMapper(typeof(AutoMapperSetup));
            services.AddSwaggerConfiguration("Employees", "V1", "Employee Management API", "Richard Bono de Oliveira", "richiebono@gmail.com");
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHealthChecks("/healthcheck");
            app.UseSwaggerConfiguration();            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
                        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
