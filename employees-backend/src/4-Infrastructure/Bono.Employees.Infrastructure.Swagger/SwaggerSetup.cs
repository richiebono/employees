using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Linq;

namespace Bono.Employees.Infrastructure.Swagger
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, string title, string version, string description, string contactName, string contactEmail)
        {
            return services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc(version.ToLower(), new OpenApiInfo
                {
                    Title = title,
                    Version = version,
                    Description = description,
                    Contact = new OpenApiContact
                    {
                        Name = contactName,
                        Email = contactEmail
                    }
                });

                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()  
                {  
                    Name = "Authorization",  
                    Type = SecuritySchemeType.ApiKey,  
                    Scheme = "Bearer",  
                    BearerFormat = "JWT",  
                    In = ParameterLocation.Header,  
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",  
                });  
                
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement  
                {  
                    {  
                          new OpenApiSecurityScheme  
                            {  
                                Reference = new OpenApiReference  
                                {  
                                    Type = ReferenceType.SecurityScheme,  
                                    Id = "Bearer"  
                                }  
                            },  
                            new string[] {}  
  
                    }  
                }); 
            });
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api/swagger";
                c.SwaggerEndpoint("../../swagger/v1/swagger.json", "API v1");
            });

            return app;
        }        
    }
}
