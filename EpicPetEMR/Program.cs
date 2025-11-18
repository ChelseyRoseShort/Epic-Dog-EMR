using EpicPetEMR.Api.Data;
using Microsoft.EntityFrameworkCore;
using EpicPetEMR.Mappers;
using EpicPetEMR.Shared.Models;
using EpicPetEMR.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;

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
app.UseStaticFiles();
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


app.MapGet("/getpet/{id}", async (AppDbContext db, int id) =>
{
    var pet = await db.Pets.Include(p => p.Family).FirstOrDefaultAsync(p => p.Id == id);
    return Results.Ok(pet.ToDto());
})
.WithName("getpetbyid")
.WithOpenApi();

// add profile pic 
app.MapPost("/uploadprofilepic/{id}", async (int id, IFormFile file, AppDbContext db, IWebHostEnvironment env) =>
{
    var pet = await db.Pets.FindAsync(id);
    if (pet == null)
    {
        return Results.NotFound("Pet not found");
    }
    var uploadsFolder = Path.Combine(env.ContentRootPath, "wwwroot", "petphotos");
    Directory.CreateDirectory(uploadsFolder);
    var safefilename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
    var fullPath = Path.Combine(uploadsFolder, safefilename);
    using var stream = new FileStream(fullPath, FileMode.Create);
    await file.CopyToAsync(stream);
    pet.ProfilePic = $"/petphotos/{safefilename}";
    await db.SaveChangesAsync();
    return Results.Ok(pet.ToDto());
}).Accepts<IFormFile>("multipart/form-data")
.Produces<PetDto>().DisableAntiforgery();

app.MapPost("/addpet", async (AppDbContext db, PetDto petDto) =>
{
    var pet = petDto.ToEntity();
     if (string.IsNullOrWhiteSpace(pet.ProfilePic))
    {
        pet.ProfilePic = pet.Species switch
        {
            Species.Cat => "/a-sleek-aesthetic-line-art-of-a-cat-in-a-side-profile-the-cat-has-sharp-geometric-angles-for-the-ears-and-soft-curves-for-the-body-vector.jpg",
            Species.Dog => "/360_F_1381163227_EfXTrDaEGIrk4izbfwh5zvP2yO8ABMyP.jpg",
            _ => ""
        };
    }
    db.Pets.Add(pet);
    await db.SaveChangesAsync();

    return Results.Created($"/addpet/{pet.Id}", pet.ToDto());
})
.WithName("AddPetDemo")
.WithOpenApi();

// Save an edited pet
app.MapPut("/updatepet", async (AppDbContext db, PetDto petDto) =>
{
    var pet = petDto.ToEntity();
    db.Pets.Update(pet);
    await db.SaveChangesAsync();

    return Results.Created($"/addpet/{pet.Id}", pet.ToDto());
})
.WithName("UpdatePet")
.WithOpenApi();

app.MapDelete("/deletepet", async (AppDbContext db, [FromBody] PetDto petDto) =>
{
    var pet = petDto.ToEntity();

    db.Pets.Attach(pet);
    db.Pets.Remove(pet);

    await db.SaveChangesAsync();

    return Results.NoContent();
})
.WithName("DeletePet")
.WithOpenApi();





app.MapGet("/example", () => new { test = "Success" })
   .WithName("ExampleEndpoint")
   .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
