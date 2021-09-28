using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Commentor.GivEtPraj.Application.Common.Interfaces;
using Commentor.GivEtPraj.Application.Contracts;
using Commentor.GivEtPraj.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Commentor.GivEtPraj.Application.Cases.Commands
{
    public record CreateCaseCommand(string Title, string Description) : IRequest<CaseSummaryDto>;

    public class CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, CaseSummaryDto>
    {
        private readonly IAppDbContext _db;
        private readonly IMapper _mapper;

        public CreateCaseCommandHandler(IAppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CaseSummaryDto> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
        {
            var newCase = new Case
            {
                Title = request.Title,
                Description = request.Description
            };
            
            _db.Cases.Add(newCase);
            await _db.SaveChangesAsync(cancellationToken);

            var summaryDto = _mapper.Map<Case, CaseSummaryDto>(newCase);
            return summaryDto;
        }
    }

    public class CreateCaseCommandValidator : AbstractValidator<CreateCaseCommand>
    {
        public CreateCaseCommandValidator()
        {
            RuleFor(x => x.Title)
                .MinimumLength(4)
                .MaximumLength(64);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(4096);
        }
    }
}