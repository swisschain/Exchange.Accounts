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

            CreateMap<AccountEdit, Domain.Entities.Account>(MemberList.Source);

            CreateMap<AccountAdd, Domain.Entities.Account>(MemberList.Source);
        }
    }
}
