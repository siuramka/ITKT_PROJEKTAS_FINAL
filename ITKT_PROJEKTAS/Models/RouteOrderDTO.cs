using ITKT_PROJEKTAS.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ITKT_PROJEKTAS.Models
{
    public class RouteOrderDTO
    {
        public int? Passingid { get; set; }
        [DisplayName("Pavadinimas")]
        public string Name { get; set; }
        [DisplayName("Data")]
        public DateTime Date { get; set; }
        [DisplayName("Ilgis(km)")]
        public double Length { get; set; }
        [DisplayName("Sunkumas")]
        public Difficulity Difficulity { get; set; }
        [DisplayName("Valtis")]
        public BoatType Boat { get; set; }
        [DisplayName("Aprašymas")]
        public string Description { get; set; }
        [DisplayName("Kaina dalyviui")]
        public double PricePerPerson { get; set; }
        [DisplayName("Dalyvių skaičius")]
        public int PeopleCount { get; set; }
        [DisplayName("Dalyvių limitas")]
        public int MaxPeople { get; set; }
        public int? PaslaugaId { get; set; }

    }
}
