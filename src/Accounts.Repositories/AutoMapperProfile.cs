using Accounts.Domain.Entities;
using Accounts.Repositories.Entities;
using AutoMapper;

namespace Accounts.Repositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountEntity>(MemberList.Destination)
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore());

            CreateMap<AccountEntity, Account>(MemberList.Destination);
        }
    }
}
