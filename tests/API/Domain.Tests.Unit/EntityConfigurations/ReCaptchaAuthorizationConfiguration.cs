using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.Tests.Unit.ConfigurationBuilderExtensions;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.EntityConfigurations;

public class ReCaptchaAuthorizationConfiguration : AbstractEntityConfiguration<ReCaptchaAuthorization>
{
    public override void Configure()
    {
        Property(x => x.ExpiresAt)
            .ShouldBeInTheFuture();
    }
}

