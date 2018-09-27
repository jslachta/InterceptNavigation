using System.Windows.Input;
using Xamarin.Forms;

namespace AdditelLink.Controls
{
    public class ALContentPage : ContentPage
    {
        public ALContentPage()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                NavigationPage.SetHasBackButton(this, false);
            }
        }

        public bool IsNavigationOverride => BackCommand != null;

        public ICommand BackCommand
        {
            get { return (ICommand)GetValue(BackCommandProperty); }
            set { SetValue(BackCommandProperty, value); }
        }

        // Using a BindableProperty as the backing store for BackCommand.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty BackCommandProperty =
            BindableProperty.Create("BackCommand", typeof(ICommand), typeof(ALContentPage));


        public IValueConverter BackCommandConverter
        {
            get { return (IValueConverter)GetValue(BackCommandConverterProperty); }
            set { SetValue(BackCommandConverterProperty, value); }
        }

        // Using a BindableProperty as the backing store for BackCommandConverter.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty BackCommandConverterProperty =
            BindableProperty.Create("BackCommandConverter", typeof(IValueConverter), typeof(ALContentPage));


        public object BackCommandParameter
        {
            get { return (object)GetValue(BackCommandParameterProperty); }
            set { SetValue(BackCommandParameterProperty, value); }
        }

        // Using a BindableProperty as the backing store for BackCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty BackCommandParameterProperty =
            BindableProperty.Create("BackCommandParameter", typeof(object), typeof(ALContentPage));



        public void InvokeBackCommand()
        {
            if (BackCommand == null)
                return;

            object resolvedParameter = this;
            if (BackCommandParameter != null)
                resolvedParameter = BackCommandParameter;
            if (BackCommandParameter != null)
                resolvedParameter = BackCommandConverter.Convert(resolvedParameter, typeof(object), null, null);

            if (BackCommand.CanExecute(resolvedParameter))
            {
                BackCommand.Execute(resolvedParameter);
            }
        }
    }
}
