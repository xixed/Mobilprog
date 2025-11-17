using Mobilprog.ViewModel;

namespace Mobilprog.View;

public partial class BattlePage : ContentPage
{
	public BattlePage(BattleViewModel battleViewModel)
	{
		InitializeComponent();
		BindingContext = battleViewModel;
    }

	override protected void OnAppearing()
	{
		base.OnAppearing();
		var vm = BindingContext as BattleViewModel;
		_ = vm.LoadBattlesAsync();
    }
}