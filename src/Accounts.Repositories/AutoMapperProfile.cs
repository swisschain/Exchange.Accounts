using System;
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
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc)))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Modified, DateTimeKind.Utc)));

            CreateMap<AccountEntity, Account>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.UtcDateTime))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => src.Modified.UtcDateTime));
        }
    }
}
