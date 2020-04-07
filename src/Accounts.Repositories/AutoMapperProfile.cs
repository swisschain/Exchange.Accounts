﻿using Accounts.Common.Domain.Entities;
using Accounts.Repositories.Entities;
using AutoMapper;

namespace Accounts.Repositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountEntity>(MemberList.Source);
            CreateMap<AccountEntity, Account>(MemberList.Destination);
        }
    }
}