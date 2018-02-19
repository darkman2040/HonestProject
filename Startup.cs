using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using HonestProject.DataModels;
using Microsoft.EntityFrameworkCore;
using HonestProject.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using HonestProject.Utilities;

namespace HonestProject
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "yourdomain.com",
                            ValidAudience = "yourdomain.com",
                            IssuerSigningKey = new SymmetricSecurityKey(
                                System.Text.Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
                        };
                    });

            services.AddMvc();

            var connection = @"Server=COLIN-PC\SQLEXPRESS;Database=HonestProject;Trusted_Connection=True;";
            services.AddDbContext<HonestProjectContext>(options =>
            options.UseSqlServer(connection));

            services.AddScoped<ISiteRepository, SiteRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IPasswordHashUtility, PasswordHashUtility>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                !Path.HasExtension(context.Request.Path.Value) &&
                !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
