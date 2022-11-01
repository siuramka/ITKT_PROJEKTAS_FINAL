namespace ITKT_PROJEKTAS.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public Route Route { get; set; }
        public BoatType Boat { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int PersonCount { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
