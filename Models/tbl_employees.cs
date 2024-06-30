using System;
using System.ComponentModel.DataAnnotations;

namespace AdminClientMVC.Models
{
    public class tbl_employees
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserTypeId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Required]
        [Range(0, 9999999999, ErrorMessage = "Phone number must be a valid numeric value.")]
        public long PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
        public string? Address { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot be longer than 500 characters.")]
        public string? Notes { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }

        public bool IsDeleted { get; set; }
    }
}
