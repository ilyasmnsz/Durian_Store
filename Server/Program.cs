using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;  
using Durians.Handler;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Durian.Models;
using Durian.Data;
using System.Text;
using Microsoft.OpenApi.Models;
// using AutoMapper;
// using AspNetCore.Authentication.Basic;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();  

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "JWTToken_Auth_API", Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//for basic authentication
// builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions,BasicAuthenticationHandler>("BasicAuthentication",null);

var _authkey = builder.Configuration.GetValue<string>("JwtSettings:securitykey");
builder.Services.AddAuthentication(item =>
{
    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(item =>
{
    item.RequireHttpsMetadata = true;
    item.SaveToken = true;
    item.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authkey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        // ClockSkew=TimeSpan.Zero
    };
});

builder.Services.AddDbContext<DurianContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

// builder.Services.AddScoped<IRefereshTokenGenerator, RefereshTokenGenerator>();

// var automapper=new MapperConfiguration(item=>item.AddProfile(new AutoMapperHandler()));
// IMapper mapper=automapper.CreateMapper();
// builder.Services.AddSingleton(mapper);


var _jwtsettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(_jwtsettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
