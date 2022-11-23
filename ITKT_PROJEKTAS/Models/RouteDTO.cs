using ITKT_PROJEKTAS.Entities;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITKT_PROJEKTAS.Models
{
    public class RouteDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [DisplayName("Pavadinimas")]
        [MaxLength(20, ErrorMessage = "Klaida. Pavadinimas turi buti iki 20 simbolių")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Data")]
        public DateTime Date { get; set; }

        [Required]
        [DisplayName("Ilgis(km)")]
        public double Length { get; set; }

        [Required]
        [DisplayName("Sunkumas")]
        public Difficulity Difficulity { get; set; }

        [Required]
        [DisplayName("Aprašymas")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Kaina dalyviui")]
        public double PricePerPerson { get; set; }

        [Required]
        [DisplayName("Leistinas kiekis dalyvių")]
        public int MaxPeople { get; set; }

    }
}
