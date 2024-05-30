using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using MathNet.Numerics;
using ReactiveUI;
using System;
using System.Reactive;




namespace Danfoss.ViewModels;

public class InvoiceViewModel(NavigationService navigationService) : ReactiveObject
{
    public ReactiveCommand<Unit, Unit>? LogOutCommand { get; } = ReactiveCommand.Create(() => navigationService.SwitchToView(NavigationService.ViewType.Login));

}

