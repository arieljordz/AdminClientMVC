using System.ComponentModel.DataAnnotations;

namespace AdminClientMVC.Models
{
    public class tbl_user_types
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Description cannot be longer than 50 characters.")]
        public string Description { get; set; }
    }
}
