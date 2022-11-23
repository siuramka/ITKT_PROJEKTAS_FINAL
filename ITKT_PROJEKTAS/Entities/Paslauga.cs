namespace ITKT_PROJEKTAS.Entities
{
    public class Paslauga
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
