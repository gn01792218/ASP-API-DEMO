using FluentValidation;

namespace ASPAPI.Validators
{
    public class AddWalkDifficultyRequestValidator:AbstractValidator<Models.DTOs.AddWalkDifficultyRequest>
    {
        public AddWalkDifficultyRequestValidator()
        {
            //RuleFor會提供各種驗證方法
            RuleFor(item => item.Code).NotEmpty(); //不可為空
        }
    }
}
