using System.Collections.Generic;
using System.Net.Http;

namespace Commentor.GivEtPraj.WebApi.Contracts.Requests
{
    public record CreateCaseRequest(
        string Title, 
        string Description,
        IList<string> Images
    );
}