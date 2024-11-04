using Microsoft.AspNetCore.Components.Authorization;

namespace Fina.Web.Security;

public interface ICookieAuthStateProvider
{
    Task<bool> CheckAuthenticatedAsync();
    Task<AuthenticationState> GetAuthenticationStateAsync();
    void NotifyAuthenticationChanged();
}