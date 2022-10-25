using Microsoft.AspNetCore.Authentication.JwtBearer;
using TvShowTracker.Config.LoadDataApi.Repository;
using TvShowTracker.Config.LoadDataApi;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TvShowTracker.Interfaces;
using TvShowTracker.Services;
using TvShowTracker.Config;
using TvShowTracker.Data;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace TvShowTracker.Startup
{
    public static class DependecyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddControllers().ConfigureApiBehaviorOptions(options =>{options.SuppressMapClientErrors = true;});
            Services.AddEndpointsApiExplorer();
            Services.AddResponseCaching();
            Services.AddSingleton<DapperRepository>();
            Services.AddHttpClient<IApiServices, ApiServiceTMDB>();
            Services.AddScoped<IUserServices, UserServices>();
            Services.AddScoped<IActorServices, ActorServices>();
            Services.AddScoped<ILoginServices, LoginServices>();
            Services.AddScoped<IGenreServices, GenreServices>();
            Services.AddScoped<ITvShowServices, TvShowServices>();
            Services.AddScoped<IFavoriteServices, FavoriteServices>();
            Services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
            Services.AddSingleton(new MapperConfiguration(cfg =>{cfg.AddProfile(new ConfigMapper());}).CreateMapper());
            Services.AddAutoMapper(typeof(ConfigMapper).GetType().Assembly);
            Services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });
            Services.AddSwaggerGen(setup =>
            {
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
                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "TvShowTracker", Version = "v1" });
                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                setup.OperationFilter<AuthResponsesOperationFilter>();
            });
            Services.AddMvc(_ =>
            {
                var authPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                _.Filters.Add(new AuthorizeFilter(authPolicy));
            });
            return Services;
        }
    }
}