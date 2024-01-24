using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.Interfaces
{
    public interface IUploadFileRepository
    {
        Task<ImageUploadResult> AddFileAsync(IFormFile file);
        Task<IEnumerable<ImageUploadResult>> AddListFileAsync(List<IFormFile> files);
        Task<DeletionResult> DeleteFileAsync(string path);
        Task<bool> DeleteFileAsyncBool(string path);
        Task<IEnumerable<DeletionResult>> DeleteListFileAsync(List<string> listId);
        //Task<FileContentResult> DownloadFileAsync(string path);
    }
}
