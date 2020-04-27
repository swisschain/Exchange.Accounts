using Accounts.Domain.Entities;
using Accounts.WebApi.Models.Account;
using AutoMapper;
using Account = Accounts.WebApi.Models.Account.Account;

namespace Accounts
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Domain.Entities.Account, Account>(MemberList.Destination);
            CreateMap<Account, Domain.Entities.Account>(MemberList.Source);

            CreateMap<Domain.Entities.Account, AccountEdit>(MemberList.Destination);
            CreateMap<AccountEdit, Domain.Entities.Account>(MemberList.Source);

            CreateMap<Domain.Entities.Account, AccountAdd>(MemberList.Destination);
            CreateMap<AccountAdd, Domain.Entities.Account>(MemberList.Source);
        }
    }
}
