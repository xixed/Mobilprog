namespace Mobilprog.View;

public partial class BattleAddPage : ContentPage
{

	public BattleAddPage(BattleAddPage battleAddPage)
	{
		InitializeComponent();
		BindingContext=battleAddPage;
	}
}