using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDurianstore.Models
{
    public class Response
    {
        public int KodeStatus { get; set; }
        public string? StatusPesan { get; set; }
        public List<Penggunas>? listPenggunas { get; set; }
        public Penggunas? pengguna { get; set;}
        public List<Durians>? listDurians { get; set; }
        public Durians? durian { get; set; }
        public List<Keranjangs>? listKeranjangs { get; set; }
        public List<Pesanans>? listPesanans { get; set; }
        public Pesanans? pesanan { get; set; }
        public List<PesananDurians>? listPesananDurians { get; set; }
        public PesananDurians? pesananDurian { get; set; }
    }
}