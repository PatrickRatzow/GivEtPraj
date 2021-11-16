using System.IO;
using System.Linq;
using Commentor.GivEtPraj.Application.Cases.Commands;

namespace Commentor.GivEtPraj.WebApi.Mappings;

public class CaseProfile : Profile
{
    public CaseProfile()
    {
        CreateMap<CreateCaseRequest, CreateCaseCommand>();
        CreateMap<CreateQueueKeyRequest, CreateQueueKeyCommand>();
    }
}