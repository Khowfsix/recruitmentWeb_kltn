namespace InsternShip.Data.ViewModels.QuestionSkill;

public class QuestionSkillUpdateModel
{
    // Update model cũng giống add model, truyền id trên param là được
    //public Guid QuestionSkillsId { get; set; }
    public Guid QuestionId { get; set; }

    public Guid SkillId { get; set; }
}
