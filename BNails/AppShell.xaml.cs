using BNails.Views;

namespace BNails
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Registro),typeof(Registro));
        }
    }
}
