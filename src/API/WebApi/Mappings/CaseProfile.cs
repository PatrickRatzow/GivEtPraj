using AutoMapper;
using Commentor.GivEtPraj.Application.Cases.Commands;
using Commentor.GivEtPraj.WebApi.Contracts.Requests;

namespace Commentor.GivEtPraj.WebApi.Mappings
{
    public class CaseProfile : Profile
    {
        public CaseProfile()
        {
            CreateMap<CreateCaseRequest, CreateCaseCommand>();
        }
    }
}