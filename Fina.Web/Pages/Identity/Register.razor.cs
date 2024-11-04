using Fina.Core.Handlers;
using Fina.Core.Requests.Account;
using Fina.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.Web.Pages.Identity;

public partial class RegisterPage: ComponentBase
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

  #region Properties
  
  public bool IsBusy { get; set; } = false;
  public RegisterRequest InputModels { get; set; } = new();
  
  #endregion

  #region Overrides
  protected override async Task OnInitializedAsync()
  {
      var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
      var user = authState.User;

      if (user.Identity is { IsAuthenticated: true })
          NavigationManager.NavigateTo("/");
  }
  
  #endregion

  #region Methods

  public async Task OnValidSubmitAsync()
  {
      IsBusy = true;

      try
      {
          var result = await Handler.RegisterAsync(InputModels);

          if (result.IsSuccess)
          {
              Snackbar.Add(result.Message, Severity.Success);
              NavigationManager.NavigateTo("/");
          }
          else
              Snackbar.Add(result.Message, Severity.Error);
      }
      catch (Exception ex)
      {
          Snackbar.Add(ex.Message, Severity.Error);
      }
      finally
      {
          IsBusy = false;
      }
  }
  
  #endregion
}