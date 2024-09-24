using WalletRu.Api.Hubs;
using WalletRu.Api.Middlewares;
using WalletRu.Application;
using WalletRu.Application.Common.Options;
using WalletRu.DAL;
using Serilog;
using Serilog.Settings.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.Configure<DbOptions>(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("postgres");
});

builder.Host.UseSerilog((context, configuration) =>
{
    var options = new ConfigurationReaderOptions
    {
        SectionName = context.HostingEnvironment.IsDevelopment() ? "Serilog_Development" : "Serilog"
    };

    configuration.ReadFrom.Configuration(builder.Configuration, options);
});

builder.Services.AddApplication();
builder.Services.AddDal(builder.Configuration);

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.MapControllers();

app.UseCors(x =>
{
    x.AllowAnyOrigin();
    x.AllowAnyHeader();
    x.AllowAnyMethod();
});

app.MapHub<MessageHub>("/messages");

app.Run();