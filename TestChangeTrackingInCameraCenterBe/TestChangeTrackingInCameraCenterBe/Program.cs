using CameraCenterBe.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using TestChangeTrackingInCameraCenterBe.Apis;

namespace TestChangeTrackingInCameraCenterBe;

public class Program
{
    public static MyTest api_test = new MyTest();
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration().MinimumLevel
                                              .Information()
                                              .WriteTo.Console(theme: AnsiConsoleTheme.Sixteen)
                                              .CreateLogger();
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel((context, option) =>
            {
                option.ListenAnyIP(50000, listenOptions =>
                {
                });
                option.ListenAnyIP(50001, listenOptions =>
                {
                });
                option.Limits.MaxConcurrentConnections = null;
                option.Limits.MaxRequestBodySize = null;
                option.Limits.MaxRequestBufferSize = null;
                //option.Limits.KeepAliveTimeout = TimeSpan.MaxValue;
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("HTTPSystem", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).WithExposedHeaders("Grpc-Status", "Grpc-Encoding", "Grpc-Accept-Encoding");
                });
            });

            builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(DataContext.configSql));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            string version = "20250206_v1"; // yyyyMMdd // port 50000 and 500001, sslkey.pfx, do not copy and none for all file
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = string.Format("{0} {1}", "TestChangeTrackingInCameraCenterBe", version),
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Bearer",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();

            app.UseCors("HTTPSystem");
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.MapGet("/", () => "TestChangeTrackingInCameraCenterBe of STVG");

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }
}
