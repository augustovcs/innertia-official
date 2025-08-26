using Auth.Interfaces;
using Auth.Services;
using Supabase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Task.Interfaces;
using Task.Services;
using Configs.JwtRules;
using Microsoft.OpenApi.Models;
using performancer.Views.Components;
using Swashbuckle.AspNetCore.SwaggerUI;

var AllowSpecificOrigins = "innertiaWeb";

var builder = WebApplication.CreateBuilder(args);

//RAZOR 
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();



builder.Services.AddCors(options =>
{
	options.AddPolicy(name: AllowSpecificOrigins,
	policy =>
	{
		policy.WithOrigins("http://localhost:5173")
		.AllowAnyHeader()
		.AllowAnyMethod();
	});
});

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


Console.WriteLine("Supabase URL: " + builder.Configuration["SupabaseUrl"]);
Console.WriteLine("Supabase KEY: " + builder.Configuration["SupabaseKey"]);

builder.Services.AddControllers();


builder.Services.AddScoped<ITestingServices, AuthTestingService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IEditService, EditService>();
builder.Services.AddScoped<IAuthService, LoginUserService>();
builder.Services.AddScoped<ITaskItem, ItemTaskService>();
builder.Services.AddScoped<JwtTokenGenerator>();

var secretKey = builder.Configuration["Jwt:Key"];


// Add CORS policy

var app = builder.Build();

app.UseCors(AllowSpecificOrigins);



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


app.UseAntiforgery();


app.MapStaticAssets();
app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();



app.Run();
