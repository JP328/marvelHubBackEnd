using FluentValidation;
using marvelHub.Model;

namespace marvelHub.Validator
{
    public class ThemeValidator : AbstractValidator<Theme>
    {
        public ThemeValidator() {
            RuleFor(p => p.Type)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(255);
        }
    }
}
