using FluentValidation;
using marvelHub.Model;

namespace marvelHub.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() {

            RuleFor(u => u.Name)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(255);

            RuleFor(u => u.Photo)
                .MaximumLength(5000);
        }
    }
}
