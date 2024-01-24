using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Service.Interfaces
{
    public interface IUploadFileService
    {
        Task<ImageUploadResult> AddFileAsync(IFormFile file);
        Task<IEnumerable<ImageUploadResult>> AddListFileAsync(List<IFormFile> files);
        Task<DeletionResult> DeleteFileAsync(string path);
        Task<IEnumerable<DeletionResult>> DeleteListFileAsync(List<string> listId);
        Task<bool> DeleteFileAsyncBool(string path);
    }
}
