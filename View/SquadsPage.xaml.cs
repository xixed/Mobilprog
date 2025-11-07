using Mobilprog.ViewModel;

namespace Mobilprog.View;

public partial class SquadsPage : ContentPage
{
    private readonly SquadsViewModel _viewModel;

    public int SquadId { get; set; }

    public SquadsPage(SquadsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadSquadDetailsAsync(SquadId);
    }
}