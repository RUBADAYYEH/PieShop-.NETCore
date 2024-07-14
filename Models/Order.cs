using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace PieShop.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }

        public List<OrderDetail>? OrderDetails { get; set; }

        [Required(ErrorMessage ="Please Enter your first name")]
        [Display(Name ="First name")]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Enter your last name")]
        [Display(Name = "Last name")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Enter your address")]
        [Display(Name = "Adress Line 1")]
        [StringLength(100)]
        public string AddressLine1 { get; set; } = string.Empty;

        [Display(Name = "Adress Line 2")]
        public string? AddressLine2 { get; set; }

        [Required(ErrorMessage = "Please Enter your Zip code")]
        [Display(Name = "Zip code")]
        [StringLength(4)]
        public string ZipCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Enter your city")]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        
        [StringLength(10)]
        public string? State { get; set; }

        [Required(ErrorMessage = "Please Enter your country")]
        [StringLength(50)]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Enter your phone number")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Enter your email")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
       
        public string Email { get; set; } = string.Empty;


        public decimal OrderTotal { get; set; }

        public DateTime OrderPlaced { get; set; }
    }
}
