using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDurianstore.Models
{
    public class Pesanans
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string? NomorPesanan { get; set; }
        public decimal TotalPesanan { get; set; }
        public string? StatusPesanan { get; set; }
    }
}