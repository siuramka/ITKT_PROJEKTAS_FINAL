namespace ITKT_PROJEKTAS.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Phone { get; set; }
    public Role Role { get; set; }

    public ICollection<Reservation> Reservations { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }
}