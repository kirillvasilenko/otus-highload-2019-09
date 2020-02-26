namespace SocialNetwork.Model
{
    public class GetUsersQuery
    {
        /// <summary>
        /// User's name
        /// </summary>
        public string Name { get; set; }
        
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
        
    }
}