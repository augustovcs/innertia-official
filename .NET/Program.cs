using Auth.Interfaces;
using Auth.Services;
using Supabase;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<Supabase.Client>(_ =>
new Supabase.Client(
    builder.Configuration["SupabaseUrl"],
    builder.Configuration["SupabaseKey"],
    new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true,
    }
));

Console.WriteLine("Supabase URL: " + builder.Configuration["SupabaseUrl"]);
Console.WriteLine("Supabase KEY: " + builder.Configuration["SupabaseKey"]);


builder.Services.AddControllers();


builder.Services.AddScoped<IAuthTesting, AuthTestingService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.Run();
