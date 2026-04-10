using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    private ClaimsPrincipal _currentUser;

    public CustomAuthStateProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _currentUser = _anonymous;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(_currentUser));
    }

    public void MarkUserAsAuthenticated(string email, string role, string userId)
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.NameIdentifier, userId)
        }, "apiauth");

        _currentUser = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }

    public void MarkUserAsLoggedOut()
    {
        _currentUser = _anonymous;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }
}
