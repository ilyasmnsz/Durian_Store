using Belanja.Models;

namespace User.Models;

public class UserDTO
{
    public int Id { get; set; }
    public string? Nama { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? Telepon { get; set; } 
    public string? Tipe { get; set; }

    public List<KeranjangDTO> KeranjangDTOs { get; set;}
}