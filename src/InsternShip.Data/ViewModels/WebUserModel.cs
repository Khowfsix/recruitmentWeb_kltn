using InsternShip.Data.Entities;

namespace InsternShip.Data.ViewModels
{
    public class WebUserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImageURL { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}