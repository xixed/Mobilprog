

using Mobilprog.ViewModel;

namespace Mobilprog.View;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
		BindingContext = mainPageViewModel;

    }
	
}
