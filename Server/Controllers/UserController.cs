using Microsoft.AspNetCore.Mvc;
using Durian.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Durian.Controllers;

namespace Durian.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly DurianContext DbContext;
    private readonly JwtSettings jwtSettings;
    public UserController(DurianContext durianContext,IOptions<JwtSettings> options)
    {
        this.DbContext = durianContext;
        this.jwtSettings = options.Value;
    }

    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate([FromBody]UserCred userCred)
    {
        var user = await this.DbContext.TblUsers.FirstOrDefaultAsync(item=> item.Id==userCred.Id && item.Password==userCred.Password);
        if (user == null)
            return Unauthorized();

        //Generate Token
        var tokenhandler = new JwtSecurityTokenHandler();
        var tokenkey = Encoding.UTF8.GetBytes(this.jwtSettings.securitykey);
        var tokendesc = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new Claim[] { new Claim(ClaimTypes.Name, user.Username) }
            ),
            Expires = DateTime.Now.AddSeconds(20),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenhandler.CreateToken(tokendesc);
        string finaltoken = tokenhandler.WriteToken(token);

        return Ok();
    }
}