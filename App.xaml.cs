using Mobilprog.View;
using Mobilprog.ViewModel;

namespace Mobilprog
{
    public partial class App : Application
    {

        
        public App(MainPage mainPage)
        {
            InitializeComponent();



            MainPage = mainPage;
            
        }

        
    }
}