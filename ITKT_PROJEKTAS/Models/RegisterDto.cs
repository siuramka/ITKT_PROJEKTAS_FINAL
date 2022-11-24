using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITKT_PROJEKTAS.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Privalomas laukas")]
        [StringLength(20)]
        [DisplayName("Vardas")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Blogas formatas")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Privalomas laukas")]
        [StringLength(20)]
        [DisplayName("Pavarde")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Blogas formatas")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Privalomas laukas")]
        [StringLength(20)]
        [DisplayName("Varotojo vardas")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Blogas formatas")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Privalomas laukas")]
        [StringLength(20)]
        [DisplayName("Tel. Numeris")]
        [RegularExpression(@"(86|\+3706)\d{3}\d{4}", ErrorMessage = "Blogas formatas")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Privalomas laukas")]
        [StringLength(20)]
        [DisplayName("Slaptazodis")]
        public string Password { get; set; }
    }
}
