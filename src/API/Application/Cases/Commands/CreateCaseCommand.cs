using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public record CreateCaseCommand(string Title, string Description, IList<string> Images) : IRequest<CaseSummaryDto>;

    public class CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, CaseSummaryDto>
    {
        private readonly IAppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;

        public CreateCaseCommandHandler(IAppDbContext db, IMapper mapper, IFileStorage fileStorage)
        {
            _db = db;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        public async Task<CaseSummaryDto> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
        {
            var images = request.Images.Select(i => new CasePicture
            {
                Id = Guid.NewGuid()
            }).ToList();

            if (images.Count > 0)
                await UploadImages(request, images);
            
            var newCase = new Case
            {
                Title = request.Title,
                Description = request.Description,
                Pictures = images
            };
            
            _db.Cases.Add(newCase);
            await _db.SaveChangesAsync(cancellationToken);

            var summaryDto = _mapper.Map<Case, CaseSummaryDto>(newCase);
            return summaryDto;
        }

        private async Task UploadImages(CreateCaseCommand request, IEnumerable<CasePicture> images)
        {
            await Task.WhenAll(images.Select((cp, index) =>
            { 
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                
                writer.Write(request.Images[index]);
                writer.Flush();

                stream.Position = 0;

                return _fileStorage.UploadFile($"cases/{cp.Id}-{index}.jpg", stream);
            }));
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