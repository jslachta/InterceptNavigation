using System;
using Xamarin.Forms;

namespace InterceptNavigation.Controls
{
    public class AContentPage : ContentPage
    {
        public event EventHandler NavigationClicked;

        public AContentPage()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                // Hide the system back button and set custom back button on iOS
                NavigationPage.SetHasBackButton(this, false);
            }
        }

        public bool InvokeNavigationClick()
        {
            if (NavigationClicked != null)
            {
                NavigationClicked(this, EventArgs.Empty);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return !InvokeNavigationClick() && base.OnBackButtonPressed();
        }
    }
}
