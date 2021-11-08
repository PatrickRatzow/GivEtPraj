using System.IO;
using System.Linq;
using Commentor.GivEtPraj.Application.Cases.Commands;

namespace Commentor.GivEtPraj.WebApi.Mappings;

public class CaseProfile : Profile
{
    public CaseProfile()
    {
        CreateMap<CreateCaseRequest, CreateCaseCommand>()
            .ForMember(
                m => m.Images,
                opts => opts.MapFrom(
                    d => d.Images.Select(i => new MemoryStream(Convert.FromBase64String(i)))
                )
            );

        CreateMap<CreateQueueKeyRequest, CreateQueueKeyCommand>();
    }
}