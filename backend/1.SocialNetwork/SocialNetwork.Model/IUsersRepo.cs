using System.Collections.Generic;
using System.Threading.Tasks;

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
    
    public interface IUsersRepo
    {
        Task<User> GetUser(long id, bool throwExceptionIfNotFound = true);
        
        Task<User> GetUserByEmail(string email, bool throwExceptionIfNotFound = true);

        Task<User> AddUser(User user);

        Task DeleteUser(long userId);

        Task<int> GetUsersCount(GetUsersQuery query);
        
        Task<IEnumerable<User>> GetUsers(int skip, int take);
    }
}