namespace SocialNetwork.App.Dtos
{
    public class UpdateUserData
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Name
        /// </summary>
        public string GivenName { get; set; }
        
        /// <summary>
        /// Last name
        /// </summary>
        public string FamilyName { get; set; }
        
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