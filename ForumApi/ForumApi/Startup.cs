using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumApi.Contexts;
using ForumApi.Environments;
using ForumApi.Interfaces.Contexts;
using ForumApi.Interfaces.Repositories;
using ForumApi.Interfaces.Services;
using ForumApi.Repositories;
using ForumApi.Services;
using ForumApi.SourceCode.Services;
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
using Microsoft.Net.Http.Headers;

namespace ForumApi
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

            string origins = Configuration.GetSection("AllowedOrigins").Value;
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(origins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.Configure<ConnectionSettings>(options => {
                options.ConnectionString = Configuration.GetSection("ForumDbContext:ConnectionString").Value;
                options.Database = Configuration.GetSection("ForumDbContext:Database").Value;
            });

            JwtTokenSettings jwtToken = new JwtTokenSettings {
                Issuer = Configuration.GetSection("JwtTokenParameter:Issuer").Value,
                Audience = Configuration.GetSection("JwtTokenParameter:Audience").Value,
                SecretKey = Configuration.GetSection("JwtTokenParameter:SecretKey").Value,
                Duration = Configuration.GetSection("JwtTokenParameter:Duration").Value
            };

            services.Configure<JwtTokenSettings>(options => {
                options.Issuer = jwtToken.Issuer;
                options.Audience = jwtToken.Audience;
                options.SecretKey = jwtToken.SecretKey;
                options.Duration = jwtToken.Duration;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtToken.Issuer,
                    ValidAudience = jwtToken.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken.SecretKey))
                };
            });
            

            services.AddTransient<IForumDbConnector, ForumDbConnector>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IRegisterService, RegisterService>();

            services.AddTransient<ForumApi.Interfaces.IImageHandler, ForumApi.Interfaces.ImageHandler>();
            services.AddTransient<ForumApi.Interfaces.IImageWriter,ForumApi.Interfaces.ImageWriter>();
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

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    int cachePeriod = env.IsDevelopment() ? 60 * 60 : 60 * 60 * 24 * 7;
                    string path = ctx.Context.Request.Path;
                    if(path.EndsWith(".jpg") || path.EndsWith(".npg"))
                    {
                        ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public, max-age={cachePeriod}";
                    }
                }
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
