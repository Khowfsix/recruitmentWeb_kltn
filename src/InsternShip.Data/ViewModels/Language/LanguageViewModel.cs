using System.ComponentModel.DataAnnotations;

namespace InsternShip.Data.ViewModels.Language
{
    public class LanguageViewModel
    {
        [Key]
        public Guid LanguageId { get; set; }

        public string LanguageName { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;
    }
}