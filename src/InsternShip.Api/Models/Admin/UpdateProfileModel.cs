namespace InsternShip.Api.Models.Admin
{
    public class UpdateProfileModel
    {
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string? ImageURL { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string PhoneNumber { get; set; }
    }
}