using Microsoft.AspNetCore.Components.Server;

namespace ITKT_PROJEKTAS.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeLength { get; set; }
        public Difficulity Difficulity { get; set; }
        public string Description { get; set; }
        public int PricePerPerson { get; set; }
        public int MaxPeople { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
