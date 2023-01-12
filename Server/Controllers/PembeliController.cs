// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Durian.Models;
// using Durian.Data;
// using User.Models;

// namespace Durian.Controllers;

// [ApiController]
// [Route("api/[controller]")]

// public class PembeliController : ControllerBase 
// {
//     private readonly DurianContext DbContext;

//     public PembeliController(DurianContext DbContext)
//     {
//         this.DbContext = DbContext;
//     }

//     [HttpGet]
//     public async Task<ActionResult> GetPembelis()
//     {
//         return Ok(await DbContext.TblUsers.ToListAsync());
//     }

//     //Get : api/Pembeli
//     [HttpGet("{id}")]
//     public async Task<ActionResult<TblUserDTO>> GetPembeli(int id)
//     {
//         var Pembeli = await DbContext.TblUsers.FindAsync(id);

//         if (Pembeli == null)
//         {
//             return NotFound();
//         }
        
//         return Ok(Pembeli);
//     }

//     //PUT : api/Pembeli
//     [HttpPut("{id}")]
//     public async Task<ActionResult> PutPembeli(int id, TblUserDTO tblUserDTO)
//     {
//         if (id != tblUserDTO.Id)
//         {
//             return BadRequest();
//         }

//         var Pembeli = await DbContext.TblUsers.FindAsync(id);
//         if (Pembeli == null)
//         {
//             return NotFound();
//         }

//         Pembeli.Nama = tblUserDTO.Nama;
//         Pembeli.Username = tblUserDTO.Username;
//         Pembeli.Email = tblUserDTO.Email;
//         Pembeli.Password = tblUserDTO.Password;
//         Pembeli.Telepon = tblUserDTO.Telepon;


//         try
//         {
//             await DbContext.SaveChangesAsync();
//         }
//         catch (DbUpdateConcurrencyException) when (!PembeliExists(id))
//         {
//             return NotFound();
//         }

//         return NoContent();
//     }
//     //POST : api/Pembeli
//     [HttpPost]
//     public async Task<ActionResult>AddPembeli(PembeliDTO pembeliDTO)
//     {
//        var Pembeli = new TblUserDTO()
//         {
//             Nama = pembeliDTO.Nama,
//             Username = pembeliDTO.Username,
//             Email = pembeliDTO.Email,
//             Password = pembeliDTO.Password,
//             Telepon = pembeliDTO.Telepon
//         };

//         await DbContext.TblUsers.AddAsync(Pembeli);
//         await DbContext.SaveChangesAsync();

//         return Ok(Pembeli);
//     }

//     //DELETE: api/Pembeli
//     [HttpDelete("{id}")]
//     public async Task<IActionResult> DeletePembeli(int id)
//     {
//         var Pembeli = await DbContext.TblUsers.FindAsync(id);
//         if (Pembeli == null)
//         {
//             return NotFound();
//         }

//         DbContext.TblUsers.Remove(Pembeli);
//         await DbContext.SaveChangesAsync();

//         return NoContent();
//     }

//     private bool PembeliExists(int id)
//     {
//         return DbContext.TblUsers.Any(e => e.Id == id);
//     }

//     private static TblUserDTO ItemToDTO(TblUserDTO pembeli) =>
//         new TblUserDTO
//         {
//             Id = pembeli.Id,
//             Nama = pembeli.Nama,
//             Username = pembeli.Username,
//             Password = pembeli.Password,
//             Email = pembeli.Email,
//             Telepon = pembeli.Telepon
//         };
// }

