using System.ComponentModel.DataAnnotations;
namespace TvShowTracker.Model
{
    public class UserPostDTO
    {
        [Required(ErrorMessage = "Please enter a email!")]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(255, ErrorMessage = "Must have at least 6 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class UserGetDTO
    {
        public string Email { get; set; }
    }
}