using BackendAssignment.CustomConfigs;
using BackendAssignment.DataAccess.Repositories.StudentRepository;
using BackendAssignment.DataAccessLayer.DBContexts;
using BackendAssignment.DataAccessLayer.Repositories.Class;
using BackendAssignment.DataAccessLayer.Repositories.ClassRepository;
using BackendAssignment.DataAccessLayer.Repositories.StudentRepository;
using BackendAssignment.DataAccessLayer.Repositories.SubjectRepository;
using BackendAssignment.Managers.ClassManager;
using BackendAssignment.Managers.StudentManager;
using BackendAssignment.Managers.SubjectManager;
using BackendAssignment.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();

// Add logging services
builder.Services.AddLogging();

builder.Services.AddScoped<IStudentManager, StudentManager>();
builder.Services.AddScoped<IClassManager, ClassManager>();
builder.Services.AddScoped<ISubjectManager, SubjectManager>();

// Add authorization services
builder.Services.AddAuthorization();

// Register custom health check
builder.Services.AddHealthChecks()
    .AddCheck<CustomHealthCheck>("custom_health_check");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/api/v1/healthcheck");

app.UseMiddleware<ErrorMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
