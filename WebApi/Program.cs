using System.Configuration;
using System.Text.Json.Serialization;
using Application;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;
using WebApi.Exceptions;
using WebApi.Filters;
using WebApi.Swagger;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((ctx, conf) =>
        {
            conf.ReadFrom.Configuration(ctx.Configuration);
        });

        // Add services to the container.
        
        builder.Services.AddScoped<LogActionFilter>();
        builder.Services.AddScoped<RequiredKeyFilter>();

        builder.Services
            .AddControllers(
                // Global Filters
                // cfg => cfg.Filters.Add(typeof(LogActionFilter))
            )
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        // Global Filters
        // builder.Services.AddMvc(options => { options.Filters.Add(typeof(LogActionFilter)); });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo() { Title = "Enigma Bank API", Version = "V1" });
            config.OperationFilter<AddHeaderParamOpsFilter>();
        });

        builder.Services.AddValidation();
        builder.Services.AddDatabase(builder.Configuration);
        builder.Services.AddRepositories();
        builder.Services.AddMappings();
        builder.Services.AddApplicationServices();

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseExceptionHandler();
        app.MapControllers();

        app.Run();
    }
}