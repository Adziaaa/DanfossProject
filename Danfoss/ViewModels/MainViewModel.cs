using System.Reactive;
using ReactiveUI;
using OxyPlot;
using OxyPlot.Series;



namespace Danfoss.ViewModels;


public class MainViewModel(NavigationService? navigationService) : ReactiveObject
{
    private readonly NavigationService? navigationService = navigationService;

    public ReactiveCommand<Unit, Unit> ShowLoginCommand { get; } = ReactiveCommand.Create(() => navigationService?.SwitchToView(NavigationService.ViewType.Login));
  
}


