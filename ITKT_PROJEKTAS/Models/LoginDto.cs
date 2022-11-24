using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ITKT_PROJEKTAS.Models
{
    public class LoginDto
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Privalomas laukas")]
        [DisplayName("Vartotojo vardas")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Blogas formatas")]
        [StringLength(20)]
        public string Username { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Privalomas laukas")]
        [DisplayName("Slaptažodis")]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
