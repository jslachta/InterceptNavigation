using System;
using System.Collections.Generic;
using InterceptNavigation.Controls;
using Xamarin.Forms;

namespace InterceptNavigation
{
    public partial class Page1 : AContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private async void OnNavigationClicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Intercepted", "Are you sure to go back?", "OK", "Cancel");
            if (result)
                await Navigation.PopAsync();
            else
                await DisplayAlert("Canceled", "Navigation is canceled", "OK");
        }
    }
}
