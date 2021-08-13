using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSchool.API.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartSchool.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SmartContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
                );

            object p = services.AddControllers().AddNewtonsoftJson(a => a.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IRepository, Repository>();

            services.AddVersionedApiExplorer(op =>
            {
                op.GroupNameFormat = "'v'VVV";
                op.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(op =>
            {
                op.DefaultApiVersion = new ApiVersion(1, 0);
                op.AssumeDefaultVersionWhenUnspecified = true;
                op.ReportApiVersions = true;
            });

            var apiProviderDescription = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();


            services.AddSwaggerGen(op =>
            {
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                    
                        op.SwaggerDoc(description.GroupName,
                                                      new Microsoft.OpenApi.Models.OpenApiInfo()
                                                      {
                                                          Title = "SmartSchool API",
                                                          Version = description.ApiVersion.ToString(),
                                                          TermsOfService = new Uri("http://SeusTermosdeUso.com"),
                                                          Description = "A descrição da WebAPI do SmartSchol",
                                                          License = new Microsoft.OpenApi.Models.OpenApiLicense
                                                          {
                                                              Name = "SmartScholl License",
                                                              Url = new Uri("http://mit.com")
                                                          },
                                                          Contact = new Microsoft.OpenApi.Models.OpenApiContact
                                                          {
                                                              Name = "Alyson Matias Cechinel",
                                                              Email = "alysonmcechinell@gmail.com",
                                                              Url = new Uri("https://www.linkedin.com/in/alyson-matias-cechinel-63a958172/")
                                                          }
                                                      });

                                        
                    }
                        var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                        var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                        op.IncludeXmlComments(xmlCommentsFullPath);
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger().UseSwaggerUI(op => {

                                                    foreach (var descriptions in apiVersionDescriptionProvider.ApiVersionDescriptions)
                                                    {
                                                        op.SwaggerEndpoint($"/swagger/{descriptions.GroupName}/swagger.json", 
                                                                            descriptions.GroupName.ToUpperInvariant()); 
                                                    }
                                                    op.RoutePrefix = "";
                                                });

            //app.UseStaticFiles();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
