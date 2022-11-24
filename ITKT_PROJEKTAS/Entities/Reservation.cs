using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITKT_PROJEKTAS.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        [DisplayName("Trasos Id")]
        public int RouteId { get; set; }
        public Route? Route { get; set; }
        [DisplayName("Valties tipas")]
        [Required(ErrorMessage = "Privalomas laukas")]
        public BoatType Boat { get; set; }
        [DisplayName("Kaina")]
        [Required(ErrorMessage = "Privalomas laukas")]
        public double Price { get; set; }
        [DisplayName("Suteikta nuolaida")]
        [Required(ErrorMessage = "Privalomas laukas")]
        public double Discount { get; set; }
        [DisplayName("Dalyviu skaičius")]
        [Required(ErrorMessage = "Privalomas laukas")]
        public int PersonCount { get; set; }
        [DisplayName("Uzsakovo Id")]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
