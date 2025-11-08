using Mobilprog.ViewModel;

namespace Mobilprog.View;

public partial class BattlePage : ContentPage
{
	public BattlePage(BattleViewModel battleViewModel)
	{
		InitializeComponent();
		BindingContext = battleViewModel;
    }
}