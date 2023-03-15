using FluentValidation;

namespace ASPAPI.Validators
{
    public class UpdateWalkequestValidator : AbstractValidator<Models.DTOs.UpdateWalkRequest>
    {
        public UpdateWalkequestValidator()
        {
            //RuleFor會提供各種驗證方法
            RuleFor(item => item.Name).NotEmpty(); //不可為空
            RuleFor(item => item.Length).GreaterThan(0); //必須大於0
        }
    }
}
