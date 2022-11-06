using Microsoft.AspNetCore.Components.Server;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ITKT_PROJEKTAS.Entities
{
    public class Route
    {
        public int Id { get; set; }
        [DisplayName("Pavadinimas")]
        public string Name { get; set; }
        [DisplayName("Data")]
        public DateTime Date { get; set; }
        [DisplayName("Ilgis(km)")]
        public int Length { get; set; }
        [DisplayName("Sunkumas")]
        public Difficulity Difficulity { get; set; }
        [DisplayName("Aprašymas")]
        public string Description { get; set; }
        [DisplayName("Kaina dalyviui")]
        public int PricePerPerson { get; set; }
        [DisplayName("Leistinas kiekis dalyvių")]
        public int MaxPeople { get; set; }
        public Reservation? Reservation { get; set; }
    }
}
