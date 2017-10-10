using System.IO;
using ConfigService.Model;
using ConfigService.Repository.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication;
// ReSharper disable ClassNeverInstantiated.Global

namespace ConfigService.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "ConfigService.Api",
                    Version = "v1",
                    Description = "A simple ConfigSettings ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "John Powell", Email = "", Url = "https://twitter.com/doozie7" },
                    License = new License { Name = "Use under MIT", Url = "https://www.britishmicro.com" }
                });

                c.DocInclusionPredicate((docName, description) => true);

                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "ConfigService.Api.xml");
                c.IncludeXmlComments(xmlPath);
            });

            // Register application services.
            //services.AddDbContext<SqlDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<IDbContext, SqlDbContext>();
            //services.AddScoped<IRepository<Customer>, ConfigService.Repository.Sql.CustomersRepository>();
            //services.AddScoped<IRepository<Setting>, ConfigService.Repository.Sql.SettingsRepository>();
            //services.AddScoped<IRepository<SettingType>, ConfigService.Repository.Sql.SettingTypesRepository>();

            services.AddScoped<IRepository<Customer>, ConfigService.Repository.InMemory.CustomersRepository>();
            services.AddScoped<IRepository<Setting>, ConfigService.Repository.InMemory.SettingsRepository>();
            services.AddScoped<IRepository<SettingType>, ConfigService.Repository.InMemory.SettingTypesRepository>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            //{
            //    Authority = "http://localhost:50151",
            //    RequireHttpsMetadata = false,
            //    ApiName = "scope.readaccess"
            //});

            app.UseStatusCodePages();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Configservice.Api v1");
            });

            app.UseMvc();
        }
    }
}