using Accounts.WebApi.Models.Account;
using FluentValidation;
using JetBrains.Annotations;

namespace Accounts.WebApi.Validators
{
    [UsedImplicitly]
    public class AccountRequestManyValidator : AbstractValidator<AccountRequestMany>
    {
        public AccountRequestManyValidator()
        {
            RuleFor(o => o.Limit)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Limit must be greater or equal then 0.")
                .LessThanOrEqualTo(1000)
                .WithMessage("Limit must be less or equal to 1000.");
        }
    }
}
