using System.ComponentModel.DataAnnotations;

namespace TicketApi
{
    public class TicketOrder
    {
        [Required(ErrorMessage = "Id is compulsory")]
        public int ConcertId { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Valid Email id is required")]
        public string Email { get; set; } = string.Empty;

        [MinLength(5, ErrorMessage = "Full Name should be atleast 5 characters long")]
        [Required(ErrorMessage = "Full Name is required")]
        public string Name { get; set; } = string.Empty;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number should be 10 digits Long")]
        public string Phone { get; set; } = string.Empty;

        [Required, Range(1, 10, ErrorMessage = "Quantity should be between 1 and 10")]
        public int Quantity { get; set; }

        [Required, CreditCard]
        public string CreditCard { get; set; } = string.Empty;

        [Required, RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$", ErrorMessage = "Invalid Expiration Date. Format: MM/YY")]
        public string Expiration { get; set; } = string.Empty;

        [Required, StringLength(3, MinimumLength = 3, ErrorMessage = "Security Code should be 3 digits long")]
        public string SecurityCode { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Province { get; set; } = string.Empty;

        [Required, StringLength(6, MinimumLength = 6, ErrorMessage = "Invalid Postal Code.")]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;


    }
}
