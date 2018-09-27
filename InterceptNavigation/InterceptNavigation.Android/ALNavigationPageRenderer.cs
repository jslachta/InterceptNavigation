using AdditelLink.Controls;
using AdditelLink.Droid.Renderers;
using Android.Content;
using Android.Widget;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using AWV7 = Android.Support.V7.Widget;

[assembly: ExportRenderer(typeof(ALNavigationPage), typeof(ALNavigationPageRenderer))]
namespace AdditelLink.Droid.Renderers
{
    public class ALNavigationPageRenderer : NavigationPageRenderer
    {
        public ALNavigationPageRenderer(Context context)
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
            if (view is ALContentPage aPage &&
                aPage.IsNavigationOverride)
            {
                _toolBar.NavigationClick -= OnNavigationClick;
                _toolBar.NavigationClick += OnNavigationClick;
            }

            return base.OnPushAsync(view, animated);
        }

        private async void OnNavigationClick(object sender, AWV7.Toolbar.NavigationClickEventArgs e)
        {
            if (Element?.CurrentPage is ALContentPage aPage &&
                aPage.IsNavigationOverride)
            {
                aPage.InvokeBackCommand();
            }
            else if (Element?.Navigation.NavigationStack.Count > 1)
            {
                await Element.Navigation.PopAsync();
            }
            else if (Element?.Parent is MasterDetailPage MDPage)
            {
                MDPage.IsPresented = !MDPage.IsPresented;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Unexcepted Click");
            }
        }
    }
}