using Android.Content;
using Android.Widget;
using InterceptNavigation.Controls;
using InterceptNavigation.Droid.Renderers;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using AWV7 = Android.Support.V7.Widget;

[assembly: ExportRenderer(typeof(ANavigationPage), typeof(ANavigationPageRenderer))]
namespace InterceptNavigation.Droid.Renderers
{
    public class ANavigationPageRenderer : NavigationPageRenderer
    {
        public ANavigationPageRenderer(Context context)
            : base(context)
        {
            _context = context;
        }

        private readonly Context _context;
        private AWV7.Toolbar _toolBar;

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);

            if (child is AWV7.Toolbar toolBar)
                _toolBar = toolBar;
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            // Center title
            for (int i = 0; i < _toolBar?.ChildCount; i++)
            {
                if (_toolBar.GetChildAt(i) is TextView titleView)
                {
                    var toolbarCenter = _toolBar.Width / 2;
                    var titleCenter = titleView.Width / 2;
                    titleView.SetX(toolbarCenter - titleCenter);
                }
            }
        }

        protected override Task<bool> OnPushAsync(Page view, bool animated)
        {
            // Override NavigationClick event here
            if (view is AContentPage aPage)
            {
                _toolBar.NavigationClick -= OnNavigationClick;
                _toolBar.NavigationClick += OnNavigationClick;
            }

            return base.OnPushAsync(view, animated);
        }

        private async void OnNavigationClick(object sender, AWV7.Toolbar.NavigationClickEventArgs e)
        {
            if (Element?.CurrentPage is AContentPage aPage &&
                aPage.InvokeNavigationClick())
            {
                // NavigationClick is intercepted, do nothing and return
                return;
            }
            // if NavigationClick is not override, do the navigation here
            else if (Element?.Navigation.NavigationStack.Count > 1)
            {
                await Element.Navigation.PopAsync();
            }
            // when navigation page is the MasterDetailPage's Detail
            else if (Element?.Parent is MasterDetailPage MDPage)
            {
                MDPage.IsPresented = !MDPage.IsPresented;
            }
            // otherwise i think the back button shoud not appear and be clicked
            else
            {
                System.Diagnostics.Debug.WriteLine("Unexcepted Click");
            }
        }
    }
}