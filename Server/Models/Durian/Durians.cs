using Belanja.Models;

namespace Durian.Models;

public class DurianDTO
{
    public int Id { get; set; }
    public string? Namadurian { get; set; }
    public string? Tentangdurian { get; set; }
    public string? Keadaandurian { get; set; }
    public decimal Hargadurian { get; set; }
    public int Stokdurian { get; set; } 
    public string? Gambardurian { get; set; }

    public List<KeranjangDTO> KeranjangDTOs { get; set;} =null!;

}