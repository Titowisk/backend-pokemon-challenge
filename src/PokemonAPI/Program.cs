using PokemonApplication;
using PokemonContracts.Options;
using PokemonInfrastructure;
using PokemonInfrastructure.Persistence.Seed;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Many-to-many relationships in EF Core can cause cycles in the serialization
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Hide exception information from client. A btter approach would be to use ErrorOr lib from Amichai Mantinband
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Options Pattern implementation
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection(DatabaseSettings.DatabaseSettingsKey));
builder.Services.Configure<SeedOptions>(
    builder.Configuration.GetSection(SeedOptions.Key));

builder.Services.AddHttpClient();

builder.Services
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Seed the database with pokemons and other information from pokeapi
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var seeder = services.GetRequiredService<PokemonDatabaseSeeder>();
        await seeder.SeedTable();
    }
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
