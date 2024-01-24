using InsternShip.Data.ViewModels.CategoryQuestion;
using System;
using System.Collections.Generic;

namespace InsternShip.Data.ViewModels.Question;

public class QuestionViewModel
{
    public Guid QuestionId { get; set; }
    public string QuestionString { get; set; } = null!;
    public Guid CategoryQuestionId { get; set; }
    public CategoryQuestionViewModel CategoryQuestion { get; set; } = null!;
}
