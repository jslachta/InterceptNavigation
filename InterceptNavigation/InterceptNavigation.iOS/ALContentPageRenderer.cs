using AdditelLink.Controls;
using AdditelLink.iOS.Renderers;
using Foundation;
using System;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ALContentPage), typeof(ALContentPageRenderer))]
namespace AdditelLink.iOS.Renderers
{
    public class ALContentPageRenderer : PageRenderer, IUIGestureRecognizerDelegate
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (NavigationController == null)
                return;

            var root = NavigationController.TopViewController;

            root.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(
                    UIImage.FromBundle("arrow_back.png"),
                    UIBarButtonItemStyle.Plain,
                    OnLeftBarButtonPressed), true);

            NavigationController.InteractivePopGestureRecognizer.Delegate = this;
        }

        private void OnLeftBarButtonPressed(object sender, EventArgs e)
        {
            if (Element is ALContentPage aPage &&
                aPage.IsNavigationOverride)
                aPage.InvokeBackCommand();

            else
                NavigationController?.PopViewController(true);
        }

        [Export("gestureRecognizerShouldBegin:")]
        public bool ShouldBegin(UIGestureRecognizer recognizer)
        {
            if (recognizer == NavigationController.InteractivePopGestureRecognizer)
                return NavigationController.ViewControllers.Count() > 1;

            return true;
        }
    }
}