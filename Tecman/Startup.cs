using Tecman.Business;
using Tecman.Business.Implementation;
using Tecman.Configurations;
using Tecman.Models.Context;
using Tecman.Repository;
using Tecman.Repository.Implementation;
using Tecman.Services;
using Tecman.Services.Implementation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

using System.Text;

namespace Tecman
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;

            Environment = environment;

            // Log config
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Able CORS
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Total-Count");
            }));



            // Adding controllers
            services.AddControllersWithViews();
            services.AddControllers();

            // Media Type
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));

            }).AddXmlSerializerFormatters();

            // Version control
            services.AddApiVersioning();

            // Dependency injection
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientBusiness, ClientBusiness>();
            services.AddScoped<IEmployeeBusiness, EmployeeBusiness>();
            services.AddScoped<IOrderServiceBusiness, OrderServiceBusiness>();
            services.AddScoped<IOrderServiceRepository, OrderServiceRepository>();
            services.AddScoped<ITokenService, TokenService>();




            services.AddScoped<IResponseApiService, ResponseApiService>();


            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient();


            services.AddOptions();

            // Adding Iugu configuration

            // Database
            var connection = Configuration.GetSection("DbContextSettings:ConnectionString").Value;
            services.AddDbContext<PostgreSqlDbContext>(options => options.UseLazyLoadingProxies().UseNpgsql(connection));


            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 3000000000;
                options.AllowSynchronousIO = true;
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = 3000000000; // if don't set default value is: 30 MB
            });

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = 300000000;
                options.MultipartBodyLengthLimit = 3000000000; // if don't set default value is: 128 MB
                options.MultipartHeadersLengthLimit = 300000000;
            });

            // Authentication
            var tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                    Configuration.GetSection("TokenConfiguration")
                ).Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfigurations.Issuer,
                    ValidAudience = tokenConfigurations.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            // Swagger API
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Tecman API V1",
                        Version = "v0.0.1",
                        Description = "Swagger Documentation",
                        Contact = new OpenApiContact
                        {
                            Name = "Dreher's Solution",
                            Url = new Uri("https://dreherssolution/pt_BR/"),
                            Email = "dreherssolution@gmail.com"
                        }
                    });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tecman v1"));

            app.Use(async (context, next) =>
            {
                context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = null; // unlimited I guess
                await next.Invoke();
            });

            // Deploy apache reverse DNS
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Adding CORS, need to be in this location
            app.UseCors();

            app.UseAuthorization();

            var options = new RewriteOptions();
            options.AddRedirect("ˆ$", "swagger");
            app.UseRewriter(options);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
            });
        }
    }
}
