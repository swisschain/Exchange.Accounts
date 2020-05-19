using Accounts.Domain.Entities;
using Accounts.WebApi.Models.Account;
using Accounts.WebApi.Models.Wallet;
using AutoMapper;

namespace Accounts
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountModel>(MemberList.Destination);

            CreateMap<AccountEditModel, Account>(MemberList.Source);

            CreateMap<AccountAddModel, Account>(MemberList.Source);


            CreateMap<Wallet, WalletModel>(MemberList.Destination);

            CreateMap<WalletEditModel, Wallet>(MemberList.Source);

            CreateMap<WalletAddModel, Wallet>(MemberList.Source);
        }
    }
}
