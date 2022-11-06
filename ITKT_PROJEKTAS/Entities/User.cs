namespace ITKT_PROJEKTAS.Entities;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    [DisplayName("Vardas")]
    public string FirstName { get; set; }
    [DisplayName("Pavardė")]
    public string LastName { get; set; }
    [DisplayName("Vartotojo vardas")]
    public string Username { get; set; }
    [DisplayName("Tel. Numeris")]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
    public string Phone { get; set; }
    public Role Role { get; set; }

    public ICollection<Reservation> Reservations { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }
}