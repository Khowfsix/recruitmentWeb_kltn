using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
namespace InsternShip.Data.Repositories
{
    public class UploadFileRepository : IUploadFileRepository
    {
        private readonly Cloudinary _cloudinary;
        

        public UploadFileRepository(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
                (
                    config.Value.CloudName,
                    config.Value.ApiKey,
                    config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(acc);
            
        }

        public async Task<IEnumerable<ImageUploadResult>> AddListFileAsync(List<IFormFile> files)
        {
            var listUploadResult = new List<ImageUploadResult>();
            var uploadResult = new ImageUploadResult();
            foreach (var file in files)
            {
                if(file.Length > 0 )
                {
                    using var stream = file.OpenReadStream();
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                    };
                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                } 
                listUploadResult.Add(uploadResult);
            }
            return listUploadResult;
        }

        public async Task<ImageUploadResult> AddFileAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if(file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams); 
            }
            return uploadResult;
        }

        public async Task<IEnumerable<DeletionResult>> DeleteListFileAsync(List<string> listId)
        {
            var listResult = new List<DeletionResult>();
            foreach(string id in listId)
            {
                string[] parts = id.Split('/');
                string publicIdWithExtension = parts[parts.Length - 1];
                var publicId = publicIdWithExtension.Split('.')[0];
                var deleteParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deleteParams);
                listResult.Add(result);
            }  
            return listResult;
        }

        public async Task<DeletionResult> DeleteFileAsync(string path)
        {
            string[] parts = path.Split('/');
            string publicIdWithExtension = parts[parts.Length - 1];
            var publicId = publicIdWithExtension.Split('.')[0];
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }

        public async Task<bool> DeleteFileAsyncBool(string path)
        {
            string[] parts = path.Split('/');
            string publicIdWithExtension = parts[parts.Length - 1];
            var publicId = publicIdWithExtension.Split('.')[0];
            var deletionParams = new DeletionParams(publicId);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);


            if (deletionResult.Result == "ok")
            {
                // Xóa thành công
                return true;
            }
            else
            {
                // Xóa không thành công

                return false;
            }
        }
        private string ConvertFileId(string path)
        {
            string[] parts = path.Split('/');
            string publicIdWithExtension = parts[parts.Length - 1];
            var publicId = publicIdWithExtension.Split('.')[0];
            return publicId;
        }

        
    }
}
