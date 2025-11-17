using Mobilprog.View;

namespace Mobilprog
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("clone", typeof(ClonePage));
            Routing.RegisterRoute("squad", typeof(SquadsPage));
            Routing.RegisterRoute("battle", typeof(BattlePage));
            Routing.RegisterRoute("battleadd", typeof(BattleAddPage));
        }
    }
}
