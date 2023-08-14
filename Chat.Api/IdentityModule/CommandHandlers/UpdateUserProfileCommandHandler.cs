using System.Composition;
using Chat.Api.CoreModule.CQRS;
using Chat.Api.CoreModule.Services;
using Chat.Api.IdentityModule.Commands;
using Chat.Api.IdentityModule.Interfaces;

namespace Chat.Api.IdentityModule.CommandHandlers
{
    [Export(typeof(ICommandHandler))]
    [Export("UpdateUserProfileCommandHandler", typeof(ICommandHandler))]
    [Shared]
    public class UpdateUserProfileCommandHandler : ACommandHandler<UpdateUserProfileCommand>
    {
        private readonly IUserRepository _userRepository;
        
        public UpdateUserProfileCommandHandler()
        {
            _userRepository = DIService.Instance.GetService<IUserRepository>();
        }
        public override async Task<CommandResponse> OnHandleAsync(UpdateUserProfileCommand command)
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