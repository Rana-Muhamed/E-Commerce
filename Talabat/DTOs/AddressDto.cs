using System.ComponentModel.DataAnnotations;

namespace Talabat.DTOs
{
    public class AddressDto
    {
        //public int Id { get; set; }

        [Required]
        public string FisrtName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
    }
}
