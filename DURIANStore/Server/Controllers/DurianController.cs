using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Durian.Models;
using Durian.Data;

namespace Durian.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DurianItemController : ControllerBase
{
    private readonly DurianContext DbContext;

    public DurianItemController(DurianContext DbContext)
    {
        this.DbContext = DbContext;
    }

    // GET: api/DurianItem
    [HttpGet]
    public async Task<ActionResult> GetDurianItems()
    {
        return Ok(await DbContext.DurianItems.ToListAsync());
    }

    // GET: api/Durian
    // <snippet_GetByID>
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
    // </snippet_GetByID>

    // PUT: api/Durian
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Update>
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
    // </snippet_Update>

    // POST: api/Durian
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Create>
    [HttpPost]
    public async Task<ActionResult> AddDurian(AddDurian addDurian)
    {
        var DurianItem = new DurianItemDTO()
        {
            Nama = addDurian.Nama,
            Harga = addDurian.Harga,
            Stok = addDurian.Stok
        };
        
        await DbContext.DurianItems.AddAsync(DurianItem);
        await DbContext.SaveChangesAsync();

        return Ok(DurianItem);
    }
    // </snippet_Create>

    // DELETE: api/Durian
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
           Harga = durianItem.Harga
       };
}