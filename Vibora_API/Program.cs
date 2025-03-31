using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using Vibora_API.Auth;
using Vibora_API.Data;
using Vibora_API.Repositories;
using Vibora_API.Services;
using Vibora_API.Validations;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

// Configure DbContext
services.AddDbContext<ViboraDBContext>(options =>
    options.UseNpgsql(config.GetConnectionString(nameof(ViboraDBContext))));

// Configure options
services.Configure<JwtOptions>(config.GetSection(nameof(JwtOptions)));
services.Configure<Vibora_API.Auth.AuthorizationOptions>(config.GetSection(nameof(Vibora_API.Auth.AuthorizationOptions)));

// Add controllers and OpenAPI
services.AddControllers();
services.AddOpenApi();

// Register repositories
services.AddScoped<IUsersRepository, UsersRepository>();
services.AddScoped<IRolesRepository, RolesRepository>();
services.AddScoped<IPostsRepository, PostsRepository>();
services.AddScoped<IThreadsRepository, ThreadsRepository>();
services.AddScoped<ICommentsRepository, CommentsRepository>();
services.AddScoped<IPermissionsRepository, PermissionsRepository>();

// Register services
services.AddScoped<IUsersService, UsersService>();
services.AddScoped<IRolesService, RolesService>();
services.AddScoped<IPostsService, PostsService>();
services.AddScoped<IThreadsService, ThreadsService>();
services.AddScoped<ICommentsService, CommentsService>();
services.AddScoped<IPermissionsService, PermissionsService>();

// Register authentication and authorization services
services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();

// Configure FluentValidation
services.AddFluentValidationAutoValidation();
services.AddFluentValidationClientsideAdapters();
services.AddValidatorsFromAssemblyContaining<UserValidator>();
services.AddValidatorsFromAssemblyContaining<RoleValidator>();
services.AddValidatorsFromAssemblyContaining<PostValidator>();
services.AddValidatorsFromAssemblyContaining<ThreadValidator>();
services.AddValidatorsFromAssemblyContaining<CommentValidator>();
services.AddValidatorsFromAssemblyContaining<PermissionValidator>();

// Configure authentication
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var secretKey = config["JwtOptions:SecretKey"];
    if (string.IsNullOrEmpty(secretKey))
    {
        throw new InvalidOperationException("SecretKey is not configured properly.");
    }

    options.TokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["JwtOptions:Issuer"],
        ValidAudience = config["JwtOptions:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["ulog"];
            return Task.CompletedTask;
        }
    };
});

// Configure authorization
services.AddAuthorizationBuilder();
services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

// Configure CORS
services.AddCors(options =>
    options.AddPolicy("ViboraCorsPolicy",
            builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders("Operation-Location");
            }));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always,
});
app.UseCors("ViboraCorsPolicy");

app.MapControllers();

app.Run();
