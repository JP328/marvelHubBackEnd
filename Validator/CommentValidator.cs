using FluentValidation;
using marvelHub.Model;

namespace marvelHub.Validator
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator() {
            RuleFor(p => p.Description)
                    .NotEmpty()
                    .MinimumLength(1)
                    .MaximumLength(255);
        }
    }
}
