using System.Text;
using EventApp.Core.Repositories;
using EventApp.Infrastructure.Mappers;
using EventApp.Infrastructure.Repositories;
using EventApp.Infrastructure.Services;
using EventApp.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using NLog.Web;

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            env.ConfigureNLog("nlog.config");
            loggerFactory.AddNLog();
                
            app.UseAuthentication();
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
                .AddScoped<IUserService, UserService>()
                .AddScoped<ITicketService, TicketService>();
        }

        private void AddAuthentication(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var jwtSettings = serviceProvider.GetService<IOptions<JwtSettings>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = jwtSettings.Value.Issuer,
                            ValidateAudience = false,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Key))
                        };
                    }
                );

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", policy => policy.RequireRole("Admin"));
            });
        }
    }
}
