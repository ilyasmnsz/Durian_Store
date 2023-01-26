// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Http;
// using Durian.Models;
// using Durian.Data;
// using Microsoft.EntityFrameworkCore;
// using System.Text;
// using Newtonsoft.Json;

// namespace Cart.Controller;
// [ApiController]
// [Route("api/[controller]")]
//     public class CartController : ControllerBase
//     {
//         private readonly DurianContext DbContext;

//         public CartController(DurianContext context)
//         {
//             this.DbContext = context;
//         }

//         // public IActionResult Index()
//         // {
//         //     List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
//         //     CartView
//         // }
//         [HttpPost]
//         public async Task<IActionResult> AddCart(int id)
//         {
//             DurianItemDTO durianItemDTO = await DbContext.DurianItems.FindAsync(id);
//             List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
//             CartItem cartItem = cart.Where(c=> c.DurianId == id).FirstOrDefault();
//             if (cartItem == null)
//             {
//                 cart.Add(new CartItem(durianItemDTO));
//             }
//             else
//             {
//                 cartItem.Jumlah +=1 ;
//             }
//             HttpContext.Session.setJson("Cart", cart);
//             // TempData["Sukses"] = "Durian Ditambahkan";
//             return Redirect(Request.Headers["Referer"].ToString());
//         }

//         [HttpGet]
//         public async Task<IActionResult> Decrease(long id)
//                 {
//                         List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

//                         CartItem cartItem = cart.Where(c => c.DurianId == id).FirstOrDefault();

//                         if (cartItem.Jumlah > 1)
//                         {
//                                 --cartItem.Jumlah;
//                         }
//                         else
//                         {
//                                 cart.RemoveAll(p => p.DurianId == id);
//                         }

//                         if (cart.Count == 0)
//                         {
//                                 HttpContext.Session.Remove("Cart");
//                         }
//                         else
//                         {
//                                 HttpContext.Session.setJson("Cart", cart);
//                         }

//                         // TempData["Success"] = "The product has been removed!";

//                         return RedirectToAction("Index");
//                 }

//                 [HttpDelete]

//                 public async Task<IActionResult> Remove(long id)
//                 {
//                         List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

//                         cart.RemoveAll(p => p.DurianId == id);

//                         if (cart.Count == 0)
//                         {
//                                 HttpContext.Session.Remove("Cart");
//                         }
//                         else
//                         {
//                                 HttpContext.Session.setJson("Cart", cart);
//                         }

//                         // TempData["Success"] = "The product has been removed!";

//                         return RedirectToAction("Index");
//                 }


//         private bool DurianItemExists(int id)
//         {
//         return DbContext.DurianItems.Any(e => e.Id == id);
//         }

//         private static DurianItemDTO ItemToDTO(DurianItemDTO durianItem) =>
//         new DurianItemDTO
//         {
//            Id = durianItem.Id,
//            Namadurian = durianItem.Namadurian,
//            Tentangdurian = durianItem.Tentangdurian,
//            Keadaandurian = durianItem.Keadaandurian,
//            Hargadurian = durianItem.Hargadurian,
//            Stokdurian = durianItem.Stokdurian,
//            Gambardurian = durianItem.Gambardurian
//         };
//     }