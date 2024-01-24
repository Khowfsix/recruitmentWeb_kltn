using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.ViewModels.UploadFileFromForm
{
    public class UploadFileFromForm
    {
        public IFormFile? File { get; set; }
    }
}
