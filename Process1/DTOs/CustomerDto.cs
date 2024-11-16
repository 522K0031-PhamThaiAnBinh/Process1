using System.ComponentModel.DataAnnotations;

namespace Process1.DTOs
{
    public class CustomerDto
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class CreateCustomerDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    }

    public class UpdateCustomerDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    }
}
