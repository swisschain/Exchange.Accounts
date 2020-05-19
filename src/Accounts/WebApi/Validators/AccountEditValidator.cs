using Accounts.WebApi.Models.Account;
using FluentValidation;
using JetBrains.Annotations;

namespace Accounts.WebApi.Validators
{
    [UsedImplicitly]
    public class AccountEditValidator : AbstractValidator<AccountEditModel>
    {
        public AccountEditValidator()
        {
            RuleFor(o => o.Id)
                .GreaterThan(0)
                .WithMessage("Account identifier is required.");

            RuleFor(o => o.Name)
                .NotEmpty()
                .WithMessage("Account name is required.")
                .MaximumLength(36)
                .WithMessage("Account name must be less than 36 characters.");
        }
    }
}
