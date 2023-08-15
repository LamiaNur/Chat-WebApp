using Chat.Framework.Database.Interfaces;

namespace Chat.Api.IdentityModule.Models
{
    public class UserModel : UserProfile, IRepositoryItem
    {
        public string Password {get; set;} = string.Empty;
        public UserProfile ToUserProfile()
        {
            var userProfile = new UserProfile()
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                UserName = this.UserName,
                BirthDay = this.BirthDay,
                About = this.About,
                ProfilePictureId = this.ProfilePictureId,
                Email = this.Email,
                PublicKey = this.PublicKey
            };
            return userProfile;
        }
    }
}