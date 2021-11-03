namespace Commentor.GivEtPraj.Application.Common.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class ReCaptchaAttribute : Attribute
{
    public float MinimumScore { get; }
    public bool AllowQueue { get; set; }
    
    public ReCaptchaAttribute(float minimumScore = 0.6f)
    {
        MinimumScore = minimumScore;
    }
}