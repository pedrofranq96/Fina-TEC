using Fina.Core.Handlers;
using Fina.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.Web.Pages.Identity;

public partial class LogoutPage : ComponentBase
{
    #region Dependencies
       
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IAccountHandler Handler { get; set; } = null!;
    
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public ICookieAuthStateProvider  AuthenticationStateProvider { get; set; } = null!;
       
    #endregion
    
    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        if (await AuthenticationStateProvider.CheckAuthenticatedAsync())
        {
            await Handler.LogoutAsync();
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            AuthenticationStateProvider.NotifyAuthenticationChanged();
        }
        await base.OnInitializedAsync();
    }
      
    #endregion
}