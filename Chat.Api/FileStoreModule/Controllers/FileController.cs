using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Services;
using Chat.Api.FileStoreModule.Commands;
using Chat.Api.FileStoreModule.Interfaces;
using Chat.Api.FileStoreModule.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.FileStoreModule.Controllers
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
            return Ok(await _commandService.HandleCommandAsync(fileUploadCommand));
        }

        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> GetFileModelAsync(FileModelQuery query)
        {
            return Ok(await _queryService.HandleQueryAsync(query));
        }
    }
}