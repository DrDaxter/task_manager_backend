using Microsoft.EntityFrameworkCore;
using taskManagerBE.Data;
using taskManagerBE.Helpers;
using taskManagerBE.Interfaces;
using taskManagerBE.Repository;

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
builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.WebHost.UseUrls("http://*:8080");

var app = builder.Build();

app.UseCors("allowFront");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();