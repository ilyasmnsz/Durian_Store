using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDurianstore.Models
{
    public class PesananDurians
    {
        public int ID { get; set; }
        public int IDPesanan { get; set; }
        public int IDDurian { get; set; }
        public decimal Harga { get; set; }
        public decimal Diskon { get ;set; }
        public int Stok { get; set; }
        public decimal TotalHarga { get; set; }
    }
}