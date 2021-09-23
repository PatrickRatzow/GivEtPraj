using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Common.Interfaces;
using Commentor.GivEtPraj.Application.Contracts;
using Commentor.GivEtPraj.Domain.Errors;
using FluentValidation;
using MediatR;
using OneOf;
using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Cases.Queries
{
    public record FindCaseQuery(int Id) : IRequest<OneOf<CaseSummaryDto, CaseNotFound>>;

    public class FindCaseQueryHandler : IRequestHandler<FindCaseQuery, OneOf<CaseSummaryDto, CaseNotFound>>
    {
        private readonly IAppDbContext _db;

        public FindCaseQueryHandler(IAppDbContext db)
        {
            _db = db;
        }

        public async Task<OneOf<CaseSummaryDto, CaseNotFound>> Handle(FindCaseQuery request, 
            CancellationToken cancellationToken)
        {
            var @case = await _db.Cases
                .Where(c => c.Id == request.Id)
                .Select(c => new CaseSummaryDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    AmountOfPictures = c.Pictures.Count
                })
                .FirstOrDefaultAsync(cancellationToken);
            
            if (@case is null) return new CaseNotFound(request.Id);
            
            return @case;
        }
    }

    public class FindCaseQueryValidator : AbstractValidator<FindCaseQuery>
    {
        public FindCaseQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}