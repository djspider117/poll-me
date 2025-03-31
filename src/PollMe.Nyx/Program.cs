
using PollMe.Nyx.Data;
using PollMe.Nyx.Data.Access;

namespace PollMe.Nyx;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddDbContext<NyxContext>();
        builder.Services.AddTransient<NyxDataSetRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
