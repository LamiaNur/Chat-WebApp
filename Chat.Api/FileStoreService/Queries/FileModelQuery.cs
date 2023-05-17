using Chat.Api.Core.Models;
using Chat.Api.FileStoreService.Interfaces;
using Chat.Api.Core.Helpers;
using Chat.Api.FileStoreService.Models;
using Chat.Api.Core.Services;

namespace Chat.Api.FileStoreService.Queries
{
    public class FileModelQuery : AQuery
    {
        public string FileId {get; set;} = string.Empty;
        public override void ValidateQuery()
        {
            if (string.IsNullOrEmpty(FileId)) 
            {
                throw new Exception("FileId is empty!!");
            }
        }
    }
}