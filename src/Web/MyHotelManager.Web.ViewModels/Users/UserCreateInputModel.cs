namespace MyHotelManager.Web.ViewModels.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserCreateInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [Range(1, 3)]
        public int GenderId { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
    }
}
