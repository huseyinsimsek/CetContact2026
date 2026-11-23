namespace CetContact.Models;
using SQLite;

[Table("Contact")]
public class PhoneContact
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [MaxLength(100)]
    public string? Name { get; set; }
    [MaxLength(100)]
    public string? PhoneNumber { get; set; }
    [MaxLength(200)]
    public string? Email { get; set; }
}