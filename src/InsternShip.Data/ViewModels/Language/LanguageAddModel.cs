using System.ComponentModel.DataAnnotations;

namespace InsternShip.Data.ViewModels.Language
{
    public class LanguageAddModel
    {
        //public Guid LanguageId { get; set; }
        
        [Required(ErrorMessage = "Must have Name")]
        [MaxLength(50)]
        public string LanguageName { get; set; } = null!;
    }
}