using Mobilprog.Models;
using Mobilprog.ViewModel;

namespace Mobilprog.View;



public partial class ClonePage : ContentPage
{
    public ClonePage(CloneViewModel cloneViewModel)
    {
        InitializeComponent();
        BindingContext = cloneViewModel;
    }

    //protected override void OnNavigatedTo(NavigatedToEventArgs args)
    //{
    //    base.OnNavigatedTo(args);

    //}

    
}