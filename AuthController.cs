using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

[Route("[controller]/[action]")]
public class AuthController : Controller
{
    private readonly ITokenAcquisition _tokenAcquisition;

    public AuthController(ITokenAcquisition tokenAcquisition)
    {
        _tokenAcquisition = tokenAcquisition;
    }

    [HttpGet]
    public async Task<IActionResult> HandleRedirect([FromForm] string code)
    {
        try
        {
            var tokenResult = await _tokenAcquisition.GetAuthenticationResultForUserAsync(
                new[] { "user.read" }, // Add the necessary scopes here
                code,
                null,
                null,
                null);

            var roles = tokenResult.ClaimsPrincipal.FindAll("roles").Select(c => c.Value).ToList();

            // Check roles
            if (roles.Contains("TestExternalApp.HCP"))
            {
                // This user can view the ID token claims page.
                //TODO: add some page
                System.Console.WriteLine($"Roles check passed {roles}");

                // Iterate through the list and print each string
                foreach (var str in roles)
                {
                    Console.WriteLine(str);
                }

                return Redirect("/roles");
            } 

            // User can only view the index page.
            return Redirect("/");
            
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}