using ITKT_PROJEKTAS.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ITKT_PROJEKTAS.Models
{
    public class ReservationEditDTO
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Privalomas laukas")]
        public int RouteId { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Privalomas laukas")]
        [DisplayName("Valties tipas")]
        public BoatType Boat { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Privalomas laukas")]
        [DisplayName("Kaina")]
        public double Price { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Privalomas laukas")]
        [DisplayName("Suteikta nuolaida")]
        public double Discount { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Privalomas laukas")]
        [DisplayName("Dalyviu skaičius")]
        public int PersonCount { get; set; }
        [DisplayName("Rezervacijos mokestis")]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Privalomas laukas")]
        [Range(0, int.MaxValue)]
        public double ReservationCost { get; set; } = 0;
        public int UserId { get; set; }
        public int? PaslaugaId { get; set; }
    }
}
