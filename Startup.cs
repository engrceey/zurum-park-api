using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ZurumPark.Data;
using ZurumPark.Repository;
using ZurumPark.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ZurumPark
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

            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(option =>
                                option.UseSqlite(Configuration.GetConnectionString("Sqlite")));

            services.AddScoped<INationalParkRepository, NationalParkRepository>();
            services.AddScoped<ITrailRepository, TrailRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddApiVersioning(options => {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion=new ApiVersion(1,0);
                options.ReportApiVersions = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("parkv1", new OpenApiInfo
                {
                    Title = "ZurumPark",
                    Version = "v1",
                    Description = "Zurum Park API",

                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "ogbondacristian@gmail.com",
                        Name = "Zurum",
                        Url = new Uri("https://www.linkedin.com/in/zurum-ogbonda-777152aa/")
                    },
                }
            );

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var Key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x => 
            {
               x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(x =>{
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =new SymmetricSecurityKey(Key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            });

            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("trailv1", new OpenApiInfo
            //     {
            //         Title = "ZurumTrail",
            //         Version = "v2",
            //         Description = "Zurum Trail API",

            //         Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            //         {
            //             Email = "ogbondacristian@gmail.com",
            //             Name = "Zurum",
            //             Url = new Uri("https://www.linkedin.com/in/zurum-ogbonda-777152aa/")
            //         },
            //     }
            // );

            // });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/parkv1/swagger.json", "ZurumPark v1"));
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/trailv1/swagger.json", "ZurumTrail v2"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
