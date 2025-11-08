using Mobilprog.View;

namespace Mobilprog
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ClonePage), typeof(ClonePage));
            Routing.RegisterRoute(nameof(SquadsPage), typeof(SquadsPage));
            Routing.RegisterRoute(nameof(BattlePage), typeof(BattlePage));
        }
    }
}
