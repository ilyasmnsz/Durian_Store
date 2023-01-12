namespace User.Models;

public class TblUserDTO
{
    public int Id { get; set; }
    // public string UserID { get; set; } =null!;
    public string? Nama { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? Telepon { get; set; } 
}