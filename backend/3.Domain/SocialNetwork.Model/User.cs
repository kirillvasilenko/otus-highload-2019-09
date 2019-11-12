using System;

namespace SocialNetwork.Model
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// Age
        /// </summary>
        public byte Age { get; set; }
        
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// Interests
        /// </summary>
        public string Interests { get; set; }
        
    }
}