using Accounts.WebApi.Models.Account;
using FluentValidation;
using JetBrains.Annotations;

namespace Accounts.WebApi.Validators
{
    [UsedImplicitly]
    public class AccountAddValidator : AbstractValidator<AccountAdd>
    {
        public AccountAddValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty()
                .WithMessage("Account name is required.")
                .MaximumLength(36)
                .WithMessage("Account name must be less than 36 characters.");
        }
    }
}
