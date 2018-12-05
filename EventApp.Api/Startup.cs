using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventApp.Core.Repositories;
using EventApp.Infrastructure.Mappers;
using EventApp.Infrastructure.Repositories;
using EventApp.Infrastructure.Services;
using EventApp.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EventApp.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<JwtSettings>(Configuration.GetSection("jwt"));

            AddSingleton(services);
            AddScoped(services);
            AddAuthentication(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            //app.UseHttpsRedirection
            app.UseMvc();
        }

        private void AddSingleton(IServiceCollection services)
        {
            services.AddSingleton(AutoMapperConfig.Initialize())
                .AddSingleton<IJwtHandler, JwtHandler>();
        }

        private void AddScoped(IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IEventService, EventService>()
                .AddScoped<IUserService, UserService>();
        }

        private void AddAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "http://localhost:5000",
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTsecretKey"))
                    };
                });
        }
    }
}
