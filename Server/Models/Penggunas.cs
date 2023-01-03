using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDurianstore.Models
{
    public class Penggunas
    {
        public int ID { get; set; }
        public string? Namapengguna { get; set; }
        public string? Katasandi { get; set; }
        public string? Email { get; set; }
        public string? Telepon { get; set; }
        public string? Alamat { get; set; }
        public decimal Dana { get; set; }
        public string? Type { get; set; }
        public int Status { get; set; }
        public DateTime DibuatPada { get; set; }
    }
}