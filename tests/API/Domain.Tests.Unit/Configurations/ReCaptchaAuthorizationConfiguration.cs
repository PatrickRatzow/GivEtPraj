using Commentor.GivEtPraj.Domain.Entities;
using DomainFixture;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Configurations;

public class ReCaptchaAuthorizationConfiguration : AbstractClassConfiguration<ReCaptchaAuthorization>
{
    public override void Configure()
    {
        Property(x => x.ExpiresAt)
            .ShouldBeInTheFuture();
    }
}

