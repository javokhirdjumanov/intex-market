using FluentValidation;
using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Services;
using IndexMarket.Application.Validator;
using IndexMarket.Application.Validators;
using IndexMarket.Domain.Enums;
using IndexMarket.Infrastructure.Auth;
using IndexMarket.Infrastructure.Context;
using IndexMarket.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace IndexMarket.Api.Extantions;
public static class ServicesExtentions
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostrgeSQL");

        services.Configure<JwtOptions>(configuration.GetSection("JwtSettings"));

        services.AddSwaggerService();

        /*services.AddDbContextPool<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            });
        });*/
        services.AddDbContextPool<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("UserPolicy", options =>
            {
                options.RequireRole(UserRoles.Admin.ToString(), UserRoles.Client.ToString());
            });
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IAddressRepository, AddressRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductShapeRepository, ProductShapeRepository>();
        services.AddTransient<ISitesRepository, SitesRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IConsultationRepository, ConsultationRepository>();
        services.AddScoped<IFileRepository, FileRepository>();

        services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserServices, UserServices>();
        services.AddSingleton<IUserFactory, UserFactory>();

        services.AddScoped<ISiteServices, SiteServices>();
        services.AddSingleton<ISiteFactory, SiteFactory>();

        services.AddScoped<ICategoryServices, CategoryServices>();

        services.AddScoped<IProductServices, ProductServices>();
        services.AddSingleton<IProductFactory, ProductFactory>();

        services.AddScoped<IOrderServices, OrderServices>();
        services.AddSingleton<IOrderFactory, OrderFactory>();

        services.AddScoped<IProductShapeService, ProductShapeService>();

        services.AddScoped<IConsultationServices, ConsultationServices>();

        services.AddScoped<IFileServices, FileServices>();

        services.AddScoped<IAuthentoicationServices, AuthentoicationServices>();

        //////////////////////////////// VALIDATORS ////////////////////////////////
        services.AddValidatorsFromAssemblyContaining<UserForCreationDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UserForModificationDtoValidator>();

        services.AddValidatorsFromAssemblyContaining<SiteForMoficationDtoValidator>();

        services.AddValidatorsFromAssemblyContaining<CategoryModifyValidator>();

        services.AddValidatorsFromAssemblyContaining<ProductCreationValidator>();
        services.AddValidatorsFromAssemblyContaining<ProductModificationValidator>();

        services.AddValidatorsFromAssemblyContaining<OrderCreationValidator>();

        return services;
    }
    private static void AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "INTEX-MARKET.Api", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[] { }
            }
        });
        });
    }
}
