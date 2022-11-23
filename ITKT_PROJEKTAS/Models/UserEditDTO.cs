using ITKT_PROJEKTAS.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ITKT_PROJEKTAS.Models
{
    public class UserEditDTO
    {
        [JsonIgnore]
        public string Id { get; set; }
        [Required]
        [DisplayName("Vardas")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Pavardė")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Vartotojo vardas")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Tel. Numeris")]
        [RegularExpression(@"(86|\+3706)\d{3}\d{4}", ErrorMessage = "Blogas formatas")]
        public string Phone { get; set; }
        [Required]
        public Role Role { get; set; }

    }
}
