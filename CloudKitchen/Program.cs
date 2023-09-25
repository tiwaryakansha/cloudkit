using Common.Helpers;
using DataLibrary;
using DataLibrary.Context;
using DataLibrary.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<ApplicationContext>();
builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseNpgsql(
    builder.Configuration.GetConnectionString("cloudKitchenConnection"),
    builder =>
    {
        builder.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName);
    }
    ).UseSnakeCaseNamingConvention()
);


// configure strongly typed settings objects
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

// Enable CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
    });
});

// Authentication Middleware

builder.Services.AddAuthentication(o =>
{
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {

        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = appSettings.Site,
        ValidAudience = appSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequiredLoggedIn", policy => policy.RequireRole("Customer", "Manager").RequireAuthenticatedUser());
});

builder.Services.AddScoped<IUserDetailsHelper, UserDetailsHelper>();
builder.Services.AddScoped<ITokenDetailsHelper, TokenDetailsHelper>();
builder.Services.AddScoped<IMenuDetailsHelper, MenuDetailsHelper>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("EnableCORS");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
MigrationManager.MigrateDatabase(app);
app.Run();


