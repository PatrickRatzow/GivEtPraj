using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace Commentor.GivEtPraj.WebApi.Filters;
public class CaptchaVerificationFilter : Attribute, IAsyncAuthorizationFilter
{

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var reCaptchaResponse = context.HttpContext.Request.Headers["X-ReCaptchaResponse"];


        HttpClient Http = new HttpClient();


        HttpResponseMessage httpResp = await Http.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6LcvD64cAAAAAAqiYQuOfEcWQmrHUxct_B0vldqn&response=" + reCaptchaResponse, null);

        ReCaptchaResponse resp = JsonConvert.DeserializeObject<ReCaptchaResponse>(await httpResp.Content.ReadAsStringAsync());

        if (resp.Success == false)
            context.Result = new UnauthorizedObjectResult($"No a valid ReCaptcha response");

        Debug.WriteLine("Filter test");
    }

    public class ReCaptchaResponse
    {
        [JsonProperty("success")] public bool Success { get; set; }
        [JsonProperty("challenge_ts")] public DateTime ChallengeTs { get; set; }
        [JsonProperty("hostName")] public string HostName { get; set; }

    }
}
