using System.ComponentModel.DataAnnotations;

namespace ITKT_PROJEKTAS.Entities
{
    public class Picture
    {
        public int Id { get; set; }
        [Required]
        public byte[] PictureBytes { get; set; }

        [Required]
        public string PictureFormat { get; set; }
        public User? UserUser { get; set; }
        public Route? RouteRoute { get; set; }
    }
}
