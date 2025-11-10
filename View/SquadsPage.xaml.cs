using Mobilprog.Models;
using Mobilprog.ViewModel;

namespace Mobilprog.View;

public partial class SquadsPage : ContentPage
{
    private readonly SquadsViewModel _viewModel;

    

    public SquadsPage(SquadsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.LoadSquadDetailsAsync(_viewModel.Squad);
    }
}