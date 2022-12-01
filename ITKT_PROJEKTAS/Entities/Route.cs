using Microsoft.AspNetCore.Components.Server;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ITKT_PROJEKTAS.Entities
{
    public class Route
    {
        public int Id { get; set; }
        [DisplayName("Pavadinimas")]
        [StringLength(20)]
        public string Name { get; set; }
        [DisplayName("Data")]
        public DateTime Date { get; set; }
        [DisplayName("Ilgis(km)")]
        [Range(0, double.MaxValue)]
        public double Length { get; set; }
        [DisplayName("Sunkumas")]
        public Difficulity Difficulity { get; set; }
        [DisplayName("Aprašymas")]
        [StringLength(1000)]
        public string Description { get; set; }
        [DisplayName("Kaina dalyviui")]
        [Range(0, double.MaxValue)]
        public double PricePerPerson { get; set; }
        [DisplayName("Leistinas kiekis dalyvių")]
        [Range(0, int.MaxValue)]
        public int MaxPeople { get; set; }
        public Reservation? Reservation { get; set; }
    }
}
