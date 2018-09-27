using Foundation;
using InterceptNavigation.Controls;
using InterceptNavigation.iOS.Renderers;
using System;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AContentPage), typeof(AContentPageRenderer))]
namespace InterceptNavigation.iOS.Renderers
{
    public class AContentPageRenderer : PageRenderer, IUIGestureRecognizerDelegate
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            // not in a navigation page
            if (NavigationController == null)
                return;

            var root = NavigationController.TopViewController;

            // replace system back button with a custom button use leftbarbuttonitem,
            // so we can intercept the navigation click event,
            // the system back button is hided in PCL.
            root.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(
                    UIImage.FromBundle("arrow_back.png"),
                    UIBarButtonItemStyle.Plain,
                    OnLeftBarButtonPressed), true);

            // replace the system button cause the navigation gesture not work,
            // so implement the popgesture delegate here
            NavigationController.InteractivePopGestureRecognizer.Delegate = this;
        }

        private void OnLeftBarButtonPressed(object sender, EventArgs e)
        {
            // the navigation click is override, do nothing and return
            if (Element is AContentPage aPage &&
                aPage.InvokeNavigationClick())
                return;
            // otherwise do the navigation here
            else
                NavigationController?.PopViewController(true);
        }

        [Export("gestureRecognizerShouldBegin:")]
        public bool ShouldBegin(UIGestureRecognizer recognizer)
        {
            // admit the pop gesture when the page is not root page
            if (recognizer == NavigationController.InteractivePopGestureRecognizer)
                return NavigationController.ViewControllers.Count() > 1;

            return true;
        }
    }
}