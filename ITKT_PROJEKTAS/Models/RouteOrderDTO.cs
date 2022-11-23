using ITKT_PROJEKTAS.Entities;

namespace ITKT_PROJEKTAS.Models
{
    public class RouteOrderDTO
    {
        public int? Passingid { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Length { get; set; }
        public Difficulity Difficulity { get; set; }
        public BoatType Boat { get; set; }
        public string Description { get; set; }
        public double PricePerPerson { get; set; }
        public int PeopleCount { get; set; }
        public int MaxPeople { get; set; }

    }
}
