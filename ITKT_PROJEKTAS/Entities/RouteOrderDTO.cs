namespace ITKT_PROJEKTAS.Entities
{
    public class RouteOrderDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Length { get; set; }
        public Difficulity Difficulity { get; set; }
        public BoatType Boat { get; set; }
        public string Description { get; set; }
        public int PricePerPerson { get; set; }
        public int PeopleCount { get; set; }
        public int MaxPeople { get; set; }

    }
}
