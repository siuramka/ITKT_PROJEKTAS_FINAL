namespace ITKT_PROJEKTAS.Entities;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    [DisplayName("Vardas")]
    [StringLength(20)]
    public string FirstName { get; set; }
    [DisplayName("Pavardė")]
    [StringLength(20)]
    public string LastName { get; set; }
    [DisplayName("Vartotojo vardas")]
    [StringLength(20)]
    public string Username { get; set; }
    [DisplayName("Tel. Numeris")]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Blogas formatas")]
    public string Phone { get; set; }
    public Role Role { get; set; }

    public ICollection<Reservation> Reservations { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }
}