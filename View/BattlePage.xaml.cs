using Mobilprog.ViewModel;

namespace Mobilprog.View;

public partial class BattlePage : ContentPage
{
	public BattlePage(BattleAddViewModel battleViewModel)
	{
		InitializeComponent();
		BindingContext = battleViewModel;
    }
}