using DemoHangFire.Data;
using DemoHangFire.Jobs;
using DemoHangFire.Models;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DemoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#region HANGFIRE
builder.Services.AddHangfire(configuration => configuration
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfireServer();
#endregion

var app = builder.Build();
var serviceProvider = app.Services;


#region Metodos
app.MapGet("/users", async (DemoDbContext context) =>
    await context.Users.ToListAsync())
    .WithName("GetUsers")
    .WithTags("User");

app.MapPost("/user", async (DemoDbContext context, UserModel user) =>
    {
        context.Users.Add(user);
        _ = await context.SaveChangesAsync();
    })
    .WithName("PostUser")
    .WithTags("User");

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHangfireDashboard();
app.UseHttpsRedirection();

Schedule schedule = new(serviceProvider);

ScheduleHangFire(schedule);

app.Run();



void ScheduleHangFire(Schedule schedule)
{
    RecurringJob.AddOrUpdate(
        "UpdateNameUser",
        () => schedule.ScheduleJob(),
        Cron.Minutely,
        TimeZoneInfo.Local);
}
