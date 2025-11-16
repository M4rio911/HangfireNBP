using Hangfire;
using HangfireNBP.API.Jobs;
using HangfireNBP.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureInfrastructure(builder.Configuration);

//Hangfire
builder.Services.ConfigureHangfire(builder.Configuration);

//Serilog
builder.Host.ConfigureSerilog();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddTransient<FetchNbpDataJob>();

var app = builder.Build();

app.UseCors();
app.UseRouting();
app.UseHangfireDashboard();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NBP API V1");
});

//Jobs
RecurringJob.AddOrUpdate<FetchNbpDataJob>("fetch-nbp-data",job => job.Execute("b"), builder.Configuration["JobsSettings:FetchNbpRates"]);

app.Run();