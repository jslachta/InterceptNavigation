using Xamarin.Forms;

namespace InterceptNavigation.Controls
{
    public class ANavigationPage : NavigationPage
    {
        public ANavigationPage() : base()
        {
            UpdateNavigationBar();
        }

        public ANavigationPage(Page root) : base(root)
        {
            UpdateNavigationBar();
        }

        private void UpdateNavigationBar()
        {
            BarBackgroundColor = Color.HotPink;
            BarTextColor = Color.White;
        }
    }
}
