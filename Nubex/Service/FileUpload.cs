using Microsoft.AspNetCore.Components.Forms;
using Nubex.Service.IService;

namespace Nubex.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment env;
        public FileUpload(IWebHostEnvironment env)
        {
            this.env = env;
        }
        public bool DeleteFile(string filePath)
        {
            if (File.Exists(env.WebRootPath+filePath))
            {
                File.Delete(env.WebRootPath + filePath);
                return true;
            }

            return false;
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            FileInfo fileInfo = new(file.Name);
            var fileName = Guid.NewGuid().ToString()+fileInfo.Extension;
            var folderDirectory = $"{env.WebRootPath}\\images\\product";
            if (!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }
            var filePath = Path.Combine(folderDirectory, fileName);
            await using FileStream fs = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(fs);

            var fullPath = $"/images/product/{fileName}";
            return fullPath;
        }
    }
}
