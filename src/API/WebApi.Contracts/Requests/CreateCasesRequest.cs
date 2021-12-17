using Commentor.GivEtPraj.Application.Contracts;

namespace Commentor.GivEtPraj.WebApi.Contracts.Requests;

public record CreateCasesRequest(List<CaseCreationDto> Cases);
