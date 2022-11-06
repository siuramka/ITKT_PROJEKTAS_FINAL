using System.ComponentModel;

namespace ITKT_PROJEKTAS.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        [DisplayName("Trasos Id")]
        public int RouteId { get; set; }
        public Route? Route { get; set; }
        [DisplayName("Valties tipas")]
        public BoatType Boat { get; set; }
        [DisplayName("Kaina")]
        public int Price { get; set; }
        [DisplayName("Akcijos suma")]
        public int Discount { get; set; }
        [DisplayName("Dalyviu skaičius")]
        public int PersonCount { get; set; }
        [DisplayName("Vartotojo Id")]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
