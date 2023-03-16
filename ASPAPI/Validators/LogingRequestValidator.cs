using FluentValidation;

namespace ASPAPI.Validators
{
    public class LogingRequestValidator:AbstractValidator<Models.DTOs.LoginRequest>
    {
        public LogingRequestValidator()
        {
            RuleFor(item => item.UserName).NotEmpty();
            RuleFor(item => item.Password).NotEmpty();
        }
    }
}
