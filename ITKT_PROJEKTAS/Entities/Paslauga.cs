using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITKT_PROJEKTAS.Entities
{
    public class Paslauga
    {
        public int Id { get; set; }
        [DisplayName("Paslaugos pavadinimas")]
        [Required(ErrorMessage = "Privalomas laukas")]
        [StringLength(20)]
        public string Name { get; set; }
        [DisplayName("Paslaugos kaina")]
        [Required(ErrorMessage = "Privalomas laukas")]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        public ICollection<Reservation>? Reservation { get; set; }

    }
}
