using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using WebApplication1.Apis;
using WebApplication1.Model;

namespace WebApplication1;

public class Program
{
    public static MySchool api_school = new MySchool();
    public static MyStudent api_student = new MyStudent();
    public static MyTeacher api_teacher = new MyTeacher();
    public static MyClass api_class = new MyClass();
    public static MyStateClass api_stateClasses = new MyStateClass();
    public static MyJson api_json = new MyJson();
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
                option.ListenAnyIP(50002, listenOptions =>
                {
                    //listenOptions.UseHttps("./sslkey.pfx", "stvg");
                });
                option.ListenAnyIP(50003, listenOptions =>
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

            builder.Services.AddEndpointsApiExplorer();
            string version = "20241213_v1"; // yyyyMMdd // port 50000 and 500001, sslkey.pfx, do not copy and none for all file
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = string.Format("{0} {1}", "WebApplication1", version),
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

            using (var scope = app.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                DataContext datacontext = services.GetRequiredService<DataContext>();
                datacontext.Database.EnsureCreated();
                datacontext.Database.Migrate();
            }
            app.UseCors("HTTPSystem");
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.MapGet("/", () => "WebApplication of STVG");
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }
}
