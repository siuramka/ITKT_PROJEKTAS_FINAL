namespace ITKT_PROJEKTAS.Entities
{
    public class Paslauga
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Reservation? Reservation { get; set; }
    }
}
