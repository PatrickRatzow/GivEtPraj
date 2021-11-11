using Commentor.GivEtPraj.Application.Contracts;

namespace Commentor.GivEtPraj.WebApi.Contracts.Requests;

public class CreateCaseRequest
{
    public CreateCaseRequest()
    {
    }

    public CreateCaseRequest(Guid deviceId, List<CaseCreationDto> cases)
    {
        DeviceId = deviceId;
        Cases = cases;

    }
    public Guid DeviceId { get; set; }
    public List<CaseCreationDto> Cases;
}