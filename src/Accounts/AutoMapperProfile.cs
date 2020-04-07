using Accounts.Common.Domain.Entities;
using Accounts.WebApi.Models.Account;
using AutoMapper;

namespace Accounts
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountModel>(MemberList.Destination);
            CreateMap<AccountModel, Account>(MemberList.Source);

            CreateMap<Account, AccountEditModel>(MemberList.Destination);
            CreateMap<AccountEditModel, Account>(MemberList.Source);
        }
    }
}
