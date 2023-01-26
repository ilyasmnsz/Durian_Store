using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using User.Models;
using Durian.Models;

namespace Belanja.Models
{
    public class KeranjangDTO
    {
        public int Id { get; set; }
        public int Jumlahdurian { get; set; }
        
        public UserDTO? UserDTO { get; set; }
        public int UserDTO_Id { get; set; }

        public DurianDTO? DurianDTO { get; set; }
        public int DurianDTO_Id { get; set; }
    }
}