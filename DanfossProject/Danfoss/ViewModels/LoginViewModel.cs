using ReactiveUI;
using System.Reactive;


namespace Danfoss.ViewModels;

public class LoginViewModel(NavigationService? navigationService) : ReactiveObject
{
    public ReactiveCommand<Unit, Unit>? LoginCommand { get; } = ReactiveCommand.Create(() => navigationService?.SwitchToView(NavigationService.ViewType.Login));
}