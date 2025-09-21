using ABInBev.Employees.API.Configuration;
using ABInBev.Employees.Business.Interfaces;
using ABInBev.Employees.Business.Services;
using ABInBev.Employees.Data.Context;
using ABInBev.Employees.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<EmployeeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeesConnection"));
});

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddScoped<EmployeeDbContext>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.ConfigAuthentication(builder.Configuration);

builder.Services.ConfigSwagger();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
    db.Database.EnsureCreated();

    var authDb = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
    authDb.Database.Migrate();
}

await app.Services.SetupAdminUser(builder.Configuration);

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
