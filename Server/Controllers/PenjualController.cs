using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Durian.Models;
using Durian.Data;

namespace Durian.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PenjualController : ControllerBase
{
    private readonly DurianContext DbContext;

    public PenjualController(DurianContext DbContext)
    {
        this.DbContext = DbContext;
    }

    // GET: api/Penjual
    [HttpGet]
    public async Task<ActionResult> GetPenjuals()
    {
        return Ok(await DbContext.Penjuals.ToListAsync());
    }

    // GET: api/TPenjual
    // <snippet_GetByID>
    [HttpGet("{id}")]
    public async Task<ActionResult<PenjualDTO>> GetPenjual(int id)
    {
        var Penjual = await DbContext.Penjuals.FindAsync(id);

        if (Penjual == null)
        {
            return NotFound();
        }

        return ItemToDTO(Penjual);
    }
    // </snippet_GetByID>

    // PUT: api/Penjual
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Update>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPenjual(int id, PenjualDTO penjualDTO)
    {
        if (id != penjualDTO.Id)
        {
            return BadRequest();
        }

        var Penjual = await DbContext.Penjuals.FindAsync(id);
        if (Penjual == null)
        {
            return NotFound();
        }

        Penjual.Nama = penjualDTO.Nama;
        Penjual.Sosmed = penjualDTO.Sosmed;
        Penjual.Telepon = penjualDTO.Telepon;
        Penjual.Alamat = penjualDTO.Alamat;

        try
        {
            await DbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!PenjualExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }
    // </snippet_Update>

    // POST: api/Penjual
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Create>
    [HttpPost]
    public async Task<ActionResult> AddPenjual(AddPenjual addPenjual)
    {
        var Penjual = new PenjualDTO()
        {
            Nama = addPenjual.Nama,
            Sosmed = addPenjual.Sosmed,
            Telepon = addPenjual.Telepon,
            Alamat = addPenjual.Alamat
        };
        
        await DbContext.Penjuals.AddAsync(Penjual);
        await DbContext.SaveChangesAsync();

        return Ok(Penjual);
    }
    // </snippet_Create>

    // DELETE: api/Penjual
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePenjual(int id)
    {
        var Penjual = await DbContext.Penjuals.FindAsync(id);
        if (Penjual == null)
        {
            return NotFound();
        }

        DbContext.Penjuals.Remove(Penjual);
        await DbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool PenjualExists(int id)
    {
        return DbContext.Penjuals.Any(e => e.Id == id);
    }

    private static PenjualDTO ItemToDTO(PenjualDTO penjual) =>
       new PenjualDTO
       {
           Id = penjual.Id,
           Nama = penjual.Nama,
           Sosmed = penjual.Sosmed,
           Telepon = penjual.Telepon,
           Alamat = penjual.Alamat


       };
}