namespace SocialNetwork.Model
{
    public class GetUsersQuery
    {
        /// <summary>
        /// Name
        /// </summary>
        public string GivenName { get; set; }
        
        /// <summary>
        /// Last name
        /// </summary>
        public string FamilyName { get; set; }
        
        /// <summary>
        /// Max age
        /// </summary>
        public byte? MaxAge { get; set; }
        
        /// <summary>
        /// Min age
        /// </summary>
        public byte? MinAge { get; set; }
        
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