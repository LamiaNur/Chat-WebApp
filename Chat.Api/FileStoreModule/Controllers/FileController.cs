using Chat.Api.FileStoreModule.Commands;
using Chat.Api.FileStoreModule.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Chat.Api.ChatModule.Hubs;
using Chat.Api.FileStoreModule.Models;
using Chat.Framework.Models;
using Chat.Framework.Proxy;

namespace Chat.Api.FileStoreModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly ICommandQueryProxy _commandQueryService;
        private readonly IHubContext _hubContext;

        public FileController(IHubContext<ChatHub> hubContext, ICommandQueryProxy commandQueryProxy)
        {
            _commandQueryService = commandQueryProxy;
            _hubContext = (IHubContext)hubContext;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadAsync(IFormFile formFile)
        {
            var context = new RequestContext();
            context.HubContext = (IHubContext)_hubContext;
            context.HttpContext = HttpContext;
            var fileUploadCommand = new UploadFileCommand()
            {
                FormFile = formFile
            };
            fileUploadCommand.SetRequestContext(context);
            return Ok(await _commandQueryService.GetCommandResponseAsync(fileUploadCommand));
        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> DownloadAsync([FromQuery] string fileId)
        {
            var query = new FileDownloadQuery
            {
                FileId = fileId
            };
            var response = await _commandQueryService.GetQueryResponseAsync(query);
            var fileDownloadResult = (FileDownloadResult)response.Items[0];
            return File(fileDownloadResult.FileBytes, fileDownloadResult.ContentType);
        }

        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> GetFileModelAsync(FileModelQuery query)
        {
            return Ok(await _commandQueryService.GetQueryResponseAsync(query));
        }
    }
}