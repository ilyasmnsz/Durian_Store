using System.ComponentModel.DataAnnotations;

namespace DurianCart.Models
{
    public class CartItem
    {
        [Key]
        public string ItemId { get; set; }
        public string CartId { get;set; }
        public DateTime DateCreated { get; set; }
        public int ProductId { get; set; }
        
    }
}