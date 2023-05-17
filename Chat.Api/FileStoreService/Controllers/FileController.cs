using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Services;
using Chat.Api.FileStoreService.Commands;
using Chat.Api.FileStoreService.Interfaces;
using Chat.Api.FileStoreService.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly ICommandService _commandService;
        private readonly IQueryService _queryService;
        private readonly IFileRepository _fileRepository;
        public FileController()
        {
            _commandService = DIService.Instance.GetService<ICommandService>();
            _queryService = DIService.Instance.GetService<IQueryService>();
            _fileRepository = DIService.Instance.GetService<IFileRepository>();
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadAsync(IFormFile formFile)
        {
            var fileUploadCommand = new UploadFileCommand()
            {
                FormFile = formFile
            };
            return Ok(await _commandService.ExecuteCommandAsync(fileUploadCommand));
        }

        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> GetFileModelAsync(FileModelQuery query)
        {
            return Ok(await _queryService.HandleQueryAsync(query));
        }
    }
}