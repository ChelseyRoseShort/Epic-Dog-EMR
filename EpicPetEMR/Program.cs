using EpicPetEMR.Api.Data;
using Microsoft.EntityFrameworkCore;
using EpicPetEMR.Mappers;
var builder = WebApplication.CreateBuilder(args);

// CORS (dev-wide). Switch to a named, restricted policy later.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy
        .AllowAnyOrigin()   // ok for dev only
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// DbContext + SQLite
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Order matters
app.UseHttpsRedirection();
app.UseCors(); // CORS before endpoints (and before auth, if you add it later)

// Auto-migrate on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/swagger"));

var summaries = new[]
{
    "Freezing","Bracing","Chilly","Cool","Mild","Warm","Balmy","Hot","Sweltering","Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        )).ToArray();

    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();



app.MapGet("/demo/pets", async (AppDbContext db) =>
{
    var pets = await db.Pets.Include(p => p.Family).ToListAsync();
    return Results.Ok(pets.Select(p => p.ToDto()));
})
.WithName("ListPetsDemo")
.WithOpenApi();


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
