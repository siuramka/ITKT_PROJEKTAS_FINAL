using ITKT_PROJEKTAS.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ITKT_PROJEKTAS.Models
{
    public class UserEditDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required(ErrorMessage = "Privalomas laukas")]
        [DisplayName("Vardas")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Privalomas laukas")]
        [DisplayName("Pavardė")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Privalomas laukas")]
        [DisplayName("Vartotojo vardas")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Privalomas laukas")]
        [DisplayName("Tel. Numeris")]
        [RegularExpression(@"(86|\+3706)\d{3}\d{4}", ErrorMessage = "Blogas formatas")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Privalomas laukas")]
        public Role Role { get; set; }

    }
}
