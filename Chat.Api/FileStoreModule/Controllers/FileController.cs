using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Services;
using Chat.Api.FileStoreModule.Commands;
using Chat.Api.FileStoreModule.Interfaces;
using Chat.Api.FileStoreModule.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Chat.Api.ChatModule.Hubs;
using Chat.Api.CoreModule.Models;
using Chat.Api.FileStoreModule.Models;

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
        private readonly IHubContext<ChatHub> _hubContext;
        public FileController(IHubContext<ChatHub> hubContext)
        {
            _commandService = DIService.Instance.GetService<ICommandService>();
            _queryService = DIService.Instance.GetService<IQueryService>();
            _fileRepository = DIService.Instance.GetService<IFileRepository>();
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadAsync(IFormFile formFile)
        {
            var context = new RequestContext();
            context.HubContext = _hubContext;
            context.HttpContext = HttpContext;
            var fileUploadCommand = new UploadFileCommand()
            {
                FormFile = formFile
            };
            return Ok(await _commandService.HandleCommandAsync(fileUploadCommand, context));
        }
        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> DownloadAsync([FromQuery] string fileId)
        {
            var query = new FileDownloadQuery
            {
                FileId = fileId
            };
            var response = await _queryService.HandleQueryAsync(query);
            var fileDownloadResult = (FileDownloadResult)response.Items[0];
            return File(fileDownloadResult.FileBytes, fileDownloadResult.ContentType);
        }

        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> GetFileModelAsync(FileModelQuery query)
        {
            return Ok(await _queryService.HandleQueryAsync(query));
        }
    }
}