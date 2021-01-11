using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Service.AzureFileService
{
    public interface IFileService
    {
        Task<Uri> UploadFileAsync(string profileId, string blobContainerName, Stream content, string contentType, string fileName);
        Task<bool> DeleteFileAsync(string profileId, string blobContainerName, string fileName);
    }
}
