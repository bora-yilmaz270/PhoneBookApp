
using MassTransit;
using Microsoft.Extensions.Options;
using PhoneBookApp.PhoneBookReportApi.Consumers;
using PhoneBookApp.PhoneBookReportApi.Services;
using PhoneBookApp.PhoneBookReportApi.Settings;
using PhoneBookApp.Shared.Messages;

namespace PhoneBookApp.PhoneBookReportApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
      
            builder.Services.AddScoped<IReportService, ReportService>();
            
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

            builder.Services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<ReportDetailEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    cfg.ReceiveEndpoint("create-reportdetail-service", e =>
                    {
                        e.ConfigureConsumer<ReportDetailEventConsumer>(context);
                    });                   
                });
            });
            var app = builder.Build();

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
        }
    }
}