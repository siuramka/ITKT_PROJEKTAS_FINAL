using ITKT_PROJEKTAS.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ITKT_PROJEKTAS.Models
{
    public class RouteImageDTO
    {
        public int? Id { get; set; }
        [DisplayName("Pavadinimas")]
        public string Name { get; set; }

        [DisplayName("Data")]
        public DateTime Date { get; set; }

        [DisplayName("Ilgis(km)")]
        public double Length { get; set; }

        [DisplayName("Sunkumas")]
        public Difficulity Difficulity { get; set; }

        [DisplayName("Aprašymas")]
        public string Description { get; set; }

        [DisplayName("Kaina dalyviui")]
        public double PricePerPerson { get; set; }

        [DisplayName("Leistinas kiekis dalyvių")]
        public int MaxPeople { get; set; }
        [Required(ErrorMessage ="Reikia įkelti paveiksliuką.")]
        public IFormFile Picture { get; set; }

    }
}
