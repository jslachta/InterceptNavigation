using Xamarin.Forms;

namespace AdditelLink.Controls
{
    public class ALNavigationPage : NavigationPage
    {
        public ALNavigationPage() : base()
        {
            UpdateNavigationBar();
        }

        public ALNavigationPage(Page root) : base(root)
        {
            UpdateNavigationBar();
        }

        private void UpdateNavigationBar()
        {
            BarBackgroundColor = (Color)Application.Current.Resources["ThemeColor"];
            BarTextColor = (Color)Application.Current.Resources["ThemeTextColor"];
        }
    }
}
