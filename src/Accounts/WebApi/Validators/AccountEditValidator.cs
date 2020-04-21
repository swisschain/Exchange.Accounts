using Accounts.WebApi.Models.Account;
using FluentValidation;
using JetBrains.Annotations;

namespace Accounts.WebApi.Validators
{
    [UsedImplicitly]
    public class AccountEditValidator : AbstractValidator<AccountEdit>
    {
        public AccountEditValidator()
        {
            RuleFor(o => o.Id)
                .NotEmpty()
                .WithMessage("Account identifier is required.")
                .MaximumLength(36)
                .WithMessage("Account identifier must be less than 36 characters.");

            RuleFor(o => o.Name)
                .NotEmpty()
                .WithMessage("Account name is required.")
                .MaximumLength(36)
                .WithMessage("Account name must be less than 36 characters.");
        }
    }
}
