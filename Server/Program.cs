using Microsoft.EntityFrameworkCore;
using EDurianstore.Models;
using EDurianstore.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<UsersContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// var _authkey=builder.Configuration.GetValue<string>("JwtSettings:securitykey");
// builder.Services.AddAuthentication(item=> {
//     item.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
//     item.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(item=> {
//     item.RequireHttpsMetadata=true;
//     item.SaveToken=true;
//     item.TokenValidationParameters= new TokenValidationParameters(){
//         ValidateIssuerSigningKey=true,
//         IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authkey)),
//         ValidateIssuer=false,
//         ValidateAudience=false
//     };
// });

// var _jwtsettings=builder.Configuration.GetSection("JwtSettings");
// builder.Services.Configure<JwtSettings>(_jwtsettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
