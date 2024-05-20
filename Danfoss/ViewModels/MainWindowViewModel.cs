using ReactiveUI;

namespace Danfoss.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    private ReactiveObject? currentViewModel;
    private readonly NavigationService? navigationService;

    public ReactiveObject? CurrentViewModel
    {
        get => currentViewModel;
        set => this.RaiseAndSetIfChanged(ref currentViewModel, value);
    }

    public MainWindowViewModel()
    {
        navigationService ??= new(this);
        SetDefaultViewModel(new MainViewModel(navigationService));
    } 

    public void SetDefaultViewModel(ReactiveObject? defaultViewModel = null)
    {
        CurrentViewModel = defaultViewModel ?? new MainViewModel(navigationService);
    }
}
