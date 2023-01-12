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
public class TblUserController : ControllerBase
{
    private readonly DurianContext DbContext;
    private readonly JwtSettings jwtSettings;

    public TblUserController(DurianContext _DbContext,IOptions<JwtSettings> options)
    {
        this.DbContext = _DbContext;
        this.jwtSettings = options.Value;
    }

    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate([FromBody]UserAuth userAuth)
    {
        var user = await this.DbContext.TblUsers.FirstOrDefaultAsync(item=>item.Username==userAuth.Username && item.Password==userAuth.Password);
        if(user==null)
            return Unauthorized();
        ////Generate Token
        var tokenhandler = new JwtSecurityTokenHandler();
        var tokenkey = Encoding.UTF8.GetBytes(this.jwtSettings.securitykey);
        var tokendesc = new SecurityTokenDescriptor{Subject = new ClaimsIdentity(
            new Claim[] {new Claim(ClaimTypes.Name, user.Username)}
        ),
        Expires=DateTime.Now.AddSeconds(8000),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenhandler.CreateToken(tokendesc);
        string finaltoken = tokenhandler.WriteToken(token);

        return Ok(finaltoken);
    }


    [HttpPost]
    public async Task<ActionResult> UserCred(UserCred userCred)
    {
        var UserCred = new TblUserDTO()
        {
            Nama = userCred.Nama,
            Username = userCred.Username,
            Email = userCred.Email,
            Password = userCred.Password,
            Telepon = userCred.Telepon
        };
        
        await DbContext.TblUsers.AddAsync(UserCred);
        await DbContext.SaveChangesAsync();

        return Ok(UserCred);
    }

    [HttpGet]
    public async Task<ActionResult> GetTblUsers()
    {
        return Ok(await DbContext.TblUsers.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TblUserDTO>> GetTblUsers(int id)
    {
        var TblUser = await DbContext.TblUsers.FindAsync(id);

        if (TblUser == null)
        {
            return NotFound();
        }

        return Ok(TblUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTblUsers(int id, TblUserDTO tblUserDTO)
    {
        if (id != tblUserDTO.Id)
        {
            return BadRequest();
        }

        var TblUsers = await DbContext.TblUsers.FindAsync(id);
        if (TblUsers == null)
        {
            return NotFound();
        }

        TblUsers.Nama = tblUserDTO.Nama;
        TblUsers.Username = tblUserDTO.Username;
        TblUsers.Email = tblUserDTO.Email;
        TblUsers.Password = tblUserDTO.Password;
        TblUsers.Telepon = tblUserDTO.Telepon;

        try
        {
            await DbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!TblUsersExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTblUsers(int id)
    {
        var TblUser = await DbContext.TblUsers.FindAsync(id);
        if (TblUser == null)
        {
            return NotFound();
        }

        DbContext.TblUsers.Remove(TblUser);
        await DbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool TblUsersExists(int id)
    {
        return DbContext.TblUsers.Any(e => e.Id == id);
    }

    private static TblUserDTO ItemToDTO(TblUserDTO tblUser) =>
       new TblUserDTO
       {
        Id = tblUser.Id,
        Nama = tblUser.Nama,
        Username = tblUser.Username,
        Password = tblUser.Password,
        Email = tblUser.Email,
        Telepon = tblUser.Telepon
       };
}