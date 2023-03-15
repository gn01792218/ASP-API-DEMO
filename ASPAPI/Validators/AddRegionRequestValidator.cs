using FluentValidation;

namespace ASPAPI.Validators
{
    public class AddRegionRequestValidator:AbstractValidator<Models.DTOs.AddRegionRequest>
    {
        public AddRegionRequestValidator()
        {
            //RuleFor會提供各種驗證方法
            RuleFor(item => item.Name).NotEmpty(); //不可為空
            RuleFor(item => item.Code).NotEmpty(); //不可為空
            RuleFor(item => item.Area).GreaterThan(0); //必須大於0
            RuleFor(item => item.Population).GreaterThanOrEqualTo(0); //必須大於0
        }
    }
}
