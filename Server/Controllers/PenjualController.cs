// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Durian.Models;
// using Durian.Data;
// using User.Models;

// namespace Durian.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// public class PenjualController : ControllerBase
// {
//     private readonly DurianContext DbContext;

//     public PenjualController(DurianContext DbContext)
//     {
//         this.DbContext = DbContext;
//     }

//     [HttpGet]
//     public async Task<ActionResult> GetPenjuals()
//     {
//         return Ok(await DbContext.TblUsers.ToListAsync());
//     }

//     [HttpGet("{id}")]
//     public async Task<ActionResult<TblUserDTO>> GetPenjual(int id)
//     {
//         var Penjual = await DbContext.TblUsers.FindAsync(id);

//         if (Penjual == null)
//         {
//             return NotFound();
//         }

//         return Ok(Penjual);
//     }

//     [HttpPut("{id}")]
//     public async Task<IActionResult> PutPenjual(int id, TblUserDTO tblUserDTO)
//     {
//         if (id != tblUserDTO.Id)
//         {
//             return BadRequest();
//         }

//         var Penjual = await DbContext.TblUsers.FindAsync(id);
//         if (Penjual == null)
//         {
//             return NotFound();
//         }

//         Penjual.Nama = tblUserDTO.Nama;
//         Penjual.Username = tblUserDTO.Username;
//         Penjual.Email = tblUserDTO.Email;
//         Penjual.Password = tblUserDTO.Password;
//         Penjual.Telepon = tblUserDTO.Telepon;

//         try
//         {
//             await DbContext.SaveChangesAsync();
//         }
//         catch (DbUpdateConcurrencyException) when (!PenjualExists(id))
//         {
//             return NotFound();
//         }

//         return NoContent();
//     }
 
//     [HttpPost]
//     public async Task<ActionResult> AddPenjual(PenjualDTO penjualDTO)
//     {
//         var Penjual = new TblUserDTO()
//         {
//             Nama = penjualDTO.Nama,
//             Username = penjualDTO.Username,
//             Email = penjualDTO.Email,
//             Password = penjualDTO.Password,
//             Telepon = penjualDTO.Telepon
//         };
        
//         await DbContext.TblUsers.AddAsync(Penjual);
//         await DbContext.SaveChangesAsync();

//         return Ok(Penjual);
//     }

//     [HttpDelete("{id}")]
//     public async Task<IActionResult> DeletePenjual(int id)
//     {
//         var Penjual = await DbContext.TblUsers.FindAsync(id);
//         if (Penjual == null)
//         {
//             return NotFound();
//         }

//         DbContext.TblUsers.Remove(Penjual);
//         await DbContext.SaveChangesAsync();

//         return NoContent();
//     }

//     private bool PenjualExists(int id)
//     {
//         return DbContext.TblUsers.Any(e => e.Id == id);
//     }

//     private static TblUserDTO ItemToDTO(TblUserDTO penjual) =>
//        new TblUserDTO
//        {
//            Id = penjual.Id,
//            Nama = penjual.Nama,
//            Username = penjual.Username,
//            Password = penjual.Password,
//            Email = penjual.Email,
//            Telepon = penjual.Telepon
//        };
// }