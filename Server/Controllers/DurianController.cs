using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Durian.Models;
using Durian.Data;
using Microsoft.AspNetCore.Authorization;

namespace Durian.Controllers;

[Authorize(Roles = "admin,user")]
[ApiController]
[Route("api/[controller]")]
public class DurianController : ControllerBase
{
    private readonly DurianContext DbContext;

    public DurianController(DurianContext DbContext)
    {
        this.DbContext = DbContext;
    }


    [HttpGet("durian")]
    public async Task<ActionResult> GetDurians()
    {
        return Ok(await DbContext.Durians.ToListAsync());
    }

    [Authorize(Roles = "admin, user")]
    [HttpGet("{id}")]
    public async Task<ActionResult<DurianDTO>> GetDurian(int id)
    {
        var DurianItem = await DbContext.Durians.FindAsync(id);

        if (DurianItem == null)
        {
            return NotFound();
        }

        return ItemToDTO(DurianItem);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDurian(int id, DurianDTO durian)
    {
        if (id != durian.Id)
        {
            return BadRequest();
        }

        var DurianItem = await DbContext.Durians.FindAsync(id);
        if (DurianItem == null)
        {
            return NotFound();
        }

        DurianItem.Namadurian = durian.Namadurian;
        DurianItem.Tentangdurian = durian.Tentangdurian;
        DurianItem.Keadaandurian = durian.Keadaandurian;
        DurianItem.Hargadurian = durian.Hargadurian;
        DurianItem.Stokdurian = durian.Stokdurian;
        DurianItem.Gambardurian = durian.Gambardurian;

        try
        {
            await DbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!DurianItemExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<ActionResult> AddDurian(AddDurian addDurian)
    {
        var DurianItem = new DurianDTO()
        {
            Namadurian = addDurian.Nama,
            Tentangdurian = addDurian.Tentang,
            Keadaandurian = addDurian.Keadaan,
            Hargadurian = addDurian.Harga,
            Stokdurian = addDurian.Stok,
            Gambardurian = addDurian.Gambar
        };
        
        await DbContext.Durians.AddAsync(DurianItem);
        await DbContext.SaveChangesAsync();

        return Ok(DurianItem);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDurian(int id)
    {
        var DurianItem = await DbContext.Durians.FindAsync(id);
        if (DurianItem == null)
        {
            return NotFound();
        }

        DbContext.Durians.Remove(DurianItem);
        await DbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool DurianItemExists(int id)
    {
        return DbContext.Durians.Any(e => e.Id == id);
    }

    private static DurianDTO ItemToDTO(DurianDTO durianItem) =>
       new DurianDTO
       {
           Id = durianItem.Id,
           Namadurian = durianItem.Namadurian,
           Tentangdurian = durianItem.Tentangdurian,
           Keadaandurian = durianItem.Keadaandurian,
           Hargadurian = durianItem.Hargadurian,
           Stokdurian = durianItem.Stokdurian,
           Gambardurian = durianItem.Gambardurian
       };
}