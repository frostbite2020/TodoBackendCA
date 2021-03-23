using Application.Common.Mappings;

namespace Application.Common.Models.UserModels
{
    public class UserModel : IMapFrom<UserProperties>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
