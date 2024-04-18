using marvelHub.Model;
using FluentValidation;

namespace marvelHub.Validator
{
    public class PostValidator : AbstractValidator<Post>
    {
        public PostValidator() {
            RuleFor(p => p.Title)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            RuleFor(p => p.Text)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(1000);
        }
    }
}
