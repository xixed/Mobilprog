

using Mobilprog.ViewModel;

namespace Mobilprog.View;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
		BindingContext = mainPageViewModel;

    }
	

	override protected void OnAppearing()
		{
		base.OnAppearing();
		var vm = BindingContext as MainPageViewModel;
		_ = vm.LoadSquadsAsync();
    }
}
