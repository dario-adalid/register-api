using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using RegisterAPI.Data;
using RegisterAPI.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("UsersDb"));
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("SqlCon"),
    sqlServerOptionsAction: sqlOption => {
        sqlOption.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        );
    }
    )

);



var app = builder.Build();

app.MapGet("api/user", async(AppDbContext context) =>{
    var users = await context.UsuariosFiesta.ToListAsync();

    return Results.Ok(users);
});

app.MapPost("api/user", async(AppDbContext context, UserFiesta usuario) =>{
    await context.UsuariosFiesta.AddAsync(usuario);

    await context.SaveChangesAsync();

    return Results.Created($"api/user/{usuario.Id}", usuario);
});

//app.UseHttpsRedirection();

using(var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
        Console.WriteLine("Database migrated succesfully");

    }
    catch(Exception ex)
    {
        Console.WriteLine($"Could not migrate DB: {ex.Message}");
    }
}

app.Run();
