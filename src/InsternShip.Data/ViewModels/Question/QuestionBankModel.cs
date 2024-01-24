using InsternShip.Data.ViewModels.CategoryQuestion;

namespace InsternShip.Data.ViewModels.Question;

public class CategoryQuestionBankModel
{
    public ICollection<CategoryQuestionViewModel> CategoryQuestions { get; set; } = new List<CategoryQuestionViewModel>();
}