using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SwaggerThemes;
using taskManagerBE.Data;
using taskManagerBE.Helpers;
using taskManagerBE.Interfaces;
using taskManagerBE.Repository;

Env.Load("keys/.env");
var builder = WebApplication.CreateBuilder(args);

//allow cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("allowFront",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.Authority = "http://localhost:8080/realms/taskmanager";
        options.Audience = "taskmanager-api";
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Nuestra API utiliza la Autenticación JWT usando el esquema Bearer. \n\r\n\r" +
                      "Ingresa la palabra a continuación el token generado en login.\n\r\n\r" +
                      "Ejemplo: \"12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<MyDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    Console.WriteLine("Connection string below");
    Console.WriteLine(connectionString);
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

//builder.WebHost.UseUrls("http://*:8080");

builder.Services.AddAuthorizationBuilder();

var app = builder.Build();

app.UseCors("allowFront");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(Theme.Dracula);

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();