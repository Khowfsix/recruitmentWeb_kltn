using System;
using System.Collections.Generic;

namespace InsternShip.Data.ViewModels.Question;

public class QuestionUpdateModel
{
    //public Guid QuestionId { get; set; }
    // Update model cũng giống add model, truyền id trên param là được
    public string QuestionString { get; set; } = null!;

    public Guid CategoryQuestionId { get; set; }
}
