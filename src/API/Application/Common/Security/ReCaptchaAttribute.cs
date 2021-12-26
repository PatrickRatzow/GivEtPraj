namespace Commentor.GivEtPraj.Application.Common.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ReCaptchaAttribute : Attribute
{
    public ReCaptchaAttribute(float minimumScore = 0.0f)
    {
        MinimumScore = minimumScore;
    }

    public float MinimumScore { get; }
    public bool AllowQueue { get; set; }
}