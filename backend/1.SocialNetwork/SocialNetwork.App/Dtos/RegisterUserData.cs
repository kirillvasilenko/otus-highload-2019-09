using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.App.Dtos
{
    public class RegisterUserData
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
        
        /// <summary>
        /// Repeated password
        /// </summary>
        [Required]
        public string RepeatedPassword { get; set; }
        
        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public string GivenName { get; set; }
        
        /// <summary>
        /// Last name
        /// </summary>
        [Required]
        public string FamilyName { get; set; }
        
        /// <summary>
        /// Age
        /// </summary>
        [Required]
        public byte Age { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [Required]
        public string City { get; set; }
        
        /// <summary>
        /// Interests
        /// </summary>
        public string Interests { get; set; }
    }
}