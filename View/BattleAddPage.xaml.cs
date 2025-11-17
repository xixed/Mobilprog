using Mobilprog.ViewModel;
using System.Net.NetworkInformation;

namespace Mobilprog.View;

public partial class BattleAddPage : ContentPage
{

	public BattleAddPage(BattleAddViewModel battleAddPage)
	{
		InitializeComponent();
		BindingContext=battleAddPage;
	}

    
}