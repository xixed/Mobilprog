using Mobilprog.View;
using Mobilprog.ViewModel;

namespace Mobilprog
{
    public partial class App : Application
    {

        
        public App(ClonePage clonePage)
        {
            InitializeComponent();

            

            MainPage = new NavigationPage(clonePage);
            
        }

        
    }
}