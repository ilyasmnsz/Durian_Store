// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Durian.Models;
// using Durian.Data;

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

//     //GET: api/Pembeli
//     [HttpGet]
//     public async Task<ActionResult> GetPembelis()
//     {
//         return Ok(await DbContext.Pembelis.ToListAsync());
//     }

//     //Get : api/Pembeli
//     [HttpGet("{id}")]
//     public async Task<ActionResult<PembeliDTO>> GetPmbeli(int id)
//     {
//         var Pembeli = await DbContext.Pembelis.FindAsync(id);

//         if (Pembeli == null)
//         {
//             return NotFound();
//         }
        
//         return ItemToDTO(Pembeli);
//     }

//     //PUT : api/Pembeli
//     [HttpPut("{id}")]
//     public async Task<ActionResult> PutPembeli(int id, PembeliDTO pembeliDTO)
//     {
//         if (id != pembeliDTO.Id)
//         {
//             return BadRequest();
//         }

//         var Pembeli = await DbContext.Pembelis.FindAsync(id);
//         if (Pembeli == null)
//         {
//             return NotFound();
//         }

//         Pembeli.Nama = pembeliDTO.Nama;
//         Pembeli.Email = pembeliDTO.Email;
//         Pembeli.Telepon = pembeliDTO.Telepon;

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
//         //POST : api/Pembeli
//         [HttpPost]
//         public async Task<ActionResult>AddPembeli(AddPembeli addPembeli)
//         {
//             var Pembeli = new PembeliDTO()
//             {
//                 Nama = addPembeli.Nama,
//                 Email = addPembeli.Email,
//                 Telepon = addPembeli.Telepon
//             };

//             await DbContext.Pembelis.AddAsync(Pembeli);
//             await DbContext.SaveChangesAsync();

//             return Ok(Pembeli);
//         }

//         //DELETE: api/Pembeli
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeletePembeli(int id)
//         {
//             var Pembeli = await DbContext.Pembelis.FindAsync(id);
//             if (Pembeli == null)
//             {
//                 return NotFound();
//             }

//             DbContext.Pembelis.Remove(Pembeli);
//             await DbContext.SaveChangesAsync();

//             return NoContent();

//         }

//             private bool PembeliExists(int id)
//             {
//                 return DbContext.Pembelis.Any(e => e.Id == id);
//             }

//             private static PembeliDTO ItemToDTO(PembeliDTO pembeli) =>
//                 new PembeliDTO
//                 {
//                     Id = pembeli.Id,
//                     Nama = pembeli.Nama,
//                     Email = pembeli.Email,
//                     Telepon = pembeli.Telepon
//                 };
// }

