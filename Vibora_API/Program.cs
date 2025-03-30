using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Vibora_API.Auth;
using Vibora_API.Data;
using Vibora_API.Repositories;
using Vibora_API.Services;
using Vibora_API.Validations;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

services.AddDbContext<ViboraDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ViboraDBContext))));
services.Configure<AuthorizationOptions>(config.GetSection(nameof(AuthorizationOptions)));

services.AddControllers();
services.AddOpenApi();

services.AddScoped<IUsersRepository, UsersRepository>();
services.AddScoped<IRolesRepository, RolesRepository>();
services.AddScoped<IPermissionsRepository, PermissionsRepository>();
services.AddScoped<IThreadsRepository, ThreadsRepository>();
services.AddScoped<IPostsRepository, PostsRepository>();
services.AddScoped<ICommentsRepository, CommentsRepository>();

services.AddScoped<IUsersService, UsersService>();
services.AddScoped<IRolesService, RolesService>();
services.AddScoped<IPermissionsService, PermissionsService>();
services.AddScoped<IThreadsService, ThreadsService>();
services.AddScoped<IPostsService, PostsService>();
services.AddScoped<ICommentsService, CommentsService>();

services.AddFluentValidationAutoValidation();
services.AddFluentValidationClientsideAdapters();

services.AddValidatorsFromAssemblyContaining<UserValidator>();
services.AddValidatorsFromAssemblyContaining<RoleValidator>();
services.AddValidatorsFromAssemblyContaining<PermissionValidator>();
services.AddValidatorsFromAssemblyContaining<ThreadValidator>();
services.AddValidatorsFromAssemblyContaining<PostValidator>();
services.AddValidatorsFromAssemblyContaining<CommentValidator>();

services.AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
