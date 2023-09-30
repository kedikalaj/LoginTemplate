using System.ComponentModel.DataAnnotations;

namespace LoginTemplate.Model.Authentication
{
    public class RegisterUserViewModel
    {
        /// <summary>
        /// Gets or sets the username of the user
        /// </summary>
        [Required(ErrorMessage = "The username is required")]
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the username of the user
        /// </summary>
        [EmailAddress]
        [Required(ErrorMessage = "The email is required")]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the username of the user
        /// </summary>
        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }

    }
}
