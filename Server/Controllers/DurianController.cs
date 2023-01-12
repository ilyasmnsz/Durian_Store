using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Durian.Models;
using Durian.Data;
using Microsoft.AspNetCore.Authorization;

namespace Durian.Controllers;

[Authorize(Roles = "admin,user")]
[ApiController]
[Route("api/[controller]")]
public class DurianItemController : ControllerBase
{
    private readonly DurianContext DbContext;

    public DurianItemController(DurianContext DbContext)
    {
        this.DbContext = DbContext;
    }


    [HttpGet]
    public async Task<ActionResult> GetDurianItems()
    {
        return Ok(await DbContext.DurianItems.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DurianItemDTO>> GetDurianItem(int id)
    {
        var DurianItem = await DbContext.DurianItems.FindAsync(id);

        if (DurianItem == null)
        {
            return NotFound();
        }

        return ItemToDTO(DurianItem);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDurianItem(int id, DurianItemDTO durianItem)
    {
        if (id != durianItem.Id)
        {
            return BadRequest();
        }

        var DurianItem = await DbContext.DurianItems.FindAsync(id);
        if (DurianItem == null)
        {
            return NotFound();
        }

        DurianItem.Nama = durianItem.Nama;
        DurianItem.Harga = durianItem.Harga;
        DurianItem.Keadaan = durianItem.Keadaan;
        DurianItem.Stok = durianItem.Stok;

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
        var DurianItem = new DurianItemDTO()
        {
            Nama = addDurian.Nama,
            Harga = addDurian.Harga,
            Keadaan = addDurian.Keadaan,
            Stok = addDurian.Stok
        };
        
        await DbContext.DurianItems.AddAsync(DurianItem);
        await DbContext.SaveChangesAsync();

        return Ok(DurianItem);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDurianItem(int id)
    {
        var DurianItem = await DbContext.DurianItems.FindAsync(id);
        if (DurianItem == null)
        {
            return NotFound();
        }

        DbContext.DurianItems.Remove(DurianItem);
        await DbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool DurianItemExists(int id)
    {
        return DbContext.DurianItems.Any(e => e.Id == id);
    }

    private static DurianItemDTO ItemToDTO(DurianItemDTO durianItem) =>
       new DurianItemDTO
       {
           Id = durianItem.Id,
           Nama = durianItem.Nama,
           Harga = durianItem.Harga,
           Keadaan = durianItem.Keadaan,
           Stok = durianItem.Stok
       };
}