using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Durian.Models;
using Belanja.Models;
using User.Models;
using Durian.Data;
using System.Text;

namespace Cart.Controllers;

[ApiController]
[Route("api/[controller]")]

public class KeranjangController : ControllerBase
{
    private readonly DurianContext DbContext;
    public KeranjangController(DurianContext durianContext)
    {
        this.DbContext = durianContext;
    }

    [HttpGet]
    public async Task<ActionResult> GetCartItem()
    {
        return Ok(await DbContext.Keranjangs.ToListAsync());
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<CartItemDTO>> GetCartItem(int id)
    // {
    //     var CartItem = await DbContext.CartItems.FindAsync(id);

    //     if (CartItem == null)
    //     {
    //         return NotFound();
    //     }

    //     return ItemToDTO(CartItem);
    // }

    [HttpPut]
    public async Task<IActionResult> PutKeranjang(int id, KeranjangDTO keranjangDTO)
    {
        if (id != keranjangDTO.Id)
        {
            return BadRequest();
        }

        var Cartitem = await DbContext.Keranjangs.FindAsync(id);
        if (Cartitem == null)
        {
            return NotFound();
        }

        Cartitem.UserDTO_Id = keranjangDTO.UserDTO_Id;
        Cartitem.DurianDTO_Id = keranjangDTO.DurianDTO_Id;
        Cartitem.Jumlahdurian = keranjangDTO.Jumlahdurian;

        try
        {
            await DbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!CartitemExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("Tambah")]
    public async Task<ActionResult> AddKeranjang( AddKeranjang addKeranjang)
    {
        var KeranjangItem = new KeranjangDTO()
        {
            UserDTO_Id = addKeranjang.UserDTO_Id,
            DurianDTO_Id = addKeranjang.DurianDTO_Id,
            Jumlahdurian = addKeranjang.Jumlahdurian
        };

        await DbContext.Keranjangs.AddAsync(KeranjangItem);
        await DbContext.SaveChangesAsync();

        return Ok(KeranjangItem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteKeranjang(int id)
    {
        var Cartitem = await DbContext.Keranjangs.FindAsync(id);
        if (Cartitem == null)
        {
            return NotFound();
        }

        DbContext.Keranjangs.Remove(Cartitem);
        await DbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool CartitemExists(int id)
    {
        return DbContext.Keranjangs.Any(e=> e.Id == id);
    }
    private static KeranjangDTO ItemToDTO(KeranjangDTO keranjangDTO) =>
        new KeranjangDTO
        {
            UserDTO_Id = keranjangDTO.UserDTO_Id,
            DurianDTO_Id = keranjangDTO.DurianDTO_Id,
            Jumlahdurian = keranjangDTO.Jumlahdurian
        };
}