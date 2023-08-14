using Chat.Api.CoreModule.CQRS;

namespace Chat.Api.FileStoreModule.Queries
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