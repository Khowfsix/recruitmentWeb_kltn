using System;
using System.Collections.Generic;

namespace InsternShip.Data.Models{
    public class LanguageModel
    {
        public Guid LanguageId { get; set; }

        public string LanguageName { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;
    }

}