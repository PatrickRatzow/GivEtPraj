﻿using AutoMapper;
using Commentor.GivEtPraj.Application.Contracts;
using Commentor.GivEtPraj.Domain.Entities;

namespace Commentor.GivEtPraj.Application.Common.Mappings
{
    public class CaseProfile : Profile
    {
        public CaseProfile()
        {
            CreateMap<Case, CaseSummaryDto>()
                .ForMember(c => c.AmountOfPictures, 
                    opts => opts.MapFrom(m => m.Pictures.Count));
        }
    }
}