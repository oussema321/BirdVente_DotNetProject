using System.ComponentModel.DataAnnotations;

namespace BirdStore.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public Bird Bird { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int BirdId { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}
