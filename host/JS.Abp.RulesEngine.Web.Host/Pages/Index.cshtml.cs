using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace JS.Abp.RulesEngine.Pages;

public class IndexModel : RulesEnginePageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
