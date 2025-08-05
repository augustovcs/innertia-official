using Auth.Interfaces;
using Auth.Services;
using Supabase;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<Supabase.Client>(_ =>
new Supabase.Client(
    builder.Configuration["SupabaseUrl"] ?? throw new ArgumentNullException("SupabaseUrl is not configured"),
    builder.Configuration["SupabaseKey"],
    new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true,
    }
));

/*
LOGGING IF YOUR SUPABASE URL AND KEY ARE CORRECT
This is useful for debugging purposes.

Console.WriteLine("Supabase URL: " + builder.Configuration["SupabaseUrl"]);
Console.WriteLine("Supabase KEY: " + builder.Configuration["SupabaseKey"]);
*/

builder.Services.AddControllers();


builder.Services.AddScoped<ITestingServices, AuthTestingService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IEditService, EditService>();
builder.Services.AddScoped<IAuthService, LoginUserService>();

// Add CORS policy

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
