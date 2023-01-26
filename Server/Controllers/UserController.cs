using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Durian.Models;
using Durian.Data;
using User.Models;

namespace Durian.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly DurianContext DbContext;
    private readonly JwtSettings jwtSettings;

    public UserController(DurianContext _DbContext,IOptions<JwtSettings> options)
    {
        this.DbContext = _DbContext;
        this.jwtSettings = options.Value;
    }

    [HttpPost("Masuk")]
    public async Task<IActionResult> Authenticate([FromBody]UserAuth userAuth)
    {
        var user = await this.DbContext.Users.FirstOrDefaultAsync(item=>item.Username==userAuth.Username && item.Password==userAuth.Password);
        if(user==null)
            return Unauthorized();
        ////Generate Token
        var tokenhandler = new JwtSecurityTokenHandler();
        var tokenkey = Encoding.UTF8.GetBytes(this.jwtSettings.securitykey);
        var tokendesc = new SecurityTokenDescriptor{Subject = new ClaimsIdentity(
            new Claim[] {new Claim(ClaimTypes.Name, user.Username), new Claim(ClaimTypes.Role, user.Tipe)}
        ),
        Expires=DateTime.Now.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenhandler.CreateToken(tokendesc);
        string finaltoken = tokenhandler.WriteToken(token);

        return Ok(new {finaltoken, user.Tipe});
    }

    // [Authorize(Roles = "admin")]
    [HttpPost("Daftar")]
    public async Task<ActionResult> UserCred(UserCred userCred)
    {
        var UserCred = new UserDTO()
        {
            Nama = userCred.Nama,
            Username = userCred.Username,
            Email = userCred.Email,
            Password = userCred.Password,
            Telepon = userCred.Telepon,
            Tipe = userCred.Tipe
        };
        
        await DbContext.Users.AddAsync(UserCred);
        await DbContext.SaveChangesAsync();

        return Ok(UserCred);
    }

    // [Authorize(Roles = "admin,user")]
    [HttpGet("List")]
    public async Task<ActionResult> GetUsers()
    {
        return Ok(await DbContext.Users.ToListAsync());
    }

    // [Authorize(Roles = "admin,user")]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUsers(int id)
    {
        var TblUser = await DbContext.Users.FindAsync(id);

        if (TblUser == null)
        {
            return NotFound();
        }

        return Ok(TblUser);
    }

    // [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUsers(int id, UserDTO UserDTO)
    {
        if (id != UserDTO.Id)
        {
            return BadRequest();
        }

        var TblUsers = await DbContext.Users.FindAsync(id);
        if (TblUsers == null)
        {
            return NotFound();
        }

        TblUsers.Nama = UserDTO.Nama;
        TblUsers.Username = UserDTO.Username;
        TblUsers.Email = UserDTO.Email;
        TblUsers.Password = UserDTO.Password;
        TblUsers.Telepon = UserDTO.Telepon;
        TblUsers.Tipe = UserDTO.Tipe;

        try
        {
            await DbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!UsersExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsers(int id)
    {
        var TblUser = await DbContext.Users.FindAsync(id);
        if (TblUser == null)
        {
            return NotFound();
        }

        DbContext.Users.Remove(TblUser);
        await DbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool UsersExists(int id)
    {
        return DbContext.Users.Any(e => e.Id == id);
    }

    private static UserDTO ItemToDTO(UserDTO User) =>
       new UserDTO
       {
        Id = User.Id,
        Nama = User.Nama,
        Username = User.Username,
        Password = User.Password,
        Email = User.Email,
        Telepon = User.Telepon,
        Tipe = User.Tipe
       };
}