using Chat.Api.IdentityModule.Commands;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;

namespace Chat.Api.IdentityModule.CommandHandlers
{
    [ServiceRegister(typeof(IRequestHandler), ServiceLifetime.Singleton)]
    public class UpdateUserProfileCommandHandler : ACommandHandler<UpdateUserProfileCommand>
    {
        private readonly IUserRepository _userRepository;
        
        public UpdateUserProfileCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        protected override async Task<CommandResponse> OnHandleAsync(UpdateUserProfileCommand command)
        {
            var response = command.CreateResponse();
            var requestUpdateModel = command.UserModel;
            var userModel = await _userRepository.GetUserByEmailAsync(requestUpdateModel.Email);
            if (userModel == null)
            {
                throw new Exception("UserModel not found");
            }
            var updateInfoCount = 0;
            if (!string.IsNullOrEmpty(requestUpdateModel.FirstName))
            {
                userModel.FirstName = requestUpdateModel.FirstName;
                updateInfoCount++;
            }
            if (!string.IsNullOrEmpty(requestUpdateModel.LastName))
            {
                userModel.LastName = requestUpdateModel.LastName;
                updateInfoCount++;
            }
            if (requestUpdateModel.BirthDay != null)
            {
                userModel.BirthDay = requestUpdateModel.BirthDay;
                updateInfoCount++;
            }
            if (!string.IsNullOrEmpty(requestUpdateModel.About))
            {
                userModel.About = requestUpdateModel.About;
                updateInfoCount++;
            }
            if (!string.IsNullOrEmpty(requestUpdateModel.ProfilePictureId))
            {
                userModel.ProfilePictureId = requestUpdateModel.ProfilePictureId;
                updateInfoCount++;
            }
            if (updateInfoCount > 0)
            {
                await _userRepository.UpdateUserAsync(userModel);
            }
            response.Message = "User Updated Successfully!!";
            response.SetData("UserProfile", userModel.ToUserProfile());
            return response;
        }
    }
}