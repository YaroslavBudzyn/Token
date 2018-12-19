using Microsoft.AspNetCore.Http;

namespace tokentest.Common.ViewModels.UserManagement
{
    public class FileInputModel
    {
        public IFormFile FileToUpload { get; set; }
    }
}
