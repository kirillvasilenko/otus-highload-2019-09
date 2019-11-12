using AutoMapper;
using SocialNetwork.Model;

namespace SocialNetwork.Dtos
{
    public class RegisterUserData
    {
        public static void ConfigureMapper(IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<RegisterUserData, User>()
                .ForMember(x => x.Id, opts => opts.Ignore());
        }
        
        public User ToDomain(IMapper mapper)
        {
            return mapper.Map<User>(this);
        }
        
        
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Repeated password
        /// </summary>
        public string RepeatedPassword { get; set; }
        
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