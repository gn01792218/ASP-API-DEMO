using FluentValidation;

namespace ASPAPI.Validators
{
    public class UpdateWalkDifficultyRequestValidator:AbstractValidator<Models.DTOs.UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyRequestValidator()
        {
            //RuleFor會提供各種驗證方法
            RuleFor(item => item.Code).NotEmpty(); //不可為空
        }
    }
}
