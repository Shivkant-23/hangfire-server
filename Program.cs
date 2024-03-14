using Hangfire.Dashboard;
using Hangfire.PostgreSql;
using Hangfire;
using System.Reflection;
//import hangfire, hangfire-codre and Hangfire.PostgreSql dependencies.



var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//hangfire database setting
builder.Services.AddHangfire(configuration => configuration
       .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
       .UsePostgreSqlStorage(builder.Configuration.GetConnectionString("HangfireConnection")
       ));

builder.Services.AddHangfireServer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//hangfire dashboard
var options = new DashboardOptions
{
    Authorization = new IDashboardAuthorizationFilter[]
    {}
};
app.UseHangfireDashboard("/hangfire", options);


app.Run();