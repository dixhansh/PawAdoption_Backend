using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PawAdoption_Backend.Data;
using PawAdoption_Backend.Mapping;
using PawAdoption_Backend.Middlewares;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Repositories;
using PawAdoption_Backend.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

// Configure CORS directly within the app services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

//Allows to access current HttpContext
builder.Services.AddHttpContextAccessor(); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//This swagger service can be used to send jwt token
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "PawAdoption_Backend", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

});

//setting up connection to database
builder.Services.AddDbContext<PawAdoptionDataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("PawAdoptionDbString")));

//Configuring Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

//Injecting Identity to our solution
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole<Guid>>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>("PawAdoption")
    .AddEntityFrameworkStores<PawAdoptionDataContext>()
    .AddApiEndpoints() /*uncomment this to add default Identity endpoints*/
    .AddDefaultTokenProviders();

//Setting up IdentityOptions
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

//Injecting JWT Authentication Service
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
});

//Injecting dependencies into the container
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepositoy, UserRepository>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();
builder.Services.AddScoped<IAdoptionApplicationService, AdoptionApplicationService>();
builder.Services.AddScoped<IAdoptionApplicationRepository, AdoptionApplicationRepositoy>();


var app = builder.Build();

// Enable CORS before routing middleware
app.UseCors("AllowSpecificOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Setting up GlobalExceptionHandling Middleware
app.UseMiddleware<CustomExceptionHandlerMiddleware>();

//app.UseHttpsRedirection(); disabling of testing purpose 

app.UseAuthentication();

app.UseAuthorization();

//This middleware will allow ASP.NET app to serve static files
app.UseStaticFiles(new StaticFileOptions
{
    //this the path to the physical folder(Images) in which we are saving the images
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images" //this RequestPath will redirect https://Localhost:portNo/Images request to the above FileProvider
});


app.MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
