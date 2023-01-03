// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Durian.Models;
// using Durian.Data;

// namespace Durian.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// public class TblUserController : ControllerBase
// {
//     private readonly DurianContext DbContext;

//     public TblUserController(DurianContext DbContext)
//     {
//         this.DbContext = DbContext;
//     }

//      [HttpGet]
//     public async Task<ActionResult> GetTblUsers()
//     {
//         return Ok(await DbContext.TblUsers.ToListAsync());
//     }

//     [HttpPost]
//     public async Task<ActionResult> UserCred(UserCred userCred)
//     {
//         var UserCred = new TblUserDTO()
//         {
//             Username = userCred.Username,
//             Password = userCred.Password,
//         };
        
//         await DbContext.TblUsers.AddAsync(UserCred);
//         await DbContext.SaveChangesAsync();

//         return Ok(UserCred);
//     }

//     private bool UserCredExists(string Username)
//     {
//         return DbContext.TblUsers.Any(e => e.Username == Username);
//     }

//     private static TblUserDTO ItemToDTO(TblUserDTO tblUser) =>
//        new TblUserDTO
//        {
//            Id = tblUser.Id,
//            Username = tblUser.Username,
//            Password = tblUser.Password,
//        };
// }