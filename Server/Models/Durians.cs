using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDurianstore.Models
{
    public class Durians
    {
        public int ID { get; set; }
        public string? Nama { get; set; }
        public string? Ukuran { get; set; }
        public string? Keadaan { get; set; }
        public decimal Harga { get; set; }
        public decimal Diskon { get; set; }
        public int Stok { get; set; }
        public DateTime PembelianPada { get; set; }
        public string? Gambar { get; set;}
        public int Status { get; set; }
    }
}