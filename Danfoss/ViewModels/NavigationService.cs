namespace Danfoss.ViewModels;
public class NavigationService(MainWindowViewModel mainWindowViewModel)
{
    private readonly MainWindowViewModel? mainWindowViewModel = mainWindowViewModel;

    public enum ViewType {Login, Invoice, Main}

    public void SwitchToView(ViewType viewType)
    {
        switch(viewType)
        {
            case ViewType.Login:
                mainWindowViewModel?.SetDefaultViewModel(new InvoiceViewModel(this));
                break;
            case ViewType.Invoice:
                mainWindowViewModel?.SetDefaultViewModel(new InvoiceViewModel(this));
                break;
            case ViewType.Main:
                mainWindowViewModel?.SetDefaultViewModel(new MainViewModel(this));
                break;
            default:
                mainWindowViewModel?.SetDefaultViewModel(new MainViewModel(this));
                break;
        }
    }

}
